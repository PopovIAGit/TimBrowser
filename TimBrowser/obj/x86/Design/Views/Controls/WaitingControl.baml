   M S B A M L   `   `   `  ����   
TimBrowser.    (TimBrowser.Views.Controls.WaitingControl   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  Bei9http://schemas.microsoft.com/expression/2010/interactions  -     >�Name$  userControl�� � 	Resources    ActiveStoryboard g� RepeatBehavior g� AutoReverse �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value��%0  &	        �� $ Forever�� . .��U� $ (UIElement.Opacity)�$ 
EllipseOne��.\�  $ 0��$ 0Q�  $ 0:0:0.8��$ 1Q�  $ 0:0:1.6��$ 0Q�U� $ (UIElement.Opacity)�$ 
EllipseTwo��.\�  $ 0:0:0.4��$ 0Q�  $ 0:0:1.2��$ 1Q�  $ 0:0:2��$ 0Q�U� $ (UIElement.Opacity)�$ EllipseThree��.\�  $ 0:0:0.8��$ 0Q�  $ 0:0:1.6��$ 1Q�  $ 0:0:2.4��$ 0Q�U� $ (UIElement.Opacity)�$ EllipseFour��.\�  $ 0:0:1.2��$ 0Q�  $ 0:0:2��$ 1Q�  $ 0:0:2.8��$ 0Q�.��� -    �Name$ 
LayoutRoot��	 J� RenderTransformOrigin$	 0.5,0.5&�
 � ColumnDefinitions
 �� $��0.25* ��� $��0.25* ��� $��0.25* ��� $��0.25* �z�V� .~��� �� �� T� .+�A� -    A�Name$ 
EllipseOne��$��16q�$��0�����	#FF0CB3EE J� Opacity$ 0Q�$��Center=����� *ActualWidth+ �� ElementName$ 
EllipseOne��A� -   $ 
EllipseTwo��$��16q�$��0�����	#FF0CB3EE$��1��$ 0Q�$��Center=����� *ActualWidth+$ 
EllipseOne��A� -   $ EllipseThree��$��16q�$��0�����	#FF0CB3EE$��2��$ 0Q�$��Center=����� *ActualWidth+$ 
EllipseOne��A� -   $ EllipseFour��$��16q�$��0�����	#FF0CB3EE$��3��$ 0Q�$��Center=����� *ActualWidth+$ 
EllipseOne��