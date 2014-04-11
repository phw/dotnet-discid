﻿﻿//  Author:
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

namespace DiscId.Test
{
    using System;
    using System.Linq;
    using NUnit.Framework;
    using DiscId;

    [TestFixture]
    public class DiscTest
    {
        [Test]
        public void GetDefaultDeviceTest()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(Disc.DefaultDevice));
        }

        [Test]
        public void PutTest()
        {
            string discId = "Wn8eRBtfLDfM0qjYPdxrz.Zjs_U-";
            int firstTrack = 1;
            int lastTrack = 10;
            int sectors = 206535;
            int[] offsets = new int[] { 150, 18901, 39738, 59557, 79152, 100126,
                                        124833, 147278, 166336, 182560 };
            int[] lengths = new int[] { 18751, 20837, 19819, 19595, 20974,
                                        24707, 22445, 19058, 16224, 23975 };
            
            var disc = Disc.Put(firstTrack, sectors, offsets);

            Assert.AreEqual(discId, disc.Id);
            Assert.AreEqual(firstTrack, disc.FirstTrackNumber);
            Assert.AreEqual(lastTrack, disc.LastTrackNumber);
            Assert.AreEqual(sectors, disc.Sectors);
            Assert.AreEqual(offsets, disc.Tracks.Select(t => t.Offset).ToArray());
            Assert.AreEqual(lengths, disc.Tracks.Select(t => t.Sectors).ToArray());
        }

		[Test, ExpectedException(typeof(DiscIdException))]
        public void PutErrorTest()
        {
            Disc.Put(-1, 0, new int[] {});
        }

		[Test, ExpectedException(typeof(DiscIdException))]
        public void ReadErrorTest()
        {
            Disc.Read("invalid_device_string");
        }

        [Test]
        public void HasReadFeatureTest()
        {
            Assert.IsTrue(Disc.HasFeatures(Features.Read));
        }

        [Test]
        public void GetFeatureListTest()
        {
			var features = Disc.GetFeatureList();
			Assert.IsTrue(features.Contains(Features.Read));
        }
    }
}
