   M S B A M L   `   `   `  ����  GcodeGenerator,    &GcodeGenerator.RectangleGcodeGenerator   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Rectangle GCode Generator��5   	   $��733.225��6+   $	��850��6<    � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ resources/icon.ico��5   	    � 
ResizeMode$ CanMinimize=�5   	   .��5	      � $��0,0,0,64q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��5q�6.   .+�5      �� $��X:��6   $��0 0 5 0q�6"   $��Center=�63   �� 5      -   5         ��Name$ XTextBox��5      $	��100��6(   $��Center=�64   $��Center=�6X   �� 5      $��Y:��6   $��10 0 5 0q�6"   $��Center=�64   �� 5      -   5        $ YTextBox��5      $	��100��6(   $��Center=�64   $��Center=�6X   �� 5      $$��Число перемычек:��6   $��10 0 5 0q�60   $��Center=�6B   �� 5      -   5         ��Name$ NumberComboBox��5      $��Center=�6/   .@�5      �� $��0��6   �� 5      $��1��6   �� 5      $��2��6   �� 5      $��3��6   �� 5      $��4��6   �� 5      $��5��6   �� 5      $��6��6   �� 5      $��7��6   �� 5      $��8��6   �� 5      $��9��6   �� 5       $��10��6   5!      �� 5"      -   5        $)��#Сгенерировать G-код��5"      $��10q�6P   $	��126��6\   �� 5#      -   5        $��Сохранить��5#      $��Center=�6B   $	��238��6]   $��0q�6i   5$      �� 5%   
   $��260,10,257,90q�6   $��1��6'   ��5&      �� 5'       �� 
TargetType" ��6   .�5(      �� 	 �� Property	 � ��
 �� Value$
 resources/rectangle.png��6/    �� Triggers 5)      �� 5*       �� Binding 6&   ��  �� ElementName$ NumberComboBox�� �� Path$ SelectedItem.Content� �� Value 16p   .a�5+      �� 	 � ��$
 resources/rectangle1.png��67   5,      �� 5-       6&   �� $ NumberComboBox��$ SelectedItem.Content� 26p   .a�5.      �� 	 � ��$
 resources/rectangle2.png��67   5/      �� 50       6&   �� $ NumberComboBox��$ SelectedItem.Content� 36p   .a�51      �� 	 � ��$
 resources/rectangle3.png��67   52      �� 53       6&   �� $ NumberComboBox��$ SelectedItem.Content� 46p   .a�54      �� 	 � ��$
 resources/rectangle4.png��67   55      �� 56       6&   �� $ NumberComboBox��$ SelectedItem.Content� 56p   .a�57      �� 	 � ��$
 resources/rectangle5.png��67   58      �� 59       6&   �� $ NumberComboBox��$ SelectedItem.Content� 66p   .a�5:      �� 	 � ��$
 resources/rectangle6.png��67   5;      �� 5<       6&   �� $ NumberComboBox��$ SelectedItem.Content� 76p   .a�5=      �� 	 � ��$
 resources/rectangle7.png��67   5>      �� 5?       6&   �� $ NumberComboBox��$ SelectedItem.Content� 86p   .a�5@      �� 	 � ��$
 resources/rectangle8.png��67   5A      �� 5B       6&   �� $ NumberComboBox��$ SelectedItem.Content� 96p   .a�5C      �� 	 � ��$
 resources/rectangle9.png��67   5D      �� 5E       6&   �� $ NumberComboBox��$ SelectedItem.Content� 106p   .a�5F      �� 	 � ��$
 resources/rectangle10.png��67   5G      5I      5K      �� 5L   
   -   5        $ GCodeTextBox��5L   B   $
��Auto=�6   $��1��6X   $��5,203,5,-49q�6e    �� TextWrapping$ WrapWithOverflow=�6z    �� AcceptsReturn . $��2��6�   5M      5N      