// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-19-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-19-2024
// ******************************************************************************************
// <copyright file="NetworkScanViewModel.cs" company="Terry D. Eppler">
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
//   NetworkScanViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
    public class NetworkScanViewModel : MainWindowBase
    {
        /// <summary>
        /// The cancel scan token
        /// </summary>
        private protected CancellationToken _cancelScanToken;

        /// <summary>
        /// The ip count
        /// </summary>
        private protected int _ipCount;

        /// <summary>
        /// The network scan model
        /// </summary>
        private protected NetworkScanModel _networkScanModel;

        /// <summary>
        /// The scan token source
        /// </summary>
        private protected CancellationTokenSource _scanTokenSource;

        /// <summary>
        /// The web interface
        /// </summary>
        private protected WebInterface _webInterface;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NetworkScanViewModel"/> class.
        /// </summary>
        public NetworkScanViewModel( )
        {
            NetworkScanModel = new NetworkScanModel( );
            WebInterface = new WebInterface( );
            NetworkScanModel.NetInfoItemSource = GetLocalNetworkInterface( );
        }

        /// <summary>
        /// Gets or sets the network scan model.
        /// </summary>
        /// <value>
        /// The network scan model.
        /// </value>
        public NetworkScanModel NetworkScanModel
        {
            get
            {
                return _networkScanModel;
            }
            set
            {
                if( _networkScanModel != value )
                {
                    _networkScanModel = value;
                    OnPropertyChanged( nameof( NetworkScanModel ) );
                }
            }
        }

        /// <summary>
        /// The web interface
        /// </summary>
        public WebInterface WebInterface
        {
            get
            {
                return _webInterface;
            }
            set
            {
                if( _webInterface != value )
                {
                    _webInterface = value;
                    OnPropertyChanged( nameof( WebInterface ) );
                }
            }
        }

        /// <summary>
        /// The ip count
        /// </summary>
        public int IpCount
        {
            get
            {
                return _ipCount;
            }
            set
            {
                if( _ipCount != value )
                {
                    _ipCount = value;
                    OnPropertyChanged( nameof( IpCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the scan token source.
        /// </summary>
        /// <value>
        /// The scan token source.
        /// </value>
        public CancellationTokenSource ScanTokenSource
        {
            get
            {
                return _scanTokenSource;
            }
            set
            {
                if( _scanTokenSource != value )
                {
                    _scanTokenSource = value;
                    OnPropertyChanged( nameof( ScanTokenSource ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the cancel scan token.
        /// </summary>
        /// <value>
        /// The cancel scan token.
        /// </value>
        public CancellationToken CancelScanToken
        {
            get
            {
                return _cancelScanToken;
            }
            set
            {
                if( _cancelScanToken != value )
                {
                    _cancelScanToken = value;
                    OnPropertyChanged( nameof( CancelScanToken ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the ip scan results.
        /// </summary>
        /// <value>
        /// The ip scan results.
        /// </value>
        public ObservableCollection<IpScanResult> IpScanResults { get; set; } =
            new ObservableCollection<IpScanResult>( );

        /// <summary>
        /// Gets the ip scan command.
        /// </summary>
        /// <value>
        /// The ip scan command.
        /// </value>
        public ICommand IpScanCommand
        {
            get
            {
                return new RelayCommand( param => IpScanCmd( param ) );
            }
        }

        /// <summary>
        /// Does the scan.
        /// </summary>
        /// <param name="ip">The ip.</param>
        private void DoScan( string ip )
        {
            PingReply _reply = null;
            string _status = null;
            string _hostname = null;
            string _time = null;
            string _ttl = null;
            Console.WriteLine( ip );
            _reply = WebInterface.PingSweep( ip );
            NetworkScanModel.Ip = ip;
            if( _reply != null )
            {
                if( _reply.Status == IPStatus.Success )
                {
                    _status = "Online";
                    _hostname = WebInterface.GetHostName( ip );
                    _time = _reply.RoundtripTime.ToString( );
                    _ttl = _reply.Options.Ttl.ToString( );
                    Application.Current.Dispatcher.Invoke( ( ) =>
                    {
                        var _item = new IpScanResult
                        {
                            IpAddress = ip,
                            Name = _hostname,
                            Status = _status,
                            Time = _time,
                            TTL = _ttl
                        };

                        IpScanResults.Add( _item );
                    } );

                    NetworkScanModel.OnlineCount++;
                }
                else
                {
                    _status = "Timeout";
                    NetworkScanModel.OfflineCount++;
                }
            }
            else
            {
                _status = "Offline";
                NetworkScanModel.OfflineCount++;
            }

            IpCount--;
            Console.WriteLine( IpCount.ToString( ) );
            if( IpCount == 0 )
            {
                NetworkScanModel.ScanButtonName = "Start";
            }
        }

        /// <summary>
        /// Starts the scan asynchronous.
        /// </summary>
        public async void StartScanAsync( )
        {
            _scanTokenSource = new CancellationTokenSource( );
            _cancelScanToken = _scanTokenSource.Token;
            var _startIpVal = WebInterface.IpAddressToLongBackwards( NetworkScanModel.StartIpAddress );
            var _startIpStr = WebInterface.LongToIpAddress( _startIpVal );
            var _endIpVal = WebInterface.IpAddressToLongBackwards( NetworkScanModel.StopIpAddress );
            var _endIpStr = WebInterface.LongToIpAddress( _endIpVal );
            IpCount = ( int )( _endIpVal - _startIpVal );
            if( IpCount <= 0 )
            {
                MessageBox.Show( "Please make sure (Start Ip) < (Stop Ip)!" );
                return;
            }

            try
            {
                await Task.Run( ( ) =>
                {
                    for( var _i = _startIpVal; _i <= _endIpVal; _i++ )
                    {
                        Console.WriteLine( _i.ToString( ) );
                        var _j = _i;
                        var _ipStr = WebInterface.LongToIpAddress( _j );
                        var _task = Task.Run( ( ) =>
                        {
                            DoScan( _ipStr );
                        }, _cancelScanToken );

                        Thread.Sleep( 1 );
                    }
                }, _cancelScanToken );
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
        /// Ips the scan command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void IpScanCmd( object parameter )
        {
            if( NetworkScanModel.ScanButtonName == "Start" )
            {
                NetworkScanModel.ScanButtonName = "Stop";
                NetworkScanModel.OfflineCount = 0;
                NetworkScanModel.OnlineCount = 0;
                IpScanResults.Clear( );

                //Do Scan
                StartScanAsync( );
            }
            else
            {
                //Do Stop
                StopScanTask( );
                NetworkScanModel.ScanButtonName = "Start";
            }
        }

        /// <summary>
        /// Gets the local network interface.
        /// </summary>
        /// <returns></returns>
        public NetInterfaceInfo[ ] GetLocalNetworkInterface( )
        {
            var _interfaces = NetworkInterface.GetAllNetworkInterfaces( );
            var _len = _interfaces.Length;
            var _info = new NetInterfaceInfo[ _len ];
            var _j = 0;
            for( var _i = 0; _i < _len; _i++ )
            {
                var _ni = _interfaces[ _i ];
                if( _ni.OperationalStatus == OperationalStatus.Up )
                {
                    var _property = _ni.GetIPProperties( );
                    foreach( var _ip in _property.UnicastAddresses )
                    {
                        if( _ip.Address.AddressFamily == AddressFamily.InterNetwork )
                        {
                            var _address = _ip.Address.ToString( );
                            var _niname = _ni.Name;
                            var _mask = _ip.IPv4Mask.ToString( );
                            _info[ _j ] = new NetInterfaceInfo
                            {
                                Description = _ni.Description,
                                Name = _niname,
                                Ip = _address,
                                Mask = _mask
                            };

                            _j++;
                        }
                    }
                }
            }

            return _info;
        }
    }
}