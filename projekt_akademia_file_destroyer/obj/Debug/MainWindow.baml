   M S B A M L   `   `   `  ����Z  Vprojekt_akademia_file_destroyer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:projekt_akademia_file_destroyerprojekt_akademia_file_destroyer  5   	   0    *projekt_akademia_file_destroyer.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   =src-clr-namespace:projekt_akademia_file_destroyer     5   	     � Title$  File Destroyer��5   	   $	��447��6    $	��611��6-    � 	Resources 5         FindCriteria S� 
ObjectType9   3projekt_akademia_file_destroyer.RadioButtonsOptionsA   ;projekt_akademia_file_destroyer.RadioButtonCheckedConverter  RadioButtonCheckedConverter��%?   &	        &	      S� 5	   
   "  62     5
   
   6*   5      .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��dataGrid��5      $��180,10,10,12q�5         CanUserAddRows .     
IsReadOnly .    AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6H   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 61   �� *id+6H     5      $
 File��6%   $ * 6P   	 63   �� *
fileName+6P     5      $ Status��6%   $ * 6P   	 65   �� *status+6P   
   RowDetailsTemplate
 5      �� 5      .R�5       �� $��10q�6=   ��6    �� *fileDetails+6=   5!      5#      �� 5'   
   -   5        $��	SE_button��5'      $��Select Files��5(      $
��Left=�5)      $��	10,10,0,0q�5*      $	��Top=�5+      $	��165��5,      $��22��5-      5.      �� 5/   
   $��Erase Method��6   $
��Left=�50      $��	10,37,0,0q�51      $	��Top=�52      $	��126��53      $	��165��54      .��5:      �� $��0,0,0,1q�6   .+�5<      	�  	� 	GroupName$ 	EnumGroup��6   $	��0,5q�64    d� 	IsChecked 5=      �� *EnumProperty+ �� 	Converter# [ 0   *projekt_akademia_file_destroyer.MethodEnum   Random �� ConverterParameter# Z .��5?      �� $
��Wrap=�6    $��Random��64   5@      	� 5B      $ 	EnumGroup��6   $	��0,5q�65    5C      �� *EnumProperty+# [    Zeros# Z .��5E      �� $
��Wrap=�6    $��Zeros (only)��64   5F      	� 5H      $ 	EnumGroup��6   $	��0,5q�64    5I      �� *EnumProperty+# [    Ones# Z .��5K      �� $
��Wrap=�6    $��Ones (only)��64   5L      	� 5N      $ 	EnumGroup��6   $	��0,5q�64    5O      �� *EnumProperty+# [    	ZerosOnes# Z .��5Q      �� $
��Wrap=�6    $��Zeros and Ones��64   5R      5S      5U      �� 5X   
   -   5        $��Delete Files ...��5X      $
��Left=�5Y      $��
10,299,0,0q�5Z      $	��Top=�5[      $	��165��5\      6   �� 5]   
   -   5        $��Exclude File��5]      $
��Left=�5^      $��
10,272,0,0q�5_      $	��Top=�5`      $	��165��5a      5b      � 5c   
   .+�5d      � -   5        $
��pBar��5d      $
��Left=�6&   $��26��5e      $��
10,334,0,0q�5f      $	��Top=�5g      $	��165��5h      6"   �� 5i      $��
78,334,0,0q�6]   $��16(�6q   $
��Left=�6   $��97��6�   $��26��6�   $	��Top=�6�   ��6   ��  �� ElementName$
 pBar�� �� Path$ Value� �� StringFormat$ {0:0}%��6�   �� 5j      -   5        $��About��5j      $��100,376,0,0q�6%   $��22��6:   $	��Top=�6F   $
��Left=�6^   $��75��6y   6�   5k      �� 5l   
   $��
Fast Erase��6   $
��Left=�5m      $��
10,168,0,0q�5n      $	��Top=�5o      $��88��5p      $	��165��5q      .��5s      �� $��0,0,0,1q�6   .+�5t      	� $ BooleanGroup��6   $	��0,5q�67    5u      �� *BooleanProperty+# [ 0	   *projekt_akademia_file_destroyer.MainWindow 	  BooleanTrue# Z .��5w      �� $
��Wrap=�6    $��Enabled��64   5x      	� 5z      $ BooleanGroup��6   $	��0,5q�67    5{      �� *BooleanProperty+# [  	  BooleanFalse# Z .��5}      �� $
��Wrap=�6    $��Disabled��64   5~      �� 5      $
��Wrap=�6   $��For large files use only.��60   $	��Top=�6Q   $��18��6i   $��9(�6u   $��Italic$�6�   $��10,0,0,0q�6�   5�      5�      5�      5�      