// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-20-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-20-2024
// ******************************************************************************************
// <copyright file="SocketModel.cs" company="Terry D. Eppler">
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
//   SocketModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Sniffy.MainWindowBase" />
    public class SocketModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The client connect button name
        /// </summary>
        private string _clientConnectButtonName;

        /// <summary>
        /// The client receive
        /// </summary>
        private string _clientReceive;

        /// <summary>
        /// The client send
        /// </summary>
        private string _clientSend;

        /// <summary>
        /// The client send button name
        /// </summary>
        private string _clientSendButtonName;

        /// <summary>
        /// The client send interval
        /// </summary>
        private int _clientSendInterval;

        /// <summary>
        /// The client send text
        /// </summary>
        private string _clientSendText;

        /// <summary>
        /// The server address
        /// </summary>
        private string _serverAddress;

        /// <summary>
        /// The server ip
        /// </summary>
        private string _serverIp;

        /// <summary>
        /// The server listen button name
        /// </summary>
        private string _serverListenButtonName;

        /// <summary>
        /// The server send
        /// </summary>
        private string _serverSend;

        /// <summary>
        /// The server send button name
        /// </summary>
        private string _serverSendButtonName;

        /// <summary>
        /// The server send interval
        /// </summary>
        private int _serverSendInterval;

        /// <summary>
        /// The server send text
        /// </summary>
        private string _serverSendText;

        /// <summary>
        /// The server receive
        /// </summary>
        private static string _serverReceive;

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SocketModel"/> class.
        /// </summary>
        public SocketModel( )
        {
            _serverAddress = "ws://127.0.0.1:65432";
            _serverSendText = "Hello Client!";
            _serverSendInterval = 1000;
            _serverSend = "Hello Client!";
            _serverSendButtonName = "Auto Send Start";
            _serverListenButtonName = "Start Listen";
            _serverIp = "ws://127.0.0.1:65432/echo";
            _clientSendText = "Hello Server!";
            _clientSendInterval = 1000;
            _clientSend = "Hello Server!";
            _clientConnectButtonName = "Connect";
            _clientSendButtonName = "Auto Send Start";
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
        /// Gets or sets the name of the server listen button.
        /// </summary>
        /// <value>
        /// The name of the server listen button.
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
        /// Gets or sets the server send text.
        /// </summary>
        /// <value>
        /// The server send text.
        /// </value>
        public string ServerSendText
        {
            get { return _serverSendText; }
            set
            {
                if( _serverSendText != value )
                {
                    _serverSendText = value;
                    OnPropertyChanged( nameof( ServerSendText ) );
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
        /// Gets or sets the name of the server send button.
        /// </summary>
        /// <value>
        /// The name of the server send button.
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
        /// Gets or sets the server receive.
        /// </summary>
        /// <value>
        /// The server receive.
        /// </value>
        public string ServerReceive
        {
            get { return _serverReceive; }
            set
            {
                if( _serverReceive != value )
                {
                    _serverReceive = value;
                    OnPropertyChanged( nameof( ServerReceive ) );
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
        /// Gets or sets the name of the client connect button.
        /// </summary>
        /// <value>
        /// The name of the client connect button.
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
        /// Gets or sets the client send text.
        /// </summary>
        /// <value>
        /// The client send text.
        /// </value>
        public string ClientSendText
        {
            get { return _clientSendText; }
            set
            {
                if( _clientSendText != value )
                {
                    _clientSendText = value;
                    OnPropertyChanged( nameof( ClientSendText ) );
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
        /// Gets or sets the name of the client send button.
        /// </summary>
        /// <value>
        /// The name of the client send button.
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
        /// Gets or sets the client receive.
        /// </summary>
        /// <value>
        /// The client receive.
        /// </value>
        public string ClientReceive
        {
            get { return _clientReceive; }
            set
            {
                if( _clientReceive != value )
                {
                    _clientReceive = value;
                    OnPropertyChanged( nameof( ClientReceive ) );
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

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
        {
            var _handler = PropertyChanged;
            _handler?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
    }
}