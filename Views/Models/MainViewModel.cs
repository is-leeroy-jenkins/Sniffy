// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-19-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-19-2024
// ******************************************************************************************
// <copyright file="MainViewModel.cs" company="Terry D. Eppler">
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
//   MainViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Threading;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Sniffy.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "ConvertIfStatementToConditionalTernaryExpression" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Local" ) ]
    public class MainViewModel : MainWindowBase
    {
        /// <summary>
        /// The filter text
        /// </summary>
        private string _filterText;

        // Switch Views

        // Menu Button Command
        /// <summary>
        /// The menucommand
        /// </summary>
        private ICommand _menucommand;

        // CollectionViewSource enables XAML code to set the
        // commonly used CollectionView properties,
        // passing these settings to the underlying view.
        /// <summary>
        /// The menu items collection
        /// </summary>
        private CollectionViewSource _menuItemsCollection;

        /// <summary>
        /// The network scan view model
        /// </summary>
        private protected NetworkScanViewModel _networkScanViewModel;

        /// <summary>
        /// The port scan view model
        /// </summary>
        private protected PortScanViewModel _portScanViewModel;

        /// <summary>
        /// The port view model
        /// </summary>
        private protected PortViewModel _portViewModel;

        /// <summary>
        /// The route view model
        /// </summary>
        private protected RouteViewModel _routeViewModel;

        /// <summary>
        /// The selected view model
        /// </summary>
        private object _selectedViewModel;

        /// <summary>
        /// The server view model
        /// </summary>
        private protected ServerViewModel _serverViewModel;

        /// <summary>
        /// The sniffer view model
        /// </summary>
        private protected SnifferViewModel _snifferViewModel;

        /// <summary>
        /// The web view model
        /// </summary>
        private protected WebViewModel _webViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel( )
        {
            var _menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems
                {
                    MenuName = "Iperf",
                    MenuImage = @"Resources/Speed.png"
                },
                new MenuItems
                {
                    MenuName = "NetworkScan",
                    MenuImage = @"Resources/IP.png"
                },
                new MenuItems
                {
                    MenuName = "PortScan",
                    MenuImage = @"Resources/port.png"
                },
                new MenuItems
                {
                    MenuName = "RouteTable",
                    MenuImage = @"Resources/route.png"
                },
                new MenuItems
                {
                    MenuName = "PortListen",
                    MenuImage = @"Resources/portlisten.png"
                },
                new MenuItems
                {
                    MenuName = "Server",
                    MenuImage = @"Resources/servers.png"
                },
                new MenuItems
                {
                    MenuName = "Sniffer",
                    MenuImage = @"Resources/sniffer.png"
                }
            };

            _menuItemsCollection = new CollectionViewSource
            {
                Source = _menuItems
            };

            _menuItemsCollection.Filter += OnMenuItemsFilter;
            _webViewModel = new WebViewModel( );
            SelectedViewModel = _webViewModel;
        }

        // ICollectionView enables collections to have the
        // functionalities of current record management,
        // custom sorting, filtering, and grouping.
        /// <summary>
        /// Gets the source collection.
        /// </summary>
        /// <value>
        /// The source collection.
        /// </value>
        public ICollectionView SourceCollection
        {
            get
            {
                return _menuItemsCollection.View;
            }
        }

        /// <summary>
        /// Gets or sets the filter text.
        /// </summary>
        /// <value>
        /// The filter text.
        /// </value>
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
                _menuItemsCollection.View.Refresh( );
                OnPropertyChanged( nameof( FilterText ) );
            }
        }

        /// <summary>
        /// Gets the run calculate command.
        /// </summary>
        /// <value>
        /// The run calculate command.
        /// </value>
        public ICommand RunCalcCommand
        {
            get
            {
                return new RelayCommand( param => RunCalc( param ) );
            }
        }

        /// <summary>
        /// Gets the run menu command.
        /// </summary>
        /// <value>
        /// The run menu command.
        /// </value>
        public ICommand RunMenuCommand
        {
            get
            {
                return new RelayCommand( param => RunMenuCmd( param ) );
            }
        }

        /// <summary>
        /// Gets or sets the selected view model.
        /// </summary>
        /// <value>
        /// The selected view model.
        /// </value>
        public object SelectedViewModel
        {
            get
            {
                return _selectedViewModel;
            }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged( "SelectedViewModel" );
            }
        }

        /// <summary>
        /// Gets the menu command.
        /// </summary>
        /// <value>
        /// The menu command.
        /// </value>
        public ICommand MenuCommand
        {
            get
            {
                if( _menucommand == null )
                {
                    _menucommand = new RelayCommand( param => SwitchViews( param ) );
                }

                return _menucommand;
            }
        }

        /// <summary>
        /// Called when [menu items filter].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FilterEventArgs"/>
        /// instance containing the event data.</param>
        private void OnMenuItemsFilter( object sender, FilterEventArgs e )
        {
            if( string.IsNullOrEmpty( FilterText ) )
            {
                e.Accepted = true;
                return;
            }

            var _item = e.Item as MenuItems;
            if( _item?.MenuName.ToUpper( )?.Contains( FilterText.ToUpper( ) ) == true )
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        /// <summary>
        /// Runs the calculate.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public void RunCalc( object parameter )
        {
            Process.Start( "calc.exe" );
        }

        /// <summary>
        /// Runs the menu command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void RunMenuCmd( object parameter )
        {
            switch( parameter )
            {
                case "Calc":
                {
                    Process.Start( "calc.exe" );
                    break;
                }
                case "About":
                {
                    var _aboutWindow = new AboutWindow( );
                    _aboutWindow.Show( );
                    Dispatcher.Run( );
                    break;
                }
                case "Iperf":
                {
                    var _iperfInfo = new ProcessStartInfo( "https://iperf.fr/" )
                    {
                        UseShellExecute = true
                    };

                    Process.Start( _iperfInfo );
                    break;
                }
                case "Pcap":
                {
                    var _hyperlink = "https://www.tcpdump.org/manpages/pcap-filter.7.html";
                    var _pcapInfo =
                        new ProcessStartInfo( _hyperlink )
                        {
                            UseShellExecute = true
                        };

                    Process.Start( _pcapInfo );
                    break;
                }
            }
        }

        /// <summary>
        /// Switches the views.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private protected void SwitchViews( object parameter )
        {
            switch( parameter )
            {
                case "Iperf":
                {
                    if( _webViewModel == null )
                    {
                        _webViewModel = new WebViewModel( );
                    }

                    SelectedViewModel = _webViewModel;// new WebViewModel();
                    break;
                }
                case "NetworkScan":
                {
                    if( _networkScanViewModel == null )
                    {
                        _networkScanViewModel = new NetworkScanViewModel( );
                    }

                    SelectedViewModel = _networkScanViewModel;// new NetworkScanViewModel();
                    break;
                }
                case "PortScan":
                {
                    if( _portScanViewModel == null )
                    {
                        _portScanViewModel = new PortScanViewModel( );
                    }

                    SelectedViewModel = _portScanViewModel;
                    break;
                }
                case "RouteTable":
                {
                    if( _routeViewModel == null )
                    {
                        _routeViewModel = new RouteViewModel( );
                    }

                    SelectedViewModel = _routeViewModel;
                    break;
                }
                case "PortListen":
                {
                    if( _portViewModel == null )
                    {
                        _portViewModel = new PortViewModel( );
                    }

                    SelectedViewModel = _portViewModel;
                    break;
                }
                case "Server":
                {
                    if( _serverViewModel == null )
                    {
                        _serverViewModel = new ServerViewModel( );
                    }

                    SelectedViewModel = _serverViewModel;
                    break;
                }
                case "Sniffer":
                {
                    if( _snifferViewModel == null )
                    {
                        _snifferViewModel = new SnifferViewModel( );
                    }

                    SelectedViewModel = _snifferViewModel;
                    break;
                }
            }
        }
    }
}