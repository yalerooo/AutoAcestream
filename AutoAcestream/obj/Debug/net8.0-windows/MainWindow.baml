   M S B A M L   `   `   `  ����  AutoAcestream     AceStreamPlayer.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Auto Acestream��5   	   $	��450��6    $	��800��6-   ���#121212 � 	Resources 5       �� 
TargetType   RoundedTextBoxStyle �� Property �� Value �� 
TargetType �� CornerRadius ��Name  ChannelButtonStyle �� RelativeSource��%�  &	        &	     �� 5	   
   " ��6   .�5
      ��  � �� �#FFFFFF6+   �� 5       � �� �#CCCCCC6,   �� 5       � ��$ 1q�60   �� 5       � ��$ 10q�6(   �� 5       � ��$ 14(�6)   �� 5       � ��$ 40��6'   �� 5       � ��$ 0,0,10,0q�6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 20��5   !   ._�5      �� $ PART_ContentHost��6+   5      5      5      5      �� 5    
   " ��6   .�5!      ��  � ��$	 100��6&   �� 5"       � ��$	 100��6'   �� 5#       � ��$ 5q�6'   �� 5$       � ��$ 0q�6(   �� 5%       � ��
 ���� 6+   �� 5&       � ��$ 0q�60   �� 5'       � �� 5(      �� 5)      " ��6&   .R�5*      �� ���#FFFFFF���#FFFFFF$��2q�5,   !   $ 15��5-   !   ._�5.      � .+�50   "   �� $ 10��6)   ���#FFFFFF$��5q�6P   ._�51   &   �� $��UniformToFill=�52   ,   $
}�True��53   ,   ��51   ,   �� *	Content+ �� *TemplatedParent+53   ,   54   #   55      56      57      59      5:      5;      .��5=      � 	 � RowDefinitions	 5?   
   �� 5@      $
��Auto �6   �� 5A      $��* �6   .+�5E   
   � $��10q�6   $��0��6   
 � ColumnDefinitions
 5F      �� 5G      $��* �6#   �� 5H      $
��Auto �6#   .+�5K      �� $��0��6   $ 20��6%   ���#CCCCCC$��1q�6M   ���#FFFFFF$��0,0,10,0q�6v   ._�5L      � $��0,0,20,0q�6   
 5M      �� 5N      $��* �6+   �� 5O      $
��Auto �6+   .+�5Q      �� -   5         ��Name$ 	searchBox��5Q      #��[  5R      $��Center=�5T      $��0��5U      $��2��6.   $��10,10,10,10q�6B   $��	0,0,-22,0q�6X   5V      5W      �� 5Z       �� Orientation$ 
Horizontal=�6   $��Right=�62   $��1��6N   .+�5[      �� -   5        $��settingsButton��5[      $��5q�6L   
������ 
������ .��5\      �� $>��8https://img.icons8.com/m_rounded/200/FFFFFF/settings.png��6   $��22��6^   $��22��6i   5]      �� 5_      -   5         ��Name$ ReloadButton��5_      $��5q�6R   
������ 
���   �.��5`      �� $ 
Horizontal=�6!   .+�5a      �� $9��3https://img.icons8.com/ios11/512/FFFFFF/refresh.png��6    $��24��6]   $��24��6h   $��0,0,5,0q�6t   5b      5c      �� 5e      -   5        $}��whttps://wikis.tid.es/gvp-public/images/thumb/9/9f/Infobox_info_icon_white.svg.png/600px-Infobox_info_icon_white.svg.png��5e      $��24��5f      $��24��6#    � Cursor$
 Hand��5g       � ToolTip$� �Para utilizar este programa, necesitas tener instalado VLC y Ace Stream.

Descargar VLC: https://www.videolan.org/vlc/
Descargar Ace Stream: https://download.acestream.media/products/acestream-full/win/latest��5i      5j      5k      �� 5n   
   $��10q�6   $��1��6#   .��5o      � -   5         �Name$ channelsPanel��5o      .+�5q      �� #��[ 6   $,��&https://path_to_your_channel_image.png��6E   �� 5r      #��[ 6   $/��)https://path_to_another_channel_image.png��6E   5t      5u      5v      5w      