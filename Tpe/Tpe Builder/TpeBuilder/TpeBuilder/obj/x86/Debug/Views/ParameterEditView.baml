   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      H DTpeParameters, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullT:clr-namespace:TpeParameters.Helpers;assembly=TpeParametersTpeParameters.Helpers 5      E ATpeBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:TpeBuilder.ViewModelTpeBuilder.ViewModel 5      (   "TpeBuilder.Views.ParameterEditView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      Jtpe:clr-namespace:TpeParameters.Helpers;assembly=TpeParameters     5      1vm"clr-namespace:TpeBuilder.ViewModel     5        � DataContext  5      1  +TpeBuilder.ViewModel.ParameterEditViewModel  5   
    � 	Resources 5         ParamTypesEnum S� 
MethodName S� 
ObjectType   System.Enum S� MethodParameters M� TypeName  ParamAppointmentsEnum  ParamValueTypesEnum��%k  &	        &	 k     &	 �     S� 5   
   $ 	GetValues��64   "  6K    5      M� 5      $ tpe:ParamTypes��6   5      S� 5   
   $ 	GetValues��6;   "  6R    5      M� 5      $ tpe:ParamAppointments��6   5      S� 5   
   $ 	GetValues��69   "  6P    5      M� 5      $ tpe:ParamValueTypes��6   5!      5#      .��5%      �  � ColumnDefinitions 5&      �� 5'      $
��0.5* �6   �� 5(      $
��0.5* �6   .+�5*      �� $
��12,0q�6   .+�5+      �� -   5         ��Name$ ParameterIndexTextBlock��5+      $
��Wrap=�63   $��15��6G   $��0q�6S   �� 6_    � Language$ ru-ru�6c   $%�Индекс параметра��6t   �� 6�   �� 6�   $ ru-ru�6�   6�   �� 5,      -   5        	 ��Name$	 ParameterIndexTextBox��5,      
 �� TextWrapping$

 Wrap=�65   $��0q�6�   ��6I   �� *CurrentParameterItem.Index+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5-      -   5        $ ParameterNameTextBlock��5-      $
��Wrap=�62   $��15��6F   $��0,12,0,0q�6R   �� 6e   $ ru-ru�6i   $1�+Наименование параметра��6z   �� 6�   �� 6�   $ ru-ru�6�   �� 6�   �� 6�   $ ru-ru�6�   6�   �� 5.      -   5        $	 ParameterNameTextBox��5.      $

 Wrap=�64   $��0q�6�   ��6H   �� *CurrentParameterItem.Name+$ TwoWay=�$ PropertyChanged=�6�   �� 5/      -   5        $ ParameterAddressTextBlock��5/      $
��Wrap=�6;   $��15��6O   $��0,12,0,0q�6[   �� 6n   $ ru-ru�6r   $#�Адрес параметра��6�   �� 6�   �� 6�   $ ru-ru�6�   6�   �� 50      -   5        $	 ParameterAddressTextBox��50      $

 Wrap=�67   $��0q�6�   ��6K   �� *CurrentParameterItem.Address+$ TwoWay=�$ PropertyChanged=�6�   �� 51      -   5        $$ ParameterVariableNameTextBlock��51      $
��Wrap=�6@   $��15��6T   $��0,12,0,0q�6`   �� 6s   $ ru-ru�6w   $3�-Наименование переменной��6�   �� 6�   �� 6�   $ ru-ru�6�   6�   �� 52      -   5        $"	 ParameterVariableNameTextBox��52      $

 Wrap=�6<   $��0q�6�   ��6P   �� *#!CurrentParameterItem.VariableName+$ TwoWay=�$ PropertyChanged=�6�   �� 53      -	   5         ��Name$ ConfigurationGroupBox��53      $��Конфигурация��66   $��0,12,0,0q�6L   .��54      �� .+�55      �� -
   5        $  ParameterTypeComboBoxTitle��55      $
��Wrap=�6>   $��15��6R   $��0,12,0,0q�6^   �� 6q   $ ru-ru�6u   $�Тип параметра��6�   �� 6�   �� 6�   $ ru-ru�6�   6�   �� 56      -   5         ��Name$ ParameterTypeComboBox��56      $��0q�57   I   ��56   8   ��  �� Source# [   �� SelectedValue 57      �� * CurrentParameterItem.ParamType+6I   �� 58      -   5        $( "ParameterAppointmentСomboBoxTitle��58      $
��Wrap=�6E   $��15��6Y   $��0,12,0,0q�6e   �� 6x   $ ru-ru�6|   $-�'Назначение параметра��6�   �� 6�   �� 6�   $ ru-ru�6�   6�   �� 59      -   5        $" ParameterAppointmentComboBox��59      $��0q�5:   K   ��59   E   �� # [  5:      �� *" CurrentParameterItem.Appointment+6K   �� 5;      $ ��Редактируется��6   $��0,12,0,0q�67    d� 	IsChecked 6I   �� *CurrentParameterItem.CanEdit+�� 5<      $)��#Избранный параметр��6   $��0,12,0,0q�6<    6N   �� *CurrentParameterItem.IsChosen+$ TwoWay=�5=      5>      �� 5@      -   5        $ InfoGroupBox��5@      $��Информация��6-   $��0,12,0,0q�6A   .��5A      �� .+�5B      �� -   5        $# ParameterDescriptionTextBlock��5B      $
��Wrap=�6A   $��15��6U   $��0,12,0,0q�6a   $��Описание��6s   �� 5C      -   5        $!	 ParameterDescriptionTextBox��5C      $

 Wrap=�6C   $��0q�6�   ��6W   �� *" CurrentParameterItem.Description+$ TwoWay=�$ PropertyChanged=�6�   �� 5D      -   5        $ ParameterInfoTextBlock��5D      $
��Wrap=�6:   $��15��6N   $��0,12,0,0q�6Z   $��Комментарий��6l   �� 5E      -   5        $	 ParameterInfoTextBox��5E      $

 Wrap=�66   $��0q�6�   ��6J   �� *CurrentParameterItem.Comment+$ TwoWay=�$ PropertyChanged=�6�   5F      5G      5H      �� 5I      $��1��6   $
��12,0q�6"   .+�5J      �� -   5        $ ValueDescriptionGroupBox��5J      $(��"Описание значения
��63   $��0q�6S   .��5K   	   �� .+�5L   
   �� -   5        $ ParameterMinimumTextBlock��5L      $
��Wrap=�67   $��15��6K   $��0q�6W   �� 6c   $ ru-ru�6g   $-�'Минимальное значение��6x   �� 6�   �� 6�   $ ru-ru�6�   6�   �� 5M   
   -   5        $	 ParameterMinimumTextBox��5M      $

 Wrap=�63   $��0q�6�   ��6G   �� *CurrentParameterItem.Minimum+$ TwoWay=�$ PropertyChanged=�6�   �� 5N   
   -   5        $ ParameterMaximumTextBlock��5N      $
��Wrap=�67   $��15��6K   $��0,12,0,0q�6W   �� 6j   $ ru-ru�6n   $/�)Максимальное значение��6   �� 6�   �� 6�   $ ru-ru�6�   �� 6�   �� 6�   $ ru-ru�6�   $� максимум��6�   6�   �� 5O   
   -   5        $	 ParameterMaximumTextBox��5O      $

 Wrap=�63   $��0q�6�   ��6G   �� *CurrentParameterItem.Maximum+$ TwoWay=�$ PropertyChanged=�6�   �� 5P   
   -   5        $ ParameterDefaulTextBlock��5P      $
��Wrap=�66   $��15��6J   $��0,12,0,0q�6V   �� 6i   $ ru-ru�6m   $.�(Значение по умолчанию��6~   �� 6�   �� 6�   $ ru-ru�6�   6�   �� 5Q   
   -   5        $	 ParameterDefaulTextBox��5Q      $

 Wrap=�62   $��0q�6�   ��6F   �� *CurrentParameterItem.Default+$ TwoWay=�$ PropertyChanged=�6�   �� 5R   
   -   5        $% ParameterValueTypeComboBoxTitle��5R      $
��Wrap=�6=   $��15��6Q   $��0,12,1.437,0q�6]   $2��,Тип значения параметра
��6s   �� 5S      -   5        $  ParameterValueTypeComboBox��5S      $��0,0,1.437,0q�5T   C   ��5S   C   �� # [  5T   
   �� * CurrentParameterItem.ValueType+6C   �� 5U   
   -   5        $ ParameterUnitTextBlock��5U      $
��Wrap=�64   $��15��6H   $��0,12,0,0q�6T   �� 6g   $ ru-ru�6k   $'�!Единица измерения��6|   6�   �� 5V   
   -   5        $	 ParameterUnitTextBox��5V      $

 Wrap=�60   $��0q�6�   ��6D   �� *CurrentParameterItem.Unit+$ TwoWay=�$ PropertyChanged=�6�   �� 5W   
   -   5        $# ParameterCoefficientTextBlock��5W      $
��Wrap=�6;   $��15��6O   $��0,12,0,0q�6[   $1��+Коэффициент (множитель)��6m   �� 5X   
   -   5        $!	 ParameterCoefficientTextBox��5X      $

 Wrap=�67   $��0q�6�   ��6K   �� *" CurrentParameterItem.Coefficient+$ TwoWay=�$ PropertyChanged=�6�   5Y   
   5Z   	   �� 5[      -    5         ��Name$ FieldsButton��5[      $��Битовые поля��6%   $	��150��6<   $��30��6H   $��0,12,0,0q�6T   y�5\      �� *FieldsButtonVisibility+5]      5_      5c      