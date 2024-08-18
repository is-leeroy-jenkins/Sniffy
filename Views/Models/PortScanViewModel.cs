// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-13-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-13-2024
// ******************************************************************************************
// <copyright file="PortScanViewModel.cs" company="Terry D. Eppler">
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
//   PortScanViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Collections.ObjectModel;
	using System.Diagnostics.CodeAnalysis;
	using System.Net.Sockets;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Input;

	/// <inheritdoc />
	/// <summary>
	/// </summary>
	/// <seealso cref="T:Sniffy.MainWindowBase" />[ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	[ SuppressMessage( "ReSharper", "InheritdocConsiderUsage" ) ]
	[ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
	[ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
	public class PortScanViewModel : MainWindowBase
	{
		/// <summary>
		/// The scan token source
		/// </summary>
		private protected CancellationTokenSource _scanTokenSource;

		/// <summary>
		/// The cancel scan token
		/// </summary>
		private protected CancellationToken cancelScanToken;

		/// <summary>
		/// The port count
		/// </summary>
		public int PortCnt;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="PortScanViewModel"/> class.
		/// </summary>
		public PortScanViewModel( )
		{
			PortScanModel = new PortScanModel( );
		}

		/// <summary>
		/// Gets or sets the port scan model.
		/// </summary>
		/// <value>
		/// The port scan model.
		/// </value>
		public PortScanModel PortScanModel { get; }

		/// <summary>
		/// Gets or sets the port scan results.
		/// </summary>
		/// <value>
		/// The port scan results.
		/// </value>
		public ObservableCollection<PortScanResult> PortScanResults { get; } =
			new ObservableCollection<PortScanResult>( );

		/// <summary>
		/// Gets the port scan command.
		/// </summary>
		/// <value>
		/// The port scan command.
		/// </value>
		public ICommand PortScanCommand
		{
			get
			{
				return new RelayCommand( param => PortScanCmd( param ) );
			}
		}

		/// <summary>
		/// Scans the port.
		/// </summary>
		/// <param name="ip">The ip.</param>
		/// <param name="port">The port.</param>
		private void ScanPort( string ip, int port )
		{
			Console.WriteLine( "Begin Scan..." + port );
			PortScanModel.Port = port.ToString( );
			using( var _client = new TcpClient( ) )
			{
				if( _client.ConnectAsync( ip, port ).Wait( PortScanModel.SocketTimeout ) )
				{
					Console.WriteLine( "port {0,5}tOpen.", port );
					PortScanModel.OpenCount++;
					Application.Current.Dispatcher.Invoke( ( ) =>
					{
						PortScanResults.Add( new PortScanResult
						{
							Port = port,
							Status = "open"
						} );
					} );
				}
				else
				{
					Console.WriteLine( "port {0,5}tClosed.", port );
					PortScanModel.CloseCount++;
				}
			}

			PortCnt--;
			if( PortCnt == 0 )
			{
				PortScanModel.ScanButtonName = "Start";
			}

			Console.WriteLine( "Port Scan Completed!" );
		}

		/// <summary>
		/// Starts the scan asynchronous.
		/// </summary>
		public async void StartScanAsync( )
		{
			_scanTokenSource = new CancellationTokenSource( );
			cancelScanToken = _scanTokenSource.Token;
			var _startPortVal = PortScanModel.StartPort;
			var _stopPortVal = PortScanModel.StopPort;
			var _ipStr = PortScanModel.IpAddress;
			PortCnt = _stopPortVal - _startPortVal;
			if( PortCnt <= 0 )
			{
				PortScanModel.ScanButtonName = "Start";
				MessageBox.Show( "Please make sure (Start Port) < (Stop Port)!" );
				return;
			}

			try
			{
				await Task.Run( ( ) =>
				{
					for( var _i = _startPortVal; _i <= _stopPortVal; _i++ )
					{
						Console.WriteLine( _i.ToString( ) );
						if( _scanTokenSource.IsCancellationRequested )
						{
							break;
						}

						var _j = _i;
						var _task = Task.Run( ( ) =>
						{
							ScanPort( _ipStr, _j );
						}, cancelScanToken );

						Thread.Sleep( 5 );
					}
				}, cancelScanToken );
			}
			catch( Exception )
			{
				// Do nothing
			}
		}

		/// <summary>
		/// Stops the scan task.
		/// </summary>
		internal void StopScanTask( )
		{
			_scanTokenSource.Cancel( );
		}

		/// <summary>
		/// Ports the scan command.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void PortScanCmd( object parameter )
		{
			if( PortScanModel.ScanButtonName == "Start" )
			{
				PortScanModel.ScanButtonName = "Stop";
				PortScanModel.OpenCount = 0;
				PortScanModel.CloseCount = 0;
				PortScanResults.Clear( );
				StartScanAsync( );
			}
			else
			{
				StopScanTask( );
				PortScanModel.ScanButtonName = "Start";
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public class PortScanResult
		{
			/// <summary>
			/// Gets or sets the port.
			/// </summary>
			/// <value>
			/// The port.
			/// </value>
			public int Port { get; set; }

			/// <summary>
			/// Gets or sets the status.
			/// </summary>
			/// <value>
			/// The status.
			/// </value>
			public string Status { get; set; }
		}
	}
}