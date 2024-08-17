// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-16-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-16-2024
// ******************************************************************************************
// <copyright file="PerformanceViewModel.cs" company="Terry D. Eppler">
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
//   PerformanceViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Windows.Input;
    using System.Text.RegularExpressions;
    using Microsoft.Win32;
    using System.IO;
    using System.Threading.Tasks;

    /// <inheritdoc />
    /// <summary>
    /// </summary>
    /// <seealso cref="T:Sniffy.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" ) ]
    public sealed class PerformanceViewModel : MainWindowBase
    {
        /// <summary>
        /// The m plot model data
        /// </summary>
        private PlotModel _plotModel;

        /// <summary>
        /// The cancel plot token
        /// </summary>
        public CancellationToken CancelPlotToken;

        /// <summary>
        /// The line series current value
        /// </summary>
        public LineSeries LineSeriesCurrentVal;

        /// <summary>
        /// The iperf process
        /// </summary>
        public ProcessInterface PerformanceProcess;

        /// <summary>
        /// The plot token source
        /// </summary>
        public CancellationTokenSource PlotTokenSource;

        /// <summary>
        /// The x time axis
        /// </summary>
        public LinearAxis XTimeAxis;

        /// <summary>
        /// The y throughput value
        /// </summary>
        public LinearAxis YThroughputVal;

        /// <summary>
        /// The y zoom factor
        /// </summary>
        public int YZoomFactor = 1;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PerformanceViewModel"/> class.
        /// </summary>
        public PerformanceViewModel( )
        {
            PerformanceProcess = new ProcessInterface( );
            PerformanceModel = new PerformanceModel( );
            InitPlot( );
        }

        /// <summary>
        /// Gets or sets the iperf model.
        /// </summary>
        /// <value>
        /// The iperf model.
        /// </value>
        public PerformanceModel PerformanceModel { get; set; }

        /// <summary>
        /// Gets or sets the plot model data.
        /// </summary>
        /// <value>
        /// The plot model data.
        /// </value>
        public PlotModel PlotModel
        {
            get
            {
                return _plotModel;
            }
            set
            {
                _plotModel = value;
                OnPropertyChanged( nameof( PlotModel ) );
            }
        }

        /// <summary>
        /// Gets the run iperf command.
        /// </summary>
        /// <value>
        /// The run iperf command.
        /// </value>
        public ICommand RunIperfCommand
        {
            get
            {
                return new RelayCommand( param => RunIperf( param ) );
            }
        }

        /// <summary>
        /// Gets the stop iperf command.
        /// </summary>
        /// <value>
        /// The stop iperf command.
        /// </value>
        public ICommand StopIperfCommand
        {
            get
            {
                return new RelayCommand( param => StopIperf( param ) );
            }
        }

        /// <summary>
        /// Gets the clear output command.
        /// </summary>
        /// <value>
        /// The clear output command.
        /// </value>
        public ICommand ClearOutputCommand
        {
            get
            {
                return new RelayCommand( param => ClearOutput( param ) );
            }
        }

        /// <summary>
        /// Gets the save output command.
        /// </summary>
        /// <value>
        /// The save output command.
        /// </value>
        public ICommand SaveOutputCommand
        {
            get
            {
                return new RelayCommand( param => SaveOutput( param ) );
            }
        }

        /// <summary>
        /// Gets the iperf help command.
        /// </summary>
        /// <value>
        /// The iperf help command.
        /// </value>
        public ICommand IperfHelpCommand
        {
            get
            {
                return new RelayCommand( param => IperfHelp( param ) );
            }
        }

        /// <summary>
        /// Initializes the plot.
        /// </summary>
        public void InitPlot( )
        {
            _plotModel = new PlotModel( );
            XTimeAxis = new LinearAxis( )
            {
                Title = "Time(s)",
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.None,
                Minimum = 0,
                AbsoluteMinimum = 0,
                Maximum = 100,

                //MajorStep = 100,
                FontSize = 13,

                //PositionTier = 6,
                Key = "Time",
                MinorGridlineStyle = LineStyle.Solid,
                IsPanEnabled = true,
                IsZoomEnabled = true
            };

            YThroughputVal = new LinearAxis( )
            {
                Title = "Throughput(Mbps)",
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.None,
                Minimum = 0,
                AbsoluteMinimum = 0,
                Maximum = 500.0,
                MajorStep = 50,
                FontSize = 13,
                PositionTier = 6,
                Key = "Throughput",
                MinorGridlineStyle = LineStyle.Solid,
                IsPanEnabled = true,
                IsZoomEnabled = true
            };

            _plotModel.Axes.Add( XTimeAxis );
            _plotModel.Axes.Add( YThroughputVal );
            LineSeriesCurrentVal = new LineSeries( )
            {
                MarkerType = MarkerType.Circle,
                StrokeThickness = 1,
                YAxisKey = "Throughput",
                Title = "Throughput",
                Color = OxyColors.Red
            };

            _plotModel.Series.Add( LineSeriesCurrentVal );
        }

        /// <summary>
        /// ies the zoom out.
        /// </summary>
        public void YZoomOut( )
        {
            YThroughputVal.Maximum *= 2;
            YThroughputVal.MajorStep =
                ( YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum ) / 10;
        }

        /// <summary>
        /// ies the zoom in.
        /// </summary>
        public void YZoomIn( )
        {
            YThroughputVal.Maximum /= 2;
            YThroughputVal.MajorStep =
                ( YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum ) / 10;
        }

        /// <summary>
        /// Clears the plot.
        /// </summary>
        public void ClearPlot( )
        {
            LineSeriesCurrentVal.Points.Clear( );
            _plotModel.InvalidatePlot( true );
        }

        /// <summary>
        /// Stops the update plot task.
        /// </summary>
        public void StopUpdatePlotTask( )
        {
            PlotTokenSource.Cancel( );
        }

        /// <summary>
        /// Runs the iperf.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void RunIperf( object parameter )
        {
            var _iperfPath = @"Libraries\iperf\" + PerformanceModel.Version;
            PerformanceProcess.StartProcess( _iperfPath, ( string )parameter,
                ProcessOutputDataReceived );

            LineSeriesCurrentVal.Points.Clear( );
            _plotModel.InvalidatePlot( true );
        }

        /// <summary>
        /// Plots the data.
        /// </summary>
        /// <param name="str">The string.</param>
        public void PlotData( string str )
        {
            string _pattern;
            if( PerformanceModel.Parallel > 1 )
            {
                _pattern =
                    @"\[SUM\]*\s*(?<a>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*-\s*(?<b>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*sec\s*(?<c>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*.Bytes\s*(?<d>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*Mbits/sec";
            }
            else
            {
                _pattern =
                    @"(?<a>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*-\s*(?<b>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*sec\s*(?<c>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*.Bytes\s*(?<d>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*Mbits/sec";
            }

            var _m = Regex.Match( str, _pattern );

            //str = "";
            //Regex r = new Regex("/(\\d+)[^\\d]+Mbits/sec/");
            if( _m.Success )
            {
                //return "
                var _timeA = _m.Groups[ "a" ].Value;
                var _timeB = _m.Groups[ "b" ].Value;
                var _bytes = _m.Groups[ "c" ].Value;
                var _bandwidth = _m.Groups[ "d" ].Value;
                PerformanceModel.Throughput = Convert.ToDouble( _bandwidth );
                var _val = Convert.ToDouble( PerformanceModel.Throughput );
                var _time = Convert.ToDouble( _timeB );
                YThroughputVal.MajorStep =
                    ( YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum ) / 10;

                LineSeriesCurrentVal.Points.Add( new DataPoint( _time, _val ) );
                _plotModel.InvalidatePlot( true );
                if( _val > YThroughputVal.ActualMaximum )
                {
                    var _xPan = ( YThroughputVal.ActualMaximum - YThroughputVal.DataMaximum - 50 )
                        * YThroughputVal.Scale;

                    YThroughputVal.Pan( _xPan );
                }
            }
            else
            {
            }
        }

        /// <summary>
        /// Processes the output data received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DataReceivedEventArgs"/>
        /// instance containing the event data.</param>
        private void ProcessOutputDataReceived( object sender, DataReceivedEventArgs e )
        {
            PerformanceModel.Output += e.Data;
            PerformanceModel.Output += "\n";
            if( e.Data != null )
            {
                PlotData( e.Data );
            }
        }

        /// <summary>
        /// Stops the iperf.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void StopIperf( object parameter )
        {
            //iperfProcess.Kill();
            PerformanceProcess.StopProcess( );
        }

        /// <summary>
        /// Clears the output.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void ClearOutput( object parameter )
        {
            PerformanceModel.Output = "";
        }

        /// <summary>
        /// Saves the output.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public async void SaveOutput( object parameter )
        {
            var _receDataSaveFileDialog = new SaveFileDialog
            {
                Title = "save iperf result",
                FileName = DateTime.Now.ToString( "yyyyMMddmmss" ),
                DefaultExt = ".txt",

                //Filter = string.Format("en", "*.*")
                Filter = "txt files(*.txt)|*.txt|All files(*.*)|*.*"
            };

            if( _receDataSaveFileDialog.ShowDialog( ) == true )
            {
                var _dataRecvPath = _receDataSaveFileDialog.FileName;

                //SavePathInfo = string.Format(CultureInfos, "{0}", DataRecvPath);
                await using var _defaultReceDataPath = new StreamWriter( _dataRecvPath, true );
                await _defaultReceDataPath.WriteAsync( PerformanceModel.Output )
                    .ConfigureAwait( false );
            }
        }

        /// <summary>
        /// Iperfs the help.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void IperfHelp( object parameter )
        {
            var _iperfPath = @"Libraries\iperf\" + PerformanceModel.Version;
            PerformanceProcess.StartProcess( _iperfPath, "--help", ProcessOutputDataReceived );
        }
    }
}