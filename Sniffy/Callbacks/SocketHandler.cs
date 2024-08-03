// ******************************************************************************************
//     Assembly:             Bitsy
//     Author:                  Terry D. Eppler
//     Created:                 08-02-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-02-2024
// ******************************************************************************************
// <copyright file="SocketHandler.cs" company="Terry D. Eppler">
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
//   SocketHandler.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Buffers;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Net.WebSockets;
    using System.Security.Authentication;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
    [ SuppressMessage( "ReSharper", "AccessToDisposedClosure" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Local" ) ]
    public class SocketHandler
    {
        /// <summary>
        /// The text encoding
        /// </summary>
        private static readonly Encoding _textEncoding = new UTF8Encoding( false, true );

        /// <summary>
        /// The is websocket
        /// </summary>
        private readonly bool _isSocket;

        /// <summary>
        /// The host
        /// </summary>
        private readonly string _host;

        /// <summary>
        /// The port
        /// </summary>
        private int _port;

        /// <summary>
        /// The address family
        /// </summary>
        private AddressFamily _addressFamily;

        /// <summary>
        /// The web socket URL
        /// </summary>
        private Uri _uri;

        /// <summary>
        /// The socket encoding
        /// </summary>
        private Encoding _binaryEncoding;

        /// <summary>
        /// The use SSL
        /// </summary>
        private bool _useSsl;

        /// <summary>
        /// The ignore SSL cert errors  
        /// </summary>
        private bool _ignoreErrors;

        /// <summary>
        /// The SSL protocols
        /// </summary>
        private SslProtocols _sslProtocols;

        /// <summary>
        /// The ct source
        /// </summary>
        private CancellationTokenSource _cancel = new CancellationTokenSource( );

        /// <summary>
        /// The send queue
        /// </summary>
        private ConcurrentQueue<(string text, bool close)> _queue =
            new ConcurrentQueue<(string, bool)>( );

        /// <summary>
        /// The send queue semaphore
        /// </summary>
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim( 0 );

        /// <summary>
        /// The receive task
        /// </summary>
        private Task _task;

        /// <summary>
        /// Occurs when [socket message].
        /// </summary>
        public event EventHandler<SocketEventArgs> SocketMessage;

        /// <summary>
        /// Occurs when [connection finished].
        /// </summary>
        public event EventHandler ConnectionFinished;

        /// <summary>
        /// The invoke asynchronous function
        /// </summary>
        private Func<Action, CancellationToken, Task> _async;

        /// <summary>
        /// Initializes a new instance of the <see cref="SocketHandler"/> class.
        /// </summary>
        /// <param name="async">The invoke asynchronous function.</param>
        /// <param name="isSocket">if set to <c>true</c> [is websocket].</param>
        /// <param name="host">The host.</param>
        /// <param name="port">The port.</param>
        /// <param name="addressFamily">The address family.</param>
        /// <param name="encoding">The binary message encoding.</param>
        /// <param name="useSsl">if set to <c>true</c> [use SSL].</param>
        /// <param name="ignoreSslCertErrors">if set to <c>true</c> [ignore SSL cert errors].</param>
        /// <param name="sslProtocols">The SSL protocols.</param>
        /// <exception cref="ArgumentNullException">addressFamily</exception>
        public SocketHandler( Func<Action, CancellationToken, Task> async, bool isSocket,
            string host, int port, AddressFamily addressFamily, Encoding encoding,
            bool useSsl, bool ignoreSslCertErrors, SslProtocols sslProtocols )
        {
            _async = async;
            _isSocket = isSocket;
            if( isSocket )
            {
                _uri = new Uri( host );
            }
            else
            {
                _host = host;
                _port = port;
                _addressFamily = addressFamily;
            }

            _binaryEncoding = encoding;
            _useSsl = useSsl;
            _ignoreErrors = ignoreSslCertErrors;
            _sslProtocols = sslProtocols;
        }

        /// <summary>
        /// Formats the SSL protocol.
        /// </summary>
        /// <param name="protocol">The protocol.</param>
        /// <returns>
        /// </returns>
        private static string FormatSslProtocol( SslProtocols protocol )
        {
            return protocol switch
            {
                SslProtocols.Tls => "TLS 1.0",
                SslProtocols.Tls11 => "TLS 1.1",
                SslProtocols.Tls12 => "TLS 1.2",
                SslProtocols.Tls13 => "TLS 1.3",
                var _other => _other.ToString( )
            };
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start( )
        {
            if( _isSocket )
            {
                var _socket = $"...connecting to WebSocket URL '{_uri!.AbsoluteUri}'…";
                OnSocketMessage( new SocketEventArgs( _socket, true ) );
            }
            else
            {
                var _endpoint = $"...connecting to '{_host}:{_port}'…";
                OnSocketMessage( new SocketEventArgs( _endpoint, true ) );
            }

            // Start the receive task.
            _task = _isSocket
                ? Task.Run( RunWebSocketReceiveTaskAsync )
                : Task.Run( RunTcpReceiveTaskAsync );
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop( )
        {
            // Cancel with token
            _cancel.Cancel( );
            _task!.GetAwaiter( ).GetResult( );
            var _cnlmsg = "Connection closed/aborted.";
            OnSocketMessage( new SocketEventArgs( _cnlmsg, true ) );
            _cancel.Dispose( );
            _semaphore.Dispose( );
        }

        /// <summary>
        /// Sends the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void Send( string text )
        {
            _queue.Enqueue( ( text, false ) );
            _semaphore.Release( );
        }

        /// <summary>
        /// Closes the send channel.
        /// </summary>
        public void CloseSendChannel( )
        {
            _queue.Enqueue( ( null, true ) );
            _semaphore.Release( );
        }

        /// <summary>
        /// Raises the <see cref="E:SocketMessage" /> event.
        /// </summary>
        /// <param name="e">The <see cref="SocketEventArgs"/>
        /// instance containing the event data.</param>
        protected virtual void OnSocketMessage( SocketEventArgs e )
        {
            SocketMessage?.Invoke( this, e );
        }

        /// <summary>
        /// Raises the <see cref="E:ConnectionFinished" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        protected virtual void OnConnectionFinished( EventArgs e )
        {
            ConnectionFinished?.Invoke( this, e );
        }

        /// <summary>
        /// Runs the TCP receive task asynchronous.
        /// </summary>
        private async Task RunTcpReceiveTaskAsync( )
        {
            try
            {
                using var _client = new TcpClient( _addressFamily );
                await _client.ConnectAsync( _host!, _port, _cancel.Token );
                SocketConfig.ConfigureSocket( _client.Client, true );
                Stream _clientStream = new NetworkStream( _client.Client, false );
                var _remoteEndpoint = _client.Client.RemoteEndPoint;
                _ = InvokeAsync( ( ) => OnSocketMessage( new SocketEventArgs(
                    $"TCP connection established to '{_remoteEndpoint}'." + ( _useSsl
                        ? " Negotiating TLS…"
                        : "" ), true ) ) );

                if( _useSsl )
                {
                    var _sslStream = new SslStream( _clientStream );
                    try
                    {
                        await _sslStream.AuthenticateAsClientAsync(
                            new SslClientAuthenticationOptions( )
                            {
                                TargetHost = _host,
                                EnabledSslProtocols = _sslProtocols,
                                RemoteCertificateValidationCallback = ( sender, certificate, chain,
                                    sslPolicyErrors ) =>
                                {
                                    if( _ignoreErrors )
                                    {
                                        return true;
                                    }

                                    return sslPolicyErrors == SslPolicyErrors.None;
                                }
                            }, _cancel.Token );

                        var _sslProtocol = _sslStream.SslProtocol;
                        var _cipherSuite = _sslStream.NegotiatedCipherSuite;
                        var _remoteCertificate = _sslStream.RemoteCertificate!;
                        _ = InvokeAsync( ( ) => OnSocketMessage( new SocketEventArgs(
                            $"TLS negotiated. Protocol: "
                            + $"{SocketHandler.FormatSslProtocol( _sslProtocol )}, "
                            + "CipherSuite: {cipherSuite}, Certificate SHA-1 Hash: "
                            + $"{_remoteCertificate.GetCertHashString( )} (issued by: "
                            + $"{_remoteCertificate.Issuer}; not after: "
                            + $"{_remoteCertificate.GetExpirationDateString( )})", true ) ) );
                    }
                    catch
                    {
                        await _sslStream.DisposeAsync( );
                        _client.Client.Close( 0 );

                        throw;
                    }

                    _clientStream = _sslStream;
                }

                await using( _clientStream )
                {
                    var _sendTask = Task.Run( async ( ) =>
                    {
                        try
                        {
                            while( true )
                            {
                                await _semaphore.WaitAsync( _cancel.Token );
                                if( !_queue.TryDequeue( out var _tuple ) )
                                {
                                    throw new InvalidOperationException( );
                                }

                                if( _tuple.close )
                                {
                                    var _clsmsg = "...closing channel!";
                                    var _clsarg = new SocketEventArgs( _clsmsg, true );
                                    _ = InvokeAsync( ( ) => OnSocketMessage( _clsarg ) );
                                    if( _clientStream is SslStream _sslStream )
                                    {
                                        await _sslStream.ShutdownAsync( );
                                    }

                                    _client.Client.Shutdown( SocketShutdown.Send );

                                    break;
                                }
                                else
                                {
                                    var _bytes = _binaryEncoding.GetByteCount( _tuple.text! );
                                    var _buffer = ArrayPool<byte>.Shared.Rent( _bytes );
                                    _bytes = _binaryEncoding.GetBytes( _tuple.text, _buffer );
                                    var _memory = _buffer.AsMemory( )[ .._bytes ];
                                    var _stxt = _binaryEncoding.GetString( _memory.Span );
                                    _ = InvokeAsync( ( ) =>
                                    {
                                        var _sargs = new SocketEventArgs( _stxt, isSendText: true );
                                        OnSocketMessage( new SocketEventArgs( _stxt,
                                            isSendText: true ) );
                                    } );

                                    await _clientStream.WriteAsync( _memory, _cancel.Token );
                                    await _clientStream.FlushAsync( _cancel.Token );
                                    ArrayPool<byte>.Shared.Return( _buffer );
                                }
                            }
                        }
                        catch( Exception _ex )
                        {
                            var _errmsg = "Error when sending data: " + _ex.Message;
                            var _errarg = new SocketEventArgs( _errmsg, true );
                            _ = InvokeAsync( ( ) => OnSocketMessage( _errarg ) );
                        }
                    } );

                    try
                    {
                        using( var _reader = new StreamReader( _clientStream, _binaryEncoding,
                            false, leaveOpen: true ) )
                        {
                            int _read;
                            var _input = new char[ 32768 ];
                            while( ( _read = await _reader.ReadAsync( _input, _cancel.Token ) )
                                > 0 )
                            {
                                var _text = new string( _input, 0, _read );
                                var _rdrarg = new SocketEventArgs( _text );
                                await InvokeAsync( ( ) =>
                                {
                                    OnSocketMessage( _rdrarg );
                                } );
                            }
                        }

                        var _clsmsg = "...channel closing!";
                        var _clsarg = new SocketEventArgs( _clsmsg, true );
                        _ = InvokeAsync( ( ) => OnSocketMessage( _clsarg ) );
                    }
                    catch( Exception _ex )
                    {
                        try
                        {
                            var _rcrmsg = "...error recieved!" + _ex.Message;
                            var _rcrarg = new SocketEventArgs( _rcrmsg, true );
                            _ = InvokeAsync( ( ) => OnSocketMessage( _rcrarg ) );
                        }
                        catch( OperationCanceledException )
                        {
                            // Ignore
                        }
                    }
                    finally
                    {
                        await _sendTask;

                        // Reset the connection.
                        _client.Client.Close( 0 );
                    }
                }
            }
            catch( Exception _ex )
            {
                try
                {
                    var _connText = "Error when establishing connection: " + _ex.Message;
                    var _connarg = new SocketEventArgs( _connText, true );
                    _ = InvokeAsync( ( ) => OnSocketMessage( _connarg ) );
                }
                catch( OperationCanceledException )
                {
                    // Ignore
                }
            }
            finally
            {
                _ = InvokeAsync( ( ) => OnConnectionFinished( EventArgs.Empty ) );
            }
        }

        /// <summary>
        /// Runs the web socket receive task asynchronous.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The WebSocket message exceeds the size of {maxMessageSize / 1024 / 1024} MiB.
        /// or
        /// </exception>
        private async Task RunWebSocketReceiveTaskAsync( )
        {
            const int size = 100 * 1024 * 1024;
            try
            {
                using var _socket = new ClientWebSocket( );
                await _socket.ConnectAsync( _uri!, _cancel.Token );
                var _text = "...connection established.";
                var _socarg = new SocketEventArgs( _text, true );
                _ = InvokeAsync( ( ) => OnSocketMessage( _socarg ) );
                var _sendTask = Task.Run( async ( ) =>
                {
                    try
                    {
                        while( true )
                        {
                            await _semaphore.WaitAsync( _cancel.Token );
                            if( !_queue.TryDequeue( out var _tuple ) )
                            {
                                throw new InvalidOperationException( );
                            }

                            if( _tuple.close )
                            {
                                var _csdmsg = "Send Channel closing.";
                                var _csdarg = new SocketEventArgs( _csdmsg, true );
                                _ = InvokeAsync( ( ) => OnSocketMessage( _csdarg ) );
                                await _socket.CloseOutputAsync( WebSocketCloseStatus.NormalClosure,
                                    null, _cancel.Token );

                                break;
                            }
                            else
                            {
                                _ = InvokeAsync( ( ) =>
                                {
                                    var _sending = "Sending WebSocket message (type: Text):";
                                    OnSocketMessage( new SocketEventArgs( _sending, true ) );
                                    OnSocketMessage( new SocketEventArgs( _tuple.text!,
                                        isSendText: true ) );
                                } );

                                var _bytes = _binaryEncoding.GetByteCount( _tuple.text! );
                                var _buffer = ArrayPool<byte>.Shared.Rent( _bytes );
                                _bytes = _binaryEncoding.GetBytes( _tuple.text, _buffer );
                                var _memory = new Memory<byte>( _buffer, 0, _bytes );
                                await _socket.SendAsync( _memory, WebSocketMessageType.Text, true,
                                    _cancel.Token );

                                ArrayPool<byte>.Shared.Return( _buffer );
                            }
                        }
                    }
                    catch( Exception _ex )
                    {
                        var _errmsg = "Error when sending data: " + _ex.Message;
                        var _errarg = new SocketEventArgs( _errmsg, true );
                        _ = InvokeAsync( ( ) => OnSocketMessage( _errarg ) );
                    }
                } );

                try
                {
                    var _input = new byte[ 32768 ];
                    using var _stream = new MemoryStream( );
                    while( true )
                    {
                        var _received = await _socket.ReceiveAsync( (Memory<byte>)_input,
                            _cancel.Token );

                        if( _received.MessageType == WebSocketMessageType.Close )
                        {
                            break;
                        }

                        _stream.Write( _input.AsSpan( )[ .._received.Count ] );
                        if( _stream.Length > size )
                        {
                            var _err = $"The message exceeds " + $"{size / 1024 / 1024} MiB.";

                            throw new InvalidOperationException( _err );
                        }

                        if( _received.EndOfMessage )
                        {
                            if( !_stream.TryGetBuffer( out var _streamBuffer ) )
                            {
                                throw new InvalidOperationException( );
                            }

                            var _results = _received.MessageType == WebSocketMessageType.Text
                                ? _binaryEncoding.GetString( _streamBuffer )
                                : SocketHandler._textEncoding.GetString( _streamBuffer );

                            _stream.Seek( 0, SeekOrigin.Begin );
                            _stream.SetLength( 0 );
                            await InvokeAsync( ( ) =>
                            {
                                var _sktmsg = "...recieved websocket message!";
                                OnSocketMessage( new SocketEventArgs( _sktmsg, true ) );
                                OnSocketMessage( new SocketEventArgs( _results ) );
                            } );
                        }
                    }

                    var _csdmsg = "...channel closed!";
                    var _csdarg = new SocketEventArgs( _csdmsg, true );
                    _ = InvokeAsync( ( ) => OnSocketMessage( _csdarg ) );
                }
                catch( Exception _ex )
                {
                    try
                    {
                        var _excmsg = "Error when receiving data: " + _ex.Message;
                        var _excarg = new SocketEventArgs( _excmsg, true );
                        _ = InvokeAsync( ( ) => OnSocketMessage( _excarg ) );
                    }
                    catch( OperationCanceledException )
                    {
                        // Ignore
                    }
                }
                finally
                {
                    await _sendTask;
                }
            }
            catch( Exception _ex )
            {
                try
                {
                    var _excmsg = "Error when receiving data: " + _ex.Message;
                    var _excarg = new SocketEventArgs( _excmsg, true );
                    _ = InvokeAsync( ( ) => OnSocketMessage( _excarg ) );
                }
                catch( OperationCanceledException )
                {
                    // Ignore
                }
            }
            finally
            {
                _ = InvokeAsync( ( ) => OnConnectionFinished( EventArgs.Empty ) );
            }
        }

        /// <summary>
        /// Invokes the asynchronous.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        private Task InvokeAsync( Action action )
        {
            return _async( ( ) =>
            {
                if( _cancel.Token.IsCancellationRequested )
                {
                    return;
                }

                action( );
            }, _cancel.Token );
        }
    }
}