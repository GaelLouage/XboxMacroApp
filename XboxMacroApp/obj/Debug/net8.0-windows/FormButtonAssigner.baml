   M S B A M L   `   `   `  ����G  CXboxMacroApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:XboxMacroAppXboxMacroApp  5   	   %    XboxMacroApp.FormButtonAssigner   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:XboxMacroApp     5   	   -   5          � Title$  MacroX Launcher��5   	   $	��500��6!   $	��400��6.   ���	#FF192838 � 
ResizeMode$ NoResize=�6Q    � WindowStyle$
 None=�6g   .��5	      � -   5         � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ 	imgGoBack��5      $��35��6W   $��35��6b   $	��Top=�6n   $
��Left=�6�   $��10q�6�    � Cursor$
 Hand��6�   $��0��6�   �� 5   
    �� Orientation$ 
Horizontal=�6   $��0��6.   $��5��6;   $	��Top=�6L   $��Right=�6d   $��	10,0,10,0q�6�   .+�5      �� -   5         ��Name$ txtMinimize��5      $��-��6-   
�������$��40(�6I   $
��Bold"�6W   $��0��6i   $��5��6v   $��Center=�6�   $��	0,-10,5,0q�6�   $��Center=�6�   $
 Hand��6�   6�   �� 5      -   5        $ txtClose��5      $��X��6*   
�������$��15(�6F   $
��Bold"�6U   $��Center=�6g   $��Right=�6�   $
 Hand��6�   6�   5      �� 5   
   -   5        $ txtProgramName��5      $��0��6,   $��2��69   $��0��6M   $��10q�6]   
�������$��24(�6|   $��Center=�6�   $��Center=�6�   �� 5   
   -   5        	 ��Name$	 
cmbButtons��5      $��0��6'   $��1��67   $��2��6D   $��10q�6X   $
 Hand��6d   $��40��6r   6~   �� 5   
   -   5        
 ��Name$
 btnAssignKey��5      $��
Assign Key��6'   $��0��6<   $��2��6L   $��3��6Y   $��10,0,10,-50q�6m   $
 Hand��6�   $��40��6�   �� 5   
   -	   5        $
 btnCloseKey��5      $��Close��6&   $��0��66   $��3��6F   $��3��6S   $��	10,0,10,0q�6g   $
 Hand��6{   $��40��6�   6�   5      5       