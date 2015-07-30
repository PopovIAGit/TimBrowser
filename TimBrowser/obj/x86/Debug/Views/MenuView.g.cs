﻿#pragma checksum "..\..\..\..\Views\MenuView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D716E11A2C2B37C86A055891107B3264"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
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


namespace TimBrowser.Views {
    
    
    /// <summary>
    /// MenuView
    /// </summary>
    public partial class MenuView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Download;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Download_RS;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LoadFile;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveFile;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Print;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Help;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MenuTitleTextBlock;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MenuVersionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MenuVersionTextBlock2;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MenuTypeBlock;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\Views\MenuView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MenuTypeDriveBlock;
        
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
            System.Uri resourceLocater = new System.Uri("/TimBrowser;component/views/menuview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MenuView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.Download = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.Download_RS = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.LoadFile = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.SaveFile = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.Print = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.Help = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\..\..\Views\MenuView.xaml"
            this.Help.Click += new System.Windows.RoutedEventHandler(this.Help_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.MenuTitleTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.MenuVersionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.MenuVersionTextBlock2 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.MenuTypeBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.MenuTypeDriveBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

