// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-19-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-19-2024
// ******************************************************************************************
// <copyright file="RouteViewModel.cs" company="Terry D. Eppler">
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
//   RouteViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Input;

    public class RouteViewModel : MainWindowBase
    {
        public ProcessInterface RouteProcess;

        public WebInterface WebInterface;

        public RouteViewModel( )
        {
            RouteModel = new RouteModel( );
            WebInterface = new WebInterface( );
            RouteProcess = new ProcessInterface( );
            GetRouteInfo( );
            RouteModel.NetInfoItemSource = GetLocalNetworkInterface( );
        }

        public RouteModel RouteModel { get; set; }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand( param => RunRoute( param ) );
            }
        }

        public ObservableCollection<Ipv4Result> Ipv4Results { get; set; } =
            new ObservableCollection<Ipv4Result>( );

        public ObservableCollection<Ipv6Result> Ipv6Results { get; set; } =
            new ObservableCollection<Ipv6Result>( );

        public NetInterfaceInfo[ ] GetLocalNetworkInterface( )
        {
            var _interfaces = NetworkInterface.GetAllNetworkInterfaces( );
            var _len = _interfaces.Length;
            var _info = new NetInterfaceInfo[ _len ];

            //string[] name = new string[len];
            for( var _i = 0; _i < _len; _i++ )
            {
                var _ni = _interfaces[ _i ];
                _info[ _i ] = new NetInterfaceInfo( );
                _info[ _i ].Description = _ni.Description;
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
                            _info[ _i ].Name = _niname;
                            _info[ _i ].Ip = _address;
                            _info[ _i ].Mask = _mask;
                        }
                    }
                }
            }

            return _info;
        }

        public void ProcessRouteInfo( string str )
        {
            var _ipv6Pattern = @"(?<a>(\d+))\s*(?<b>(\d+))\s*(?<c>.* )\s*(?<d>.*)";
            var _ipv4Pattern =
                @"(?<a>(\d+)\.(\d+)\.(\d+)\.(\d+))\s*(?<b>(\d+)\.(\d+)\.(\d+)\.(\d+))\s*(?<c>.* )\s*(?<d>(\d+)\.(\d+)\.(\d+)\.(\d+))\s*(?<e>\d+)";

            if( str != null
                && !str.Contains( "..." ) )
            {
                var _m = Regex.Match( str, _ipv4Pattern );
                if( _m.Success )
                {
                    var _dest = _m.Groups[ "a" ].Value;
                    var _mask = _m.Groups[ "b" ].Value;
                    var _gateway = _m.Groups[ "c" ].Value;
                    var _interfaces = _m.Groups[ "d" ].Value;
                    var _metric = _m.Groups[ "e" ].Value;
                    Application.Current.Dispatcher.Invoke( ( ) =>
                    {
                        var _results = new Ipv4Result
                        {
                            Destination = _dest,
                            Mask = _mask,
                            Gateway = _gateway.Trim( ),
                            Interface = _interfaces,
                            Metric = Convert.ToInt32( _metric )
                        };

                        Ipv4Results.Add( _results );
                    } );
                }
                else
                {
                }

                if( str.Contains( ":" ) )
                {
                    var _n = Regex.Match( str, _ipv6Pattern );
                    if( _n.Success )
                    {
                        var _interfaces = _n.Groups[ "a" ].Value;
                        var _metric = _n.Groups[ "b" ].Value;
                        var _dest = _n.Groups[ "c" ].Value;
                        var _gateway = _n.Groups[ "d" ].Value;
                        if( _dest == " "
                            && _gateway != "" )
                        {
                            _dest = _gateway;
                            _gateway = "";
                        }

                        Application.Current.Dispatcher.Invoke( ( ) =>
                        {
                            var _results = new Ipv6Result
                            {
                                Interface = _interfaces,
                                Metric = Convert.ToInt32( _metric ),
                                DestinationAddress = _dest.Trim( ),
                                Gateway = _gateway
                            };

                            Ipv6Results.Add( _results );
                        } );
                    }
                    else
                    {
                    }
                }
            }
        }

        private void ProcessOutputDataReceived( object sender, DataReceivedEventArgs e )
        {
            ProcessRouteInfo( e.Data );
            Console.WriteLine( e.Data );
        }

        public void GetRouteInfo( )
        {
            var _routeCmd = "cmd.exe";
            RouteProcess.StartProcess( _routeCmd, "/c route print", ProcessOutputDataReceived );
        }

        public void RunRoute( object parameter )
        {
            Ipv4Results.Clear( );
            Ipv6Results.Clear( );
            GetRouteInfo( );
        }
    }
}