namespace DiscId
{
    using System;
    using System.Runtime.InteropServices;

    public sealed class Disc : IDisposable
    {
        private IntPtr handle;
        
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

        private string GetLastError()
        {
            var msg = Lib.discid_get_error_msg(handle);
            return Marshal.PtrToStringAnsi(msg);
        }

        protected void Dispose(bool disposing)
        {
            if (handle != IntPtr.Zero) 
            {
                Lib.discid_free(handle);
                handle = IntPtr.Zero;
            }
        }
    }
}
