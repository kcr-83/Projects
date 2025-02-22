﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SerialPortApp.Base;

namespace SerialPortApp.ViewModel
{
    class MainWindowViewModel: BindableBase {
	
        public MainWindowViewModel() { 
            NavCommand = new MyICommandT<string>(OnNav); 
        } 
		
        private PortViewModel portViewModel = new PortViewModel(); 
		
        private BindableBase _CurrentViewModel; 
		
        public BindableBase CurrentViewModel { 
            get {return _CurrentViewModel;} 
            set {SetProperty(ref _CurrentViewModel, value);} 
        }
		
        public MyICommandT<string> NavCommand { get; private set; }

        private void OnNav(string destination) {
		
            switch (destination) { 
                case "portSelect": 
                    CurrentViewModel = portViewModel; 
                    break; 
            } 
        } 
    } 
}
