# NetKit.Chat.Client

Softeq.NetKit.Chat.Client is a client that allows to quickly test NetKit.Chat Nuget. 
Service tests the following types of functional:
1. Channels
2. Messages
3. Members

## Dependencies
 - [Softeq.NetKit.Chat.SignalRClient (1.0.5)](https://github.com/Softeq/NetKit.Chat.SignalRClient "Softeq.NetKit.Chat.SignalRClient (1.0.5)")

# Getting Started

## Configure

Update appsettings.json:

```{
  "Chat": {
    "Url": "[chat app service url]"
  },
  "Auth": {
    "Url": "[auth app service url]",
    "UserName": "[first user email that exist in Auth database]",
    "Password": "[first user's password]",
    "InvitedUserName": "[second user email that exist in Auth database]",
    "Identity": {
      "ClientId": "[identity server client id]",
      "ClientSecret": "[identity server client secret]",
      "Scope": "[identity server allowed scope]" 
    } 
  }
}
```
Configure your notification hub for iOS push notifications. 

 Run Softeq.NetKit.Chat.SignalRClient.Sample console app. 

## About
This project is maintained by [Softeq Development Corp.](https://www.softeq.com/)
We specialize in .NET core applications.

 - [Facebook](https://web.facebook.com/Softeq.by/)
 - [Instagram](https://www.instagram.com/softeq/)
 - [Twitter](https://twitter.com/Softeq)
 - [Vk](https://vk.com/club21079655)

## Contributing

We welcome any contributions.

## License

The NetKit.Chat.Client project is available for free use, as described by the [LICENSE](/LICENSE) (MIT).
