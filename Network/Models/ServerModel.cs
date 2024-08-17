﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-12-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-12-2024
// ******************************************************************************************
// <copyright file="ServerModel.cs" company="Terry D. Eppler">
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
//   ServerModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System.Diagnostics.CodeAnalysis;

	/// <inheritdoc />
	/// <summary>
	/// </summary>
	/// <seealso cref="T:Sniffy.MainWindowBase" />
	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	public class ServerModel : MainWindowBase
	{
		/// <summary>
		/// The net information item source
		/// </summary>
		private NetInterfaceInfo[ ] _netInfoItemSource;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServerModel"/> class.
		/// </summary>
		public ServerModel( )
		{
		}

		/// <summary>
		/// Gets or sets the net information item source.
		/// </summary>
		/// <value>
		/// The net information item source.
		/// </value>
		public NetInterfaceInfo[ ] NetInfoItemSource
		{
			get
			{
				return _netInfoItemSource;
			}
			set
			{
				if( _netInfoItemSource != value )
				{
					_netInfoItemSource = value;
					OnPropertyChanged( nameof( NetInfoItemSource ) );
				}
			}
		}
	}
}