   M S B A M L   `   `   `  ����   
TimBrowser$    TimBrowser.Views.LogChosenView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       � 	Resources     ParametersListDataTemplate � ColumnDefinitions ��Name �� StringFormat " TimChosenParameterDataTemplate ��Name �� CornerRadius �Name � RowDefinitions ��Name��%x  &	        &	 �    7   �� .R�� $��1q� �� $
��0.1* ��� $
��0.5* ��� $
��0.4* �.+��� $ NumberTextBlock��$	��4,2q�$
��Wrap=�$��Center=�$��Center=�$��0��$��13.333(����� *Number+�� $ DateTimeTextBlock��$	��4,2q�$
��Wrap=�$��Center=�$��Center=�$��1��$��13.333(����� *DateTimeString+$ {0:dd.MM.yy HH:mm:ss}���� $ ValueTextBlock��$	��4,2q�$
��Wrap=�$��2��$��2��$��Center=�$��Center=�$��13.333(����� *ValueString+�� .R��� $ 
MainBorder��
�������$��1q�$ 1��$��4q�._�� $ MainGrid��$��Center=�$��1,0,1,1q� �� $
��0.1* ��� $
��0.9* �.+��� $ TitleTextBlock��$
��Wrap=�$��2q�$��Center=�$��16(����� *ParameterName+�� $ ParametersList��$��1��8��  $��Stretch=�$��Stretch=�$��Disabled=�$��Disabled=���Z� ��Z� ���� *ChosenParameters+.��� -   $ MainGrid��.+��� -   $ ChosenListView��#��[ $��Stretch=�	 � IsSharedSizeScope	 . ���� *
ChosenList+��Z� ��Z� 