﻿using Sniffy;
using System;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sniffy
{
    public class PortScanViewModel : MainWindowBase
    {
        public PortScanModel PortScanModel { get; set; }

        private protected CancellationTokenSource scanTokenSource;

        private protected CancellationToken cancelScanToken;
        public int portCnt = 0;

        public ObservableCollection<PortScanResult> PortScanResults { get; set; } = new ObservableCollection<PortScanResult>{ };

        private void ScanPort(string ip, int port)
        {
            Console.WriteLine("Begin Scan..." + port.ToString());
            PortScanModel.Port = port.ToString();
            using (TcpClient client = new TcpClient())
            {
                if (client.ConnectAsync(ip, port).Wait(PortScanModel.SocketTimeout))
                {
                    Console.WriteLine("port {0,5}tOpen.", port);
                    PortScanModel.OpenCount++;
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        PortScanResults.Add(new PortScanResult { Port = port, Status = "open" });
                    });
                }
                else
                {
                    Console.WriteLine("port {0,5}tClosed.", port);
                    PortScanModel.CloseCount++;
                }
            }
            portCnt--;
            if (portCnt == 0)
            {
                PortScanModel.ScanButtonName = "Start";
            }
            Console.WriteLine("Port Scan Completed!");
        }
        public async void StartScanAsync()
        {
            scanTokenSource = new CancellationTokenSource();
            cancelScanToken = scanTokenSource.Token;
            int startPortVal = PortScanModel.StartPort;
            int stopPortVal = PortScanModel.StopPort;

            string ipStr = PortScanModel.Ip;
            portCnt = stopPortVal - startPortVal;
            if (portCnt <= 0)
            {
                PortScanModel.ScanButtonName = "Start";
                MessageBox.Show("Please make sure (Start Port) < (Stop Port)!");
                return;
            }
            try
            {
                await Task.Run(() =>
                {
                    for (int i = startPortVal; i <= stopPortVal; i++)
                    {
                        Console.WriteLine(i.ToString());
                        if (scanTokenSource.IsCancellationRequested)
                        {
                            break;
                        }
                        var j = i;
                        var task = Task.Run(() =>
                        {
                            ScanPort(ipStr, j);
                        }, cancelScanToken);
                        Thread.Sleep(5);
                    }
                }, cancelScanToken);
            }
            catch (Exception ex)
            {

            }
        }
        internal void StopScanTask()
        {
            scanTokenSource.Cancel();
        }
        public void PortScanCmd(object parameter)
        {
            if (PortScanModel.ScanButtonName == "Start")
            {
                PortScanModel.ScanButtonName = "Stop";
                PortScanModel.OpenCount = 0;
                PortScanModel.CloseCount = 0;
                PortScanResults.Clear();

                StartScanAsync();
            }
            else
            {
                StopScanTask();
                PortScanModel.ScanButtonName = "Start";
            }
        }
        public ICommand PortScanCommand
        {
            get
            {
                return new RelayCommand(param => PortScanCmd(param));
            }
        }
        public PortScanViewModel()
        {
            PortScanModel = new PortScanModel();

        }
    }
}
