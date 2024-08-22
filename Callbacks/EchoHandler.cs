// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-20-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-20-2024
// ******************************************************************************************
// <copyright file="EchoHandler.cs" company="Terry D. Eppler">
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
//   EchoHandler.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using WebSocketSharp;
    using WebSocketSharp.Server;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:WebSocketSharp.Server.WebSocketBehavior" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    public class EchoHandler : WebSocketBehavior
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Sniffy.EchoHandler" /> class.
        /// </summary>
        public EchoHandler( )
        {
        }

        /// <summary>
        /// Gets or sets the socket receiver.
        /// </summary>
        /// <value>
        /// The socket receiver.
        /// </value>
        public static ObservableCollection<string> SocketReceiver { get; set; } =
            new ObservableCollection<string>( );

        /// <inheritdoc />
        /// <summary>
        /// Called when the <see cref="T:WebSocketSharp.WebSocket" /> 
        /// used in a session receives a message.
        /// </summary>
        /// <param name="e">A <see cref="T:WebSocketSharp.MessageEventArgs" /> 
        /// that represents the event data passed to
        /// a <see cref="E:WebSocketSharp.WebSocket.OnMessage" /> event.</param>
        protected override void OnMessage( MessageEventArgs e )
        {
            Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
            {
                var _time = "[" + StartTime + "][";
                var _from = Context.UserEndPoint.ToString( );
                var _str = "][" + e.Data + "]\n";
                SocketReceiver.Add( _time + _from + _str );
            } ) );

            Send( e.Data );
        }

        /// <inheritdoc />
        /// <summary>
        /// Called when the WebSocket connection 
        /// used in a session has been established.
        /// </summary>
        protected override void OnOpen( )
        {
            var _time = "[" + StartTime + "][";
            var _from = Context.UserEndPoint.ToString( );
            var _status = "][" + State + "]\n";
            SocketReceiver.Add( _time + _from + _status );
        }

        /// <inheritdoc />
        /// <summary>
        /// Raises the Close event.
        /// </summary>
        /// <param name="e">A <see cref="T:WebSocketSharp.CloseEventArgs" />
        /// that represents the event data passed to
        /// a <see cref="E:WebSocketSharp.WebSocket.OnClose" /> event.</param>
        protected override void OnClose( CloseEventArgs e )
        {
            var _time = "[" + StartTime + "][";
            var _reason = e.Reason;
            var _status = "][" + State + "]\n";
            SocketReceiver.Add( _time + _reason + _status );
        }

        /// <inheritdoc />
        /// <summary>
        /// Called when the <see cref="T:WebSocketSharp.WebSocket" />
        /// used in a session gets an error.
        /// </summary>
        /// <param name="e">A <see cref="T:WebSocketSharp.ErrorEventArgs" />
        /// that represents the event data passed to
        /// a <see cref="E:WebSocketSharp.WebSocket.OnError" /> event.</param>
        protected override void OnError( ErrorEventArgs e )
        {
            var _time = "[" + StartTime + "][";
            var _reason = e.Message;
            var _status = "][" + State + "]\n";
            SocketReceiver.Add( _time + _reason + _status );
        }
    }
}