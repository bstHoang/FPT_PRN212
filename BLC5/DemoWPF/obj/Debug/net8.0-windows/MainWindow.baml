   M S B A M L   `   `   `  ����B  >DemoWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:DemoWPFDemoWPF  5   	       DemoWPF.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:DemoWPF     5   	     � Title$  
MainWindow��5   	   $	��300��6   $	��820��6)   .��5	      � .+�5
   
   �� -   5        $��Select JSON File��5
      $
��Left=�6,   $	��Top=�5      $	��120��6)   5      �� 5   
   -   5         ��Name$ FilePathTextBox��5      $
��Left=�6+   $��	130,0,0,0q�5      $	��Top=�6%   $	��400��6=    �� 
IsReadOnly$
 True��5      �� 5   
   -   5        $
��Show��5      $
��Left=�6    $	��Top=�5      $��76��6)   $��	22,25,0,0q�5   *   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ StudentDataGrid��5      $
��Left=�6,   $��	10,50,0,0q�5      $	��Top=�6&   $	��200��6>   $	��500��5         AutoGenerateColumns$ False��5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5      $
 Name��6%   $ 2* 6L    63   �� *Name+6L     5      $	 Sex��6%   $ * 6J    62   �� *Sex+6J     5      $	 DOB��6%   $ * 6J    62   �� *DOB+6J   5      �� 5   
   -   5        $ 	IdTextBox��5      $
��Left=�6%   $��
617,50,0,0q�5       $	��Top=�6&   $	��160��5!      $��22��6   ${�False��6*   �� 5"   
   -   5        $ NameTextBox��5"      $
��Left=�6'   $��
617,93,0,0q�5#      $	��Top=�6&   $	��160��5$      $��22��6   �� 5%   
   -   5        	 ��Name$	 SexComboBox��5%      $
��Left=�6(   $��617,134,0,0q�5&      $	��Top=�6(   $	��160��6@   .@�5'      �� $
��Male��6   �� 5(      $��Female��6   �� 5)      $��Other��6   5*      (  "System.Windows.Controls.DatePicker  5+   
   -   5        
  Name$
 DOBDatePicker��5,      $
��Left=�6(   $��617,177,0,0q�5-      $	��Top=�6&   $	��160��6?   $��22��5.      �� 5/   
   -	   5        $��Insert��5/      $
��Left=�6"   $��540,214,0,0q�50      $	��Top=�6&   $��74��51       J� RenderTransformOrigin$ 0.349,0.633&�52      �� 53   
   -
   5        $��Update��53      $
��Left=�6"   $��629,214,0,0q�54      $	��Top=�6&   $��75��55      6   �� 56   
   -   5        $��Delete��56      $
��Left=�6"   $��721,214,0,0q�57      $	��Top=�6&   $��75��58      $ 0.847,0.538&�59      �� 5:   
   $��ID��6   $
��Left=�6   $��
562,48,0,0q�5;      $	��Top=�6$   $��30��6<   �� 5<   
   $
��Name��6   $
��Left=�6   $��
554,93,0,0q�5=      $	��Top=�6$   �� 5>   
   $	��DOB��6   $
��Left=�6   $��554,175,0,0q�5?      $	��Top=�6%   �� 5@   
   $	��Sex��6   $
��Left=�6   $��554,134,0,0q�5A      $	��Top=�6%   5B      5C      