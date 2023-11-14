using Syncfusion.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2.Common;

namespace WpfApp2.Menu.SerialConnection.ViewModel
{
    public class SerialConnectionViewModel : BaseViewModel
    {
        private SerialPort serialPort = new();

        private readonly RelayCommand? serialConnectCommand;
        private readonly RelayCommand? sendCommand;
        public RelayCommand SerialConnectCommand => serialConnectCommand ?? new RelayCommand(SerialConnect);
        public RelayCommand SendCommand => sendCommand ?? new RelayCommand(Send);

        private ObservableCollection<BaseItemSource> comPorts = new();
        private ObservableCollection<BaseItemSource> baudRates = new();
        private ObservableCollection<BaseItemSource> dataBits = new();
        private ObservableCollection<BaseItemSource> parities = new();
        private ObservableCollection<BaseItemSource> stopBits = new();
        private BaseItemSource? selectedComport;
        private BaseItemSource? selectedBaudRate;
        private BaseItemSource? selectedDataBit;
        private BaseItemSource? selectedParity;
        private BaseItemSource? selectedStopBit;
        private string connectionLog = string.Empty;
        private string dataLog = string.Empty;
        private string sendMessage = string.Empty;
        private bool isDTR = true;
        private bool isRTS = true;

        public ObservableCollection<BaseItemSource> ComPorts
        {
            get => comPorts;
            set
            {
                comPorts = value;
                OnPropertyChanged(nameof(comPorts));
            }
        }
        public ObservableCollection<BaseItemSource> BaudRates
        {
            get => baudRates;
            set
            {
                baudRates = value;
                OnPropertyChanged(nameof(baudRates));
            }
        }
        public ObservableCollection<BaseItemSource> DataBits
        {
            get => dataBits;
            set
            {
                dataBits = value;
                OnPropertyChanged(nameof(dataBits));
            }
        }
        public ObservableCollection<BaseItemSource> Parities
        {
            get => parities;
            set
            {
                parities = value;
                OnPropertyChanged(nameof(parities));
            }
        }
        public ObservableCollection<BaseItemSource> StopBits
        {
            get => stopBits;
            set
            {
                stopBits = value;
                OnPropertyChanged(nameof(stopBits));
            }
        }
        public BaseItemSource? SelectedComport
        {
            get => selectedComport;
            set
            {
                selectedComport = value;
                OnPropertyChanged(nameof(selectedComport));
            }
        }
        public BaseItemSource? SelectedBaudRate
        {
            get => selectedBaudRate;
            set
            {
                selectedBaudRate = value;
                OnPropertyChanged(nameof(selectedBaudRate));
            }
        }
        public BaseItemSource? SelectedDataBit
        {
            get => selectedDataBit;
            set
            {
                selectedDataBit = value;
                OnPropertyChanged(nameof(selectedDataBit));
            }
        }
        public BaseItemSource? SelectedParity
        {
            get => selectedParity;
            set
            {
                selectedParity = value;
                OnPropertyChanged(nameof(selectedParity));
            }
        }
        public BaseItemSource? SelectedStopBit
        {
            get => selectedStopBit;
            set
            {
                selectedStopBit = value;
                OnPropertyChanged(nameof(selectedStopBit));
            }
        }
        public string ConnectionLog
        {
            get => connectionLog;
            set
            {
                connectionLog = value;
                OnPropertyChanged(nameof(connectionLog));
            }
        }
        public string DataLog
        {
            get => dataLog;
            set
            {
                dataLog = value;
                OnPropertyChanged(nameof(dataLog));
            }
        }
        public string SendMessage
        {
            get => sendMessage;
            set
            {
                sendMessage = value;
                OnPropertyChanged(nameof(sendMessage));
            }
        }
        public bool IsDTR
        {
            get => isDTR;
            set
            {
                isDTR = value;
                OnPropertyChanged(nameof(isDTR));
            }
        }
        public bool IsRTS
        {
            get => isRTS;
            set
            {
                isRTS = value;
                OnPropertyChanged(nameof(isRTS));
            }
        }




        public SerialConnectionViewModel()
        {
            serialPort.DataReceived += SerialPort_DataReceived;

            SerialPort.GetPortNames().ForEach(port =>
            {
                comPorts.Add(new() { Name = port });
            });

            baudRates.Add(new() { Name = "4800" });
            baudRates.Add(new() { Name = "7200" });
            baudRates.Add(new() { Name = "9600" });
            baudRates.Add(new() { Name = "14400" });
            baudRates.Add(new() { Name = "19200" });
            baudRates.Add(new() { Name = "38400" });
            baudRates.Add(new() { Name = "57600" });
            baudRates.Add(new() { Name = "115200" });

            dataBits.Add(new() { Name = "5" });
            dataBits.Add(new() { Name = "6" });
            dataBits.Add(new() { Name = "7" });
            dataBits.Add(new() { Name = "8" });

            parities.Add(new() { Name = "Even" });
            parities.Add(new() { Name = "Mark" });
            parities.Add(new() { Name = "None" });
            parities.Add(new() { Name = "Odd" });
            parities.Add(new() { Name = "Space" });

            stopBits.Add(new() { Name = "None" });
            stopBits.Add(new() { Name = "One" });
            stopBits.Add(new() { Name = "OnePointFive" });
            stopBits.Add(new() { Name = "Two" });
        }

        public void SerialClose()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        private void SerialConnect()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    SerialClose();
                    SaveLogMessage($"{serialPort.PortName} Disconnected");
                    return;
                }
                  

                if (string.IsNullOrEmpty(selectedComport?.Name))
                {
                    JSTTMessageBox.Show("COM Port를 선택하세요");
                    return;
                }

                serialPort.PortName = selectedComport!.Name!;
                serialPort.BaudRate = int.Parse(selectedBaudRate!.Name!);
                serialPort.DataBits = int.Parse(selectedDataBit!.Name!);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), selectedParity!.Name!);
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), selectedStopBit!.Name!);
                serialPort.DtrEnable = isDTR;
                serialPort.RtsEnable = IsRTS;
                serialPort.Open();

                SaveLogMessage($"{selectedComport?.Name} Connected");

                serialPort.Write("Test");
            }
            catch (Exception ex)
            {
                JSTTMessageBox.Show(ex.Message);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (sender is SerialPort port)
                {
                    string recivedMessage = $"Recived : {port.ReadExisting()}";

                    StringBuilder sb = new(dataLog);
                    sb.Append(recivedMessage);
                    DataLog = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                JSTTMessageBox.Show(ex.Message);
            }
        }

        private void SaveLogMessage(string message)
        {
            StringBuilder sb = new(connectionLog);
            sb.AppendLine(message);
            ConnectionLog = sb.ToString();
        }

        private void Send()
        {
            try
            {
                if (serialPort.IsOpen == false)
                {
                    SendMessage = string.Empty;
                    return;
                }

                serialPort.WriteLine(sendMessage);

                StringBuilder sb = new(dataLog);
                sb.AppendLine($"Send : {sendMessage}");
                DataLog = sb.ToString();

                SendMessage = string.Empty;
            }
            catch (Exception ex)
            {
                JSTTMessageBox.Show(ex.Message);
            }
        }

        public void SendButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Send();
            }
        }

    }
}
