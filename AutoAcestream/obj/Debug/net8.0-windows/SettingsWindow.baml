   M S B A M L   `   `   `  ����  AutoAcestream     TuNamespace.SettingsWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Ajustes��5   	   $	��400��6   $	��400��6&   ���#121212.��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Ruta de VLC��6   
�������$��0��6:   $��0,0,0,5q�6G   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� -   5         ��Name$ vlcPathInput��5      $��0��6,   $��0,0,10,0q�6<   �� 5      -   5         ��Name$ browseButton��5      $��Examinar��6+   $��1��6>   6N   5      �� 5   
   $��Selección de URL��6   
�������$��2��6?   $��0,20,0,5q�6L   �� 5   
   $��3��6    �� Orientation$ Vertical=�6"   .+�5      	� -   5         	�Name$ 	radioUrl1��5      $'��!Usar URL 1 (Lista predeterminada)��6-   $	��0,5q�6Y   
�������6f   	� 5      -   5        $ 	radioUrl2��5      $$��Usar URL 2 (Lista alternativa)��6-   $	��0,5q�6V   
�������6c   5      �� 5!   
   $"��URL Personalizada (opcional)��6   
�������$��4��6K   $��0,20,0,5q�6X   �� 5"   
   -   5        $ customUrlInput��5"      $��5��6*   $��0,0,0,10q�67   �� 5$   
   -   5        $ 
saveButton��5$      $��Guardar��6%   $��6��67   $��0,20,0,0q�6D   
�������$
��10,5q�6�   ��5%      �� 5&       �� 
TargetType" ��6"   .R�5'      ��  �� CornerRadius$ 10��6   $��1q�6/   
�������#��z��6V   ._�5(      �� $��Center=�6+   $��Center=�6H   #��z��6c   5)      5*      5,      5/      50      