![](https://github.com/is-leeroy-jenkins/Sniffy/blob/master/Resources/Assets/Github/Sniffy.png)
# Sniffy
A simple .NET WPF application that can be used to establish TCP (raw) or WebSocket connections and exchange
text messages for testing/debugging purposes.

- Supports TCP connections with a dual-mode socket (IPv6 and IPv4) and optionally using TLS
- Supports WebSocket connections (sending text messages and receiving text or binary messages)
- Supports half-closing the socket (send channel) before fully closing it
- Binary messages are encoded/decoded using a specified encoding like Windows-1252 (1 character per byte) or UTF-8


![](https://github.com/is-leeroy-jenkins/Sniffy/blob/master/Resources/Assets/Github/csharp.png) 
## Code 

Sniffy is built on NET 6 and supports AnyCPU as well as x86/x64 specific builds



 - [Callbacks](https://github.com/is-leeroy-jenkins/Sniffy/tree/master/Callbacks) - Delegates, Events, etc.
 - [Configuration](https://github.com/is-leeroy-jenkins/Sniffy/tree/master/Configuration) - Configuration classes used by sockethandlers
 - [Enumerations](https://github.com/is-leeroy-jenkins/Sniffy/tree/master/Enumerations) - various enumerations used in Sniffy.
 - [Extensions](https://github.com/is-leeroy-jenkins/Sniffy/tree/master/Extensions)- extension methods for sniffy.
 - [Static](https://github.com/is-leeroy-jenkins/Sniffy/tree/master/Static) - static types used by sniffy.
 - [Controls](https://github.com/is-leeroy-jenkins/Sniffy/tree/master/UI/Controls) - classes associated with the user controls.
 - [Windows](https://github.com/is-leeroy-jenkins/Sniffy/tree/master/UI/Windows) - classes associated with the application windows.


## Example (General)
![](https://github.com/is-leeroy-jenkins/Sniffy/blob/master/Resources/Assets/Github/Sniffy-Intro.gif)


## Example (TCP):

![](https://github.com/is-leeroy-jenkins/Sniffy/blob/master/Resources/Assets/Github/example-tcp.gif)

## Example (WebSocket):

![](https://github.com/is-leeroy-jenkins/Sniffy/blob/master/Resources/Assets/Github/example-websocket.gif)
