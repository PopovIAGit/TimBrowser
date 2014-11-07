﻿#pragma checksum "..\..\..\..\..\Views\Bluetooth\BluetoothView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E6454D12A5E628CB51730A673717B07D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Caliburn.Micro;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TimBrowser.ViewModels;
using TimBrowser.Views.Controls;


namespace TimBrowser.Views {
    
    
    /// <summary>
    /// BluetoothView
    /// </summary>
    public partial class BluetoothView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 46 "..\..\..\..\..\Views\Bluetooth\BluetoothView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock InfoTextTextBlock;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Views\Bluetooth\BluetoothView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DiscoverDevicesButton;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\Views\Bluetooth\BluetoothView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid DeviceListGrid;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\Views\Bluetooth\BluetoothView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox DevicesListBox;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\..\Views\Bluetooth\BluetoothView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConnectDeviceButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TimBrowser;component/views/bluetooth/bluetoothview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\Bluetooth\BluetoothView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.InfoTextTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.DiscoverDevicesButton = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.DeviceListGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.DevicesListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.ConnectDeviceButton = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

