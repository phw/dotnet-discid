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

    [Flags]
    public enum Features
    {
        Read = 1 << 0,
        Mcn = 1 << 1,
        Isrc = 1 << 2,
    }

    internal static class FeatureUtils
    {
        public static bool TestFeatureIsAvailableOrNotSet(Features featureList, Features testForFeature)
        {
            if (TestFeatureIsSelected(featureList, testForFeature))
            {
                return NativeMethods.discid_has_feature((UInt32)testForFeature);
            }

            // Feature not set
            return true;
        }

        private static bool TestFeatureIsSelected(Features featureList, Features testForFeature)
        {
            return (featureList & testForFeature) == testForFeature;
        }
    }
}
