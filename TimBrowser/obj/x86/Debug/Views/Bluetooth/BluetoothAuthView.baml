   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      U QCaliburn.Micro, Version=1.5.2.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21fG4clr-namespace:Caliburn.Micro;assembly=Caliburn.MicroCaliburn.Micro 5       
TimBrowser(   "TimBrowser.Views.BluetoothAuthView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      binterPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      Icaliburn4clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro     5      .   (System.Windows.Interactivity.Interaction    Triggers  5      /   )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .   (System.Windows.Interactivity.TriggerBase   Actions. 5      "  Caliburn.Micro.ActionMessage     
MethodName$ 
ViewLoaded��6%      
Parameters. 5        Caliburn.Micro.Parameter     Value$ $source��6%   5      5      .��5      � -   5         �Name$ 
MainLayout��5       � ColumnDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6    � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $
��0.5* �6   .+�5   
   �� -   5        	 ��Name$	 PinCodeBorder��5      ���	#00FF7946$��1q�6@   $
��12,0q�6T   $��2��6b   $��50��6v   {�5      �� *IsAuthControlsEnabled+._�5      �� $��0q�6   .+�5       �� -   5        
 ��Name$
 PinCodeTitleTextBlock��5       $
��Wrap=�6;   $��Segoe UI Symbol)�6m   $��10.667(�6�   ���	#FF3D3D3D$��0q�6�   ��6O   �� *TitleMessage+6�   �� 5!      -   5         ��Name$ PinCodeTextBox��5!       �� TextWrapping$
 Wrap=�6,   $	��1,0q�6@   $��Center=�6g   $��40��6�   $��Center=�6�    �� TextAlignment$ Center=�6�   ��5"      �� *	PinCode+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��6W     5#        5$      $ TextChanged��6!   . 5%        $ PinCodeTextInput��6&   5&      .��5'   )   5(      5)      5*      �� 5,   
   -   5         ��Name$ AuthRequestButton��5,      $��Авторизация��6,   $��Center=�6B   $��Center=�6_   $��1��6z   $	��100��6�   $��40��6�   {�5-      �� *IsAuthControlsEnabled+�� 5/   
   -   5        $ AuthCancelButton��5/      $��Отмена��6+   $��Center=�6<   $��Center=�6Y   $��1��6t   $��1��6�   $	��100��6�   $��40��6�   {�50      �� *IsAuthControlsEnabled+52      54      