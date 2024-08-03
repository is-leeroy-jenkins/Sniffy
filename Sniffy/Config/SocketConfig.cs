// ******************************************************************************************
//     Assembly:             Bitsy
//     Author:                  Terry D. Eppler
//     Created:                 08-02-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-02-2024
// ******************************************************************************************
// <copyright file="SocketConfig.cs" company="Terry D. Eppler">
//    Sniffy is a tiny web browser used is a budget, finance, and accounting tool for analysts with
//    the US Environmental Protection Agency (US EPA).
//    Copyright ©  2024  Terry Eppler
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
//   SocketConfig.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Sockets;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "UseCollectionExpression" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public static class SocketConfig
    {
        /// <summary>
        /// The int one as bytes
        /// </summary>
        private static readonly byte[ ] IntOneAsBytes = BitConverter.GetBytes( 1 );

        /// <summary>
        /// The win10 version1703
        /// </summary>
        private static readonly Version Win10Version1703 = new Version( 10, 0, 15063 );

        /// <summary>
        /// Disables the Nagle algorithm and delayed ACKs for TCP sockets
        /// in order to improve response time.
        /// </summary>
        /// <param name="socket">The socket.</param>
        /// <param name="enableKeepAlive">if set to
        /// <c>true</c> [enable keep alive].</param>
        /// <remarks>
        /// You should call this method after the
        /// socket is connected, not before. Otherwise,
        /// methods such as <see cref="TcpClient.ConnectAsync(string, int)" /> may throw a
        /// <see cref="PlatformNotSupportedException" /> on non-Windows OSes. See:
        /// https://github.com/dotnet/runtime/issues/24917
        /// </remarks>
        public static void ConfigureSocket( Socket socket, bool enableKeepAlive = false )
        {
            socket.NoDelay = true;
            SocketConfig.DisableTcpDelayedAck( socket );
            if( enableKeepAlive && ( !OperatingSystem.IsWindows( )
                || OperatingSystem.IsWindowsVersionAtLeast( SocketConfig.Win10Version1703.Major,
                    SocketConfig.Win10Version1703.Minor, SocketConfig.Win10Version1703.Build ) ) )
            {
                try
                {
                    socket.SetSocketOption( SocketOptionLevel.Socket, SocketOptionName.KeepAlive,
                        true );

                    socket.SetSocketOption( SocketOptionLevel.Tcp,
                        SocketOptionName.TcpKeepAliveTime, 15 );

                    socket.SetSocketOption( SocketOptionLevel.Tcp,
                        SocketOptionName.TcpKeepAliveInterval, 3 );

                    socket.SetSocketOption( SocketOptionLevel.Tcp,
                        SocketOptionName.TcpKeepAliveRetryCount, 5 );
                }
                catch( SocketException )
                {
                    // Ignore; 
                }
            }
        }

        /// <summary>
        /// Disables the TCP delayed ack.
        /// </summary>
        /// <param name="socket">The socket.</param>
        private static void DisableTcpDelayedAck( Socket socket )
        {
            if( socket.ProtocolType is ProtocolType.Tcp
                && OperatingSystem.IsWindows( ) )
            {
                try
                {
                    const int SIO_TCP_SET_ACK_FREQUENCY = unchecked( (int)0x98000017 );
                    socket.IOControl( SIO_TCP_SET_ACK_FREQUENCY, SocketConfig.IntOneAsBytes,
                        Array.Empty<byte>( ) );
                }
                catch( SocketException )
                {
                    // Ignore; 
                }
            }
        }
    }
}