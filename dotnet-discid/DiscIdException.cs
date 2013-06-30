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
    using System.Runtime.Serialization;

    [Serializable]
    public class DiscIdException : Exception, ISerializable
    {
        public DiscIdException()
            : base()
        {
        }

        public DiscIdException(string message)
            : base(message)
        {
        }

        public DiscIdException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DiscIdException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

