   M S B A M L   `   `   `  ���� Z  Vprojekt_akademia_file_destroyer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:projekt_akademia_file_destroyerprojekt_akademia_file_destroyer  0    *projekt_akademia_file_destroyer.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     =src-clr-namespace:projekt_akademia_file_destroyer       � Title$  File Destroyer��$	��447��$	��611�� � 	Resources    FindCriteria S� 
ObjectType9   3projekt_akademia_file_destroyer.RadioButtonsOptionsA   ;projekt_akademia_file_destroyer.RadioButtonCheckedConverter  RadioButtonCheckedConverter��%#   &	        &	 
     S� "    .��� .+�&   System.Windows.Controls.DataGrid  -   $��dataGrid��$��180,10,10,12q�   CanUserAddRows .     
IsReadOnly .    AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *id+  $
 File��$ * 	 �� *
fileName+  $ Status��$ * 	 �� *status+
   RowDetailsTemplate
 �� .R��� $��10q����� *fileDetails+�� -   $��	SE_button��$��Select Files��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��165��$��22���� $��Erase Method��$
��Left=�$��	10,37,0,0q�$	��Top=�$	��126��$	��165��.���� $��0,0,0,1q�.+�	�  	� 	GroupName$ 	EnumGroup��$	��0,5q� d� 	IsChecked �� *EnumProperty+ �� 	Converter# [ 0   *projekt_akademia_file_destroyer.MethodEnum   Random �� ConverterParameter# Z .���� $
��Wrap=�$��Random��	� $ 	EnumGroup��$	��0,5q� �� *EnumProperty+# [    Zeros# Z .���� $
��Wrap=�$��Zeros (only)��	� $ 	EnumGroup��$	��0,5q� �� *EnumProperty+# [    Ones# Z .���� $
��Wrap=�$��Ones (only)��	� $ 	EnumGroup��$	��0,5q� �� *EnumProperty+# [    	ZerosOnes# Z .���� $
��Wrap=�$��Zeros and Ones���� -   $��Delete Files ...��$
��Left=�$��
10,299,0,0q�$	��Top=�$	��165���� -   $��Exclude File��$
��Left=�$��
10,272,0,0q�$	��Top=�$	��165��� .+�� -   $
��pBar��$
��Left=�$��26��$��
10,334,0,0q�$	��Top=�$	��165���� $��
78,334,0,0q�$��16(�$
��Left=�$��97��$��26��$	��Top=�����  �� ElementName$
 pBar�� �� Path$ Value� �� StringFormat$ {0:0}%���� -   $��About��$��100,376,0,0q�$��22��$	��Top=�$
��Left=�$��75���� $��
Fast Erase��$
��Left=�$��
10,168,0,0q�$	��Top=�$��88��$	��165��.���� $��0,0,0,1q�.+�	� $ BooleanGroup��$	��0,5q� �� *BooleanProperty+# [ 0	   *projekt_akademia_file_destroyer.MainWindow 	  BooleanTrue# Z .���� $
��Wrap=�$��Enabled��	� $ BooleanGroup��$	��0,5q� �� *BooleanProperty+# [  	  BooleanFalse# Z .���� $
��Wrap=�$��Disabled���� $
��Wrap=�$��For large files use only.��$	��Top=�$��18��$��9(�$��Italic$�$��10,0,0,0q�