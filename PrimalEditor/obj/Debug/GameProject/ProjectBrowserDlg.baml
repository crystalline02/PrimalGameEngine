   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PrimalEditor.GameProjectPrimalEditor.GameProject  5   	   0    *PrimalEditor.GameProject.ProjectBrowserDlg   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:PrimalEditor.GameProject     5   	     � 
ResizeMode$  NoResize=�5       � WindowStartupLocation$ CenterScreen=�60    � Title$ ProjectBrowserDlg��5   	   $	��800��6#   $	��600��6/      PrimalDialogStyle#��[  5	   	    � 	Resources 5
       �� 
TargetType �� Property �� Value!  System.Windows.SystemColors   ControlTextBrushKey �� 
TargetType	 ��Name
 �� Path �� RelativeSource �� Mode J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   
BlurRadius   ShadowDepth   Opacity �� Triggers P� Property P� Value  Editor.FontColorBrush " Editor.Disabled.FontColorBrush ]� 
Conditions �� Property �� Value��%�  'd�       7  7  7  �� 5   
   " d�6   .�5      ��  � ��
 ���� 6+   �� 5       � |�$ False��6*   �� 5       � ��
 ���� 6,   �� 5       � ��# �  6+   �� 5       � ��$ 0q�60   �� 5       � ��$ Center=�64   �� 5       � ��$ Center=�62   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 0q�6(   �� 5       � ��$ 38(�6)   �� 5       � ��$ Bahnschrift SemiBold)�6+   �� 5       � ��$	 230��6&   �� 5       � ��$ 20, 0q�6'   �� 5       � ��$ 0q�6(   �� 5       � �� 5      �� 5      " d�6&   .R�5      �� $	 ToggleButtonText��6%   #��z��5    %   #��z��5!   %   ��5   %   �� $
 Content� �� $ TemplatedParent=� 5"        5#   "   $ 10Q�63   $ 2Q�6C   $	 0.7Q�6S   5%       5&      P� 5'       �@d�	IsChecked$
 TrueY�6;   . �5(   "   ��  � ��$ 40(�6=   �� 5)   "    � ��$
 Bold"�6?   �� 5*   "   8    � ��6?   5+      P� 5,       �@d�	IsChecked$ FalseY�6;   . �5-   "   �� 8   � ��6?   5.      ]� 5/       50   "   �� 51   &    �@J�IsMouseOver$
 True��6G   �� 52   &    �@d�	IsChecked$ FalseY�6E   ./�54   "   ��  � ��$ 40(�6=   �� 55   "    � ��$
 Bold"�6?   �� 56   "   8   � ��6?   57      ]� 58       59   "   �� 5:   &    �@J�IsMouseOver$
 True��6G   �� 5;   &    �@d�	IsChecked$
 TrueY�6E   ./�5=   "   ��  � ��$ 40(�6=   �� 5>   "    � ��$
 Bold"�6?   �� 5?   "    � ��
 �����6?   5@      5B      5D      5E      5F      .��5H      �  � RowDefinitions 5I   
   �� 5J      $
��Auto �6   �� 5K      .+�5M   
   �� -   5         ��Name$ SpotLightCanvas��5M      $��0��6*   .+�5N      �� -   5         ��Name$ SpotLightRect��5P       �� Left$	 150��5Q      $
��Left=�6*   $��Center=�6E   ��5N      �� $
 ActualWidth� �� ElementName$ OpenProjectButton����5O      �� $
 ActualHeight� �� $ FindAncestor=� �� AncestorType" ����5R      
� 5W       
� GradientOrigin$ 0.5, 1&�6*    
� Center$ 0.5, 1&�6B    
� RadiusY$	 0.6Q�6R     
� RadiusX$	  0.6Q�6`   ��5Y      � ! � Color$! White��6+   " � Offset$" 0Q�69   � 5Z      $! Transparent��6+   $" 1Q�6?   5\      # J� OpacityMask# 5^      
� 5_      $ 0.5, 1&�6*   $ 0.5, 1&�6B   $	 0.4Q�6R   $	  0.5Q�6`   ��5a      � $! White��6+   $" 0Q�69   � 5b      $! Transparent��6+   $" 1Q�6?   5d      5f      5g      �� 5h   
   $��1��6   $	��Top=�5i      $��Stretch=�5j      $��2��5k      ��5l      �� 5m      $ �� 
StartPoint$
$ 0, 0&�6&   % �� EndPoint$
% 1, 0&�68   ��5o      � $! Transparent��6'   $" 0Q�6;   � 5p      $! White��6'   $	" 0.4Q�65   � 5q      $! White��6'   $	" 0.4Q�65   � 5r      $! Transparent��6'   $" 1Q�6;   5t      5v      �� 5w   
   $��0��6   & �� Orientation$& 
Horizontal=�6#   $��Center=�6<   $��Center=�6Y   $��20q�6t   .+�5x      d� -   5        ' d�Name$' OpenProjectButton��5{      $��Open Project��5y      ( d� 	IsChecked$
( TrueY�5z      5|      d� 5}      -   5        $' NewProjectButton��5�      $��New Project��5~      $( FalseY�5      5�      5�      �� 5�   
   -   5        ) ��Name$) browserPanel��5�   W   $��1��6   $& 
Horizontal=�6"   $
��Left=�6;    5�        5�      $ 5Q�6#   $ 10Q�63   $	 0.7Q�6C   .+�5�      1   +PrimalEditor.GameProject.OpenProjectControl  -   5        *  Name$* OpenProjectView��5�   '   0   *PrimalEditor.GameProject.NewProjectControl  5�      -   5        +  Name$+ NewProjectView��5�   &   $��800, 0, 0, 0q�6>   5�      5�      5�      