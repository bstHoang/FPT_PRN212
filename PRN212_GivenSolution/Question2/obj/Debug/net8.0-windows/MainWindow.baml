   M S B A M L   `   `   `  ����D  @Question2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Question2	Question2  5   	       Question2.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Question2     5   	     � Title$  
MainWindow��5   	   $	��450��6   $
��1050��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��2* �6   �� 5      $��1* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dataGridProjects��5         AutoGenerateColumns$ False��5      $��Stretch=�5      $��Stretch=�5      $��10q�5         CanUserAddRows$ False��5      $��0��5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H   4  .System.Windows.Controls.DataGridTemplateColumn  5      $
 Name��6)   $ 3* 67   	   CellTemplate	 5      �� 5      .R�5      �� $��CharacterEllipsis=�5       $��20��5!      ��5   (   �� *Name+5!      5"      5$        5&      $ Description��6)   $ 3* 6>   	 5'      �� 5(      .R�5)      �� $��CharacterEllipsis=�5*      $��20��5+      ��5)   (   �� *Description+5+      5,      5.        50      $ 	StartDate��6%   $ 3* 6V    68   �� *	StartDate+6V     51      $
 Type��6%   $ 2* 6L    63   �� *Type+6L   53      � 56   
   $
��Left=�6   $��Center=�57      $��10q�58      $	��300��59      $��1��5:       5;      �� 5<      $��* �6#   �� 5>      $��3* �6#   
 � RowDefinitions
 5B      �� 5C      $
��Auto �6    �� 5E      $
��Auto �6    �� 5G      $
��Auto �6    �� 5I      $
��Auto �6    �� 5K      $
��Auto �6    �� 5M      $
��Auto �6    �� 5O      $
��Auto �6    .+�5T      �� $��Current Project��6   $
��Bold"�5U      $��0��5V      $��2��5W      $��0,0,0,10q�5X      �� 5[      $��ID��6   $��1��6"   $��0��6/   $��Center=�6?   $��	0,15,10,5q�6Z   �� 5\      -   5         ��Name$ txtID��5\       �� 
IsReadOnly$
 True��6%   $��1��67   $��1��6D   $	��154��6T   $
��Left=�6`   $��0,15,0,5q�6{   �� 5_      $
��Name��6   $��2��6$   $��0��61   $��Center=�6A   $��	0,15,10,5q�6\   �� 5`      -   5        $��txtName��5`      $��2��6%   $��1��62   $��0,15,0,5q�6B   �� 5c      $��Description��6   $��3��6+   $��0��68   $	��Top=�6H   $��	0,15,10,5q�6`   �� 5d      -   5        $��txtDescription��5d      $��3��6,   $��1��69   $��90��6I    �� TextWrapping$
 Wrap=�6U    �� AcceptsReturn$
 True��6i   $��0,15,0,5q�6~   �� 5g      $��	StartDate��6   $��4��6)   $��0��66   $��Center=�6F   $��	0,15,10,5q�6a   (  "System.Windows.Controls.DatePicker  5h      -   5        $��datePickerStartDate��5h      $��4��64   $��1��6A   $��0,15,0,5q�6Q   �� 5k      $
��Type��6   $��5��6$   $��0��61   $��Center=�6A   $��	0,15,10,5q�6\   �� 5l      -   5         ��Name$ comboBoxType��5l      $	��150��6-   $
��Left=�69   $��5��6T   $��1��6a   $��0,15,0,5q�6q   �� 5o       �� Orientation$ 
Horizontal=�6   $��Center=�62   $��6��6O   $��2��6\   $��0,10,0,0q�6p   $	��323��6�   .+�5p      �� -   5         ��Name$ 
btnRefresh��5p      $��Refresh��6-   $��	12,0,30,0q�6X   $��63��6k   �� 5q      -   5        $ btnAdd��5q      $	��Add��6)   $��0,0,30,0q�6L   $��71��6^   �� 5r      -	   5        $ btnEdit��5r      $
��Edit��6*   $��0,0,30,0q�6O   $��72��6a   5s      5t      5u      5v      