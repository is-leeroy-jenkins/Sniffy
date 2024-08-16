﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-15-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-15-2024
// ******************************************************************************************
// <copyright file="TcpModel.cs" company="Terry D. Eppler">
//     A tiny .NET WPF tool that can be used to establish TCP (raw) or WebSocket connections
//     and exchange text messages for testing/debugging purposes.
// 
//     Copyright ©  2020 Terry D. Eppler
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
//   TcpModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System.Diagnostics.CodeAnalysis;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Sniffy.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "PropertyCanBeMadeInitOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class TcpModel : MainWindowBase
    {
        /// <summary>
        /// The client connect BTN name
        /// </summary>
        private string _clientConnectBtnName;

        /// <summary>
        /// The client recv
        /// </summary>
        private string _clientRecv;

        /// <summary>
        /// The client send
        /// </summary>
        private string _clientSend;

        /// <summary>
        /// The client send BTN name
        /// </summary>
        private string _clientSendBtnName;

        /// <summary>
        /// The client send interval
        /// </summary>
        private int _clientSendInterval;

        /// <summary>
        /// The client send string
        /// </summary>
        private string _clientSendStr;

        /// <summary>
        /// The listen port
        /// </summary>
        private int _listenPort;

        /// <summary>
        /// The local port
        /// </summary>
        private string _localPort;

        /// <summary>
        /// The server ip
        /// </summary>
        private string _serverIp;

        /// <summary>
        /// The server listen BTN name
        /// </summary>
        private string _serverListenBtnName;

        /// <summary>
        /// The server port
        /// </summary>
        private int _serverPort;

        /// <summary>
        /// The server recv
        /// </summary>
        private string _serverRecv;

        /// <summary>
        /// The server send
        /// </summary>
        private string _serverSend;

        /// <summary>
        /// The server send BTN name
        /// </summary>
        private string _serverSendBtnName;

        /// <summary>
        /// The server send interval
        /// </summary>
        private int _serverSendInterval;

        /// <summary>
        /// The server send string
        /// </summary>
        private string _serverSendStr;

        /// <summary>
        /// The server status
        /// </summary>
        private string _serverStatus;

        /// <summary>
        /// Initializes a new instance of the <see cref="TcpModel"/> class.
        /// </summary>
        public TcpModel( )
        {
            ListenPort = 65432;
            ServerSendStr = "Hello Client!";
            ServerSendInterval = 1000;
            ServerSend = "Hello Client!";
            ServerSendBtnName = "Auto Send Start";
            ServerListenBtnName = "Start Listen";
            ServerIp = "127.0.0.1";
            ServerPort = 65432;
            ClientSendStr = "Hello Server!";
            ClientSendInterval = 1000;
            ClientSend = "Hello Server!";
            ClientConnectBtnName = "Connect";
            ClientSendBtnName = "Auto Send Start";
            LocalPort = "0";
        }

        /// <summary>
        /// Gets or sets the listen port.
        /// </summary>
        /// <value>
        /// The listen port.
        /// </value>
        public int ListenPort
        {
            get { return _listenPort; }
            set
            {
                if( _listenPort != value )
                {
                    _listenPort = value;
                    OnPropertyChanged( nameof( ListenPort ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the server listen BTN.
        /// </summary>
        /// <value>
        /// The name of the server listen BTN.
        /// </value>
        public string ServerListenBtnName
        {
            get { return _serverListenBtnName; }
            set
            {
                if( _serverListenBtnName != value )
                {
                    _serverListenBtnName = value;
                    OnPropertyChanged( nameof( ServerListenBtnName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server send string.
        /// </summary>
        /// <value>
        /// The server send string.
        /// </value>
        public string ServerSendStr
        {
            get { return _serverSendStr; }
            set
            {
                if( _serverSendStr != value )
                {
                    _serverSendStr = value;
                    OnPropertyChanged( nameof( ServerSendStr ) );
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
        public string ServerSendBtnName
        {
            get { return _serverSendBtnName; }
            set
            {
                if( _serverSendBtnName != value )
                {
                    _serverSendBtnName = value;
                    OnPropertyChanged( nameof( ServerSendBtnName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server status.
        /// </summary>
        /// <value>
        /// The server status.
        /// </value>
        public string ServerStatus
        {
            get { return _serverStatus; }
            set
            {
                if( _serverStatus != value )
                {
                    _serverStatus = value;
                    OnPropertyChanged( nameof( ServerStatus ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server recv.
        /// </summary>
        /// <value>
        /// The server recv.
        /// </value>
        public string ServerRecv
        {
            get { return _serverRecv; }
            set
            {
                if( _serverRecv != value )
                {
                    _serverRecv = value;
                    OnPropertyChanged( nameof( ServerRecv ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server send.
        /// </summary>
        /// <value>
        /// The server send.
        /// </value>
        public string ServerSend
        {
            get { return _serverSend; }
            set
            {
                if( _serverSend != value )
                {
                    _serverSend = value;
                    OnPropertyChanged( nameof( ServerSend ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        public int ServerPort
        {
            get { return _serverPort; }
            set
            {
                if( _serverPort != value )
                {
                    _serverPort = value;
                    OnPropertyChanged( nameof( ServerPort ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the local port.
        /// </summary>
        /// <value>
        /// The local port.
        /// </value>
        public string LocalPort
        {
            get { return _localPort; }
            set
            {
                if( _localPort != value )
                {
                    _localPort = value;
                    OnPropertyChanged( nameof( LocalPort ) );
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
        public string ClientConnectBtnName
        {
            get { return _clientConnectBtnName; }
            set
            {
                if( _clientConnectBtnName != value )
                {
                    _clientConnectBtnName = value;
                    OnPropertyChanged( nameof( ClientConnectBtnName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the client send string.
        /// </summary>
        /// <value>
        /// The client send string.
        /// </value>
        public string ClientSendStr
        {
            get { return _clientSendStr; }
            set
            {
                if( _clientSendStr != value )
                {
                    _clientSendStr = value;
                    OnPropertyChanged( nameof( ClientSendStr ) );
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
        public string ClientSendBtnName
        {
            get { return _clientSendBtnName; }
            set
            {
                if( _clientSendBtnName != value )
                {
                    _clientSendBtnName = value;
                    OnPropertyChanged( nameof( ClientSendBtnName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the client recv.
        /// </summary>
        /// <value>
        /// The client recv.
        /// </value>
        public string ClientRecv
        {
            get { return _clientRecv; }
            set
            {
                if( _clientRecv != value )
                {
                    _clientRecv = value;
                    OnPropertyChanged( nameof( ClientRecv ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the client send.
        /// </summary>
        /// <value>
        /// The client send.
        /// </value>
        public string ClientSend
        {
            get { return _clientSend; }
            set
            {
                if( _clientSend != value )
                {
                    _clientSend = value;
                    OnPropertyChanged( nameof( ClientSend ) );
                }
            }
        }
    }
}