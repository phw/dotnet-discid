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

namespace DiscIdTest
{
	using System;
	using NUnit.Framework;
	using DiscId;

	[TestFixture]
    public class DiscIdTest
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
            
            var disc = DiscId.Disc.Put(firstTrack, sectors, offsets);

            Assert.AreEqual(discId, disc.Id);
            Assert.AreEqual(firstTrack, disc.FirstTrackNumber);
            Assert.AreEqual(lastTrack, disc.LastTrackNumber);
            Assert.AreEqual(sectors, disc.Sectors);
        }

        [Test]
        public void HasReadFeatureTest()
        {
            Assert.IsTrue(Disc.HasFeatures(Features.Read));
        }
    }
}
