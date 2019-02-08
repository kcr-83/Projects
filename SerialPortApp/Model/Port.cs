using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortApp.Model
{
    public class Port : INotifyPropertyChanged, IDataErrorInfo
    {
        private SerialPort _serialPort;

        public event PropertyChangedEventHandler PropertyChanged;

        public Port()
        {
            _serialPort = new SerialPort();
        }

        public string PortName
        {
            get { return _serialPort.PortName; }
            set
            {
                _serialPort.PortName = value;
                OnPropertyChanged("PortName");
            }
        }

        public int BaudRate
        {
            get { return _serialPort.BaudRate; }
            set
            {
                _serialPort.BaudRate = value;
                OnPropertyChanged("BaudRate");
            }
        }

        public Parity Parity
        {
            get { return _serialPort.Parity; }
            set
            {
                _serialPort.Parity = value;
                OnPropertyChanged("Parity");
            }
        }

        public int DataBits
        {
            get { return _serialPort.DataBits; }
            set
            {
                _serialPort.DataBits = value;
                OnPropertyChanged("PortDataBits");
            }
        }

        public StopBits StopBits
        {
            get { return _serialPort.StopBits; }
            set
            {
                _serialPort.StopBits = value;
                OnPropertyChanged("PortStopBits");
            }
        }

        public Handshake Handshake
        {
            get { return _serialPort.Handshake; }
            set
            {
                _serialPort.Handshake = value;
                OnPropertyChanged("PortHandshake");
            }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public string[] AvailablePortNames
        {
            get { return SerialPort.GetPortNames(); }
        }


        string IDataErrorInfo.Error
        {
            get { return null; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return this.GetValidationError(propertyName); }
        }

        private string GetValidationError(string propertyName)
        {
            //TODO
            return String.Empty;
        }
    }
}

