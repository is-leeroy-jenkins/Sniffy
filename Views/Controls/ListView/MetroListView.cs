// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-12-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-12-2024
// ******************************************************************************************
// <copyright file="MetroListView.cs" company="Terry D. Eppler">
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
//   MetroListView.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Windows.Controls;
	using System.Windows.Media;

	/// <inheritdoc />
	/// <summary>
	/// </summary>
	/// <seealso cref="T:System.Windows.Controls.ListView" />
	[ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
	[ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	public class MetroListView : ListView
	{
		/// <summary>
		/// The theme
		/// </summary>
		private protected readonly DarkTheme _theme = new DarkTheme( );

		/// <inheritdoc />
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="T:Sniffy.MetroListView" /> class.
		/// </summary>
		public MetroListView( )
			: base( )
		{
			// Control Properties
			Width = 300;
			Height = 270;
			FontSize = 12;
			Background = _theme.ControlColor;
			BorderBrush = _theme.BorderColor;
			Foreground = _theme.ForeColor;
		}

		/// <summary>
		/// Fails the specified _ex.
		/// </summary>
		/// <param name="_ex">The _ex.</param>
		private protected void Fail( Exception _ex )
		{
			var _error = new ErrorWindow( _ex );
			_error?.SetText( );
			_error?.ShowDialog( );
		}
	}
}