# .NET bindings for MusicBrainz libdiscid
[![Build Status](https://travis-ci.org/phw/dotnet-discid.svg?branch=master)](https://travis-ci.org/phw/dotnet-discid) [![NuGet version](https://badge.fury.io/nu/DotNetDiscId.svg)](http://badge.fury.io/nu/DotNetDiscId)

## About
dotnet-discid provides .NET bindings for the MusicBrainz DiscID library [libdiscid](https://github.com/metabrainz/libdiscid).
It allows calculating DiscIDs (MusicBrainz and freedb) for Audio CDs. Additionally
the library can extract the MCN/UPC/EAN and the ISRCs from disc.

## Requirements
* .NET or Mono
* libdiscid >= 0.1.0

To utilize all features you will need libdiscid 0.5.0 or later. The libdiscid
[feature matrix](http://musicbrainz.org/doc/libdiscid#Feature_Matrix) shows which
features are available in each version.

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

Source and binary releases of dotnet-discid are available at:
http://users.musicbrainz.org/~outsidecontext/dotnet-discid/

## Usage
In order to use dotnet-discid you have to reference the DotNetDiscId.dll assembly in
your project and make sure the native discid library is available (see notes above).

Below is a simple usage example. The dotnet-discid-example project provides a more
[complete example](https://github.com/phw/dotnet-discid/blob/master/dotnet-discid-example/Program.cs).

```C#
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
```

## Contribute
The source code for dotnet-discid is available on
[GitHub](https://github.com/phw/dotnet-discid).

Please report any issues on the
[issue tracker](https://github.com/phw/dotnet-discid/issues).

## License
dotnet-discid is released under the GNU Lesser General Public License Version 3. See LICENSE.txt for details.
