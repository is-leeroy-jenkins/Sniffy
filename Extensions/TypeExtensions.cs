// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-15-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-15-2024
// ******************************************************************************************
// <copyright file="TypeExtensions.cs" company="Terry D. Eppler">
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
//   TypeExtensions.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Xml.Serialization;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "CompareNonConstrainedGenericWithNull" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public static class TypeExtensions
    {
        /// <summary>
        /// XMLs serialize.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns>
        /// string
        /// </returns>
        public static string SerializeToXml<T>( this T type )
        {
            try
            {
                var _serializer = new XmlSerializer( type.GetType( ) );
                using var _writer = new StringWriter( );
                _serializer?.Serialize( _writer, type );
                var _string = _writer?.GetStringBuilder( )?.ToString( );
                using var _reader = new StringReader( _string );
                return _reader?.ReadToEnd( ) ?? string.Empty;
            }
            catch( Exception ex )
            {
                TypeExtensions.Fail( ex );
                return default( string );
            }
        }

        /// <summary>
        /// Invokes if.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="action">The action.</param>
        public static void InvokeIf( this Control control, Action action )
        {
            try
            {
                ThrowIf.Null( action, nameof( action ) );
                if( control.Dispatcher.CheckAccess( ) )
                {
                    action?.Invoke( );
                }
                else
                {
                    control.Dispatcher.BeginInvoke( action );
                }
            }
            catch( Exception ex )
            {
                TypeExtensions.Fail( ex );
            }
        }

        /// <summary>
        /// Invokes if.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="action">The action.</param>
        public static void InvokeIf( this UserControl control, Action action )
        {
            try
            {
                ThrowIf.Null( action, nameof( action ) );
                if( control.Dispatcher.CheckAccess( ) )
                {
                    action?.Invoke( );
                }
                else
                {
                    control.Dispatcher.BeginInvoke( action );
                }
            }
            catch( Exception ex )
            {
                TypeExtensions.Fail( ex );
            }
        }

        /// <summary>
        /// Invokes if.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="action">The action.</param>
        public static void InvokeIf( this Window window, Action action )
        {
            try
            {
                ThrowIf.Null( action, nameof( action ) );
                if( window.Dispatcher.CheckAccess( ) )
                {
                    action?.Invoke( );
                }
                else
                {
                    window.Dispatcher.BeginInvoke( action );
                }
            }
            catch( Exception ex )
            {
                TypeExtensions.Fail( ex );
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private static void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}