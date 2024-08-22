using Sniffy;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using WebSocketSharp.Server;
using WebSocketSharp;

namespace Sniffy
{
    public class WebSocketViewModel : MainWindowBase
    {
        public SocketModel SocketModel { get; set; }

        #region WebSocket Server
        WebSocketServer wsServer = null;
        public static ObservableCollection<string> wsRecv { get; set; } = new ObservableCollection<string> { };

        public ICommand StartListenCommand
        {
            get
            {
                return new RelayCommand(param => StartListen(param));
            }
        }
        public void StartListen(object parameter)
        {
            if (SocketModel.ServerListenButtonName == "Start Listen")
            {
                SocketModel.ServerListenButtonName = "Stop Listen";

                wsServer = new WebSocketServer(SocketModel.ServerAddress);
                wsServer.AddWebSocketService<EchoHandler>("/echo");

                wsServer.Start();

                wsRecv.Add("[" + DateTime.Now + "][" + "WebSocket Server Started]\n");
            }
            else
            {
                SocketModel.ServerListenButtonName = "Start Listen";
                wsServer.Stop();
                wsRecv.Add("[" + DateTime.Now + "][" + "WebSocket Server Stopped]\n");
            }
        }
        public class EchoHandler : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    string time = "[" + this.StartTime + "][";
                    string from = this.Context.UserEndPoint.ToString();
                    string str = "][" + e.Data + "]\n";

                    wsRecv.Add(time + from + str);

                }));
                Send(e.Data);
            }
            protected override void OnOpen()
            {
                string time = "[" + this.StartTime + "][";
                string from = this.Context.UserEndPoint.ToString();
                string status = "][" + this.State + "]\n";    
                wsRecv.Add(time + from + status);

            }
            protected override void OnClose(CloseEventArgs e)
            {
                string time = "[" + this.StartTime + "][";
                string reason = e.Reason;
                string status = "][" + this.State + "]\n";
                wsRecv.Add(time + reason + status);

            }
            protected override void OnError(ErrorEventArgs e)
            {
                string time = "[" + this.StartTime + "][";
                string reason = e.Message;
                string status = "][" + this.State + "]\n";
                wsRecv.Add(time + reason + status);
            }
        }

        public ICommand ServerAutoSendCommand
        {
            get
            {
                return new RelayCommand(param => ServerAutoSend(param));
            }
        }
        private System.Windows.Threading.DispatcherTimer mServerAutoSendTimer;

        private void ServerAutoSendTimerFunc(object sender, EventArgs e)
        {
            wsServer.WebSocketServices.Broadcast(SocketModel.ServerSendText);
        }

        public void ServerAutoSend(object parameter)
        {
            if (SocketModel.ServerSendButtonName == "Auto Send Start")
            {
                SocketModel.ServerSendButtonName = "Auto Send Stop";
                mServerAutoSendTimer = new System.Windows.Threading.DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, SocketModel.ServerSendInterval)
                };

                mServerAutoSendTimer.Tick += ServerAutoSendTimerFunc;
                mServerAutoSendTimer.Start();

            }
            else
            {
                SocketModel.ServerSendButtonName = "Auto Send Start";
                mServerAutoSendTimer.Stop();
            }
        }

        public ICommand ServerSendClearCommand
        {
            get
            {
                return new RelayCommand(param => ServerSendClear(param));
            }
        }
        public void ServerSendClear(object parameter)
        {
            SocketModel.ServerSend = "";
            wsRecv.Clear();
        }
        public ICommand ServerSendCommand
        {
            get
            {
                return new RelayCommand(param => ServerSend(param));
            }
        }
        public void ServerSend(object parameter)
        {
            wsServer.WebSocketServices.Broadcast(SocketModel.ServerSend);
        }
        #endregion

        #region WebSocket Client

        public WebSocket wsClient;
        public static ObservableCollection<string> wsClientRecv { get; set; } = new ObservableCollection<string> { };
        public ICommand ClientConnectCommand
        {
            get
            {
                return new RelayCommand(param => ClientConnect(param));
            }
        }
        public void ClientConnect(object parameter)
        {

            if (SocketModel.ClientConnectButtonName == "Connect")
            {
                using (var ws = new WebSocket(SocketModel.ServerIp))
                {
                    ws.OnOpen += (sender, e) => {

                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            string time = "[" + DateTime.Now + "]";
                            string str = "[WebSocket Open]\n";

                            wsClientRecv.Add(time + str);

                        }));
                    };

                    ws.OnMessage += (sender, e) => {
                        var fmt = "[WebSocket Message] {0}";
                        var body = !e.IsPing ? e.Data : "A ping was received.";
                        Console.WriteLine(fmt, body);

                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            string time = "[" + DateTime.Now + "]";
                            string str = "[" + e.Data + "]\n";

                            wsClientRecv.Add(time + str);

                        }));
                    };

                    ws.OnError += (sender, e) => {
                        var fmt = "[WebSocket Error] {0}";
                        Console.WriteLine(fmt, e.Message);
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            string time = "[" + DateTime.Now + "]";
                            string str = "[WebSocket Error][" + e.Message + "]\n";

                            wsClientRecv.Add(time + str);
                            SocketModel.ClientConnectButtonName = "Connect";

                        }));
                    };

                    ws.OnClose += (sender, e) => {
                        var fmt = "[WebSocket Close ({0})] {1}";
                        Console.WriteLine(fmt, e.Code, e.Reason);
                        App.Current.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            string time = "[" + DateTime.Now + "]";
                            string str = "[WebSocket Close][" + e.Reason + "]\n";

                            wsClientRecv.Add(time + str);
                            SocketModel.ClientConnectButtonName = "Connect";
                        }));
                    };

                    // Connect to the server.
                    wsClient = ws;
                }
                try
                {
                    Task.Run(() =>
                    {
                        wsClient.Connect();
                        SocketModel.ClientConnectButtonName = "DisConnect";
                    });
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                SocketModel.ClientConnectButtonName = "Connect";
                wsClient.Close();
            }
        }

        public ICommand ClientSendClearCommand
        {
            get
            {
                return new RelayCommand(param => ClientSendClear(param));
            }
        }
        public void ClientSendClear(object parameter)
        {
            SocketModel.ClientSend = "";
            wsClientRecv.Clear();
        }
        public ICommand ClientSendCommand
        {
            get
            {
                return new RelayCommand(param => ClientSend(param));
            }
        }
        public void ClientSend(object parameter)
        {
            wsClient.Send(SocketModel.ClientSend);
        }
        public ICommand ClientAutoSendCommand
        {
            get
            {
                return new RelayCommand(param => ClientAutoSend(param));
            }
        }

        private System.Windows.Threading.DispatcherTimer mClientAutoSendTimer;
        private void ClientAutoSendFunc(object sender, EventArgs e)
        {
            wsClient.Send(SocketModel.ClientSendText);
        }

        public void ClientAutoSend(object parameter)
        {
            if (SocketModel.ClientSendButtonName == "Auto Send Start")
            {
                SocketModel.ClientSendButtonName = "Auto Send Stop";
                mClientAutoSendTimer = new DispatcherTimer()
                {
                    Interval = new TimeSpan(0, 0, 0, 0, SocketModel.ClientSendInterval)
                };
                mClientAutoSendTimer.Tick += ClientAutoSendFunc;
                mClientAutoSendTimer.Start();
            }
            else
            {
                SocketModel.ClientSendButtonName = "Auto Send Start";
                mClientAutoSendTimer.Stop();
            }
        }
         #endregion
 
        public WebSocketViewModel()
        {
            SocketModel = new SocketModel();
        }
    }
}
