
namespace Sniffy
{
    using OxyPlot;
    using OxyPlot.Axes;
    using OxyPlot.Series;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using System.Text.RegularExpressions;
    using Microsoft.Win32;
    using System.IO;
    using System.Diagnostics.CodeAnalysis;

    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    public class WebViewModel : MainWindowBase
    {
        public ProcessInterface IperfProcess;

        public WebModel WebModel { get; set; }

        private PlotModel _mPlotModelData = new PlotModel();
        public PlotModel PlotModelData
        {
            get { return _mPlotModelData; }
            set { _mPlotModelData = value; OnPropertyChanged(nameof(PlotModelData)); }
        }

        public LinearAxis XTimeAxis;
        public LinearAxis YThroughputVal;
        public LineSeries LineSeriesCurrentVal;
        public int YZoomFactor = 1;
        public void InitPlot()
        {
            PlotModelData = new PlotModel();

            XTimeAxis = new LinearAxis()
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
            YThroughputVal = new LinearAxis()
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

            PlotModelData.Axes.Add(XTimeAxis);
            PlotModelData.Axes.Add(YThroughputVal);


            LineSeriesCurrentVal = new LineSeries()
            {
                MarkerType = MarkerType.Circle,
                StrokeThickness = 1,
                YAxisKey = "Throughput",
                Title = "Throughput",
                Color = OxyColors.Red
            };

            PlotModelData.Series.Add(LineSeriesCurrentVal);
        }


        public void YZoomOut()
        {
            YThroughputVal.Maximum *= 2;
            YThroughputVal.MajorStep = (YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum) / 10;
        }
        public void YZoomIn()
        {
            YThroughputVal.Maximum /= 2;
            YThroughputVal.MajorStep = (YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum) / 10;

        }
        public void ClearPlot()
        {
            LineSeriesCurrentVal.Points.Clear();
            PlotModelData.InvalidatePlot(true);
        }
        internal CancellationTokenSource plotTokenSource;
        internal CancellationToken cancelPlotToken;
        internal void StopUpdatePlotTask()
        {
            plotTokenSource.Cancel();
        }
        //public void UpdatePlotTask()
        //{
        //    double val = 300;
        //    plotTokenSource = new CancellationTokenSource();
        //    cancelPlotToken = plotTokenSource.Token;
        //    Task.Run(
        //        () =>
        //        {
        //            while (true)
        //            {
        //                if (plotTokenSource.IsCancellationRequested)
        //                {
        //                    break;
        //                }
        //                val = Convert.ToDouble(IperfModel.Throughput);
        //                var date = DateTime.Now;
        //                lineSeriesCurrentVal.Points.Add(DateTimeAxis.CreateDataPoint(date, val));

        //                PlotModelData.InvalidatePlot(true);

        //                if (date.ToOADate() > XTimeAxis.ActualMaximum)
        //                {
        //                    var xPan = (XTimeAxis.ActualMaximum - XTimeAxis.DataMaximum) * XTimeAxis.Scale;
        //                    XTimeAxis.Pan(xPan);
        //                }
        //                Thread.Sleep(1000);
        //            }
        //        }, cancelPlotToken);
        //}
        
        public void RunIperf(object parameter)
        {
            string _iperfPath = @"Third-party\iperf\" + WebModel.Version;

            IperfProcess.StartProcess(_iperfPath, (string)parameter, ProcessOutputDataReceived);
            LineSeriesCurrentVal.Points.Clear();
            PlotModelData.InvalidatePlot(true);

        }
        public void PlotData(string str)
        {
            string _pattern;
            if(WebModel.Parallel > 1)
            {
                _pattern = @"\[SUM\]*\s*(?<a>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*-\s*(?<b>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*sec\s*(?<c>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*.Bytes\s*(?<d>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*Mbits/sec";

            }
            else
            {
                _pattern = @"(?<a>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*-\s*(?<b>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*sec\s*(?<c>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*.Bytes\s*(?<d>[1-9]\d*.\d*|0.\d*[1-9]\d*)\s*Mbits/sec";

            }
            Match _m = Regex.Match(str, _pattern);
            
            //str = "";
            //Regex r = new Regex("/(\\d+)[^\\d]+Mbits/sec/");
            
            if (_m.Success)
            {
                //return "此次验证不通过";
                string _timeA = _m.Groups["a"].Value;
                string _timeB = _m.Groups["b"].Value;
                string _bytes = _m.Groups["c"].Value;
                string _bandwidth = _m.Groups["d"].Value;
                WebModel.Throughput = Convert.ToDouble(_bandwidth);
                var _val = Convert.ToDouble(WebModel.Throughput);
                var _time = Convert.ToDouble(_timeB);

                YThroughputVal.MajorStep = (YThroughputVal.ActualMaximum - YThroughputVal.ActualMinimum) /10;
                LineSeriesCurrentVal.Points.Add(new DataPoint(_time, _val));

                PlotModelData.InvalidatePlot(true);
                if (_val > YThroughputVal.ActualMaximum)
                {
                    var _xPan = (YThroughputVal.ActualMaximum - YThroughputVal.DataMaximum - 50) * YThroughputVal.Scale;
                    YThroughputVal.Pan(_xPan);
                }
            }
            else
            {

            }
        }
        private void ProcessOutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            WebModel.Output += e.Data;
            WebModel.Output += "\n";
            if(e.Data != null)
            {
                PlotData(e.Data);
            }
        }
        public void StopIperf(object parameter)
        {
            //iperfProcess.Kill();
            IperfProcess.StopProcess();
        }
        public ICommand RunIperfCommand
        {
            get
            {
                return new RelayCommand(param => RunIperf(param));
            }
        }
        public ICommand StopIperfCommand
        {
            get
            {
                return new RelayCommand(param => StopIperf(param));
            }
        }
        public void ClearOutput(object parameter)
        {
            WebModel.Output = "";

        }
        public ICommand ClearOutputCommand
        {
            get
            {
                return new RelayCommand(param => ClearOutput(param));
            }
        }
        public async void SaveOutput(object parameter)
        {
            ///*IperfModel.Output*/ = "";
            SaveFileDialog _receDataSaveFileDialog = new SaveFileDialog
            {
                Title = "save iperf result",
                FileName = DateTime.Now.ToString("yyyyMMddmmss"),
                DefaultExt = ".txt",
                //Filter = string.Format("en", "*.*")
                Filter = "txt files(*.txt)|*.txt|All files(*.*)|*.*"
        };

            if (_receDataSaveFileDialog.ShowDialog() == true)
            {
                string _dataRecvPath = _receDataSaveFileDialog.FileName;
                //SavePathInfo = string.Format(CultureInfos, "{0}", DataRecvPath);
                using (StreamWriter _defaultReceDataPath = new StreamWriter(_dataRecvPath, true))
                {
                    await _defaultReceDataPath.WriteAsync(WebModel.Output).ConfigureAwait(false);
                }
            }

        }
        public ICommand SaveOutputCommand
        {
            get
            {
                return new RelayCommand(param => SaveOutput(param));
            }
        }
        public void IperfHelp(object parameter)
        {
            string _iperfPath = @"Third-party\iperf\" + WebModel.Version;
            IperfProcess.StartProcess(_iperfPath, "--help", ProcessOutputDataReceived);

        }
        public ICommand IperfHelpCommand
        {
            get
            {
                return new RelayCommand(param => IperfHelp(param));
            }
        }
        public WebViewModel()
        {
            IperfProcess  = new ProcessInterface();
            WebModel = new WebModel();

            InitPlot();
        }
    }
}
