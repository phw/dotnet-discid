namespace DiscId
{
    using System;
    using System.Runtime.InteropServices;

    internal static class Lib
    {
        [DllImport("discid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr discid_new();

        [DllImport("discid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr discid_free(IntPtr d);

        [DllImport("discid.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 discid_read(IntPtr d, string device);

        //[DllImport("discid.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern Int32 discid_read_sparse(IntPtr d, string device, UInt32 features);

        [DllImport("discid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr discid_get_error_msg(IntPtr d);

        [DllImport("discid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr discid_get_id(IntPtr d);

        [DllImport("discid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr discid_get_default_device();

        [DllImport("discid.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr discid_get_version_string();
    }
}
