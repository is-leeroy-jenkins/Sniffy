// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-20-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-20-2024
// ******************************************************************************************
// <copyright file="ConnectionModel.cs" company="Terry D. Eppler">
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
//   ConnectionModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;
    using PcapDotNet.Packets.IpV4;

    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class ConnectionModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The client address
        /// </summary>
        private protected IpV4Address _clientAddress;

        /// <summary>
        /// The client to server byte count
        /// </summary>
        private protected long _clientToServerByteCount;

        /// <summary>
        /// The client to server packet count
        /// </summary>
        private protected long _clientToServerPacketCount;

        /// <summary>
        /// The server address
        /// </summary>
        private protected IpV4Address _serverAddress;

        /// <summary>
        /// The server to client byte count
        /// </summary>
        private protected long _serverToClientByteCount;

        /// <summary>
        /// The server to client packet count
        /// </summary>
        private protected long _serverToClientPacketCount;

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ConnectionModel"/> class.
        /// </summary>
        /// <param name = "clientAddress" > </param>
        /// <param name = "serverAddress" > </param>
        public ConnectionModel( IpV4Address clientAddress, IpV4Address serverAddress )
        {
            _clientAddress = clientAddress;
            _serverAddress = serverAddress;
            _clientToServerPacketCount = 0;
            _serverToClientPacketCount = 0;
            _clientToServerByteCount = 0;
            _serverToClientByteCount = 0;
        }

        /// <summary>
        /// Gets or sets the address a.
        /// </summary>
        /// <value>
        /// The address a.
        /// </value>
        public IpV4Address ClientAddress
        {
            get
            {
                return _clientAddress;
            }
            set
            {
                if( _clientAddress != value )
                {
                    _clientAddress = value;
                    OnPropertyChanged( nameof( ClientAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the address b.
        /// </summary>
        /// <value>
        /// The address b.
        /// </value>
        public IpV4Address ServerAddress
        {
            get
            {
                return _serverAddress;
            }
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
        /// Gets or sets the packet count a to b.
        /// </summary>
        /// <value>
        /// The packet count a to b.
        /// </value>
        public long ClientToServerPacketCount
        {
            get
            {
                return _clientToServerPacketCount;
            }
            set
            {
                if( _clientToServerPacketCount != value )
                {
                    _clientToServerPacketCount = value;
                    OnPropertyChanged( nameof( ClientToServerPacketCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the packet count b to a.
        /// </summary>
        /// <value>
        /// The packet count b to a.
        /// </value>
        public long ServerToClientPacketCount
        {
            get
            {
                return _serverToClientPacketCount;
            }
            set
            {
                if( _serverToClientPacketCount != value )
                {
                    _serverToClientPacketCount = value;
                    OnPropertyChanged( nameof( ServerToClientPacketCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the byte count a to b.
        /// </summary>
        /// <value>
        /// The byte count a to b.
        /// </value>
        public long ClientToServerByteCount
        {
            get
            {
                return _clientToServerByteCount;
            }
            set
            {
                if( _clientToServerByteCount != value )
                {
                    _clientToServerByteCount = value;
                    OnPropertyChanged( nameof( ClientToServerByteCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the byte count b to a.
        /// </summary>
        /// <value>
        /// The byte count b to a.
        /// </value>
        public long ServerToClientByteCount
        {
            get
            {
                return _serverToClientByteCount;
            }
            set
            {
                if( _serverToClientByteCount != value )
                {
                    _serverToClientByteCount = value;
                    OnPropertyChanged( nameof( ServerToClientByteCount ) );
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