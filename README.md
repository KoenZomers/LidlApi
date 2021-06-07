# Lidl Plus API in .NET 5

[![licence badge]][licence]
[![issues badge]][issues]
[![prwelcome badge]][prwelcome]
[![.NET 5 CI](https://github.com/KoenZomers/LidlApi/actions/workflows/dotnet.yml/badge.svg)](https://github.com/KoenZomers/LidlApi/actions/workflows/dotnet.yml)

[licence badge]:https://img.shields.io/badge/license-Apache2-blue.svg
[issues badge]:https://img.shields.io/github/issues/koenzomers/LidlApi.svg
[prwelcome badge]:https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square

[licence]:https://github.com/koenzomers/LidlApi/blob/master/LICENSE.md
[issues]:https://github.com/koenzomers/LidlApi/issues
[prwelcome]:http://makeapullrequest.com

API in .NET 5 using C# which can be used to read data from the Lidl Plus API used by the Lidl Plus app. Includes Unit Tests and a sample ConsoleApp to test the API. All assemblies are signed and compiled against .NET 5 which makes it usable cross platform and accross all .NET supporting languages.

Note that in no way this code is supported by Lidl itself and it may break at any time if Lidl updates their server side implementation.

## Usage

If you want to build the code locally, copy the `App.sample.config` file under ConsoleApp and UnitTest to `App.config` and fill it with your Lidl Plus e-mail address and password.

You can create a new session to connect to the Lidl Plus services using:

```C#
// Create a new Session instance to connect with the Lidl APi
var session = new KoenZomers.Lidl.Api.Session();
```

This will connect it to the Lidl Plus services of The Netherlands and return content in Dutch. If you wish to connect to Lidl Plus for another county and/or language, you can pass this in through one of the optional arguments in the constructor, i.e. for Germany use:

```C#
// Create a new Session instance to connect with the Lidl APi
var session = new KoenZomers.Lidl.Api.Session(language: "DE-DE", country: "DE");
```

The other optional parameters in the constructor you typically don't need to and should not change from their defaults.

Once you have the session initiated, you need to authenticate it first using:

```C#
// Authenticate to the Lidl Plus API using an e-mail address and password
await session.Authenticate("johndoe@hotmail.com", "myLidlPlusPassword");
```

Instead of using the e-mail address and password to authenticate, you can also use a refresh token to do so, which is a faster way to authenticate as it requires fewer steps behind the scenes:

```C#
// Authenticate to the Lidl Plus API using a refresh token
await session.Authenticate("refreshToken");
```

Once authenticated, you can call one of the methods to retrieve data from the Lidl Plus services, such as:

```C#
// Retrieve all your receipts
await session.GetReceipts();

// Retrieve all your coupons
await session.GetCoupons();

// Retrieve all the scratch coupons you receive after a purchase for discounts on items
await session.GetScratchCoupons();

// Scratch open the coupon with the provided id
await session.RedeemScratchCoupon("couponid");

// Mark a specific receipt as favorite
await session.MakeReceiptFavorite("receiptid");
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
