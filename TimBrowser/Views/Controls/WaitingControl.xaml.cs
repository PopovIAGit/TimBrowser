using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimBrowser.Views.Controls
{
    /// <summary>
    /// Interaction logic for WaitingControl.xaml
    /// </summary>
    public partial class WaitingControl : UserControl
    {
        public WaitingControl()
        {
            InitializeComponent();

            _activeStoryboard = (Storyboard)this.Resources["ActiveStoryboard"];
        }

        private static Storyboard _activeStoryboard;

        public bool Active
        {
            get { return (bool)GetValue(ActiveProperty); }
            set { SetValue(ActiveProperty, value); }
        }

        public static DependencyProperty ActiveProperty =
    DependencyProperty.Register("Active", typeof(bool), typeof(WaitingControl),
    new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnActivePropertyChanged)));

        private static void OnActivePropertyChanged(DependencyObject dependencyObject, 
               DependencyPropertyChangedEventArgs e)
        {
            bool value = (bool)e.NewValue;

            if (value)
                _activeStoryboard.Begin();
            else
                _activeStoryboard.Stop();

        }
    }
}
