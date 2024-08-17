// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-14-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-14-2024
// ******************************************************************************************
// <copyright file="WebSocketViewModel.cs" company="Terry D. Eppler">
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
//   WebSocketViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Windows.Threading;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using WebSocketSharp.Server;
    using WebSocketSharp;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Sniffy.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
    public class WebSocketViewModel : MainWindowBase
    {
        /// <summary>
        /// The m client automatic send timer
        /// </summary>
        private DispatcherTimer _clientAutoSendTimer;

        /// <summary>
        /// The m server automatic send timer
        /// </summary>
        private DispatcherTimer _serverAutoSendTimer;

        /// <summary>
        /// The ws server
        /// </summary>
        private WebSocketServer _serverSocket;

        /// <summary>
        /// The ws client
        /// </summary>
        public WebSocket ClientSocket;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="WebSocketViewModel"/> class.
        /// </summary>
        public WebSocketViewModel( )
        {
            WebSocketModel = new WebSocketModel( );
        }

        /// <summary>
        /// Gets or sets the web socket model.
        /// </summary>
        /// <value>
        /// The web socket model.
        /// </value>
        public WebSocketModel WebSocketModel { get; set; }

        /// <summary>
        /// Gets or sets the ws recv.
        /// </summary>
        /// <value>
        /// The ws recv.
        /// </value>
        public static ObservableCollection<string> WsRecv { get; set; } =
            new ObservableCollection<string>( );

        /// <summary>
        /// Gets the start listen command.
        /// </summary>
        /// <value>
        /// The start listen command.
        /// </value>
        public ICommand StartListenCommand
        {
            get
            {
                return new RelayCommand( param => StartListen( param ) );
            }
        }

        /// <summary>
        /// Gets the server automatic send command.
        /// </summary>
        /// <value>
        /// The server automatic send command.
        /// </value>
        public ICommand ServerAutoSendCommand
        {
            get
            {
                return new RelayCommand( param => ServerAutoSend( param ) );
            }
        }

        /// <summary>
        /// Gets the server send clear command.
        /// </summary>
        /// <value>
        /// The server send clear command.
        /// </value>
        public ICommand ServerSendClearCommand
        {
            get
            {
                return new RelayCommand( param => ServerSendClear( param ) );
            }
        }

        /// <summary>
        /// Gets the server send command.
        /// </summary>
        /// <value>
        /// The server send command.
        /// </value>
        public ICommand ServerSendCommand
        {
            get
            {
                return new RelayCommand( param => ServerSend( param ) );
            }
        }

        /// <summary>
        /// Gets or sets the ws client recv.
        /// </summary>
        /// <value>
        /// The ws client recv.
        /// </value>
        public static ObservableCollection<string> WsClientRecv { get; set; } =
            new ObservableCollection<string>( );

        /// <summary>
        /// Gets the client connect command.
        /// </summary>
        /// <value>
        /// The client connect command.
        /// </value>
        public ICommand ClientConnectCommand
        {
            get
            {
                return new RelayCommand( param => ClientConnect( param ) );
            }
        }

        /// <summary>
        /// Gets the client send clear command.
        /// </summary>
        /// <value>
        /// The client send clear command.
        /// </value>
        public ICommand ClientSendClearCommand
        {
            get
            {
                return new RelayCommand( param => ClientSendClear( param ) );
            }
        }

        /// <summary>
        /// Gets the client send command.
        /// </summary>
        /// <value>
        /// The client send command.
        /// </value>
        public ICommand ClientSendCommand
        {
            get
            {
                return new RelayCommand( param => ClientSend( param ) );
            }
        }

        /// <summary>
        /// Gets the client automatic send command.
        /// </summary>
        /// <value>
        /// The client automatic send command.
        /// </value>
        public ICommand ClientAutoSendCommand
        {
            get
            {
                return new RelayCommand( param => ClientAutoSend( param ) );
            }
        }

        /// <summary>
        /// Starts the listen.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void StartListen( object parameter )
        {
            if( WebSocketModel.ServerListenButtonName == "Start Listen" )
            {
                WebSocketModel.ServerListenButtonName = "Stop Listen";
                _serverSocket = new WebSocketServer( WebSocketModel.ServerAddress );
                _serverSocket.AddWebSocketService<EchoHandler>( "/echo" );
                _serverSocket.Start( );
                WsRecv.Add( "[" + DateTime.Now + "][" + "WebSocket Server Started]\n" );
            }
            else
            {
                WebSocketModel.ServerListenButtonName = "Start Listen";
                _serverSocket.Stop( );
                WsRecv.Add( "[" + DateTime.Now + "][" + "WebSocket Server Stopped]\n" );
            }
        }

        /// <summary>
        /// Servers the automatic send timer function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ServerAutoSendTimerFunc( object sender, EventArgs e )
        {
            _serverSocket.WebSocketServices.Broadcast( WebSocketModel.SendingServerText );
        }

        /// <summary>
        /// Servers the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerAutoSend( object parameter )
        {
            if( WebSocketModel.ServerSendButtonName == "Auto Send Start" )
            {
                WebSocketModel.ServerSendButtonName = "Auto Send Stop";
                _serverAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        WebSocketModel.ServerSendInterval )
                };

                _serverAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                _serverAutoSendTimer.Start( );
            }
            else
            {
                WebSocketModel.ServerSendButtonName = "Auto Send Start";
                _serverAutoSendTimer.Stop( );
            }
        }

        /// <summary>
        /// Servers the send clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerSendClear( object parameter )
        {
            WebSocketModel.SendingServer = "";
            WsRecv.Clear( );
        }

        /// <summary>
        /// Servers the send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ServerSend( object parameter )
        {
            _serverSocket.WebSocketServices.Broadcast( WebSocketModel.SendingServer );
        }

        /// <summary>
        /// Clients the connect.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientConnect( object parameter )
        {
            if( WebSocketModel.ClientConnectButtonName == "Connect" )
            {
                using( var _ws = new WebSocket( WebSocketModel.ServerIp ) )
                {
                    _ws.OnOpen += ( sender, e ) =>
                    {
                        Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Open]\n";
                            WsClientRecv.Add( _time + _str );
                        } ) );
                    };

                    _ws.OnMessage += ( sender, e ) =>
                    {
                        var _fmt = "[WebSocket Message] {0}";
                        var _body = !e.IsPing
                            ? e.Data
                            : "A ping was received.";

                        Console.WriteLine( _fmt, _body );
                        Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[" + e.Data + "]\n";
                            WsClientRecv.Add( _time + _str );
                        } ) );
                    };

                    _ws.OnError += ( sender, e ) =>
                    {
                        var _fmt = "[WebSocket Error] {0}";
                        Console.WriteLine( _fmt, e.Message );
                        Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Error][" + e.Message + "]\n";
                            WsClientRecv.Add( _time + _str );
                            WebSocketModel.ClientConnectButtonName = "Connect";
                        } ) );
                    };

                    _ws.OnClose += ( sender, e ) =>
                    {
                        var _fmt = "[WebSocket Close ({0})] {1}";
                        Console.WriteLine( _fmt, e.Code, e.Reason );
                        Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                        {
                            var _time = "[" + DateTime.Now + "]";
                            var _str = "[WebSocket Close][" + e.Reason + "]\n";
                            WsClientRecv.Add( _time + _str );
                            WebSocketModel.ClientConnectButtonName = "Connect";
                        } ) );
                    };

                    // Connect to the server.
                    ClientSocket = _ws;
                }

                try
                {
                    Task.Run( ( ) =>
                    {
                        ClientSocket.Connect( );
                        WebSocketModel.ClientConnectButtonName = "DisConnect";
                    } );
                }
                catch( Exception )
                {
                    // Do nothing
                }
            }
            else
            {
                WebSocketModel.ClientConnectButtonName = "Connect";
                ClientSocket.Close( );
            }
        }

        /// <summary>
        /// Clients the send clear.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientSendClear( object parameter )
        {
            WebSocketModel.SendingClient = "";
            WsClientRecv.Clear( );
        }

        /// <summary>
        /// Clients the send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientSend( object parameter )
        {
            ClientSocket.Send( WebSocketModel.SendingClient );
        }

        /// <summary>
        /// Clients the automatic send function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClientAutoSendFunc( object sender, EventArgs e )
        {
            ClientSocket.Send( WebSocketModel.SendingClientText );
        }

        /// <summary>
        /// Clients the automatic send.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClientAutoSend( object parameter )
        {
            if( WebSocketModel.ClientSendButtonName == "Auto Send Start" )
            {
                WebSocketModel.ClientSendButtonName = "Auto Send Stop";
                _clientAutoSendTimer = new DispatcherTimer( )
                {
                    Interval = new TimeSpan( 0, 0, 0, 0,
                        WebSocketModel.ClientSendInterval )
                };

                _clientAutoSendTimer.Tick += ClientAutoSendFunc;
                _clientAutoSendTimer.Start( );
            }
            else
            {
                WebSocketModel.ClientSendButtonName = "Auto Send Start";
                _clientAutoSendTimer.Stop( );
            }
        }
    }
}