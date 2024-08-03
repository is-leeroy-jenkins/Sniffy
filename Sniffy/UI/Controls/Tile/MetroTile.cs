// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-03-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-03-2024
// ******************************************************************************************
// <copyright file="MetroTile.cs" company="Terry D. Eppler">
//    Sniffy is tiny .NET WPF tool that can be used to establish TCP (raw) or
//    WebSocket connections and exchange text messages for
//    testing/debugging purposes based on WPF, Net 6, and written in C
//    Copyright ©  2022  Terry Eppler
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
//    You can contact me at:   terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   MetroTile.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Syncfusion.Windows.Controls.Notification;

    /// <inheritdoc />
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public class MetroTile : SfHubTile
    {
        /// <summary>
        /// The dark
        /// </summary>
        private protected readonly DarkMode _theme = new DarkMode( );

        /// <summary>
        /// The back color
        /// </summary>
        private protected Color _backColor = new Color( )
        {
            A = 255,
            R = 40,
            G = 40,
            B = 40
        };

        /// <summary>
        /// The back hover color
        /// </summary>
        private protected Color _backHover = new Color( )
        {
            A = 255,
            R = 17,
            G = 53,
            B = 84
        };

        /// <summary>
        /// The fore color
        /// </summary>
        private protected Color _foreColor = new Color( )
        {
            A = 255,
            R = 222,
            G = 222,
            B = 222
        };

        /// <summary>
        /// The fore hover color
        /// </summary>
        private protected Color _foreHover = new Color( )
        {
            A = 255,
            R = 255,
            G = 255,
            B = 255
        };

        /// <summary>
        /// The border color
        /// </summary>
        private protected Color _borderColor = new Color( )
        {
            A = 255,
            R = 40,
            G = 40,
            B = 40
        };

        /// <summary>
        /// The border hover color
        /// </summary>
        private protected Color _borderHover = new Color( )
        {
            A = 255,
            R = 106,
            G = 189,
            B = 252
        };

        public MetroTile( )
            : base( )
        {
            // Control Properties
            SetResourceReference( StyleProperty, typeof( SfHubTile ) );
            Width = 200;
            Height = 100;
            FontFamily = _theme.FontFamily;
            FontSize = _theme.FontSize;
            BorderThickness = _theme.BorderThickness;
            Background = _theme.ControlColor;
            Foreground = _theme.ForeColor;
            BorderBrush = _theme.ControlColor;

            // Wire Events
            MouseEnter += OnMouseEnter;
            MouseLeave += OnMouseLeave;
        }

        /// <summary> Called when [mouse enter]. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e">
        /// The
        /// <see cref="EventArgs"/>
        /// instance containing the event data.
        /// </param>
        private protected virtual void OnMouseEnter( object sender, MouseEventArgs e )
        {
            try
            {
                Background = _theme.DarkBlueColor;
                Foreground = _theme.WhiteColor;
                BorderBrush = _theme.LightBlueColor;
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary> Called when [mouse leave]. </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e">
        /// The
        /// <see cref="EventArgs"/>
        /// instance containing the event data.
        /// </param>
        private protected virtual void OnMouseLeave( object sender, MouseEventArgs e )
        {
            try
            {
                Background = _theme.ControlColor;
                Foreground = _theme.LightBlueColor;
                BorderBrush = _theme.ControlColor;
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Fails the specified ex.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private protected void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}