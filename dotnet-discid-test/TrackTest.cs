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

namespace DiscId.Test
{
    using System;
    using NUnit.Framework;
    using DiscId;

    [TestFixture]
    public class TrackTest
    {
        [Test]
        public void TrackConstructorTest()
        {
            int number = 3;
            int offset = 1035;
            int sectors = 23643;
            string isrc = "US4E40731510";

            var track = new Track(number, offset, sectors, isrc);

            Assert.AreEqual(number, track.Number);
            Assert.AreEqual(offset, track.Offset);
            Assert.AreEqual(sectors, track.Sectors);
            Assert.AreEqual(isrc, track.Isrc);
        }
    }
}

