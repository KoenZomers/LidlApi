# Lidl Plus API in .NET 5

[![licence badge]][licence]
[![issues badge]][issues]
[![prwelcome badge]][prwelcome]

[licence badge]:https://img.shields.io/badge/license-Apache2-blue.svg
[issues badge]:https://img.shields.io/github/issues/koenzomers/LidlApi.svg
[prwelcome badge]:https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square

[licence]:https://github.com/koenzomers/LidlApi/blob/master/LICENSE.md
[issues]:https://github.com/koenzomers/LidlApi/issues
[prwelcome]:http://makeapullrequest.com

API in .NET 5 using C# which can be used to read data from the Lidl Plus API used by the Lidl Plus app. Includes Unit Tests and a sample ConsoleApp to test the API. All assemblies are signed and compiled against .NET 5 which makes it usable cross platform and accross all .NET supporting languages.

Note that in no way this code is supported by Lidl itself and it may break at any time if Lidl updates their server side implementation.

## Usage

```C#
// Create a new Session instance to connect with the Lidl APi
var session = new KoenZomers.Lidl.Api.Session();
```

Have a look at the Unit Tests and/or Console Application for seeing additional samples about the available data you can pull from the API.

## NuGet

Also available as NuGet Package: [KoenZomers.Lidl.Api](https://www.nuget.org/packages/KoenZomers.Lidl.Api/)

## Version History

Version 1.0 - June 4, 2021

- Initial version

## Feedback

Comments\suggestions\bug reports are welcome!

Koen Zomers
koen@zomers.eu
