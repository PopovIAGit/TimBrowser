﻿#pragma checksum "..\..\..\..\Views\LogEvParametersView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "414535EDB9A00520F202F6E582BF4AC1A39D590C85D5206A46DB20E728432A90"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TimBrowser.Converters;
using TimBrowser.Views.Controls;


namespace TimBrowser.Views {
    
    
    /// <summary>
    /// LogEvParametersView
    /// </summary>
    public partial class LogEvParametersView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 53 "..\..\..\..\Views\LogEvParametersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid LogEvParametersDataGrid;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Views\LogEvParametersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid EventNameGrid;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Views\LogEvParametersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock EventNameTextBlock;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\Views\LogEvParametersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock FilterTextBlock;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\..\Views\LogEvParametersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox FilterListBox;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\Views\LogEvParametersView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TimBrowser.Views.Controls.BitParameterControl ParameterControl;
        
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
            System.Uri resourceLocater = new System.Uri("/TimBrowser;component/views/logevparametersview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\LogEvParametersView.xaml"
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
            this.LogEvParametersDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.EventNameGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.EventNameTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.FilterTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.FilterListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.ParameterControl = ((TimBrowser.Views.Controls.BitParameterControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

