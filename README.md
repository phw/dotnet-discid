# .NET bindings for MusicBrainz libdiscid

## About
dotnet-discid provides .NET bindings for the MusicBrainz DiscID library libdiscid.
It allows calculating DiscIDs (MusicBrainz and freedb) for Audio CDs. Additionally
the library can extract the MCN/UPC/EAN and the ISRCs from disc.

## Requirements
* .NET or Mono
* libdiscid >= 0.1.0

## Installation
If you are using Visual Studio the easiest way to install and use dotnet-discid is by
installing the NuGet package from https://nuget.org/packages/DotNetDiscId/ . The
package contains the .NET assembly and the corresponding native DLLs (both 32 and 64 bit).
You just have to make sure to set your build configuration to either x86 or x64 instead
of AnyCPU.

If you compile the DotNetDiscId.dll yourself and use it in a project make sure you also
have the native libdiscid libraries available. Pre-compiled versions of libdiscid for
Windows can be downloaded from http://musicbrainz.org/doc/libdiscid . On Windows it is
recommended to ship the discid.dll in your project. If you are using Mono on other
platforms such as Linux or OSX you can use a system wide installation of libdiscid, e.g.
installed using the platform's package manager.

## Usage
*Note*: dotnet-discid is still under development. Not all features of libdiscid are supported
and the interface is not yet stable. The basic disc ID calculation is implemented, though, and
should not change drastically for a final release.

    try
    { 
        string device = DiscId.Disc.DefaultDevice;
        using (var disc = DiscId.Disc.Read(device, Features.Mcn | Features.Isrc))
        {
            Console.Out.WriteLine("DiscId         : {0}", disc.Id);
            Console.Out.WriteLine("FreeDB ID      : {0}", disc.FreedbId);
            Console.Out.WriteLine("MCN            : {0}", disc.Mcn);
            Console.Out.WriteLine("First track no.: {0}", disc.FirstTrackNumber);
            Console.Out.WriteLine("Last track no. : {0}", disc.LastTrackNumber);
            Console.Out.WriteLine("Sectors        : {0}", disc.Sectors);
            Console.Out.WriteLine("Submission URL : {0}", disc.SubmissionUrl);
        }
    }
    catch (DiscIdException ex)
    {
        Console.Out.WriteLine("Could not read disc: {0}.", ex.Message);
    }

## Contribute
The source code for dotnet-discid is available on
[GitHub](https://github.com/phw/dotnet-discid).

Please report any issues on the
[issue tracker](https://github.com/phw/dotnet-discid/issues).

## License
ruby-discid is released under the GNU Lesser General Public License Version 3. See LICENSE.txt for details.
