// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-19-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-19-2024
// ******************************************************************************************
// <copyright file="ServerViewModel.cs" company="Terry D. Eppler">
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
//   ServerViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System.Collections.ObjectModel;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Sniffy.MainWindowBase" />
    public class ServerViewModel : MainWindowBase
    {
        /// <summary>
        /// The server views
        /// </summary>
        private ObservableCollection<object> _serverViews;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerViewModel"/> class.
        /// </summary>
        public ServerViewModel( )
        {
            ServerModel = new ServerModel( );
            _serverViews = new ObservableCollection<object>( );
            _serverViews.Add( new TcpViewModel( ) );
            _serverViews.Add( new UdpViewModel( ) );
            _serverViews.Add( new WebSocketViewModel( ) );
            ServerModel.NetInfoItemSource = GetLocalNetworkInterface( );
        }

        /// <summary>
        /// Gets or sets the server model.
        /// </summary>
        /// <value>
        /// The server model.
        /// </value>
        public ServerModel ServerModel { get; set; }

        /// <summary>
        /// Gets the server views.
        /// </summary>
        /// <value>
        /// The server views.
        /// </value>
        public ObservableCollection<object> ServerViews { get { return _serverViews; } }

        /// <summary>
        /// Gets the local network interface.
        /// </summary>
        /// <returns></returns>
        public NetInterfaceInfo[ ] GetLocalNetworkInterface( )
        {
            var _interfaces = NetworkInterface.GetAllNetworkInterfaces( );
            var _len = _interfaces.Length;
            var _info = new NetInterfaceInfo[ _len ];
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
    }
}