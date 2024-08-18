// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-16-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-16-2024
// ******************************************************************************************
// <copyright file="WebSocketModel.cs" company="Terry D. Eppler">
//     A tiny .NET WPF tool that can be used to establish TCP (raw) or WebSocket connections
//     and exchange text messages for testing/debugging purposes.
// 
//     Copyright ©  2021 Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the “Software”),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at:  terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   WebSocketModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System.Diagnostics.CodeAnalysis;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Sniffy.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class WebSocketModel : MainWindowBase
    {
        /// <summary>
        /// The client connect BTN name
        /// </summary>
        private string _clientConnectButtonName;

        /// <summary>
        /// The client recv
        /// </summary>
        private string _receivingClient;

        /// <summary>
        /// The client send
        /// </summary>
        private string _sendingClient;

        /// <summary>
        /// The client send BTN name
        /// </summary>
        private string _clientSendButtonName;

        /// <summary>
        /// The client send interval
        /// </summary>
        private int _clientSendInterval;

        /// <summary>
        /// The client send string
        /// </summary>
        private string _sendingClientText;

        /// <summary>
        /// The server address
        /// </summary>
        private string _serverAddress;

        /// <summary>
        /// The server ip
        /// </summary>
        private string _serverIp;

        /// <summary>
        /// The server listen BTN name
        /// </summary>
        private string _serverListenButtonName;

        /// <summary>
        /// The server send
        /// </summary>
        private string _sendingServer;

        /// <summary>
        /// The server send BTN name
        /// </summary>
        private string _serverSendButtonName;

        /// <summary>
        /// The server send interval
        /// </summary>
        private int _serverSendInterval;

        /// <summary>
        /// The server send string
        /// </summary>
        private string _sendingServerText;

        /// <summary>
        /// The server recv
        /// </summary>
        private static string _receivingServer;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketModel"/> class.
        /// </summary>
        public WebSocketModel( )
        {
            ServerAddress = "ws://127.0.0.1:65432";
            SendingServerText = "Hello Client!";
            ServerSendInterval = 1000;
            SendingServer = "Hello Client!";
            ServerSendButtonName = "Auto Send Start";
            ServerListenButtonName = "Start Listen";
            ServerIp = "ws://127.0.0.1:65432/echo";
            SendingClientText = "Hello Server!";
            ClientSendInterval = 1000;
            SendingClient = "Hello Server!";
            ClientConnectButtonName = "Connect";
            ClientSendButtonName = "Auto Send Start";
        }

        /// <summary>
        /// Gets or sets the server address.
        /// </summary>
        /// <value>
        /// The server address.
        /// </value>
        public string ServerAddress
        {
            get { return _serverAddress; }
            set
            {
                if( _serverAddress != value )
                {
                    _serverAddress = value;
                    OnPropertyChanged( nameof( ServerAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the server listen BTN.
        /// </summary>
        /// <value>
        /// The name of the server listen BTN.
        /// </value>
        public string ServerListenButtonName
        {
            get { return _serverListenButtonName; }
            set
            {
                if( _serverListenButtonName != value )
                {
                    _serverListenButtonName = value;
                    OnPropertyChanged( nameof( ServerListenButtonName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server send string.
        /// </summary>
        /// <value>
        /// The server send string.
        /// </value>
        public string SendingServerText
        {
            get { return _sendingServerText; }
            set
            {
                if( _sendingServerText != value )
                {
                    _sendingServerText = value;
                    OnPropertyChanged( nameof( SendingServerText ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server send interval.
        /// </summary>
        /// <value>
        /// The server send interval.
        /// </value>
        public int ServerSendInterval
        {
            get { return _serverSendInterval; }
            set
            {
                if( _serverSendInterval != value )
                {
                    _serverSendInterval = value;
                    OnPropertyChanged( nameof( ServerSendInterval ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the server send BTN.
        /// </summary>
        /// <value>
        /// The name of the server send BTN.
        /// </value>
        public string ServerSendButtonName
        {
            get { return _serverSendButtonName; }
            set
            {
                if( _serverSendButtonName != value )
                {
                    _serverSendButtonName = value;
                    OnPropertyChanged( nameof( ServerSendButtonName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server recv.
        /// </summary>
        /// <value>
        /// The server recv.
        /// </value>
        public string ReceivingServer
        {
            get { return _receivingServer; }
            set
            {
                if( _receivingServer != value )
                {
                    _receivingServer = value;
                    OnPropertyChanged( nameof( ReceivingServer ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server send.
        /// </summary>
        /// <value>
        /// The server send.
        /// </value>
        public string SendingServer
        {
            get { return _sendingServer; }
            set
            {
                if( _sendingServer != value )
                {
                    _sendingServer = value;
                    OnPropertyChanged( nameof( SendingServer ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        public string ServerIp
        {
            get { return _serverIp; }
            set
            {
                if( _serverIp != value )
                {
                    _serverIp = value;
                    OnPropertyChanged( nameof( ServerIp ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the client connect BTN.
        /// </summary>
        /// <value>
        /// The name of the client connect BTN.
        /// </value>
        public string ClientConnectButtonName
        {
            get { return _clientConnectButtonName; }
            set
            {
                if( _clientConnectButtonName != value )
                {
                    _clientConnectButtonName = value;
                    OnPropertyChanged( nameof( ClientConnectButtonName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the client send string.
        /// </summary>
        /// <value>
        /// The client send string.
        /// </value>
        public string SendingClientText
        {
            get { return _sendingClientText; }
            set
            {
                if( _sendingClientText != value )
                {
                    _sendingClientText = value;
                    OnPropertyChanged( nameof( SendingClientText ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the client send interval.
        /// </summary>
        /// <value>
        /// The client send interval.
        /// </value>
        public int ClientSendInterval
        {
            get { return _clientSendInterval; }
            set
            {
                if( _clientSendInterval != value )
                {
                    _clientSendInterval = value;
                    OnPropertyChanged( nameof( ClientSendInterval ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the client send BTN.
        /// </summary>
        /// <value>
        /// The name of the client send BTN.
        /// </value>
        public string ClientSendButtonName
        {
            get { return _clientSendButtonName; }
            set
            {
                if( _clientSendButtonName != value )
                {
                    _clientSendButtonName = value;
                    OnPropertyChanged( nameof( ClientSendButtonName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the client recv.
        /// </summary>
        /// <value>
        /// The client recv.
        /// </value>
        public string ReceivingClient
        {
            get { return _receivingClient; }
            set
            {
                if( _receivingClient != value )
                {
                    _receivingClient = value;
                    OnPropertyChanged( nameof( ReceivingClient ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the client send.
        /// </summary>
        /// <value>
        /// The client send.
        /// </value>
        public string SendingClient
        {
            get { return _sendingClient; }
            set
            {
                if( _sendingClient != value )
                {
                    _sendingClient = value;
                    OnPropertyChanged( nameof( SendingClient ) );
                }
            }
        }
    }
}