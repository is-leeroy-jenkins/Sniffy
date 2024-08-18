﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-18-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-18-2024
// ******************************************************************************************
// <copyright file="PortScanModel.cs" company="Terry D. Eppler">
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
//   PortScanModel.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Sniffy.MainWindowBase" />
    public class PortScanModel : MainWindowBase
    {
        /// <summary>
        /// The close count
        /// </summary>
        private int _closeCnt;

        /// <summary>
        /// The ip
        /// </summary>
        private string _ip;

        /// <summary>
        /// The open count
        /// </summary>
        private int _openCnt;

        /// <summary>
        /// The port
        /// </summary>
        private string _port;

        /// <summary>
        /// The scan button name
        /// </summary>
        private string _scanButtonName;

        /// <summary>
        /// The socket timeout
        /// </summary>
        private int _socketTimeout;

        /// <summary>
        /// The start port
        /// </summary>
        private int _startPort;

        /// <summary>
        /// The stop port
        /// </summary>
        private int _stopPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortScanModel"/> class.
        /// </summary>
        public PortScanModel( )
        {
            Ip = "192.168.1.1";
            StartPort = 1;
            StopPort = 65535;
            SocketTimeout = 1000;
            ScanButtonName = "Start";
            CloseCnt = 0;
            OpenCnt = 0;
        }

        /// <summary>
        /// Gets or sets the start port.
        /// </summary>
        /// <value>
        /// The start port.
        /// </value>
        public int StartPort
        {
            get { return _startPort; }
            set
            {
                if( _startPort != value )
                {
                    _startPort = value;
                    OnPropertyChanged( nameof( StartPort ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the stop port.
        /// </summary>
        /// <value>
        /// The stop port.
        /// </value>
        public int StopPort
        {
            get { return _stopPort; }
            set
            {
                if( _stopPort != value )
                {
                    _stopPort = value;
                    OnPropertyChanged( nameof( StopPort ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the ip.
        /// </summary>
        /// <value>
        /// The ip.
        /// </value>
        public string Ip
        {
            get { return _ip; }
            set
            {
                if( _ip != value )
                {
                    _ip = value;
                    OnPropertyChanged( nameof( Ip ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>
        /// The port.
        /// </value>
        public string Port
        {
            get { return _port; }
            set
            {
                if( _port != value )
                {
                    _port = value;
                    OnPropertyChanged( nameof( Port ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the open count.
        /// </summary>
        /// <value>
        /// The open count.
        /// </value>
        public int OpenCnt
        {
            get { return _openCnt; }
            set
            {
                if( _openCnt != value )
                {
                    _openCnt = value;
                    OnPropertyChanged( nameof( OpenCnt ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the close count.
        /// </summary>
        /// <value>
        /// The close count.
        /// </value>
        public int CloseCnt
        {
            get { return _closeCnt; }
            set
            {
                if( _closeCnt != value )
                {
                    _closeCnt = value;
                    OnPropertyChanged( nameof( CloseCnt ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of the scan button.
        /// </summary>
        /// <value>
        /// The name of the scan button.
        /// </value>
        public string ScanButtonName
        {
            get { return _scanButtonName; }
            set
            {
                if( _scanButtonName != value )
                {
                    _scanButtonName = value;
                    OnPropertyChanged( nameof( ScanButtonName ) );
                }
            }
        }

        /// <summary>
        /// Gets or sets the socket timeout.
        /// </summary>
        /// <value>
        /// The socket timeout.
        /// </value>
        public int SocketTimeout
        {
            get { return _socketTimeout; }
            set
            {
                if( _socketTimeout != value )
                {
                    _socketTimeout = value;
                    OnPropertyChanged( nameof( SocketTimeout ) );
                }
            }
        }
    }
}