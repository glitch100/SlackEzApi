# SlackEzAPI

SlackEzAPI is a simple wrapper around the Slack API so you don't have to mess around with it. It's very simple at the moment offering up only certain functionality, however it's growing slowly, and will eventually wrap the entire API. Current features:

  - ChatMessage
  - ChatMessageWithAttachment
  - ChatInvite
  - ChannelInfo
  - ChannelKick
  - ChatDelete
  - Slash Command Consumption



### Version
1.0.0

### Tech

### Installation

Use the nuget package (coming soon), or simply add a reference to the binary, and then add in two entries in the `web.config` app settings:

```xml
  <appSettings>
    <add key="SlackAPIToken" value="xxxxxxxxxxxxxxxxxxxxxxxxxx" />
    <add key="SlackSlashCommandToken" value="xxxxxxxxxxxxxxxx" />
  </appSettings>
```

