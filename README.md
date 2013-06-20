# .NET bindings for MusicBrainz libdiscid

## About
dotnet-discid provides .NET bindings for the MusicBrainz DiscID library libdiscid.
It allows calculating DiscIDs (MusicBrainz and freedb) for Audio CDs. Additionally
the library can extract the MCN/UPC/EAN and the ISRCs from disc.

## Requirements
* .NET or Mono
* libdiscid >= 0.1.0

## Usage
*Note*: dotnet-discid is still under development. Not all features of libdiscid are supported
and the interface is not yet stable.

The basic disc ID calculation is implemented, though, and should not change drastically for a final release.

    using (var disc = DiscId.Disc.Read(Features.Mcn | Features.Isrc))
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

## Contribute
The source code for dotnet-discid is available on
[GitHub](https://github.com/phw/dotnet-discid).

Please report any issues on the
[issue tracker](https://github.com/phw/dotnet-discid/issues).

## License
ruby-discid is released under the GNU Lesser General Public License Version 3. See LICENSE.txt for details.
