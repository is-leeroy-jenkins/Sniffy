// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-18-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-18-2024
// ******************************************************************************************
// <copyright file="MenuItems.cs" company="Terry D. Eppler">
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
//   MenuItems.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "InheritdocConsiderUsage" ) ]
    [ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
    public class MenuItems : INotifyPropertyChanged
    {
        /// <summary>
        /// The menu image
        /// </summary>
        private string _menuImage;

        /// <summary>
        /// The menu name
        /// </summary>
        private string _menuName;

        /// <summary>
        /// The tag
        /// </summary>
        private object _tag;

        /// <summary>
        /// The tool tip
        /// </summary>
        private string _toolTip;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MenuItems"/> class.
        /// </summary>
        public MenuItems( )
        {
        }

        /// <summary>
        /// Gets or sets the name of the menu.
        /// </summary>
        /// <value>
        /// The name of the menu.
        /// </value>
        public string MenuName
        {
            get
            {
                return _menuName;
            }
            set
            {
                if( _menuName != value )
                {
                    _menuName = value;
                    OnPropertyChanged( nameof( MenuName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the menu image.
        /// </summary>
        /// <value>
        /// The menu image.
        /// </value>
        public string MenuImage
        {
            get
            {
                return _menuImage;
            }
            set
            {
                if( _menuImage != value )
                {
                    _menuImage = value;
                    OnPropertyChanged( nameof( MenuName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public object Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                if( _tag != value )
                {
                    _tag = value;
                    OnPropertyChanged( nameof( Tag ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the tool tip.
        /// </summary>
        /// <value>
        /// The tool tip.
        /// </value>
        public string ToolTip
        {
            get
            {
                return _toolTip;
            }
            set
            {
                if( _toolTip != value )
                {
                    _toolTip = value;
                    OnPropertyChanged( nameof( ToolTip ) );
                }
            }
        }

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">
        /// Name of the property.
        /// </param>
        public void OnPropertyChanged( [ CallerMemberName ] string propertyName = null )
        {
            var _handler = PropertyChanged;
            _handler?.Invoke( this, new PropertyChangedEventArgs( propertyName ) );
        }
    }
}