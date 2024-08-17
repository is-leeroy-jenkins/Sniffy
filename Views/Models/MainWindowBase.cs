// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-12-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-12-2024
// ******************************************************************************************
// <copyright file="MainWindowBase.cs" company="Terry D. Eppler">
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
//   MainWindowBase.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics.CodeAnalysis;
	using System.Runtime.CompilerServices;

	/// <inheritdoc />
	/// <summary>
	/// </summary>
	/// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
	[ SuppressMessage( "ReSharper", "MemberCanBeProtected.Global" ) ]
	[ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
	public class MainWindowBase : INotifyPropertyChanged
	{
		/// <inheritdoc />
		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Updates the specified field.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="field">The field.</param>
		/// <param name="value">The value.</param>
		/// <param name="propertyName">Name of the property.</param>
		public void Update<T>( ref T field, T value, [ CallerMemberName ] string propertyName = null )
		{
			if( EqualityComparer<T>.Default.Equals( field, value ) )
			{
				return;
			}

			field = value;
			OnPropertyChanged( propertyName );
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