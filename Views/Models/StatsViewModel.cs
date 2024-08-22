﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-19-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-19-2024
// ******************************************************************************************
// <copyright file="StatsViewModel.cs" company="Terry D. Eppler">
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
//   StatsViewModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Windows.Threading;
    using System.Collections.ObjectModel;
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Sniffy.MainWindowBase" />
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class StatsViewModel : MainWindowBase
    {
        /// <summary>
        /// The plot timer
        /// </summary>
        private readonly DispatcherTimer _plotTimer = new DispatcherTimer( );

        /// <summary>
        /// The byte count
        /// </summary>
        private long _byteCount;

        /// <summary>
        /// The capture time
        /// </summary>
        private string _captureTime;

        /// <summary>
        /// The packet count
        /// </summary>
        private long _packetCount;

        /// <summary>
        /// The previous byte count
        /// </summary>
        private long _prevByteCount;

        /// <summary>
        /// The previous time elapsed
        /// </summary>
        private long _prevTimeElapsed;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatsViewModel"/> class.
        /// </summary>
        public StatsViewModel( )
        {
            InitPlot( );
            ProtocolStats = SnifferStatsProcess.ProtocolStats;
            ConnectionStats = SnifferStatsProcess.ConnectionStats;
            _prevTimeElapsed = 0;
            _prevByteCount = 0;
        }

        /// <summary>
        /// Gets the protocol stats.
        /// </summary>
        /// <value>
        /// The protocol stats.
        /// </value>
        public ObservableCollection<ProtocolModel> ProtocolStats { get; private set; }

        /// <summary>
        /// Gets the connection stats.
        /// </summary>
        /// <value>
        /// The connection stats.
        /// </value>
        public ObservableCollection<ConnectionModel> ConnectionStats { get; private set; }

        /// <summary>
        /// Gets or sets the capture time.
        /// </summary>
        /// <value>
        /// The capture time.
        /// </value>
        public string CaptureTime
        {
            get { return _captureTime; }
            set
            {
                if( _captureTime != value )
                {
                    _captureTime = value;
                    OnPropertyChanged( nameof( CaptureTime ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the packet count.
        /// </summary>
        /// <value>
        /// The packet count.
        /// </value>
        public long PacketCount
        {
            get { return _packetCount; }
            set
            {
                if( _packetCount != value )
                {
                    _packetCount = value;
                    OnPropertyChanged( nameof( PacketCount ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the byte count.
        /// </summary>
        /// <value>
        /// The byte count.
        /// </value>
        public long ByteCount
        {
            get { return _byteCount; }
            set
            {
                if( _byteCount != value )
                {
                    _byteCount = value;
                    OnPropertyChanged( nameof( ByteCount ) );
                }
            }
        }

        /// <summary>
        /// Gets the plot model data.
        /// </summary>
        /// <value>
        /// The plot model data.
        /// </value>
        public PlotModel PlotModelData { get; private set; }

        /// <summary>
        /// Initializes the plot.
        /// </summary>
        private void InitPlot( )
        {
            PlotModelData = new PlotModel
            {
                Title = "Bandwidth (kB/s)"
            };

            PlotModelData.Axes.Add( new LinearAxis
            {
                Title = "Time (s)",
                Position = AxisPosition.Bottom
            } );

            PlotModelData.Series.Add( new LineSeries
            {
                Title = "Bandwidth",
                LineStyle = LineStyle.Solid
            } );

            _plotTimer.Interval = TimeSpan.FromMilliseconds( 500 );
            _plotTimer.Tick += UpdatePlotModel;
            _plotTimer.Start( );
        }

        /// <summary>
        /// Updates the plot model.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void UpdatePlotModel( object sender, EventArgs e )
        {
            var _timeElapsed = SnifferStatsProcess.StopWatch.ElapsedMilliseconds;
            if( _timeElapsed - _prevTimeElapsed > 0 )
            {
                var _ts = SnifferStatsProcess.StopWatch.Elapsed;
                Application.Current.Dispatcher.BeginInvoke( new Action( ( ) =>
                {
                    PacketCount = SnifferStatsProcess.PacketCount;
                    ByteCount = SnifferStatsProcess.ByteCount;
                    CaptureTime = String.Format( "{0:00}:{1:00}:{2:00}.{3:000}", _ts.Hours,
                        _ts.Minutes, _ts.Seconds, _ts.Milliseconds );
                } ) );

                var _diffCount = ByteCount - _prevByteCount > 0
                    ? ByteCount - _prevByteCount
                    : 0;

                var _kBPerSec = _diffCount / ( _timeElapsed - _prevTimeElapsed ) * 0.001 * 1024;
                lock( PlotModelData.SyncRoot )
                {
                    var _bandWidthLine = ( LineSeries )PlotModelData.Series[ 0 ];
                    _bandWidthLine.Points.Add( new DataPoint( _timeElapsed * 0.001, _kBPerSec ) );
                }

                PlotModelData.InvalidatePlot( true );
                _prevTimeElapsed = _timeElapsed;
                _prevByteCount = ByteCount;
            }
        }
    }
}