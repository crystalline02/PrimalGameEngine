   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:PrimalEditor.UtilitiesPrimalEditor.Utilities  5      *    $PrimalEditor.Utilities.LoggerControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:PrimalEditor.Utilities     5       !  Editor.Window.GrayColorBrush3#��[  5        � 	Resources  5
       �� 
TargetType �� Property  FocusVisual �� Value # Editor.Button.NormalBorderBrush �� 
TargetType ��Name �� CornerRadius J� Opacity J� SnapsToDevicePixels	 �� Triggers
 P� Property P� Value �� 
TargetName��%
  'd�       7  7  �� 5   
   " d�6   .�5      ��  �@�FocusVisualStyle8   61   �� 5       � ��8  6,   �� 5       � ��$	 1.8q�60   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 1q�6(   -   5        �� 5       � ��$ 45��6&   �� 5       � �� 5      �� 5      " d�6&   .R�5      �� $ border��5   !   $ 2��5   !   $��1q�5   !   $	 0.6Q�5   !   #��z��5   !   #��z��5   !   #��z��5   !   $
 true��5   !   5      	 5       P� 5!      
 �@J�IsMouseOver$
 true��6=   . �5"   "   �� $ border��6;    � ��$ 0q�6O   �� 5#   "   $ border��6D    � ��$ 1q�6X   5$      P� 5%      
 �@d�	IsChecked$
 trueY�6;   . �5&   "   �� $ border��6;    � ��$ 0q�6O   �� 5'   "   $ border��6<    �@J�Opacity$ 1Q�6P   5(      P� 5)      
 � {�$ false��6;   . �5*   "   ��  �@J�Opacity$	 0.5Q�6<   5+      5-      5/      50      51      .��53      ]� .+�54   
   �� $	��Top=�6    ! Editor.Window.GrayColorBrush4#��[ 55       J� Effect 56      3  -System.Windows.Media.Effects.DropShadowEffect  57         ShadowDepth$ 2Q�6#      
BlurRadius$ 5Q�63   ._�59      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   $
��1, 3q�6N   .+�5:      �� -   5        $��Clear��5:      $
��5, 0q�6)   $��60��67   $��Center=�6B   6_   d� 5;      -   5         d�Name$ FilterInfoBtn��5?       d� 	IsChecked$
 TrueY�5;      $
��2, 0q�5<        Editor.GreenColorBrush#��[ 5=      $��Center=�5>      5?      d� 5@      -   5        $ FilterWarningBtn��5D      $
 TrueY�5@      $
��2, 0q�5A        Editor.OrangeColorBrush#��[ 5B      $��Center=�5C      5D      d� 5E      -   5        $ FilterErrorBtn��5I      $
 TrueY�5E      $
��2, 0q�5F        Editor.RedColorBrush#��[ 5G      $��Center=�5H      5I      5J      5K      �� 5L   
   .��5N      �� ��6   �� #  PrimalEditor.Utilities.Logger   MessageFiler �� Source# Z ��5O      �� 5P      .R�5Q      �� $ Vertical=�6%   5R      ��5T      �� 5U      .R�5W      ]�  � ToolTip 6$   �� *
MetaData+��5X      �� 5Y   "   " ]�6(   .�5Z   &   ��  � ��
 ���� 6C    �� Triggers 5[   &   P� 5\   *   
 �@J�IsMouseOver$
 True��6I   . �5]   .   ��  � ��  Editor.BackgroundColorBrush# [ 6K   5^   +   5`   #   .+�5b      �� ��6(   �� *Time+ �� StringFormat$ hh:mm:ss����5c   "   �� 5d   &   " ��6,     PrimalTextBlockStyle �� BasedOn# [ 6L    5e   *   �� 5f   .   (  "PrimalEditor.Utilities.MessageType   Info �� Value# Z 6S    �� Binding 6:   �� *Type+.a�5g   2   ��  � ��# [ 6O   5h   /   �� 5i   .      Warning# Z 6S    6:   �� *Type+.a�5j   2   ��  � ��# [ 6O   5k   /   �� 5l   .      Error# Z 6S    6:   �� *Type+.a�5m   2   ��  � ��# [ 6O   5n   /   5p   '   5r      �� 5s      $
��Wrap=�6A   #��[ 6U   ��6(   �� *	Message+6U   5t      5u      5w      5x      5y      5z      