namespace DiscId
{
    using System;
    using System.Runtime.InteropServices;

    public sealed class Disc : IDisposable
    {
        private IntPtr handle;

        private const int MAX_OFFSET_LENGTH = 100;
        
        private Disc()
        {
            handle = Lib.discid_new();
        }

        ~Disc() 
        {
            Dispose(false);
        }

        public static Disc Read(string device = null)
        {
            var disc = new Disc();
            disc.ReadInternal(device);
            return disc;
        }

        public static Disc Put(int firstTrack, int sectors, int[] offsets)
        {
            var disc = new Disc();
            disc.PutInternal(firstTrack, sectors, offsets);
            return disc;
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void ReadInternal(string device = null)
        {
            var result = Lib.discid_read(handle, device);

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
