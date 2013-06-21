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
    public sealed class Track
    {
        public Track(int number, int offset, int sectors, string isrc)
        {
            this.Number = number;
            this.Offset = offset;
            this.Sectors = sectors;
            this.Isrc = isrc;
        }

        public int Number { get; private set; }

        public int Offset { get; private set; }

        public int Sectors { get; private set; }

        public string Isrc { get; private set; }
    }
}

