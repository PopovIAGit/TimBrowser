   M S B A M L   `   `   `  ����  
TimBrowser0    *TimBrowser.Views.LogEvChosenParametersView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5	       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value   ParametersListDataTemplate � ColumnDefinitions ��Name	 �� StringFormat " TimChosenParameterDataTemplate
 ��Name �� CornerRadius �Name � RowDefinitions ��Name �� Color��%f  '��       &	  �     &	 b    7   (�� �    55      * SystemColors.HighlightBrushKey+)�� 5   
   " ��6    5      P� 5       �@��
IsSelected . . �5      ��  � ��
 �   �63   5      5      �� 5   
   .R�5      � $��1q�6    5      �� 5      $
��0.1* �6'   �� 5      $
��0.5* �6'   �� 5      $
��0.4* �6'   .+�5      �� $ NumberTextBlock��6   $	��4,2q�65   $
��Wrap=�6B   $��Center=�6n   $��Center=�6�   $��0��6�   $��13.333(�6�   ��6V   �� *Number+6�   �� 5      $ DateTimeTextBlock��6   $	��4,2q�67   $
��Wrap=�6D   $��Center=�6�   $��Center=�6�   $��1��6�   $��13.333(�6  ��6X   �� *DateTimeString+$	 {0:dd.MM.yy HH:mm:ss}��6  �� 5      $ ValueTextBlock��6   $	��4,2q�64   $
��Wrap=�6A   $��2��6r   $��2��6�   $��Center=�6�   $��Center=�6�   $��13.333(�6�   ��6U   �� *ValueString+6�   5      5      �� 5    
   .R�5!      �� $
 
MainBorder��6   
�������$��1q�6@   $ 1��6T   $��4q�6e   ._�5#      � $ MainGrid��6   $��Center=�6)   $��1,0,1,1q�6D    5$      �� 5%      $
��0.1* �6(   �� 5&      $
��0.9* �6(   .+�5(      �� $ TitleTextBlock��6    $
��Wrap=�68   $��2q�6�   $��Center=�6�   $��16(�6�   ��6L   �� *ParameterName+6�   �� 5+      $ ParametersList��6#   $��1��6;   8��  $��Stretch=�5-   
   $��Stretch=�6/   $��Disabled=�5.   
   $��Disabled=�6@   ��5+   H   Z� ��6^   Z� ��5,   
   �� *ChosenParameters+5.   @   5/      50      52      �� 55   
   $ Transparent��56      58      .��5:      � 
������ .+�5;   
   �� -   5         ��Name$ ChosenListView��5;      #��[ 5=      $��Stretch=�5>       � IsSharedSizeScope . $��Visible=�5@      ��5<      �� *LogChosenParameters+��5=   F   Z� ��5?   1   Z� 5@      5f      5h      