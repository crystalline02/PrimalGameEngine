   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PrimalEditor.EditorsPrimalEditor.Editors  5      C&clr-namespace:PrimalEditor.GameProjectPrimalEditor.GameProject  5      /    )PrimalEditor.Editors.ProjectLayoutControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:PrimalEditor.Editors     5      5gp&clr-namespace:PrimalEditor.GameProject     5      .��5      �   � RowDefinitions  5   
   �� 5      $��32 �6   �� 5      .+�5   
   �� $��0��6   $��	Add Scene��6B   $
��Left=�6V   $��5q�6q   $|�False��6|   ��6   �� *AddSceneCommand+6|   �� 5   
   $��1��6   $
��Auto=�6$   $
��Auto=�6I   .��5      �� ��6   �� *Scenes+��5      �� 5      .R�5      �� 5      ��5      �� 5      .R�5!      9�  9� 
IsExpanded 6#   �� *
IsActive+ �� Mode$ OneWay=���5"      � 5#   "   .+�5$   &   ��  �� Orientation$ 
Horizontal=�61   .+�5%   *   �� ��64   �� *Name+�� 5&   *   -   5        $��+��5&   1   $��
5, 0, 0, 0q�5)   1   {�5(   1   �� *
IsActive+5)   1   �� 5+   *   $��-��61   $��
5, 0, 0, 0q�5.   1   ��5,   1   �� * DataContext.RemoveSceneCommand+ �� RelativeSource ��  �� AncestorType UserControl��5-   1   �� 5.   1   5/   '   50   #   .��52      � .+�53   "   ��  ��Name$ gameEntitesListBox��56   *   -   5         �� SelectionMode$ Extended=�55   *   ��53   *   �� *GameEntities+{�54   *   �� *
IsActive+��58   &   �� 59   *   .R�5:   .   �� 5;   +   ��5=   &   �� 5?   *   .R�5@   .   �� $ 
Horizontal=�69   .+�5A   2   �� ��6<   �� *Name+�� 5B   2   $��-��69   $��
5, 0, 0, 0q�5E   9   ��5C   9   �� *!DataContext.RemoveEntityCommand+ ��  �� Mode$ FindAncestor=� ListBox��5D   9   �� 5E   9   5F   /   5G   +   5I   #   5J      5K      5L      5N      5O      5P      5Q      