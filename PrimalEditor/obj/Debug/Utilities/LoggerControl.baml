   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:PrimalEditor.UtilitiesPrimalEditor.Utilities  5      *    $PrimalEditor.Utilities.LoggerControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:PrimalEditor.Utilities     5      .��5
      ]� .+�5   
   �� $	��Top=�6   ._�5      ��   �� Orientation$  
Horizontal=�6   $��Right=�62   .+�5      �� -   5        $��Clear��5      $
��5, 0q�6)   $��60��67   $��Center=�6B   6_   d� 5      -   5         d�Name$ FilterInfoBtn��5   �    d� 	IsChecked$
 TrueY�6   $
��Info��60   $
��5, 0q�6?   $��60��6M   $��Center=�6X   6�   d� 5      -   5        $ FilterWarningBtn��5   �   $
 TrueY�6   $��Warning��60   $
��5, 0q�6B   $��60��6P   $��Center=�6[   6�   d� 5      -   5        $ FilterErrorBtn��5   �   $
 TrueY�6   $��Error��60   $
��5, 0q�6@   $��60��6N   $��Center=�6Y   6�   5      5      �� 5   
   .��5      �� ��6   �� #  PrimalEditor.Utilities.Logger   MessageFiler �� Source# Z ��5      �� 5      .R�5      �� $  Vertical=�6%   5      ��5      �� 5      .R�5      ]�  � ToolTip 6$   �� *
MetaData+��5      �� 5    "    �� 
TargetType" ]�6(   .�5!   &   ��  �� Property � �� �� Value
 ���� 6C   	 �� Triggers	 5"   &   P� 5#   *   
 P� Property
 �@J�IsMouseOver P� Value$
 True��6I   . �5$   .   ��  � ��
 �����6K   5%   +   5'   #   .+�5)      �� ��6(   �� *Time+ �� StringFormat$ hh:mm:ss����5*   "   �� 5+   &   " ��6,   	 5,   *   �� 5-   .   (  "PrimalEditor.Utilities.MessageType   Info �� Value# Z 6S    �� Binding 6:   �� *Type+.a�5.   2   ��  � ��
 �   �6O   5/   /   �� 50   .      Warning# Z 6S    6:   �� *Type+.a�51   2   ��  � ��
 � ���6O   52   /   �� 53   .      Error# Z 6S    6:   �� *Type+.a�54   2   ��  � ��
 �  ��6O   55   /   57   '   59      �� 5:      $
��Wrap=�6A   ��6(   �� *	Message+6A   5;      5<      5>      5?      5@      5A      