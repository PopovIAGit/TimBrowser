   M S B A M L   `   `   `  ���� c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  U QCaliburn.Micro, Version=1.5.2.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21fG4clr-namespace:Caliburn.Micro;assembly=Caliburn.MicroCaliburn.Micro  
TimBrowser(   "TimBrowser.Views.BluetoothAuthView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     binterPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     Icaliburn4clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro     .   (System.Windows.Interactivity.Interaction    Triggers  /   )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.   (System.Windows.Interactivity.TriggerBase   Actions. "  Caliburn.Micro.ActionMessage     
MethodName$ 
ViewLoaded��   
Parameters.   Caliburn.Micro.Parameter     Value$ $source��.��� -    �Name$ 
MainLayout�� � ColumnDefinitions �� $
��0.5* ��� $
��0.5* � � RowDefinitions �� $
��0.5* ��� $
��0.5* �.+��� -   	 ��Name$	 PinCodeBorder�����	#00FF7946$��1q�$
��12,0q�$��2��$��50��{��� *IsAuthControlsEnabled+._��� $��0q�.+��� -   
 ��Name$
 PinCodeTitleTextBlock��$
��Wrap=�$��Segoe UI Symbol)�$��10.667(����	#FF3D3D3D$��0q����� *TitleMessage+�� -    ��Name$ PinCodeTextBox�� �� TextWrapping$
 Wrap=�$	��1,0q�$��Center=�$��40��$��Center=� �� TextAlignment$ Center=����� *	PinCode+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��    $ TextChanged��.   $ PinCodeTextInput��.���� -    ��Name$ AuthRequestButton��$��Авторизация��$��Center=�$��Center=�$��1��$	��100��$��40��{��� *IsAuthControlsEnabled+�� -   $ AuthCancelButton��$��Отмена��$��Center=�$��Center=�$��1��$��1��$	��100��$��40��{��� *IsAuthControlsEnabled+