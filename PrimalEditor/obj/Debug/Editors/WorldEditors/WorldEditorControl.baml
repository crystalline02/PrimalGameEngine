   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PrimalEditor.EditorsPrimalEditor.Editors  5      ?$clr-namespace:PrimalEditor.UtilitiesPrimalEditor.Utilities  5      Q-clr-namespace:PrimalEditor.Utilities.ControlsPrimalEditor.Utilities.Controls  5      -    'PrimalEditor.Editors.WorldEditorControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:PrimalEditor.Editors     5      5util$clr-namespace:PrimalEditor.Utilities     5      ?ctrls-clr-namespace:PrimalEditor.Utilities.Controls     5      $
|�True��5	        J� InputBindings  5      �� 5   
   
 �� Key$ S��6Y    �� 	Modifiers$
 Ctrlf�6a    �� Command 6   �� *SaveProjectCommand+ �� CommandParameter 6<   �� 6a   �� 5   
   $ Z��65   $
 Ctrlf�6=    6   �� *UndoCommand+6=   �� 5   
   $ Y��65   $
 Ctrlf�6=    6   �� *RedoCommand+6=   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      �� 5      .+�5      �� $��1��6   $��5��5      $��Stretch=�5       $	��Top=�5!       �� ResizeDirection$
 Rows=�5"      
������ 5#      �� 5$       �� Orientation$ Vertical=�6   $��0��60   .+�5%      �� -   5        $��Add new script��5%      $
��Left=�62   6M   /  )PrimalEditor.Utilities.Controls.NumberBox  5&      	   Value$
	 17.5��6"   $��Center=�6/   /  )PrimalEditor.Utilities.Controls.VectorBox  5'      
   X$
 -0.12��6"      Z$
 0.25��6,     5(      $

 9.86��6"   $��Right=�6+     5)         	Demension$ Vector4=�6"   $��Center=�66     5*      $ Vector2=�6"   $��Center=�66      Orientation$ Vertical=�6S     5+      $��Stretch=�6"   $ Vertical=�6@   5,      �� 5.      $��1��6   $��
0, 5, 0, 0q�6'   .@�5/      �� $��History��6   .��50      ,   &PrimalEditor.Utilities.UndoRedoControl   � DataContext 6+   �� *UndoRedoManager+51      �� 52      $��Logger��6   .��53      *   $PrimalEditor.Utilities.LoggerControl  54      55      56      � 58   
   $��1��6   $��
5, 0, 0, 0q�6    59      �� 5:      �� 5;      �� 5<      .+�5?      �� $��1��6   $��5��5@      $��Stretch=�5A      $	��Top=�5B      $
 Rows=�5C      
������ 5D      /   )PrimalEditor.Editors.ProjectLayoutControl  5F      $��0��6)   $��5,0,0,0q�66   ,   &PrimalEditor.Editors.GameEntityControl  5H      $��1��6&   $��
5, 5, 0, 0q�63   $��2��6G   5I      �� 5K   
   $��1��6   $��5��5L      $
��Left=�5M      $��Stretch=�5N      $ Columns=�5O      
������ 5P      5Q      5R      