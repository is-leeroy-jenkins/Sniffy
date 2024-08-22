namespace Sniffy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;

    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class WebCommandConverter : IMultiValueConverter
    {
        //Binding sequences start ----->

        // <Binding Path = "IperfModel.Role" />
        //< Binding Path="IperfModel.ServerIp"/>
        //<Binding Path = "IperfModel.Port" />
        //< Binding Path="IperfModel.Parallel"/>
        //<Binding Path = "IperfModel.Time" />
        //< Binding Path="IperfModel.Interval"/>
        //<Binding Path = "IperfModel.TcpFlag" />
        //< Binding Path="IperfModel.TcpWindowSize"/>
        //<Binding Path = "IperfModel.TcpWindowUnit" />
        //< Binding Path="IperfModel.UdpFlag"/>
        //<Binding Path = "IperfModel.BandWidth" />
        //< Binding Path="IperfModel.BandWidthUnit"/>
        //<Binding Path = "IperfModel.PacketLen" />
        //< Binding Path="IperfModel.Reverse"/>
        //Binding sequences end <----
        //public string Command { get; set; }

        /// <summary>
        /// Converts source values to a value for the binding target.
        /// The data binding engine calls this method when it propagates
        /// the values from source bindings to the binding target.
        /// </summary>
        /// <param name="values">The array of values that the source bindings
        /// in the <see cref="T:System.Windows.Data.MultiBinding" /> produces.
        /// The value <see cref="F:System.Windows.DependencyProperty.UnsetValue" />
        /// indicates that the source binding has no value to provide for conversion.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value.If the method returns <see langword="null" />,
        /// the valid <see langword="null" /> value is used.A return value of
        /// <see cref="T:System.Windows.DependencyProperty" />.
        /// <see cref="F:System.Windows.DependencyProperty.UnsetValue" />
        /// indicates that the converter did not produce a value, and that the
        /// binding will use the <see cref="P:System.Windows.Data.BindingBase.FallbackValue" />
        /// if it is available, or else will use the default value.A return value of
        /// <see cref="T:System.Windows.Data.Binding" />.
        /// <see cref="F:System.Windows.Data.Binding.DoNothing" />
        /// indicates that the binding does not transfer the value or use the
        /// <see cref="P:System.Windows.Data.BindingBase.FallbackValue" /> or the default value.
        /// </returns>
        public object Convert( object[ ] values, Type targetType, object parameter,
            CultureInfo culture )
        {
            try
            {
                var _command = "";
                var _role = values[ 0 ].ToString( );
                var _serverIp = values[ 1 ].ToString( );
                var _port = ( int )values[ 2 ];
                var _parallel = ( int )values[ 3 ];
                var _time = ( int )values[ 4 ];
                var _interval = ( int )values[ 5 ];
                var _tcpFlag = ( bool )values[ 6 ];
                var _tcpWindowSize = ( int )values[ 7 ];
                var _tcpWindowUnit = values[ 8 ].ToString( );
                var _udpFlag = ( bool )values[ 9 ];
                var _bandwidth = ( int )values[ 10 ];
                var _bandwidthUnit = values[ 11 ].ToString( );
                var _packetLen = ( int )values[ 12 ];
                var _reverse = ( bool )values[ 13 ];
                if( _role == "-s" )
                {
                    _command += _role;
                    _command += " -p ";
                    _command += _port;
                    _command += " -i ";
                    _command += _interval;
                }
                else if( _role == "-c" )
                {
                    _command += _role;
                    _command += " ";
                    _command += _serverIp;
                    _command += " -p ";
                    _command += _port;
                    _command += " -P ";
                    _command += _parallel;
                    _command += " -t ";
                    _command += _time;
                    _command += " -i ";
                    _command += _interval;
                    if( _tcpFlag )
                    {
                        _command += " -w ";
                        _command += _tcpWindowSize;
                        _command += _tcpWindowUnit;
                    }

                    if( _udpFlag )
                    {
                        _command += " -b ";
                        _command += _bandwidth;
                        _command += _bandwidthUnit;
                        _command += " -u ";
                    }

                    if( _packetLen > 0 )
                    {
                        _command += " -l ";
                        _command += _packetLen;
                    }

                    if( _reverse )
                    {
                        _command += " -R";
                    }
                }
                else
                {
                    _command = "command not support!";
                }

                return _command;
            }
            catch( Exception ex )
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a binding target value to the source binding values.
        /// </summary>
        /// <param name="value">The value that the binding target produces.</param>
        /// <param name="targetTypes">The array of types to convert to.
        /// The array length indicates the number and types of values
        /// that are suggested for the method to return.</param>
        /// <param name="parameter">The converter parameter to use.
        /// </param>
        /// <param name="culture">The culture to use in the converter.
        /// </param>
        /// <returns>
        /// An array of values that have been converted from
        /// the target value back to the source values.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object[ ] ConvertBack( object value, Type[ ] targetTypes, object parameter,
            CultureInfo culture )
        {
            try
            {
                throw new NotImplementedException( );
            }
            catch( Exception ex )
            {
                return null;
            }
        }
    }
}