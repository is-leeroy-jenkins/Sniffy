﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-13-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-13-2024
// ******************************************************************************************
// <copyright file="TcpViewModel.cs" company="Terry D. Eppler">
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
//   TcpViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Net;
	using System.Net.Sockets;
	using System.Windows.Input;
	using System.Windows.Threading;
	using System.Collections.ObjectModel;
	using System.Diagnostics.CodeAnalysis;
	using System.Windows;

	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="Sniffy.MainWindowBase" />
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	[ SuppressMessage( "ReSharper", "InheritdocConsiderUsage" ) ]
	[ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	[ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	[ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
	public class TcpViewModel : MainWindowBase
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
		/// The TCP client socket
		/// </summary>
		private TcpClientSocket _tcpClientSocket;

		/// <summary>
		/// The TCP server socket
		/// </summary>
		private TcpServerSocket _tcpServerSocket;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="TcpViewModel"/> class.
		/// </summary>
		public TcpViewModel( )
		{
			TcpModel = new TcpModel( );
		}

		/// <summary>
		/// Gets or sets the TCP model.
		/// </summary>
		/// <value>
		/// The TCP model.
		/// </value>
		public TcpModel TcpModel { get; set; }

		/// <summary>
		/// Gets or sets the TCP server infos.
		/// </summary>
		/// <value>
		/// The TCP server infos.
		/// </value>
		public ObservableCollection<TcpServerInfo> TcpServerInfos { get; set; } =
			new ObservableCollection<TcpServerInfo>( );

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
		/// Gets the server recv clear command.
		/// </summary>
		/// <value>
		/// The server recv clear command.
		/// </value>
		public ICommand ServerRecvClearCommand
		{
			get
			{
				return new RelayCommand( param => ServerRecvClear( param ) );
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
		/// Gets the client recv clear command.
		/// </summary>
		/// <value>
		/// The client recv clear command.
		/// </value>
		public ICommand ClientRecvClearCommand
		{
			get
			{
				return new RelayCommand( param => ClientRecvClear( param ) );
			}
		}

		/// <summary>
		/// Starts the listen.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void StartListen( object parameter )
		{
			if( TcpModel.ServerListenBtnName == "Start Listen" )
			{
				TcpModel.ServerListenBtnName = "Stop Listen";
				_tcpServerSocket =
					new TcpServerSocket( IPAddress.Any.ToString( ), TcpModel.ListenPort )
					{
						recvEvent = Recv,
						connectEvent = ConnectCallback,
						disConnectEvent = DisConnectCallback
					};

				_tcpServerSocket.Start( );
				TcpModel.ServerStatus += "Tcp Server Started!\n";
			}
			else
			{
				TcpModel.ServerListenBtnName = "Start Listen";
				_tcpServerSocket.CloseAllClientSocket( );
				TcpServerInfos.Clear( );
				TcpModel.ServerStatus += "Tcp Server Stopped!\n";
			}
		}

		/// <summary>
		/// Recvs the specified socket.
		/// </summary>
		/// <param name="socket">The socket.</param>
		/// <param name="message">The message.</param>
		private void Recv( Socket socket, string message )
		{
			Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
			{
				TcpModel.ServerRecv += "[" + socket.RemoteEndPoint + "] :";
				TcpModel.ServerRecv += message;
				TcpModel.ServerRecv += "\n";
			} ) );
		}

		/// <summary>
		/// Connects the callback.
		/// </summary>
		/// <param name="socket">The socket.</param>
		private void ConnectCallback( Socket socket )
		{
			Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
			{
				var _time = DateTime.Now;
				TcpServerInfos.Add( new TcpServerInfo
				{
					RemoteIp = socket.RemoteEndPoint?.ToString( )?.Split( ':' )[ 0 ],
					Port = socket.RemoteEndPoint?.ToString( )?.Split( ':' )[ 1 ],
					Time = _time
				} );

				TcpModel.ServerStatus +=
					"++[" + socket.RemoteEndPoint + "] connected at " + _time + "\n";
			} ) );
		}

		/// <summary>
		/// Dises the connect callback.
		/// </summary>
		/// <param name="socket">The socket.</param>
		private void DisConnectCallback( Socket socket )
		{
			Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
			{
				foreach( var _info in TcpServerInfos )
				{
					if( socket.RemoteEndPoint != null
						&& _info.RemoteIp == socket.RemoteEndPoint.ToString( )?.Split( ':' )[ 0 ]
						&& _info.Port == socket.RemoteEndPoint.ToString( )?.Split( ':' )[ 1 ] )
					{
						TcpServerInfos.Remove( _info );
						TcpModel.ServerStatus += "--[" + socket.RemoteEndPoint
							+ "] disconnected at " + _info.Time + "\n";

						break;
					}
				}
			} ) );
		}

		/// <summary>
		/// Servers the automatic send timer function.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ServerAutoSendTimerFunc( object sender, EventArgs e )
		{
			if( _tcpServerSocket != null )
			{
				_tcpServerSocket.SendMessageToAllClientsAsync( TcpModel.ServerSendStr );
			}
		}

		/// <summary>
		/// Servers the automatic send.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ServerAutoSend( object parameter )
		{
			if( TcpModel.ServerSendBtnName == "Auto Send Start" )
			{
				TcpModel.ServerSendBtnName = "Auto Send Stop";
				_serverAutoSendTimer = new DispatcherTimer( )
				{
					Interval = new TimeSpan( 0, 0, 0, 0,
						TcpModel.ServerSendInterval )
				};

				_serverAutoSendTimer.Tick += ServerAutoSendTimerFunc;
				_serverAutoSendTimer.Start( );
			}
			else
			{
				TcpModel.ServerSendBtnName = "Auto Send Start";
				_serverAutoSendTimer.Stop( );
			}
		}

		/// <summary>
		/// Servers the recv clear.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ServerRecvClear( object parameter )
		{
			TcpModel.ServerRecv = "";
		}

		/// <summary>
		/// Servers the send clear.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ServerSendClear( object parameter )
		{
			TcpModel.ServerSend = "";
		}

		/// <summary>
		/// Servers the send.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ServerSend( object parameter )
		{
			if( _tcpServerSocket != null )
			{
				_tcpServerSocket.SendMessageToAllClientsAsync( TcpModel.ServerSend );
			}
		}

		/// <summary>
		/// Clients the connect.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientConnect( object parameter )
		{
			if( TcpModel.ClientConnectBtnName == "Connect" )
			{
				TcpModel.ClientConnectBtnName = "Disconnect";
				_tcpClientSocket = new TcpClientSocket( TcpModel.ServerIp, TcpModel.ServerPort );
				_tcpClientSocket.recvEvent = ClientRecvCb;
				_tcpClientSocket.connectEvent = ClientConnectCb;
				_tcpClientSocket.disConnectEvent = ClientDisConnectCb;
				_tcpClientSocket.Start( );
			}
			else
			{
				TcpModel.ClientConnectBtnName = "Connect";
				_tcpClientSocket.CloseClientSocket( );
			}
		}

		/// <summary>
		/// Clients the connect cb.
		/// </summary>
		/// <param name="socket">The socket.</param>
		private void ClientConnectCb( Socket socket )
		{
			Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
			{
				var _time = DateTime.Now;
				TcpModel.ClientRecv +=
					"++[" + socket.RemoteEndPoint + "] connected at " + _time + "\n";

				TcpModel.LocalPort = socket.LocalEndPoint.ToString( ).Split( ':' )[ 1 ];
			} ) );
		}

		/// <summary>
		/// Clients the dis connect cb.
		/// </summary>
		/// <param name="socket">The socket.</param>
		private void ClientDisConnectCb( Socket socket )
		{
			Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
			{
				TcpModel.ClientRecv += "--[" + socket.RemoteEndPoint + "] disconnected at "
					+ DateTime.Now + "\n";

				if( TcpModel.ClientConnectBtnName == "Disconnect" )
				{
					TcpModel.ClientConnectBtnName = "Connect";
					_tcpClientSocket.CloseClientSocket( );
				}
			} ) );
		}

		/// <summary>
		/// Clients the recv cb.
		/// </summary>
		/// <param name="msg">The MSG.</param>
		private void ClientRecvCb( string msg )
		{
			Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
			{
				TcpModel.ClientRecv += msg;
				TcpModel.ClientRecv += "\n";
			} ) );
		}

		/// <summary>
		/// Clients the send clear.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientSendClear( object parameter )
		{
			TcpModel.ClientSend = "";
		}

		/// <summary>
		/// Clients the send.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientSend( object parameter )
		{
			if( _tcpClientSocket != null )
			{
				_tcpClientSocket.SendAsync( TcpModel.ClientSend );
			}
		}

		/// <summary>
		/// Clients the automatic send function.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ClientAutoSendFunc( object sender, EventArgs e )
		{
			if( _tcpClientSocket != null )
			{
				_tcpClientSocket.SendAsync( TcpModel.ClientSendStr );
			}
		}

		/// <summary>
		/// Clients the automatic send.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientAutoSend( object parameter )
		{
			if( TcpModel.ClientSendBtnName == "Auto Send Start" )
			{
				TcpModel.ClientSendBtnName = "Auto Send Stop";
				_clientAutoSendTimer = new DispatcherTimer( )
				{
					Interval = new TimeSpan( 0, 0, 0, 0,
						TcpModel.ClientSendInterval )
				};

				_clientAutoSendTimer.Tick += ClientAutoSendFunc;
				_clientAutoSendTimer.Start( );
			}
			else
			{
				TcpModel.ClientSendBtnName = "Auto Send Start";
				_clientAutoSendTimer.Stop( );
			}
		}

		/// <summary>
		/// Clients the recv clear.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientRecvClear( object parameter )
		{
			TcpModel.ClientRecv = "";
		}
	}
}