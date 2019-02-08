using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerialPortApp.Base;
using SerialPortApp.Model;

namespace SerialPortApp.ViewModel
{
    public class PortViewModel : BindableBase
    {
        private Port _selectedPort;
        public Port SelectedPort { 
            get { 
                return _selectedPort; 
            } 
	
            set { 
                _selectedPort = value;
            } 
        }

        public ObservableCollection<string> AvailablePorts
        {
            get;
            set;
        }

        public PortViewModel() { 
            LoadPorts();
        }

        private void LoadPorts()
        {
            AvailablePorts = new ObservableCollection<string>();
            var _ports = SerialPort.GetPortNames();
            foreach (var _port in _ports)
            {
                AvailablePorts.Add(_port);
            }
        }
    }
}
