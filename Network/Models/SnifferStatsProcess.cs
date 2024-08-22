// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-12-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-12-2024
// ******************************************************************************************
// <copyright file="SnifferStatsProcess.cs" company="Terry D. Eppler">
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
//   SnifferStatsProcess.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using PcapDotNet.Packets;
	using PcapDotNet.Packets.IpV4;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;

	/// <summary>
	/// 
	/// </summary>
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	[ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Local" ) ]
	public static class SnifferStatsProcess
	{
		/// <summary>
		/// Initializes the <see cref="SnifferStatsProcess"/> class.
		/// </summary>
		static SnifferStatsProcess( )
		{
			StopWatch = new Stopwatch( );
			ProtocolStats = new ObservableCollection<ProtocolModel>( )
			{
				new ProtocolModel( IpV4Protocol.Tcp ),
				new ProtocolModel( IpV4Protocol.Udp ),
				new ProtocolModel( IpV4Protocol.Ip ),
				new ProtocolModel( IpV4Protocol.Stream ),
				new ProtocolModel( IpV4Protocol.InternetControlMessageProtocol ),
				new ProtocolModel( IpV4Protocol.InternetGroupManagementProtocol )
			};

			ConnectionStats = new ObservableCollection<ConnectionModel>( );
			PacketCount = 0;
			ByteCount = 0;
		}

		/// <summary>
		/// Gets the stop watch.
		/// </summary>
		/// <value>
		/// The stop watch.
		/// </value>
		public static Stopwatch StopWatch { get; private set; }

		/// <summary>
		/// Gets the packet count.
		/// </summary>
		/// <value>
		/// The packet count.
		/// </value>
		public static long PacketCount { get; private set; }

		/// <summary>
		/// Gets the byte count.
		/// </summary>
		/// <value>
		/// The byte count.
		/// </value>
		public static long ByteCount { get; private set; }

		public static ObservableCollection<ProtocolModel> ProtocolModels { get; private set; }

		/// <summary>
		/// Gets the protocol stats.
		/// </summary>
		/// <value>
		/// The protocol stats.
		/// </value>
		public static ObservableCollection<ProtocolModel> ProtocolStats { get; private set; }

		/// <summary>
		/// Gets the connection stats.
		/// </summary>
		/// <value>
		/// The connection stats.
		/// </value>
		public static ObservableCollection<ConnectionModel> ConnectionStats
		{
			get;
			private set;
		}

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public static void Start( )
		{
			StopWatch.Start( );
			PacketCount = 0;
			ByteCount = 0;
			ConnectionStats.Clear( );
			foreach( var stat in ProtocolStats )
			{
				stat.ByteCount = 0;
				stat.PacketCount = 0;
			}
		}

		/// <summary>
		/// Stops this instance.
		/// </summary>
		public static void Stop( )
		{
			StopWatch.Stop( );
		}

		/// <summary>
		/// Updates the stats.
		/// </summary>
		/// <param name="newPacket">The new packet.</param>
		public static void UpdateStats( Packet newPacket )
		{
			ByteCount += newPacket.Length;
			PacketCount++;
			if( newPacket.Ethernet.IpV4.Protocol == IpV4Protocol.Tcp )
			{
				ProtocolStats[ 0 ].PacketCount++;
				ProtocolStats[ 0 ].ByteCount += newPacket.Length;
			}
			else if( newPacket.Ethernet.IpV4.Protocol == IpV4Protocol.Udp )
			{
				ProtocolStats[ 1 ].PacketCount++;
				ProtocolStats[ 1 ].ByteCount += newPacket.Length;
			}
			else if( newPacket.Ethernet.IpV4.Protocol == IpV4Protocol.Ip )
			{
				ProtocolStats[ 2 ].PacketCount++;
				ProtocolStats[ 2 ].ByteCount += newPacket.Length;
			}
			else if( newPacket.Ethernet.IpV4.Protocol == IpV4Protocol.Stream )
			{
				ProtocolStats[ 3 ].PacketCount++;
				ProtocolStats[ 3 ].ByteCount += newPacket.Length;
			}
			else if( newPacket.Ethernet.IpV4.Protocol
				== IpV4Protocol.InternetControlMessageProtocol )
			{
				ProtocolStats[ 4 ].PacketCount++;
				ProtocolStats[ 4 ].ByteCount += newPacket.Length;
			}
			else if( newPacket.Ethernet.IpV4.Protocol
				== IpV4Protocol.InternetGroupManagementProtocol )
			{
				ProtocolStats[ 5 ].PacketCount++;
				ProtocolStats[ 5 ].ByteCount += newPacket.Length;
			}

			var _connStats = ConnectionStats.Where( c =>
				( c.ClientAddress == newPacket.Ethernet.IpV4.Source
					|| c.ClientAddress == newPacket.Ethernet.IpV4.Destination )
				&& ( c.ServerAddress == newPacket.Ethernet.IpV4.Source
					|| c.ServerAddress == newPacket.Ethernet.IpV4.Destination ) ).FirstOrDefault( );

			if( _connStats == null )
			{
				_connStats = new ConnectionModel( newPacket.Ethernet.IpV4.Source,
					newPacket.Ethernet.IpV4.Destination );

				_connStats.ClientToServerByteCount = newPacket.Length;
				_connStats.ClientToServerPacketCount++;
				ConnectionStats.Add( _connStats );
			}
			else
			{
				if( _connStats.ClientAddress == newPacket.Ethernet.IpV4.Source )
				{
					_connStats.ClientToServerPacketCount++;
					_connStats.ClientToServerByteCount += newPacket.Length;
				}
				else
				{
					_connStats.ServerToClientPacketCount++;
					_connStats.ServerToClientByteCount += newPacket.Length;
				}
			}
		}
	}
}