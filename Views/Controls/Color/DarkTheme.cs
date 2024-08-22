// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-01-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-01-2024
// ******************************************************************************************
// <copyright file="DarkTheme.cs" company="Terry D. Eppler">
//    Sniffy is a tiny .NET WPF tool that can be used to establish TCP (raw) or 
//    WebSocket connections and exchange text messages for testing/debugging purposes.
//
//    Copyright ©  2023 Terry Eppler
// 
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
//    You can contact me at: terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   DarkTheme.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Sniffy.Palette" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    public class DarkTheme
        : Palette
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Sniffy.DarkTheme" /> class.
        /// </summary>
        public DarkTheme( )
            : base( )
        {
            ForeColor = new SolidColorBrush( _foreColor );
            BackColor = new SolidColorBrush( _backColor );
            BorderColor = new SolidColorBrush( _borderColor );
            WallColor = new SolidColorBrush( _wallColor );
            ControlColor = new SolidColorBrush( _controlColor );
            SteelBlueColor = new SolidColorBrush( _steelBlueColor );
            OrangeColor = new SolidColorBrush( _orangeColor );
            GrayColor = new SolidColorBrush( Colors.DarkGray );
            YellowColor = new SolidColorBrush( _yellowColor );
            RedColor = new SolidColorBrush(_redColor);
            DarkBlueColor = new SolidColorBrush(_darkBlueColor);
            KhakiColor = new SolidColorBrush( _khakiColor );
            GreenColor = new SolidColorBrush( _greenColor );
            LightBlueColor = new SolidColorBrush( _lightBlue );
            BlackColor = new SolidColorBrush( _blackColor );
            WhiteColor = new SolidColorBrush( _whiteColor );
            FontFamily = new FontFamily( "Segoe UI" );
            FontSize = 12;
            Padding = new Thickness( 1 );
            Margin = new Thickness( 3 );
            BorderThickness = new Thickness( 1 );
            WindowStyle = WindowStyle.SingleBorderWindow;
            SizeMode = ResizeMode.CanResize;
            StartLocation = WindowStartupLocation.CenterScreen;
            _color = CreateColors( );
            _colorModel = CreateColorModel( );
            _colorMap = CreateColorMap( );
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the colors.
        /// </summary>
        /// <returns></returns>
        private protected override SolidColorBrush[ ] CreateColors( )
        {
            try
            {
                var _array = new[ ]
                {
                    SteelBlueColor,
                    GrayColor,
                    YellowColor,
                    RedColor,
                    DarkBlueColor,
                    KhakiColor,
                    GreenColor,
                    LightBlueColor
                };

                return _array?.Length > 0
                    ? _array
                    : default( SolidColorBrush[ ] );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( SolidColorBrush[ ] );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the color model.
        /// </summary>
        /// <returns>
        /// List( Brush )
        /// </returns>
        private protected override IList<Brush> CreateColorModel( )
        {
            try
            {
                var _list = new List<Brush>
                {
                    SteelBlueColor,
                    GrayColor,
                    YellowColor,
                    RedColor,
                    DarkBlueColor,
                    KhakiColor,
                    GreenColor,
                    OrangeColor,
                    LightBlueColor
                };

                return _list?.Count > 0
                    ? _list
                    : default( List<Brush> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( List<Brush> );
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Creates the color map.
        /// </summary>
        /// <returns>
        /// Dictionary(string, Brush )
        /// </returns>
        private protected override IDictionary<string, Brush> CreateColorMap( )
        {
            try
            {
                var _map = new Dictionary<string, Brush>( );
                _map.Add( "ItemHoverColor", SteelBlueColor );
                _map.Add( "GrayColor", GrayColor );
                _map.Add( "YellowColor", YellowColor );
                _map.Add( "RedColor", RedColor);
                _map.Add( "DarkBlueColor", DarkBlueColor );
                _map.Add( "KhakiColor", KhakiColor );
                _map.Add( "GreenColor", GreenColor );
                _map.Add( "OrangeColor", OrangeColor );
                _map.Add( "LightBlue", LightBlueColor );
                return _map?.Count > 0
                    ? _map
                    : default( IDictionary<string, Brush> );
            }
            catch( Exception ex )
            {
                Fail( ex );
                return default( IDictionary<string, Brush> );
            }
        }
    }
}