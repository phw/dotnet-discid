//  Author:
//       Philipp Wolfer <ph.wolfer@gmail.com>
//
//  Copyright (c) 2013 Philipp Wolfer
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace DiscId
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public sealed class Disc : IDisposable
    {
        private IntPtr handle;

        private IDictionary<int, Track> tracks;

        private const int MAX_OFFSET_LENGTH = 100;
        
        private Disc()
        {
            handle = Lib.discid_new();
            tracks = new Dictionary<int, Track>();
        }

        ~Disc() 
        {
            Dispose(false);
        }

        public static Disc Read(string device = null, Features features = 0)
        {
            var disc = new Disc();
            disc.ReadInternal(device, features);
            return disc;
        }

        public static Disc Read(Features features)
        {
            return Read(null, features);
        }

        public static Disc Put(int firstTrack, int sectors, int[] offsets)
        {
            var disc = new Disc();
            disc.PutInternal(firstTrack, sectors, offsets);
            return disc;
        }

        public static bool HasFeatures(Features features)
        {
            try
            {
                bool result = FeatureUtils.TestFeatureIsAvailableOrNotSet(features, Features.Read);
                result &= FeatureUtils.TestFeatureIsAvailableOrNotSet(features, Features.Mcn);
                result &= FeatureUtils.TestFeatureIsAvailableOrNotSet(features, Features.Isrc);
                return result;
            }
            catch (EntryPointNotFoundException)
            {
                return features == Features.Read;
            }
        }

        public static string DefaultDevice
        {
            get
            {
                var device = Lib.discid_get_default_device();
                return Marshal.PtrToStringAnsi(device);
            }
        }

        public string Id
        {
            get
            {
                var id = Lib.discid_get_id(handle);
                return Marshal.PtrToStringAnsi(id);
            }
        }

        public string Mcn
        {
            get
            {
                var id = Lib.discid_get_mcn(handle);
                return Marshal.PtrToStringAnsi(id);
            }
        }

        public int FirstTrackNumber
        {
            get
            {
                return Lib.discid_get_first_track_num(handle);
            }
        }

        public int LastTrackNumber
        {
            get
            {
                return Lib.discid_get_last_track_num(handle);
            }
        }

        public int Sectors
        {
            get
            {
                return Lib.discid_get_sectors(handle);
            }
        }

        public string FreedbId
        {
            get
            {
                var id = Lib.discid_get_freedb_id(handle);
                return Marshal.PtrToStringAnsi(id);
            }
        }

        public Uri SubmissionUrl
        {
            get
            {
                var urlPtr = Lib.discid_get_submission_url(handle);
                var url = Marshal.PtrToStringAnsi(urlPtr);
                return new Uri(url);
            }
        }

        public IEnumerable<Track> Tracks
        {
            get
            {
                for (int number = FirstTrackNumber; number <= LastTrackNumber; number++)
                {
                    Track track;
                    if (!this.tracks.TryGetValue(number, out track))
                    {
                        int offset = Lib.discid_get_track_offset(handle, number);
                        int sectors = Lib.discid_get_track_length(handle, number);
                        var isrcPtr = Lib.discid_get_track_isrc(handle, number);
                        string isrc = Marshal.PtrToStringAnsi(isrcPtr);
                        track = new Track(number, offset, sectors, isrc);
                        this.tracks[number] = track;
                    }

                    yield return track; 
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ReadInternal(string device, Features features)
        {
            int result;
            try
            {
                result = Lib.discid_read_sparse(handle, device, (UInt32)features);
            }
            catch (EntryPointNotFoundException)
            {
                result = Lib.discid_read(handle, device);
            }

            if (result == 0)
            {
                throw new Exception(GetLastError());
            }
        }

        private void PutInternal(int firstTrack, int sectors, int[] offsets)
        {
            var lastTrack = offsets.Length - 1 + firstTrack;

            var cOffsets = new int[MAX_OFFSET_LENGTH];
            cOffsets[0] = sectors;
            offsets.CopyTo(cOffsets, 1);

            var result = Lib.discid_put(handle, firstTrack, lastTrack, cOffsets);

            if (result == 0)
            {
                throw new Exception(GetLastError());
            }
        }

        private string GetLastError()
        {
            var msg = Lib.discid_get_error_msg(handle);
            return Marshal.PtrToStringAnsi(msg);
        }

        private void Dispose(bool disposing)
        {
            if (handle != IntPtr.Zero) 
            {
                Lib.discid_free(handle);
                handle = IntPtr.Zero;
            }
        }
    }
}
