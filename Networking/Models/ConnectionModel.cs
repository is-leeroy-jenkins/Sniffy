// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-11-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-11-2024
// ******************************************************************************************
// <copyright file="Ipv4ConnectionStats.cs" company="Terry D. Eppler">
//    Sniffy is a tiny .NET WPF tool for network interaction written C sharp.
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
//   Ipv4ConnectionStats.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using PcapDotNet.Packets.IpV4;

	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	[ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
	public class ConnectionModel
	{
		/// <summary>
		/// Gets or sets the address a.
		/// </summary>
		/// <value>
		/// The address a.
		/// </value>
		public IpV4Address ClientAddress { get; set; }

		/// <summary>
		/// Gets or sets the address b.
		/// </summary>
		/// <value>
		/// The address b.
		/// </value>
		public IpV4Address ServerAddress { get; set; }

		/// <summary>
		/// Gets or sets the packet count a to b.
		/// </summary>
		/// <value>
		/// The packet count a to b.
		/// </value>
		public long ClientServerPacketCount { get; set; }

		/// <summary>
		/// Gets or sets the packet count b to a.
		/// </summary>
		/// <value>
		/// The packet count b to a.
		/// </value>
		public long ServerClientPacketCount { get; set; }

		/// <summary>
		/// Gets or sets the byte count a to b.
		/// </summary>
		/// <value>
		/// The byte count a to b.
		/// </value>
		public long ClientServerByteCount { get; set; }

		/// <summary>
		/// Gets or sets the byte count b to a.
		/// </summary>
		/// <value>
		/// The byte count b to a.
		/// </value>
		public long ServerClientByteCount { get; set; }

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ConnectionModel"/> class.
		/// </summary>
		/// <param name = "clientAddress" > </param>
		/// <param name = "serverAddress" > </param>
		public ConnectionModel( IpV4Address clientAddress, IpV4Address serverAddress )
		{
			ClientAddress = clientAddress;
			ServerAddress = serverAddress;
			ClientServerPacketCount = 0;
			ServerClientPacketCount = 0;
			ClientServerByteCount = 0;
			ServerClientByteCount = 0;
		}
	}
}