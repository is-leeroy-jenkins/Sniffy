// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-15-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-15-2024
// ******************************************************************************************
// <copyright file="ProtocolStats.cs" company="Terry D. Eppler">
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
//   ProtocolStats.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using PcapDotNet.Packets.IpV4;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Sniffy.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public class ProtocolStats : MainWindowBase
    {
        /// <summary>
        /// The byte count
        /// </summary>
        private long _byteCount;

        /// <summary>
        /// The packet count
        /// </summary>
        private long _packetCount;

        public ProtocolStats( )
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProtocolStats"/> class.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        public ProtocolStats( IpV4Protocol protocol )
        {
            Protocol = protocol;
            PacketCount = 0;
            ByteCount = 0;
        }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public IpV4Protocol Protocol { get; set; }

        /// <summary>
        /// Gets or sets the packet count.
        /// </summary>
        /// <value>
        /// The packet count.
        /// </value>
        public long PacketCount
        {
            get
            {
                return _packetCount;
            }
            set
            {
                if( _packetCount != value )
                {
                    _packetCount = value;
                    OnPropertyChanged( nameof( PacketCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the byte count.
        /// </summary>
        /// <value>
        /// The byte count.
        /// </value>
        public long ByteCount
        {
            get
            {
                return _byteCount;
            }
            set
            {
                if( _byteCount != value )
                {
                    _byteCount = value;
                    OnPropertyChanged( nameof( ByteCount ) );
                }
            }
        }
    }
}