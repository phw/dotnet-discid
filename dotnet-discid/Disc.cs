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

    public sealed class Disc : IDisposable
    {
        private const int MaxOffsetLength = 100;

        private IntPtr handle;

        private IDictionary<int, Track> tracks;

        private Disc()
        {
            handle = NativeMethods.discid_new();
            tracks = new Dictionary<int, Track>();
        }

        ~Disc() 
        {
            Dispose(false);
        }

        public static Disc Read()
        {
            return Read(null, 0);
        }

        public static Disc Read(string device)
        {
            return Read(device, 0);
        }

        public static Disc Read(Features features)
        {
            return Read(null, features);
        }

        public static Disc Read(string device, Features features)
        {
            var disc = new Disc();
            disc.ReadInternal(device, features);
            return disc;
        }

        public static Disc Put(int firstTrack, int sectors, int[] offsets)
        {
            var disc = new Disc();
            disc.PutInternal(firstTrack, sectors, offsets);
            return disc;
        }

        public static bool HasFeatures(Features features)
        {
            bool result = FeatureUtils.TestFeatureIsAvailableOrNotSet(features, Features.Read);
            result &= FeatureUtils.TestFeatureIsAvailableOrNotSet(features, Features.Mcn);
            result &= FeatureUtils.TestFeatureIsAvailableOrNotSet(features, Features.Isrc);
            return result;
        }

        public static IEnumerable<Features> GetFeatureList()
        {
            var result = new List<Features>();
            foreach (Features feature in Enum.GetValues(typeof(Features)))
            {
                if (NativeMethods.discid_has_feature((UInt32)feature))
                {
                    result.Add(feature);
                }
            }

            return result;
        }

        public static string DefaultDevice
        {
            get
            {
                return NativeMethods.discid_get_default_device();
            }
        }

        public static string LibdiscidVersion
        {
            get
            {
                return NativeMethods.discid_get_version_string();
            }
        }

        public string Id
        {
            get
            {
                return NativeMethods.discid_get_id(handle);
            }
        }

        public string Mcn
        {
            get
            {
                return NativeMethods.discid_get_mcn(handle);
            }
        }

        public int FirstTrackNumber
        {
            get
            {
                return NativeMethods.discid_get_first_track_num(handle);
            }
        }

        public int LastTrackNumber
        {
            get
            {
                return NativeMethods.discid_get_last_track_num(handle);
            }
        }

        public int Sectors
        {
            get
            {
                return NativeMethods.discid_get_sectors(handle);
            }
        }

        public string FreedbId
        {
            get
            {
                return NativeMethods.discid_get_freedb_id(handle);
            }
        }

        public Uri SubmissionUrl
        {
            get
            {
                var url = NativeMethods.discid_get_submission_url(handle);
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
                        int offset = NativeMethods.discid_get_track_offset(handle, number);
                        int sectors = NativeMethods.discid_get_track_length(handle, number);
                        string isrc = NativeMethods.discid_get_track_isrc(handle, number);
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
            if (!NativeMethods.discid_read(handle, device, (UInt32)features))
            {
                throw new DiscIdException(GetLastError());
            }
        }

        private void PutInternal(int firstTrack, int sectors, int[] offsets)
        {
            var lastTrack = offsets.Length - 1 + firstTrack;

            var cOffsets = new int[MaxOffsetLength];
            cOffsets[0] = sectors;
            offsets.CopyTo(cOffsets, 1);

            if (!NativeMethods.discid_put(handle, firstTrack, lastTrack, cOffsets))
            {
                throw new DiscIdException(GetLastError());
            }
        }

        private string GetLastError()
        {
            return NativeMethods.discid_get_error_msg(handle);
        }

        private void Dispose(bool disposing)
        {
            if (handle != IntPtr.Zero) 
            {
                NativeMethods.discid_free(handle);
                handle = IntPtr.Zero;
            }
        }
    }
}
