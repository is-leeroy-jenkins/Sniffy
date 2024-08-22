﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-18-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-18-2024
// ******************************************************************************************
// <copyright file="BytesSuffixConverter.cs" company="Terry D. Eppler">
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
//   BytesSuffixConverter.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Windows.Data.IValueConverter" />
    [ ValueConversion( typeof( long ), typeof( string ) ) ]
    [ SuppressMessage( "ReSharper", "JoinDeclarationAndInitializer" ) ]
    public class BytesSuffixConverter : IValueConverter
    {
        /// <summary>
        /// The suffix
        /// </summary>
        private readonly string[ ] _suffix =
        {
            "B",
            "KB",
            "MB",
            "GB",
            "TB"
        };

        /// <inheritdoc />
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />,
        /// the valid null value is used.
        /// </returns>
        public object Convert( object value, Type targetType, object parameter,
            CultureInfo culture )
        {
            if( value != null )
            {
                var _bytes = ( long )value;
                return FormatBytes( _bytes );
            }
            else
            {
                return default( object );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns <see langword="null" />,
        /// the valid null value is used.
        /// </returns>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public object ConvertBack( object value, Type targetType, object parameter,
            CultureInfo culture )
        {
            throw new NotImplementedException( );
        }

        /// <summary>
        /// Formats the bytes.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        private string FormatBytes( long bytes )
        {
            var _index = 0;
            string _formatBytes;
            double _fileSize = bytes;
            while( ( int )( _fileSize / 1024.0f ) > 0 )
            {
                _fileSize /= 1024.0f;
                _index++;
            }

            _formatBytes = _index == 0
                ? $"{bytes} {_suffix[ _index ]}"
                : $"{_fileSize:0.00} {_suffix[ _index ]}";

            return _formatBytes;
        }
    }
}