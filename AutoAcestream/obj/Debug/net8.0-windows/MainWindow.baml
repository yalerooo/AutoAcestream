   M S B A M L   `   `   `  ����H  DAutoAcestream, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AceStreamPlayerAceStreamPlayer  5   	        AceStreamPlayer.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   /localclr-namespace:AceStreamPlayer     5   	     � Title$  Auto Acestream��5   	   $	��450��6    $	��800��6-   ���#121212 � 	Resources 5       �� 
TargetType   RoundedTextBoxStyle �� Property �� Value �� 
TargetType �� CornerRadius ��Name  ChannelButtonStyle��%�  &	        '��     '� �    &	 =    �� 5   
   " ��6   .�5      ��  � �� �#FFFFFF6+   �� 5       � �� �#CCCCCC6,   �� 5       � ��$ 1q�60   �� 5       � ��$ 10q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ 40��6'   �� 5       � ��$ 0,0,10,0q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 20��5   !   ._�5      �� $ PART_ContentHost��6+   5      5      5      5      �� 5"   
   " ��6   .�5#      ��  � ��$ 60��6&   �� 5$       � ��$ 60��6'   �� 5%       � ��$ Uniform=�6(   5&      �� 5)   
   " �6   .�5*      ��  �@�Orientation$ 
Horizontal=�6,   �� 5+       � ��$ 10q�6'   �� 5,       � ��$	 Top=�62   5-      �� 50   
   " ��6   .�51      ��  � ��$	 100��6&   �� 52       � ��$	 100��6'   �� 53       � ��$ 5q�6'   �� 54       � �� �#FFFFFF6+   �� 55       � �� �#CCCCCC6,   �� 56       � ��$ 1q�60   �� 57       � ��$ 10q�6(   �� 58       � ��$	 Top=�69   �� 59       � ��$ Center=�6;   5:      5;      .��5=      �  � RowDefinitions 5>   
   �� 5?      $
��Auto �6   �� 5@      $��* �6   .+�5C   
   � $��10q�6   $��0��6   	 � ColumnDefinitions	 5D      �� 5E      $��* �6#   �� 5F      $
��Auto �6#   .+�5J      �� $��0��6   $ 20��6%   ���#CCCCCC$��1q�6M   ���#FFFFFF$��0,0,10,0q�6v   ._�5K      � $��0,0,20,0q�6   	 5L      �� 5M      $��* �6+   �� 5N      $
��Auto �6+   .+�5P      �� -   5        
 ��Name$
 	searchBox��5P      #��[  5Q      $��Center=�5S      $��0��5T      $��2��6.   $��10,10,10,10q�6B   $��	0,0,-22,0q�6X   5U      5V      �� 5Y      $��0��6   $�� ��6(   $��	10,0,10,0q�5[      $��Center=�5\      
������� J� IsHitTestVisible$ False��5^      $
��Left=�5_      $y�Visible=�5`      �� 5d       �� Orientation$ 
Horizontal=�6   $��Right=�62   $��1��6N   .+�5e      �� -   5        $��settingsButton��5e      $��5q�6L   
������ 
������ .��5f      �� $>��8https://img.icons8.com/m_rounded/200/FFFFFF/settings.png��6   $��22��6^   $��22��6i   5g      �� 5i      -   5         ��Name$ ReloadButton��5i      $��5q�6R   
������ 
���   �.��5j      �� $ 
Horizontal=�6!   .+�5k      �� $9��3https://img.icons8.com/ios11/512/FFFFFF/refresh.png��6    $��24��6]   $��24��6h   $��0,0,5,0q�6t   5l      5m      �� 5o      -   5        $}��whttps://wikis.tid.es/gvp-public/images/thumb/9/9f/Infobox_info_icon_white.svg.png/600px-Infobox_info_icon_white.svg.png��5o      $��24��5p      $��24��6#    � Cursor$
 Hand��5q       � ToolTip$� �Para utilizar este programa, necesitas tener instalado VLC y Ace Stream.

Descargar VLC: https://www.videolan.org/vlc/
Descargar Ace Stream: https://download.acestream.media/products/acestream-full/win/latest��5s      5t      5u      �� 5x   
   $��10q�6   $��1��6#   .��5y      � -   5         �Name$ channelsPanel��5y      5|      5}      5~      5      