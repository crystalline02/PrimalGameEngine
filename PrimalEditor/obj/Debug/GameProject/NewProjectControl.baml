   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PrimalEditor.GameProjectPrimalEditor.GameProject  5      0    *PrimalEditor.GameProject.NewProjectControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:PrimalEditor.GameProject     5      $	��800��5      $	��460��6+     � DataContext  5      )  #PrimalEditor.GameProject.NewProject  5   
   .��5      � .+�5   
   �� 
�������.+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��0, 20, 0, 0q�6O   .+�5      �� -   5         ��Name$ templateListBox��5   \   $	��160��6   $	��260��6&    �� SelectedIndex$ 0��6u   ��63   �� *ProjectTemplates+��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   �� $��30��6@   $��30��6K   $��
0, 0, 5, 0q�6X   ��6(   �� *Icon+6X   �� 5   "   ��6,   �� *ProjectType+5      5      5!      �� 5"      $	��500��6   $	��260��6%   $��20, 0, 0, 0q�62   ��5#      �� 5$       j� Stretch$ Uniform=�6w    �� ImageSource 6%   ��  �� Path$ SelectedItem.ScreenShot� �� ElementName$ templateListBox��6w   5&      5'      �� 5(      $ 
Horizontal=�6   $��Center=�62   $��0, 40, 0, 0q�6O   $��24��6d   .+�5)      �� $
��Name��6   $��60��6(   $��17(�63   �� 5*      $	��600��6   $��
8, 0, 0, 0q�6&   $��16(�6|   ��6:   �� *ProjectName+ �� UpdateSourceTrigger$ PropertyChanged=�6|   5+      �� 5,      $ 
Horizontal=�6   $��Center=�62   $��0, 10, 0, 0q�6O   $��24��6d   .+�5-      �� $
��Path��6   $��60��6(   $��17(�63   �� 5.      $	��500��6   $��
8, 0, 0, 0q�6&   $��16(�6|   ��6:   �� *ProjectPath+$ PropertyChanged=�6|   �� 5/      $��Browse��6   $��90��6*   $��17(�65   $��10, 0, 0, 0q�6C   50      �� 51      $ 
Horizontal=�6   $��Center=�62   $��0, 25, 0, 0q�6O   .+�52      �� -   5        $��Create��52      $��17(�6*   $��95��68   $��30��6C   	 �� 	IsDefault$
	 True��6O   {�6`   �� *	IsValid+6~   �� 53      $��Cancel��6   $��17(�6*   $��95��68   $��30��6C   $��150, 0, 0, 0q�6O   
 �� IsCancel$

 True��6e   54      �� 55      $��
0, 5, 0, 0q�62   $��Center=�6F   ��6   �� *
ErrorMsg+6F   56      57      58      