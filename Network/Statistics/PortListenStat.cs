﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-12-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-12-2024
// ******************************************************************************************
// <copyright file="PortListenStat.cs" company="Terry D. Eppler">
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
//   PortListenStat.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// 
	/// </summary>
	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	[ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
	public class PortListenStat
	{
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="PortListenStat"/> class.
		/// </summary>
		public PortListenStat( )
		{
		}

		/// <summary>
		/// Gets or sets the protocol.
		/// </summary>
		/// <value>
		/// The protocol.
		/// </value>
		public string Protocol { get; set; }

		/// <summary>
		/// Gets or sets the local address.
		/// </summary>
		/// <value>
		/// The local address.
		/// </value>
		public string LocalAddress { get; set; }

		/// <summary>
		/// Gets or sets the local port.
		/// </summary>
		/// <value>
		/// The local port.
		/// </value>
		public string LocalPort { get; set; }

		/// <summary>
		/// Gets or sets the remote address.
		/// </summary>
		/// <value>
		/// The remote address.
		/// </value>
		public string RemoteAddress { get; set; }

		/// <summary>
		/// Gets or sets the remote port.
		/// </summary>
		/// <value>
		/// The remote port.
		/// </value>
		public string RemotePort { get; set; }

		/// <summary>
		/// Gets or sets the status.
		/// </summary>
		/// <value>
		/// The status.
		/// </value>
		public string Status { get; set; }

		/// <summary>
		/// Gets or sets the pid.
		/// </summary>
		/// <value>
		/// The pid.
		/// </value>
		public int Pid { get; set; }

		/// <summary>
		/// Gets or sets the program.
		/// </summary>
		/// <value>
		/// The program.
		/// </value>
		public string Program { get; set; }
	}
}