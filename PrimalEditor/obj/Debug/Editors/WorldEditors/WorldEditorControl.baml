   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PrimalEditor.EditorsPrimalEditor.Editors  5      ?$clr-namespace:PrimalEditor.UtilitiesPrimalEditor.Utilities  5      -    'PrimalEditor.Editors.WorldEditorControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:PrimalEditor.Editors     5      5util$clr-namespace:PrimalEditor.Utilities     5      $
|�True��5        J� InputBindings  5      �� 5   
   
 �� Key$ S��6Y    �� 	Modifiers$
 Ctrlf�6a    �� Command 6   �� *SaveProjectCommand+ �� CommandParameter 6<   �� 6a   �� 5   
   $ Z��65   $
 Ctrlf�6=    6   �� *UndoCommand+6=   �� 5   
   $ Y��65   $
 Ctrlf�6=    6   �� *RedoCommand+6=   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      �� 5      .+�5      �� $��1��6   $��5��6(   $��Stretch=�63   $	��Top=�6Q    �� ResizeDirection$
 Rows=�6i   �� 5      $��1��6   $��
0, 5, 0, 0q�6'   .@�5       �� $��History��6   .��5!      ,   &PrimalEditor.Utilities.UndoRedoControl   � DataContext 6+   �� *UndoRedoManager+5"      �� 5#      $��Logger��6   .��5$      *   $PrimalEditor.Utilities.LoggerControl  5%      5&      5'      � 5)   
   $��1��6   $��
5, 0, 0, 0q�6    5*      �� 5+      �� 5,      .+�5/      �� $��1��6   $��5��6(   $��Stretch=�63   $	��Top=�6Q   $
 Rows=�6i   /   )PrimalEditor.Editors.ProjectLayoutControl  51      $��0��6)   $��
5, 0, 0, 0q�66   ,   &PrimalEditor.Editors.GameEntityControl  53      $��1��6&   $��
5, 5, 0, 0q�63   54      �� 56   
   $��1��6   $��5��6(   $
��Left=�62   $��Stretch=�6M   $ Columns=�6i   57      58      