// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-13-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-13-2024
// ******************************************************************************************
// <copyright file="UdpViewModel.cs" company="Terry D. Eppler">
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
//   UdpViewModel.cs
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
	public class UdpViewModel : MainWindowBase
	{
		/// <summary>
		/// The m client automatic send timer
		/// </summary>
		private DispatcherTimer _mClientAutoSendTimer;

		/// <summary>
		/// The m server automatic send timer
		/// </summary>
		private DispatcherTimer _mServerAutoSendTimer;

		/// <summary>
		/// The UDP client socket
		/// </summary>
		private UdpClientSocket _udpClientSocket;

		/// <summary>
		/// The UDP server socket
		/// </summary>
		private UdpServerSocket _udpServerSocket;

		/// <summary>
		/// Initializes a new instance of the <see cref="UdpViewModel"/> class.
		/// </summary>
		public UdpViewModel( )
		{
			UdpModel = new UdpModel( );
		}

		/// <summary>
		/// Gets or sets the UDP model.
		/// </summary>
		/// <value>
		/// The UDP model.
		/// </value>
		public UdpModel UdpModel { get; set; }

		/// <summary>
		/// Gets or sets the UDP client infos.
		/// </summary>
		/// <value>
		/// The UDP client infos.
		/// </value>
		public ObservableCollection<UdpClientInfo> UdpClientInfos { get; set; } =
			new ObservableCollection<UdpClientInfo>( );

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
			if( UdpModel.ServerListenBtnName == "Start Listen" )
			{
				UdpModel.ServerListenBtnName = "Stop Listen";
				_udpServerSocket =
					new UdpServerSocket( IPAddress.Any.ToString( ), UdpModel.ListenPort );

				_udpServerSocket.recvEvent = Recv;
				_udpServerSocket.Start( );
				UdpModel.ServerStatus += "Udp Server Started!\n";
			}
			else
			{
				UdpModel.ServerListenBtnName = "Start Listen";
				UdpClientInfos.Clear( );
				UdpModel.ServerStatus += "Udp Server Stopped!\n";
			}
		}

		/// <summary>
		/// Recvs the specified point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <param name="message">The message.</param>
		/// <param name="len">The length.</param>
		private void Recv( EndPoint point, string message, int len )
		{
			Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
			{
				UdpModel.ServerRecv += "[" + point + "] :";
				UdpModel.ServerRecv += message;
				UdpModel.ServerRecv += "\n";
				var _time = DateTime.Now;
				UdpClientInfos.Add( new UdpClientInfo
				{
					RemoteIp = point.ToString( ).Split( ':' )[ 0 ],
					Port = point.ToString( ).Split( ':' )[ 1 ],
					RecvBytes = len,
					Time = _time
				} );

				UdpModel.ServerStatus += "++[" + point + "] connected at " + _time + "\n";
			} ) );
		}

		/// <summary>
		/// Servers the automatic send timer function.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ServerAutoSendTimerFunc( object sender, EventArgs e )
		{
			_udpServerSocket.SendMessageToAllClientsAsync( UdpModel.ServerSendStr );
		}

		/// <summary>
		/// Servers the automatic send.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ServerAutoSend( object parameter )
		{
			if( UdpModel.ServerSendBtnName == "Auto Send Start" )
			{
				UdpModel.ServerSendBtnName = "Auto Send Stop";
				_mServerAutoSendTimer = new DispatcherTimer( )
				{
					Interval = new TimeSpan( 0, 0, 0, 0,
						UdpModel.ServerSendInterval )
				};

				_mServerAutoSendTimer.Tick += ServerAutoSendTimerFunc;
				_mServerAutoSendTimer.Start( );
			}
			else
			{
				UdpModel.ServerSendBtnName = "Auto Send Start";
				_mServerAutoSendTimer.Stop( );
			}
		}

		/// <summary>
		/// Servers the recv clear.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ServerRecvClear( object parameter )
		{
			UdpModel.ServerRecv = "";
		}

		/// <summary>
		/// Servers the send clear.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ServerSendClear( object parameter )
		{
			UdpModel.ServerSend = "";
		}

		/// <summary>
		/// Servers the send.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ServerSend( object parameter )
		{
			_udpServerSocket.SendMessageToAllClientsAsync( UdpModel.ServerSend );
		}

		/// <summary>
		/// Clients the connect.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientConnect( object parameter )
		{
			if( UdpModel.ClientConnectBtnName == "Connect" )
			{
				UdpModel.ClientConnectBtnName = "Disconnect";
				_udpClientSocket = new UdpClientSocket( UdpModel.ServerIp, UdpModel.ServerPort );
				_udpClientSocket.recvEvent = ClientRecvCb;
				_udpClientSocket.Start( );
			}
			else
			{
				UdpModel.ClientConnectBtnName = "Connect";
				_udpClientSocket.CloseClientSocket( );
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
				UdpModel.ClientRecv +=
					"++[" + socket.RemoteEndPoint + "] connected at " + _time + "\n";

				UdpModel.LocalPort = socket.LocalEndPoint.ToString( ).Split( ':' )[ 1 ];
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
				UdpModel.ClientRecv += "--[" + socket.RemoteEndPoint + "] disconnected at "
					+ DateTime.Now + "\n";

				if( UdpModel.ClientConnectBtnName == "Disconnect" )
				{
					UdpModel.ClientConnectBtnName = "Connect";
					_udpClientSocket.CloseClientSocket( );
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
				UdpModel.ClientRecv += msg;
				UdpModel.ClientRecv += "\n";
			} ) );
		}

		/// <summary>
		/// Clients the send clear.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientSendClear( object parameter )
		{
			UdpModel.ClientSend = "";
		}

		/// <summary>
		/// Clients the send.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientSend( object parameter )
		{
			_udpClientSocket.SendAsync( UdpModel.ClientSend );
		}

		/// <summary>
		/// Clients the automatic send function.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void ClientAutoSendFunc( object sender, EventArgs e )
		{
			_udpClientSocket.SendAsync( UdpModel.ClientSendStr );
		}

		/// <summary>
		/// Clients the automatic send.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientAutoSend( object parameter )
		{
			if( UdpModel.ClientSendBtnName == "Auto Send Start" )
			{
				UdpModel.ClientSendBtnName = "Auto Send Stop";
				_mClientAutoSendTimer = new DispatcherTimer( )
				{
					Interval = new TimeSpan( 0, 0, 0, 0,
						UdpModel.ClientSendInterval )
				};

				_mClientAutoSendTimer.Tick += ClientAutoSendFunc;
				_mClientAutoSendTimer.Start( );
			}
			else
			{
				UdpModel.ClientSendBtnName = "Auto Send Start";
				_mClientAutoSendTimer.Stop( );
			}
		}

		/// <summary>
		/// Clients the recv clear.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void ClientRecvClear( object parameter )
		{
			UdpModel.ClientRecv = "";
		}
	}
}