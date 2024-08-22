// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-01-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-01-2024
// ******************************************************************************************
// <copyright file="MetroComboBox.cs" company="Terry D. Eppler">
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
//   MetroComboBox.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using Syncfusion.Windows.Tools.Controls;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:System.Windows.Controls.ComboBox" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Local" ) ]
    public class MetroComboBox : ComboBoxAdv
    {
        /// <summary>
        /// The theme
        /// </summary>
        private protected readonly DarkTheme _theme = new DarkTheme( );

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Sniffy.UI.Controls.ComboBox.MetroComboBox" /> class.
        /// </summary>
        public MetroComboBox( )
            : base( )

        {
            // Control Properties
            SetResourceReference( StyleProperty, typeof( ComboBoxAdv ) );
            Width = 150;
            Height = 30;
            FontFamily = _theme.FontFamily;
            FontSize = _theme.FontSize;
            Padding = new Thickness( 10, 1, 1, 1 );
            Margin = new Thickness( 1 );
            BorderThickness = new Thickness( 1 );
            IsEditable = true;
            AutoCompleteMode = AutoCompleteModes.None;
            AllowMultiSelect = false;
            IsTextSearchEnabled = false;
            AllowSelectAll = false;
            Background = _theme.ControlColor;
            Foreground = _theme.ForeColor;
            BorderBrush = _theme.BorderColor;
        }

        /// <summary>
        /// Creates the item.
        /// </summary>
        /// <param name="name">The name.</param>
        public void CreateItem( string name )
        {
            try
            {
                var _item = new MetroComboBoxItem
                {
                    Background = _theme.ControlColor,
                    Foreground = _theme.ForeColor,
                    BorderBrush = _theme.ControlColor,
                    Content = name,
                    Tag = name,
                    Height = 22
                };

                Items.Add( _item );
            }
            catch( Exception ex )
            {
                Fail( ex );
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