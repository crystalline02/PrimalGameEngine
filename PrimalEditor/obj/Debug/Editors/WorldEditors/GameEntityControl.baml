   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PrimalEditor.EditorsPrimalEditor.Editors  5      ,    &PrimalEditor.Editors.GameEntityControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:PrimalEditor.Editors     5      .��5
      �� $��Disabled=�6   $
��Auto=�6<   ��5   
   �� 5        �� 
TargetType"  ��6    �� Triggers 5      P� 5       P� Property �@�DataContext P� Value 65   Z� . �5      ��  �� Property � y� �� Value$ 	Collapsed=�67   5      5      .��5   
   ��  �� Orientation$ Vertical=�6   .+�5      �� $��25��6   
�������._�5      d� $
��Auto��6   $
��Left=�6,   $��
8, 3, 0, 3q�6G   ��5      ]� 5      .+�5      F� =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Right=�5   #   
���   �$��1��5   #   
���   � �� Stretch$ Uniform=�5   #   $��
3, 3, 3, 3q�5   #   $��13��67   �� 5       $��Add Component��6(   $��1q�6=   5!      5#      5$      � 5%       � ColumnDefinitions 5&      �� 5'      $��45 �6'   �� 5(      �� 5)      $
��Auto �6'   .+�5+      �� $
��Name��6   $��0��6(   $��
8, 3, 0, 3q�68   $��Center=�6L   $��Center=�6i   �� 5-      $��1��6z   $��
8, 3, 0, 3q�6�   ��6   �� *Name+	 �� Mode$	 OneWay=�
 �� UpdateSourceTrigger$
 Explicit=�
 � Tag 6[   �� *RenameCommand+6�   �� 5.      $ 
Horizontal=�6   $��2��66   $��
8, 3, 0, 3q�6F   .+�5/      �� $��	IsEnabled��6    $��
5, 3, 0, 3q�61   �� 50      $��3q�53       d� 	IsChecked 50      �� *
IsEnable+$	 OneWay=���51      �� *EnableCommand+��52      �� *	IsChecked+ �� RelativeSource ��  �� Mode$
 Self=�53      54      55      �� 56      ��6   �� *
Components+��57      �� 58      .R�59      �� 5:      5<      5=      5>      5?      