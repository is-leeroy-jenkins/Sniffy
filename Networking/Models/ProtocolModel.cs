// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-18-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-18-2024
// ******************************************************************************************
// <copyright file="ProtocolModel.cs" company="Terry D. Eppler">
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
//   ProtocolModel.cs
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
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class ProtocolModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The client address
        /// </summary>
        private protected string _clientAddress;

        /// <summary>
        /// The client port
        /// </summary>
        private protected string _clientPort;

        /// <summary>
        /// The identifier
        /// </summary>
        private protected int _id;

        /// <summary>
        /// The program name
        /// </summary>
        private protected string _programName;

        /// <summary>
        /// The protocol
        /// </summary>
        private protected string _protocol;

        /// <summary>
        /// The server address
        /// </summary>
        private protected string _serverAddress;

        /// <summary>
        /// The server port
        /// </summary>
        private protected string _serverPort;

        /// <summary>
        /// The status
        /// </summary>
        private protected string _status;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ProtocolModel"/> class.
        /// </summary>
        public ProtocolModel( )
        {
        }

        /// <summary>
        /// Gets or sets the protocol.
        /// </summary>
        /// <value>
        /// The protocol.
        /// </value>
        public string Protocol
        {
            get
            {
                return _protocol;
            }
            set
            {
                if( _protocol != value )
                {
                    _protocol = value;
                    OnPropertyChanged( nameof( _protocol ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the local address.
        /// </summary>
        /// <value>
        /// The local address.
        /// </value>
        public string ClientAddress
        {
            get
            {
                return _clientAddress;
            }
            set
            {
                if( _clientAddress != value )
                {
                    _clientAddress = value;
                    OnPropertyChanged( nameof( _clientAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the local port.
        /// </summary>
        /// <value>
        /// The local port.
        /// </value>
        public string ClientPort
        {
            get
            {
                return _clientPort;
            }
            set
            {
                if( _clientPort != value )
                {
                    _clientPort = value;
                    OnPropertyChanged( nameof( _clientAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the remote address.
        /// </summary>
        /// <value>
        /// The remote address.
        /// </value>
        public string ServerAddress
        {
            get
            {
                return _serverAddress;
            }
            set
            {
                if( _serverAddress != value )
                {
                    _serverAddress = value;
                    OnPropertyChanged( nameof( _serverAddress ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the remote port.
        /// </summary>
        /// <value>
        /// The remote port.
        /// </value>
        public string ServerPort
        {
            get
            {
                return _serverPort;
            }
            set
            {
                if( _serverPort != value )
                {
                    _serverPort = value;
                    OnPropertyChanged( nameof( _serverPort ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if( _status != value )
                {
                    _status = value;
                    OnPropertyChanged( nameof( _status ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the pid.
        /// </summary>
        /// <value>
        /// The pid.
        /// </value>
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                if( _id != value )
                {
                    _id = value;
                    OnPropertyChanged( nameof( _id ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        /// <value>
        /// The program.
        /// </value>
        public string ProgramName
        {
            get
            {
                return _programName;
            }
            set
            {
                if( _programName != value )
                {
                    _programName = value;
                    OnPropertyChanged( nameof( ProgramName ) );
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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