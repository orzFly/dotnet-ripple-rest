RippleRest.NET
==============

This is a client that interacts with the Ripple network using the Ripple REST APIs.

A documentation can be downloaded from [here] (https://github.com/orzFly/dotnet-ripple-rest/releases)

There is a demo program included in this repository.

NuGet
-----
To install Ripple Rest for .NET, run the following command in the Package Manager Console

```
PM> Install-Package RippleRest
```

Usage
-----
You can use RippleRest by setting a default client instance:

```csharp
RippleRestClient.DefaultInstance = new RippleRestClient("http://localhost:5990/");
...
var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
account.GetTrustlines();
```

or by passing client to every call:

```csharp
var client = new RippleRestClient("http://localhost:5990/");
var account = new Account(this.AccountAddressBox.Text, this.AccountSecretBox.Text);
account.GetTrustlines(client);
```
