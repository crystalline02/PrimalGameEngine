   M S B A M L   `   `   `  ����G  CPrimalEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:PrimalEditor.GameProjectPrimalEditor.GameProject  5      1    +PrimalEditor.GameProject.OpenProjectControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:PrimalEditor.GameProject     5      $	��800��5       $	��460��6,     � DataContext  5
      *  $PrimalEditor.GameProject.OpenProject  5   
   .��5      � .+�5   
   �� 
�������.+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   $��0, 20, 0, 0q�6O   .+�5      �� -   5         ��Name$ projectsDataListBox��5   T   $	��160��6   $	��320��6&    �� SelectedIndex$ 0��6q   ��63   �� *
Projects+��5      �� 5       �� 
TargetType" ��6    .�5      -   5        5      ��5      �� 5      .R�5      �� $ 
Horizontal=�6)   .+�5   "   �� $��30��6@   $��30��6K   $��
0, 0, 5, 0q�6X   ��6(   �� *Icon+6X   �� 5   "   ��6,   �� *ProjectName+5      5      5      �� 5      $	��500��6   $	��320��6%   $��20, 0, 0, 0q�62   ��5       �� 5!       j� Stretch$ Uniform=�6{    �� ImageSource 6%   ��  �� Path$ SelectedItem.ScreenShot� �� ElementName$ projectsDataListBox��6{   5#      5$      �� 5%      $ 
Horizontal=�6   $��Center=�62   $��0, 63, 0, 0q�6O   .+�5&      �� -   5        $
��Open��5&      $��17(�6(   $��95��66   $��30��6A   	 �� 	IsDefault$
	 True��6M   6^   �� 5'      $��Cancel��6   $��17(�6*   $��95��68   $��30��6C   $��150, 0, 0, 0q�6O   
 �� IsCancel$

 True��6e   5(      5)      5*      5+      