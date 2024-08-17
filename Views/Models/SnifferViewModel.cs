﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-13-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-13-2024
// ******************************************************************************************
// <copyright file="SnifferViewModel.cs" company="Terry D. Eppler">
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
//   SnifferViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System.Collections.ObjectModel;
	using System.Diagnostics.CodeAnalysis;

	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	[ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Local" ) ]
	[ SuppressMessage( "ReSharper", "ConvertToAutoProperty" ) ]
	public class SnifferViewModel : MainWindowBase
	{
		/// <summary>
		/// The sniffer views
		/// </summary>
		private ObservableCollection<object> _snifferViews;

		/// <summary>
		/// Initializes a new instance of the <see cref="SnifferViewModel"/> class.
		/// </summary>
		public SnifferViewModel( )
		{
			_snifferViews = new ObservableCollection<object>
			{
				new SnifferCaptureViewModel( ),
				new SnifferStatsViewModel( )
			};
		}

		/// <summary>
		/// Gets the sniffer views.
		/// </summary>
		/// <value>
		/// The sniffer views.
		/// </value>
		public ObservableCollection<object> SnifferViews
		{
			get
			{
				return _snifferViews;
			}
		}
	}
}