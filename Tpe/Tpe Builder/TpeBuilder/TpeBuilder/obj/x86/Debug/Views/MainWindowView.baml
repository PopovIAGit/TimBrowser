   M S B A M L   `   `   `  ����E  ATpeBuilder, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:TpeBuilder.ViewsTpeBuilder.Views  5   	   ;"clr-namespace:TpeBuilder.ViewModelTpeBuilder.ViewModel  5   	   %    TpeBuilder.Views.MainWindowView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0viewsclr-namespace:TpeBuilder.Views     5   	   1vm"clr-namespace:TpeBuilder.ViewModel     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6F     � Title$"  Tpe Builder Platinum Edition��5   	   $	��700��6.   $	��900��6;    � DataContext 5	      .   (TpeBuilder.ViewModel.MainWindowViewModel  5
   
   .��5      �  � RowDefinitions 5      �� 5      $��0.127* �6   �� 5      $��0.873* �6    � ColumnDefinitions 5      �� 5      $
��236* �6   �� 5      $
��642* �6   .+�5   
   $   TpeBuilder.Views.TableTreeView  $��1q�6   $��1��6)   �� 5   
   $��1��6=   $��1��6J   ��6   �� *CurrentEditView+6J   �� 5   
   -   5        $��+ таблица��5      $
��Left=�6%   $	��Top=�6@   $��75��6X   $��8,2,0,0q�5   9   {�6   �� *CanCreateNewTable+69   �� 5   
   -   5        $��+ группа��5      $	��Top=�6$   $��8,27.96,0,0q�6<   $
��Left=�6n   $��75��6�   �� 5   
   -   5        $��+ параметр��5      $��8,53.92,0,8.08q�6&   $
��Left=�6z   $��75��6�   �� 5   
   -   5         ��Name$ UpButton��5      $��Up��6#   $��97,27.96,100.613,36.114q�60   5       �� 5!   
   -   5        $ 
DownButton��5!      $
��Down��6%   $��97,0,100.613,8.08q�64   $��20��6O   $��Bottom=�5"   *   �� 5#   
   -   5        $��Load TPE��5#      $
��Left=�6$   $��87,8,0,0q�6?   $	��Top=�6Q   $��75��6i   $��1��6t   6�   �� 5$   
   -   5        $��Save TPE��5$      $��Right=�6$   $��0,8,563.387,0q�6@   $	��Top=�6W   $��75��6o   $��1��6�   �� 5%   
   -   5        $��- параметр��5%      $��0,54,8,8q�6&   $��Right=�6S   $��75��6o   6z   �� 5&   
   -	   5        $��- группа��5&      $	��Top=�6$   $��0,26,8,0q�6<   $��Right=�6N   $��75��6j   6u   �� 5'   
   -
   5        $��Save XML��5'      $��Right=�6$   $��0,33.96,563.387,28.154q�6@   $��75��6`   $��1��6k   6�   �� 5(   
   $��Load XML��6   $
��Left=�6$   $��87,33.96,0,28.154q�6?   $��75��6Z   $��1��6e   5+      5,      