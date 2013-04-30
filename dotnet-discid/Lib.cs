namespace DiscId
{
    using System;
    using System.Runtime.InteropServices;

    internal static class Lib
    {
        private const string LIBRARY_NAME = "discid.dll";

        private const CallingConvention CALLING_CONVENTION = CallingConvention.Cdecl;

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_new();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_free(IntPtr d);

        [DllImport(LIBRARY_NAME, CharSet = CharSet.Ansi, CallingConvention = CALLING_CONVENTION)]
        public static extern Int32 discid_read(IntPtr d, string device);

        //[DllImport(LIBRARY_NAME, CharSet = CharSet.Ansi, CallingConvention = CALLING_CONVENTION)]
        //public static extern Int32 discid_read_sparse(IntPtr d, string device, UInt32 features);

        [DllImport(LIBRARY_NAME, CharSet = CharSet.Ansi, CallingConvention = CALLING_CONVENTION)]
        public static extern Int32 discid_put(IntPtr d, Int32 first, Int32 last, Int32[] offsets);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_error_msg(IntPtr d);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_id(IntPtr d);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_freedb_id(IntPtr d);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_submission_url(IntPtr d);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_mcn(IntPtr d);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern Int32 discid_get_first_track_num(IntPtr d);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern Int32 discid_get_last_track_num(IntPtr d);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern Int32 discid_get_sectors(IntPtr d);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_default_device();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_version_string();
    }
}
