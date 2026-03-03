# Transmitly.ChannelProvider.Smtp

A [Transmitly](https://github.com/transmitly/transmitly) channel provider for sending Email communications through SMTP using [MailKit](https://github.com/jstedfast/MailKit).

## Installation

Install from NuGet:

```shell
dotnet add package Transmitly.ChannelProvider.Smtp
```

## Quick Start

```csharp
using Transmitly;
using Transmitly.ChannelProvider.Smtp.Configuration;

ICommunicationsClient communicationsClient = new CommunicationsClientBuilder()
	.AddSmtpSupport(options =>
	{
		options.Host = "smtp.example.com";
		options.Port = 587;
		options.UserName = "smtp-user";
		options.Password = "smtp-password";
	})
	.AddPipeline("welcome-email", pipeline =>
	{
		pipeline.AddEmail("welcome@my.app".AsIdentityAddress("Welcome Team"), email =>
		{
			email.Subject.AddStringTemplate("Welcome to My App");
			email.HtmlBody.AddStringTemplate("<h1>Welcome to My App</h1><p>Thanks for joining us.</p><p><a href=\"https://my.app/get-started\">Get started</a></p>");
			email.TextBody.AddStringTemplate("Welcome to My App\n\nThanks for joining us.\nGet started: https://my.app/get-started");
		});
	})
	.BuildClient();

var result = await communicationsClient.DispatchAsync(
	"welcome-email",
	"newuser@my.app".AsIdentityAddress("New User"),
	new { });
```

## SMTP Options

`AddSmtpSupport(options => ...)` accepts `SmtpOptions` from `Transmitly.ChannelProvider.Smtp.Configuration`.


| Option | Required (MailKit) | Effective Default | Description |
| --- | --- | --- | --- |
| `Host` | Yes | `null` | SMTP server host name. |
| `Port` | No | `587` (`465` when `SocketOptions = SslOnConnect`) | SMTP server port used for MailKit connect. |
| `SocketOptions` | No | `Auto` | TLS/SSL behavior (`None`, `Auto`, `SslOnConnect`, `StartTls`, `StartTlsWhenAvailable`). |
| `Encoding` | No | `UTF8` | Encoding used for MailKit authentication. |
| `Credentials` | Conditional | `null` | SMTP server credentials. |
| `UserName` | Conditional | `null` | SMTP server username. |
| `Password` | Conditional | `null` | SMTP server password. |

## Multiple SMTP Providers

You can register multiple SMTP providers with different `providerId` values and route communications to a GDPR or non-GDPR SMTP server.

```csharp
using Transmitly;
using Transmitly.ChannelProvider.Smtp.Configuration;

ICommunicationsClient client = new CommunicationsClientBuilder()
	.AddSmtpSupport(o =>
	{
		o.Host = "smtp.eu-gdpr.example.com";
		o.Port = 587;
		o.SocketOptions = SecureSocketOptions.StartTls;
		o.UserName = "gdpr-user";
		o.Password = "gdpr-password";
	}, providerId: "gdpr")
	.AddSmtpSupport(o =>
	{
		o.Host = "smtp.global.example.com";
		o.Port = 587;
		o.SocketOptions = SecureSocketOptions.StartTls;
		o.UserName = "non-gdpr-user";
		o.Password = "non-gdpr-password";
	}, providerId: "non-gdpr")
	.AddPipeline("eu-account-alert", pipeline =>
	{
		pipeline.AddEmail("privacy@my.app".AsIdentityAddress("Privacy Team"), email =>
		{
			email.Subject.AddStringTemplate("Important account alert");
			email.TextBody.AddStringTemplate("There is an update on your account.");
			email.AddChannelProviderFilter(Id.ChannelProvider.Smtp("gdpr"));
		});
	})
	.AddPipeline("global-marketing-email", pipeline =>
	{
		pipeline.AddEmail("news@my.app".AsIdentityAddress("Marketing"), email =>
		{
			email.Subject.AddStringTemplate("Latest product updates");
			email.TextBody.AddStringTemplate("See what's new this month.");
			email.AddChannelProviderFilter(Id.ChannelProvider.Smtp("non-gdpr"));
		});
	})
	.BuildClient();
```

## Related Packages

- [Transmitly.ChannelProvider.Smtp.Configuration](https://github.com/transmitly/transmitly-channel-provider-smtp-configuration)
- [Transmitly.ChannelProvider.Smtp.MailKit](https://github.com/transmitly/transmitly-channel-provider-smtp-mailkit)

For broader concepts such as channels, pipelines, delivery reports, and template engines, see the main [Transmitly](https://github.com/transmitly/transmitly) project.

---
_Copyright (c) Code Impressions, LLC. This open-source project is sponsored and maintained by Code Impressions and is licensed under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
