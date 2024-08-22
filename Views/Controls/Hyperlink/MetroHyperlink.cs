// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-08-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-08-2024
// ******************************************************************************************
// <copyright file="MetroHyperlink.cs" company="Terry D. Eppler">
//    Sniffy is a quick & dirty WPF application that interacts with OpenAI GPT-3.5 Turbo API
//    based on NET6 and written in C-Sharp.
// 
//    Copyright ©  2024  Terry D. Eppler
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
//   MetroHyperlink.cs
// </summary>
// ******************************************************************************************



namespace Sniffy
{
    using ModernWpf.Controls;
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Media;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Wpf.Ui.Controls.HyperlinkButton" />
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    public class MetroHyperlink : HyperlinkButton
    {
        /// <summary>
        /// The theme
        /// </summary>
        private protected readonly DarkTheme _theme = new DarkTheme( );

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Sniffy.MetroHyperlink" /> class.
        /// </summary>
        public MetroHyperlink( )
            : base( )
        {
            // Basic Settings
            Height = 110;
            Width = 22;
            FontFamily = _theme.FontFamily;
            FontSize = _theme.FontSize;
            Background = _theme.Transparent;
            Foreground = _theme.BorderColor;
            BorderBrush = _theme.Transparent;
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Sniffy.MetroHyperlink" /> class.
        /// </summary>
        /// <param name="text"> </param>
        /// <param name="uri">The URI.</param>
        public MetroHyperlink( string text, string uri )
            : this( )
        {
            Content = text;
            NavigateUri = new Uri( uri );
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