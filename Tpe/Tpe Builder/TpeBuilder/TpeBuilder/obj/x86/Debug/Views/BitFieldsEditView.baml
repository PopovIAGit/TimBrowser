   M S B A M L   `   `   `  ����E  ATpeBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:TpeBuilder.ViewModelTpeBuilder.ViewModel  5   	   (    "TpeBuilder.Views.BitFieldsEditView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   1vm"clr-namespace:TpeBuilder.ViewModel     5   	     � Title$  BitFieldsEditView��5   	   $	��500��6#   $	��600��60    � DataContext 5      1   +TpeBuilder.ViewModel.BitFieldsEditViewModel  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��0��6   .+�5      � .+�5      �� -   5        $��Добавить��5      $	��100��6,   $
��Left=�68   $��12,0,0,0q�6S   6e   �� 5      -   5        $��Удалить��5      $	��100��6+   $��Right=�67   $��0,0,12,0q�6S   6e   5      � 5       5      �� 5      $��* �6'   .+�5      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .  $��
12,12,12,1q�6Z   $��Stretch=�5      $��Stretch=�65   $��0��6Q   ��5      �� *
FieldItems+ �� SelectedItem 5      �� *SelectedBitField+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Бит/значение��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.23* 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *
BitValue+	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=�6�     5       $ Описание��6-   $
 0.6* 6�    6?   �� *Description+$	 TwoWay=�$
 PropertyChanged=�6�     5!      $* $Код (не обязательно)��6-   $ 0.32* 6�    6K   �� *SpecialDescription+$	 TwoWay=�$
 PropertyChanged=�6�   5#      5$      5&      5'      5(      