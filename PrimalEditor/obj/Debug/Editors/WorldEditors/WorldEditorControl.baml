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
������ 5#      �� 5$       �� Orientation$ Vertical=�6   $��0��60   .+�5%      /  )PrimalEditor.Utilities.Controls.NumberBox  	   Value$	 8��6"   $��Center=�6,   /  )PrimalEditor.Utilities.Controls.VectorBox  5&      
   	Demension$
 Vector2=�6"      Orientation$ 
Horizontal=�66   $��Center=�6O     5'      $
 Vector3=�6"   $ 
Horizontal=�66   $��Right=�6O     5(      $
 Vector4=�6"   $ Vertical=�66   $
��Left=�6M     5)      $
 Vector4=�6"   $ Vertical=�66   /  )PrimalEditor.Utilities.Controls.ScalerBox  5*      $		 6.8��6"   $
��Left=�6.   5+      �� 5-      $��1��6   $��
0, 5, 0, 0q�6'   .@�5.      �� $��History��6   .��5/      ,   &PrimalEditor.Utilities.UndoRedoControl   � DataContext 6+   �� *UndoRedoManager+50      �� 51      $��Logger��6   .��52      *   $PrimalEditor.Utilities.LoggerControl  53      54      55      � 57   
   $��1��6   $��
5, 0, 0, 0q�6    58      �� 59      �� 5:      �� 5;      .+�5>      �� $��1��6   $��5��5?      $��Stretch=�5@      $	��Top=�5A      $
 Rows=�5B      
������ 5C      /   )PrimalEditor.Editors.ProjectLayoutControl  5E      $��0��6)   $��5,0,0,0q�66   ,   &PrimalEditor.Editors.GameEntityControl  5G      $��1��6&   $��
5, 5, 0, 0q�63   $��2��6G   5H      �� 5J   
   $��1��6   $��5��5K      $
��Left=�5L      $��Stretch=�5M      $ Columns=�5N      
������ 5O      5P      5Q      