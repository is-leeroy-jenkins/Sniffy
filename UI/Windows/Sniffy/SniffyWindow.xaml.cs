// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-02-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-02-2024
// ******************************************************************************************
// <copyright file="SniffyWindow.xaml.cs" company="Terry D. Eppler">
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
//   SniffyWindow.xaml.cs
// </summary>
// ******************************************************************************************

#pragma warning disable SYSLIB0039
namespace Sniffy
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Sockets;
    using System.Security.Authentication;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Threading;
    using Syncfusion.SfSkinManager;
    using Syncfusion.Windows.Tools.Controls;
    using KeyEventArgs = System.Windows.Input.KeyEventArgs;
    using ToastNotifications;
    using ToastNotifications.Lifetime;
    using ToastNotifications.Messages;
    using ToastNotifications.Position;
    using static App;
    using Application = System.Windows.Application;

    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for SniffyWindow.xaml
    /// </summary>
    /// <seealso cref="T:System.Windows.Window" />
    /// <seealso cref="T:System.Windows.Markup.IComponentConnector" />
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Local" ) ]
    [ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "InconsistentNaming" ) ]
    public partial class SniffyWindow : Window
    {
        /// <summary>
        /// The path
        /// </summary>
        private protected object _path;

        /// <summary>
        /// The busy
        /// </summary>
        private protected bool _busy;

        /// <summary>
        /// The time
        /// </summary>
        private protected int _time;

        /// <summary>
        /// The seconds
        /// </summary>
        private protected int _seconds;

        /// <summary>
        /// The update status
        /// </summary>
        private protected Action _statusUpdate;

        /// <summary>
        /// The timer
        /// </summary>
        private protected TimerCallback _timerCallback;

        /// <summary>
        /// The timer
        /// </summary>
        private protected System.Threading.Timer _timer;

        /// <summary>
        /// The available encodings
        /// </summary>
        private static IReadOnlyDictionary<string, Encoding> _encodings;

        /// <summary>
        /// The current socket handler
        /// </summary>
        private SocketHandler _socketHandler;

        /// <summary>
        /// The current inline paragraph
        /// </summary>
        private Paragraph _paragraph;

        /// <summary>
        /// The back color
        /// </summary>
        private protected Color _backColor = new Color( )
        {
            A = 255,
            R = 20,
            G = 20,
            B = 20
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
        /// The border color
        /// </summary>
        private protected Color _borderColor = new Color( )
        {
            A = 255,
            R = 0,
            G = 120,
            B = 212
        };

        /// <summary>
        /// Gets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        /// <c> true </c>
        /// if this instance is busy; otherwise,
        /// <c> false </c>
        /// </value>
        public bool IsBusy
        {
            get
            {
                if( _path == null )
                {
                    _path = new object( );
                    lock( _path )
                    {
                        return _busy;
                    }
                }
                else
                {
                    lock( _path )
                    {
                        return _busy;
                    }
                }
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Static constructor that initializes the
        /// <see cref="T:Sniffy.SniffyWindow" /> class.
        /// </summary>
        static SniffyWindow( )
        {
            Encoding.RegisterProvider( CodePagesEncodingProvider.Instance );
            var _win1252 = Encoding.GetEncoding( 1252 );
            var _utf8 = new UTF8Encoding( false );
            SniffyWindow._encodings =
                new Dictionary<string, Encoding>( StringComparer.OrdinalIgnoreCase )
                {
                    {
                        _win1252.WebName, _win1252
                    },
                    {
                        _utf8.WebName, _utf8
                    }
                };
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Sniffy.SniffyWindow" /> class.
        /// </summary>
        public SniffyWindow( )
        {
            // Theme Properties
            SfSkinManager.ApplyStylesOnApplication = true;
            SfSkinManager.SetTheme( this, new Theme( "FluentDark", Controls ) );

            // Window Initialization
            InitializeComponent( );
            InitializeDelegates( );
            RegisterCallbacks( );

            // Window Properties
            Height = 625;
            Width = 725;
            Padding = new Thickness( 1 );
            Margin = new Thickness( 1 );
            BorderThickness = new Thickness( 1 );
            WindowStyle = WindowStyle.SingleBorderWindow;
            Title = "Sniffy";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Background = new SolidColorBrush( _backColor );
            Foreground = new SolidColorBrush( _foreColor );
            BorderBrush = new SolidColorBrush( _borderColor );

            // Window Events
            Loaded += OnLoaded;
            Closing += OnClosing;
        }

        /// <summary>
        /// Registers the callbacks.
        /// </summary>
        private void RegisterCallbacks( )
        {
            try
            {
                ConnectButton.Click += OnConnectButtonClick;
                SendButton.Click += OnSendButtonClick;
                ProtocolComboBox.SelectionChanged += OnProtocolComboBoxSelectionChanged;
                SslCheckBox.Checked += OnUseSslCheckBoxChanged;
                SslCheckBox.Unchecked += OnUseSslCheckBoxChanged;
                SingleLineCheckBox.Checked += OnSingleLineCheckChanged;
                SingleLineCheckBox.Unchecked += OnSingleLineCheckChanged;
                OutputTextBox.KeyDown += OnSendTextBoxKeyDown;
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Initializes the delegates.
        /// </summary>
        private void InitializeDelegates( )
        {
            try
            {
                _statusUpdate += UpdateStatus;
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Initializes the timer.
        /// </summary>
        private void InitializeTimer( )
        {
            try
            {
                _timerCallback += UpdateStatus;
                _timer = new System.Threading.Timer( _timerCallback, null, 0, 260 );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Updates the controls.
        /// </summary>
        private void UpdateControls( )
        {
            ProtocolComboBox.IsEnabled = UrlTextBox.IsEnabled = SingleLineCheckBox.IsEnabled =
                BinaryEncodingComboBox.IsEnabled = _socketHandler == null;

            CloseSendChannelButton.IsEnabled = SendButton.IsEnabled = _socketHandler != null;
            var _isWebSocket =
                ( (ComboBoxItemAdv)ProtocolComboBox.SelectedItem ).Tag is "websocket";

            PortTextBox.IsEnabled = SslCheckBox.IsEnabled =
                DualModeComboBox.IsEnabled = _socketHandler == null && !_isWebSocket;

            IgnoreCertErrorsCheckBox.IsEnabled = Tls1CheckBox.IsEnabled = Tls11CheckBox.IsEnabled =
                Tls12CheckBox.IsEnabled = Tls13CheckBox.IsEnabled =
                    SslCheckBox.IsEnabled && SslCheckBox.IsChecked == true;
        }

        /// <summary>
        /// Stops the connection.
        /// </summary>
        private void StopConnection( )
        {
            try
            {
                _socketHandler.Stop( );
                _socketHandler = null;
                ConnectButton.Content = "Connect";
                var _message = "    Sniffy has been closed!";
                SendSuccessNotification( _message );
                UpdateControls( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Adds the stream text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="isMetaText">if set to <c>true</c> [is meta text].</param>
        /// <param name="isSendText">if set to <c>true</c> [is send text].</param>
        private void AddStreamText( string text, bool isMetaText = false, bool isSendText = false )
        {
            var _run = new Run( text );
            if( isMetaText )
            {
                _run.Foreground = Brushes.Red;
            }
            else if( isSendText )
            {
                _run.Foreground = Brushes.DarkCyan;
            }

            if( isMetaText )
            {
                _paragraph = null;
                FlowDocument.Blocks.Add( new Paragraph( _run )
                {
                    Margin = new Thickness( )
                } );
            }
            else
            {
                if( _paragraph is null )
                {
                    _paragraph = new Paragraph( _run )
                    {
                        Margin = new Thickness( )
                    };

                    FlowDocument.Blocks.Add( _paragraph );
                }
                else
                {
                    _paragraph.Inlines.Add( _run );
                }
            }

            InputTextBox.ScrollToEnd( );
        }

        /// <summary>
        /// Creates the notifier.
        /// </summary>
        /// <returns></returns>
        private Notifier CreateNotifier( )
        {
            try
            {
                var _position = new PrimaryScreenPositionProvider( Corner.BottomRight, 10, 10 );
                var _lifeTime = new TimeAndCountBasedLifetimeSupervisor( TimeSpan.FromSeconds( 5 ),
                    MaximumNotificationCount.UnlimitedNotifications( ) );

                return new Notifier( _cfg =>
                {
                    _cfg.LifetimeSupervisor = _lifeTime;
                    _cfg.PositionProvider = _position;
                    _cfg.Dispatcher = Application.Current.Dispatcher;
                } );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
                return default( Notifier );
            }
        }

        /// <summary>
        /// Sends the notification.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        private void SendInfoNotification( string message )
        {
            try
            {
                ThrowIf.Null( message, nameof( message ) );
                var _notifier = CreateNotifier( );
                _notifier.ShowInformation( message );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Sends the success notification.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendSuccessNotification( string message )
        {
            try
            {
                ThrowIf.Null( message, nameof( message ) );
                var _notifier = CreateNotifier( );
                _notifier.ShowSuccess( message );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Sends the error notification.
        /// </summary>
        /// <param name="message">The message.</param>
        private void SendErrorNotification( string message )
        {
            try
            {
                ThrowIf.Null( message, nameof( message ) );
                var _notifier = CreateNotifier( );
                _notifier.ShowError( message );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        private void UpdateStatus( )
        {
            try
            {
                StatusLabel.Content = DateTime.Now.ToLongTimeString( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Updates the status.
        /// </summary>
        private void UpdateStatus( object state )
        {
            try
            {
                Dispatcher.BeginInvoke( _statusUpdate );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Invokes if needed.
        /// </summary>
        /// <param name="action">The action.</param>
        public void InvokeIf( Action action )
        {
            try
            {
                ThrowIf.Null( action, nameof( action ) );
                if( Dispatcher.CheckAccess( ) )
                {
                    action?.Invoke( );
                }
                else
                {
                    Dispatcher.BeginInvoke( action );
                }
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Invokes if.
        /// </summary>
        /// <param name="action">The action.</param>
        public void InvokeIf( Action<object> action )
        {
            try
            {
                ThrowIf.Null( action, nameof( action ) );
                if( Dispatcher.CheckAccess( ) )
                {
                    action?.Invoke( null );
                }
                else
                {
                    Dispatcher.BeginInvoke( action );
                }
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Fades the in asynchronous.
        /// </summary>
        /// <param name="form">The o.</param>
        /// <param name="interval">The interval.</param>
        private async void FadeInAsync( Window form, int interval = 80 )
        {
            try
            {
                ThrowIf.Null( form, nameof( form ) );
                while( form.Opacity < 1.0 )
                {
                    await Task.Delay( interval );
                    form.Opacity += 0.05;
                }

                form.Opacity = 1;
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Fades the out asynchronous.
        /// </summary>
        /// <param name="form">The o.</param>
        /// <param name="interval">The interval.</param>
        private async void FadeOutAsync( Window form, int interval = 80 )
        {
            try
            {
                ThrowIf.Null( form, nameof( form ) );
                while( form.Opacity > 0.0 )
                {
                    await Task.Delay( interval );
                    form.Opacity -= 0.05;
                }

                form.Opacity = 0;
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private void OnLoaded( object sender, RoutedEventArgs e )
        {
            try
            {
                ProtocolComboBox.SelectedItem = ProtocolComboBox.Items[ 0 ];
                SingleLineCheckBox.IsChecked = true;
                ConnectButton.Content = "Connect";
                InitializeTimer( );
                Opacity = 0;
                FadeInAsync( this );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Handles the CBX protocol selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/>
        /// instance containing the event data.</param>
        private void OnProtocolComboBoxSelectionChanged( object sender,
            SelectionChangedEventArgs e )
        {
            UpdateControls( );
        }

        /// <summary>
        /// Handles the CHK use SSL checked unchecked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private void OnUseSslCheckBoxChanged( object sender, RoutedEventArgs e )
        {
            UpdateControls( );
        }

        /// <summary>
        /// Handles the BTN connect click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private void OnConnectButtonClick( object sender, RoutedEventArgs e )
        {
            if( _socketHandler == null )
            {
                _paragraph = null;
                FlowDocument.Blocks.Clear( );
                try
                {
                    var _isWebSocket =
                        ( (ComboBoxItemAdv)ProtocolComboBox.SelectedItem ).Tag is "websocket";

                    var _host = UrlTextBox.Text;
                    var _port = _isWebSocket
                        ? 0
                        : ushort.Parse( PortTextBox.Text );

                    var _addressFamily = _isWebSocket
                        ? default( AddressFamily )
                        : (string)( (ComboBoxItemAdv)DualModeComboBox.SelectedItem ).Tag switch
                        {
                            "dual" => AddressFamily.Unknown,
                            "ipv4" => AddressFamily.InterNetwork,
                            "ipv6" => AddressFamily.InterNetworkV6,
                            _ => throw new InvalidOperationException( )
                        };

                    var _encoding = SniffyWindow._encodings[
                        (string)( (ComboBoxItemAdv)BinaryEncodingComboBox.SelectedItem ).Tag ];

                    var _useSsl = SslCheckBox.IsChecked == true;
                    var _ignoreSslCertErrors = IgnoreCertErrorsCheckBox.IsChecked == true;
                    var _sslProtocols = default( SslProtocols );
                    if( Tls1CheckBox.IsChecked == true )
                    {
                        _sslProtocols |= SslProtocols.Tls;
                    }

                    if( Tls11CheckBox.IsChecked == true )
                    {
                        _sslProtocols |= SslProtocols.Tls11;
                    }

                    if( Tls12CheckBox.IsChecked == true )
                    {
                        _sslProtocols |= SslProtocols.Tls12;
                    }

                    if( Tls13CheckBox.IsChecked == true )
                    {
                        _sslProtocols |= SslProtocols.Tls13;
                    }

                    var _clientState = new SocketHandler( async ( action, token ) =>
                            await Dispatcher.InvokeAsync( action, DispatcherPriority.Normal,
                                token ), _isWebSocket, _host, _port, _addressFamily, _encoding,
                        _useSsl, _ignoreSslCertErrors, _sslProtocols );

                    _clientState.SocketMessage += ( s, e ) =>
                        AddStreamText( e.Text, e.IsMetaText, e.IsSendText );

                    _clientState.ConnectionFinished += ( s, e ) => StopConnection( );
                    _clientState.Start( );
                    _socketHandler = _clientState;
                    ConnectButton.Content = "Abort";
                    var _message = "    Sniffy is connected!";
                    SendSuccessNotification( _message );
                    UpdateControls( );
                }
                catch( Exception _ex )
                {
                    TaskDialog.ShowDialog( new WindowInteropHelper( this ).Handle,
                        new TaskDialogPage( )
                        {
                            Caption = Title,
                            Heading = _ex.Message,
                            Icon = TaskDialogIcon.Error
                        } );
                }
            }
            else
            {
                StopConnection( );
            }
        }

        /// <summary>
        /// Handles the BTN send click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private void OnSendButtonClick( object sender, RoutedEventArgs e )
        {
            var _ending = (string)( (ComboBoxItemAdv)LineEncodingComboBox.SelectedItem ).Tag;
            var _text = OutputTextBox.Text;
            _text = _text.Replace( "\r\n", "\n" ).Replace( "\n", _ending );
            if( SingleLineCheckBox.IsChecked == true )
            {
                _text += _ending;
            }

            _socketHandler!.Send( _text );
            OutputTextBox.Text = string.Empty;
        }

        /// <summary>
        /// Handles the BTN close send channel click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private void OnCloseSendChannelButtonClick( object sender, RoutedEventArgs e )
        {
            _socketHandler!.CloseSendChannel( );
            CloseSendChannelButton.IsEnabled = false;
            SendButton.IsEnabled = false;
        }

        /// <summary>
        /// Handles the CHK single line checked changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private void OnSingleLineCheckChanged( object sender, RoutedEventArgs e )
        {
            OutputTextBox.AcceptsReturn = SingleLineCheckBox.IsChecked != true;
        }

        /// <summary>
        /// Handles the text send text key down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/>
        /// instance containing the event data.</param>
        private void OnSendTextBoxKeyDown( object sender, KeyEventArgs e )
        {
            if( e.Key == Key.Enter
                && SingleLineCheckBox.IsChecked == true
                && SendButton.IsEnabled )
            {
                OnSendButtonClick( sender, e );
            }
        }

        /// <summary>
        /// Handles the window closed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private void OnClosing( object sender, EventArgs e )
        {
            if( _socketHandler != null )
            {
                _socketHandler.Stop( );
                _socketHandler = null;
            }

            _timer?.Dispose( );
            FadeOutAsync( this );
            Application.Current.Shutdown( );
        }

        /// <summary>
        /// Called when [calculator menu option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>Cal
        private void OnCalculatorMenuOptionClick( object sender, EventArgs e )
        {
            try
            {
                var _calculator = new CalculatorWindow( );
                _calculator.ShowDialog( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [file menu option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private void OnFileMenuOptionClick( object sender, EventArgs e )
        {
            try
            {
                var _dialog = new SaveFileDialog( );
                _dialog.ShowDialog( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [folder menu option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private void OnFolderMenuOptionClick( object sender, EventArgs e )
        {
            try
            {
                var _dialog = new FolderBrowserDialog( );
                _dialog.ShowDialog( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [control panel option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private void OnControlPanelOptionClick( object sender, EventArgs e )
        {
            try
            {
                WinMinion.LaunchControlPanel( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [task manager option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private void OnTaskManagerOptionClick( object sender, EventArgs e )
        {
            try
            {
                WinMinion.LaunchTaskManager( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [close option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private void OnCloseOptionClick( object sender, EventArgs e )
        {
            try
            {
                Opacity = 0;
                FadeOutAsync( this );
                Application.Current.Shutdown( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [chrome option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// containing the event data.</param>
        private void OnChromeOptionClick( object sender, EventArgs e )
        {
            try
            {
                WebMinion.RunChrome( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [edge option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// instance containing the event data.</param>
        private void OnEdgeOptionClick( object sender, EventArgs e )
        {
            try
            {
                WebMinion.RunEdge( );
            }
            catch( Exception _ex )
            {
                Fail( _ex );
            }
        }

        /// <summary>
        /// Called when [firefox option click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/>
        /// containing the event data.</param>
        private void OnFirefoxOptionClick( object sender, EventArgs e )
        {
            try
            {
                WebMinion.RunFirefox( );
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