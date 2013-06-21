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

        [DllImport(LIBRARY_NAME, CharSet = CharSet.Ansi, CallingConvention = CALLING_CONVENTION)]
        public static extern Int32 discid_read_sparse(IntPtr d, string device, UInt32 features);

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
        public static extern Int32 discid_get_track_offset(IntPtr d, Int32 trackNum);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern Int32 discid_get_track_length(IntPtr d, Int32 trackNum);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_track_isrc(IntPtr d, Int32 trackNum);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_default_device();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern Int32 discid_has_feature(UInt32 feature);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        public static extern IntPtr discid_get_version_string();
    }
}
