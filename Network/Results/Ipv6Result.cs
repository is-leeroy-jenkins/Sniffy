// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-20-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-20-2024
// ******************************************************************************************
// <copyright file="Ipv6Result.cs" company="Terry D. Eppler">
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
//   Ipv6Result.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.CompilerServices;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class Ipv6Result : INotifyPropertyChanged
    {
        /// <summary>
        /// The destination address
        /// </summary>
        private protected string _destinationAddress;

        /// <summary>
        /// The gateway
        /// </summary>
        private protected string _gateway;

        /// <summary>
        /// The interface
        /// </summary>
        private protected string _interface;

        /// <summary>
        /// The metric
        /// </summary>
        private protected int _metric;

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Ipv6Result"/> class.
        /// </summary>
        public Ipv6Result( )
        {
        }

        ///<summary>
        /// Gets or sets the interface.
        /// </summary>
        /// <value>
        /// The interface.
        /// </value>
        public string Interface
        {
            get
            {
                return _interface;
            }
            set
            {
                if( _interface != value )
                {
                    _interface = value;
                    OnPropertyChanged( nameof( Interface ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the metric.
        /// </summary>
        /// <value>
        /// The metric.
        /// </value>
        public int Metric
        {
            get
            {
                return _metric;
            }
            set
            {
                if( _metric != value )
                {
                    _metric = value;
                    OnPropertyChanged( nameof( Metric ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the dest address.
        /// </summary>
        /// <value>
        /// The dest address.
        /// </value>
        public string DestinationAddress
        {
            get
            {
                return _destinationAddress;
            }
            set
            {
                if( _destinationAddress != value )
                {
                    _destinationAddress = value;
                    OnPropertyChanged( nameof( DestinationAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the gateway.
        /// </summary>
        /// <value>
        /// The gateway.
        /// </value>
        public string Gateway
        {
            get
            {
                return _gateway;
            }
            set
            {
                if( _gateway != value )
                {
                    _gateway = value;
                    OnPropertyChanged( nameof( Gateway ) );
                }
            }
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