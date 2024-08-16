﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-13-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-13-2024
// ******************************************************************************************
// <copyright file="PerformanceCommandConverter.cs" company="Terry D. Eppler">
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
//   PerformanceCommandConverter.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using System.Diagnostics.CodeAnalysis;

	/// <summary>
	/// 
	/// </summary>
	/// <seealso cref="System.Windows.Data.IMultiValueConverter" />
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	[ SuppressMessage( "ReSharper", "InheritdocConsiderUsage" ) ]
	[ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	[ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	[ SuppressMessage( "ReSharper", "ConvertIfStatementToSwitchStatement" ) ]
	public class PerformanceCommandConverter : IMultiValueConverter
	{
		/// <summary>
		/// Converts source values to a value for the binding target.
		/// The data binding engine calls this method when it propagates the values
		/// from source bindings to the binding target.
		/// </summary>
		/// <param name="values">The array of values that the source bindings in the
		/// <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value
		/// <see cref="F:System.Windows.DependencyProperty.UnsetValue" />
		/// indicates that the source binding has no value to provide for conversion.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value.If the method returns <see langword="null" />,
		/// the valid <see langword="null" /> value is used.A return value of
		/// <see cref="T:System.Windows.DependencyProperty" />.
		/// <see cref="F:System.Windows.DependencyProperty.UnsetValue" />
		/// indicates that the converter did not produce a value, and that the binding will use the
		/// <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> if it is available,
		/// or else will use the default value.A return value of
		/// <see cref="T:System.Windows.Data.Binding" />.
		/// <see cref="F:System.Windows.Data.Binding.DoNothing" />
		/// indicates that the binding does not transfer the value or use the
		/// <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> or the default value.
		/// </returns>
		public object Convert( object[ ] values, Type targetType, object parameter,
			CultureInfo culture )
		{
			try
			{
				var _command = "";
				var _role = values[ 0 ].ToString( );
				var _serverIp = values[ 1 ].ToString( );
				var _port = ( int )values[ 2 ];
				var _parallel = ( int )values[ 3 ];
				var _time = ( int )values[ 4 ];
				var _interval = ( int )values[ 5 ];
				var _tcpFlag = ( bool )values[ 6 ];
				var _tcpWindowSize = ( int )values[ 7 ];
				var _tcpWindowUnit = values[ 8 ].ToString( );
				var _udpFlag = ( bool )values[ 9 ];
				var _bandwidth = ( int )values[ 10 ];
				var _bandwidthUnit = values[ 11 ].ToString( );
				var _packetLen = ( int )values[ 12 ];
				var _reverse = ( bool )values[ 13 ];
				if( _role == "-s" )
				{
					_command += _role;
					_command += " -p ";
					_command += _port;
					_command += " -i ";
					_command += _interval;
				}
				else if( _role == "-c" )
				{
					_command += _role;
					_command += " ";
					_command += _serverIp;
					_command += " -p ";
					_command += _port;
					_command += " -P ";
					_command += _parallel;
					_command += " -t ";
					_command += _time;
					_command += " -i ";
					_command += _interval;
					if( _tcpFlag )
					{
						_command += " -w ";
						_command += _tcpWindowSize;
						_command += _tcpWindowUnit;
					}

					if( _udpFlag )
					{
						_command += " -b ";
						_command += _bandwidth;
						_command += _bandwidthUnit;
						_command += " -u ";
					}

					if( _packetLen > 0 )
					{
						_command += " -l ";
						_command += _packetLen;
					}

					if( _reverse )
					{
						_command += " -R";
					}
				}
				else
				{
					_command = "command not support!";
				}

				return _command;
			}
			catch( Exception )
			{
				return null;
			}
		}

		/// <summary>
		/// Converts a binding target value to the source binding values.
		/// </summary>
		/// <param name="value">
		/// The value that the binding target produces.</param>
		/// <param name="targetTypes">The array of types to convert to.
		/// The array length indicates the number and types of values that
		/// are suggested for the method to return.
		/// </param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// An array of values that have been converted from the
		/// target value back to the source values.
		/// </returns>
		/// <exception cref="System.NotImplementedException">
		/// </exception>
		public object[ ] ConvertBack( object value, Type[ ] targetTypes, object parameter,
			CultureInfo culture )
		{
			try
			{
				throw new NotImplementedException( );
			}
			catch( Exception )
			{
				return null;
			}
		}
	}
}