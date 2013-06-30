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

    internal static class NativeMethods
    {
        private const string LibraryName = "discid.dll";

        private const CallingConvention LibraryCallingConvention = CallingConvention.Cdecl;

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern IntPtr discid_new();

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern void discid_free(IntPtr d);

        public static bool discid_read(IntPtr d, string device, UInt32 features)
        {
            try
            {
                return internal_discid_read_sparse(d, device, features) != 0;
            }
            catch (EntryPointNotFoundException)
            {
                return internal_discid_read(d, device) != 0;
            }
        }

        public static bool discid_put(IntPtr d, Int32 first, Int32 last, Int32[] offsets)
        {
            return internal_discid_put(d, first, last, offsets) != 0;
        }

        public static string discid_get_error_msg(IntPtr d)
        {
            var msg = internal_discid_get_error_msg(d);
            return Marshal.PtrToStringAnsi(msg);
        }

        public static string discid_get_id(IntPtr d)
        {
            var id = internal_discid_get_id(d);
            return Marshal.PtrToStringAnsi(id);
        }

        public static string discid_get_freedb_id(IntPtr d)
        {
            var id = internal_discid_get_freedb_id(d);
            return Marshal.PtrToStringAnsi(id);
        }

        public static string discid_get_submission_url(IntPtr d)
        {
            var url = internal_discid_get_submission_url(d);
            return Marshal.PtrToStringAnsi(url);
        }

        public static string discid_get_mcn(IntPtr d)
        {
            try
            {
                var mcn = internal_discid_get_mcn(d);
                return Marshal.PtrToStringAnsi(mcn);
            }
            catch (EntryPointNotFoundException)
            {
                return null;
            }
        }

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern Int32 discid_get_first_track_num(IntPtr d);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern Int32 discid_get_last_track_num(IntPtr d);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern Int32 discid_get_sectors(IntPtr d);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern Int32 discid_get_track_offset(IntPtr d, Int32 trackNum);

        [DllImport(LibraryName, CallingConvention = LibraryCallingConvention)]
        public static extern Int32 discid_get_track_length(IntPtr d, Int32 trackNum);

        public static string discid_get_track_isrc(IntPtr d, Int32 trackNum)
        {
            try
            {
                var isrc = internal_discid_get_track_isrc(d, trackNum);
                return Marshal.PtrToStringAnsi(isrc);
            }
            catch (EntryPointNotFoundException)
            {
                return null;
            }
        }

        public static string discid_get_default_device()
        {
            var device = internal_discid_get_default_device();
            return Marshal.PtrToStringAnsi(device);
        }

        public static bool discid_has_feature(UInt32 feature)
        {
            try
            {
                return internal_discid_has_feature(feature) != 0;
            }
            catch (EntryPointNotFoundException)
            {
                return feature == (UInt32)Features.Read;
            }
        }

        public static string discid_get_version_string()
        {
            try
            {
                var version = internal_discid_get_version_string();
                return Marshal.PtrToStringAnsi(version);
            }
            catch (EntryPointNotFoundException)
            {
                return "libdiscid < 0.4.0";
            }
        }

        [DllImport(LibraryName, EntryPoint = "discid_read", CallingConvention = LibraryCallingConvention, BestFitMapping = false)]
        private static extern Int32 internal_discid_read(IntPtr d, [MarshalAs(UnmanagedType.LPStr)]string device);

        [DllImport(LibraryName, EntryPoint = "discid_read_sparse", CallingConvention = LibraryCallingConvention, BestFitMapping = false)]
        private static extern Int32 internal_discid_read_sparse(IntPtr d, [MarshalAs(UnmanagedType.LPStr)]string device, UInt32 features);

        [DllImport(LibraryName, EntryPoint = "discid_put", CallingConvention = LibraryCallingConvention)]
        private static extern Int32 internal_discid_put(IntPtr d, Int32 first, Int32 last, Int32[] offsets);

        [DllImport(LibraryName, EntryPoint = "discid_get_error_msg", CallingConvention = LibraryCallingConvention)]
        private static extern IntPtr internal_discid_get_error_msg(IntPtr d);

        [DllImport(LibraryName, EntryPoint = "discid_get_id", CallingConvention = LibraryCallingConvention)]
        private static extern IntPtr internal_discid_get_id(IntPtr d);

        [DllImport(LibraryName, EntryPoint = "discid_get_freedb_id", CallingConvention = LibraryCallingConvention)]
        private static extern IntPtr internal_discid_get_freedb_id(IntPtr d);

        [DllImport(LibraryName, EntryPoint = "discid_get_submission_url", CallingConvention = LibraryCallingConvention)]
        private static extern IntPtr internal_discid_get_submission_url(IntPtr d);

        [DllImport(LibraryName, EntryPoint = "discid_get_mcn", CallingConvention = LibraryCallingConvention)]
        private static extern IntPtr internal_discid_get_mcn(IntPtr d);

        [DllImport(LibraryName, EntryPoint = "discid_get_track_isrc", CallingConvention = LibraryCallingConvention)]
        private static extern IntPtr internal_discid_get_track_isrc(IntPtr d, Int32 trackNum);

        [DllImport(LibraryName, EntryPoint = "discid_get_default_device", CallingConvention = LibraryCallingConvention)]
        private static extern IntPtr internal_discid_get_default_device();

        [DllImport(LibraryName, EntryPoint = "discid_has_feature", CallingConvention = LibraryCallingConvention)]
        private static extern Int32 internal_discid_has_feature(UInt32 feature);

        [DllImport(LibraryName, EntryPoint = "discid_get_version_string", CallingConvention = LibraryCallingConvention)]
        private static extern IntPtr internal_discid_get_version_string();
    }
}
