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

namespace DiscIdExample
{
    using System;
    using DiscId;

    class Program
    {
        static void Main(string[] args)
        {
			Console.Out.WriteLine("Using device   : {0}", Disc.DefaultDevice);

            using (var disc = Disc.Read())
            {
                Console.Out.WriteLine("DiscId         : {0}", disc.Id);
                Console.Out.WriteLine("FreeDB ID      : {0}", disc.FreedbId);
                Console.Out.WriteLine("MCN            : {0}", disc.Mcn);
                Console.Out.WriteLine("First track no.: {0}", disc.FirstTrackNumber);
                Console.Out.WriteLine("Last track no. : {0}", disc.LastTrackNumber);
                Console.Out.WriteLine("Sectors        : {0}", disc.Sectors);
                Console.Out.WriteLine("Submission URL : {0}", disc.SubmissionUrl);
                Console.In.ReadLine();
            }
        }
    }
}
