   M S B A M L   `   `   `  ���� H  DAutoAcestream, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AceStreamPlayerAceStreamPlayer       AceStreamPlayer.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     /localclr-namespace:AceStreamPlayer       � Title$  Auto Acestream��$	��450��$	��800�����#121212 � 	Resources  �� 
TargetType   RoundedTextBoxStyle �� Property �� Value �� 
TargetType �� CornerRadius ��Name  ChannelButtonStyle��%�  &	        '��     '� l    &	 �    �� " ��.���  � �� �#FFFFFF��  � �� �#CCCCCC��  � ��$ 1q���  � ��$ 10q���  � ��$ 14(���  � ��$ 40����  � ��$ 0,0,10,0q���  � �� �� " ��.R��� #��z��#��z��#��z��$ 20��._��� $ PART_ContentHost���� " ��.���  � ��$ 60����  � ��$ 60����  � ��$ Uniform=��� " �.���  �@�Orientation$ 
Horizontal=���  � ��$ 10q���  � ��$	 Top=��� " ��.���  � ��$	 100����  � ��$	 100����  � ��$ 5q���  � �� �#FFFFFF��  � �� �#CCCCCC��  � ��$ 1q���  � ��$ 10q���  � ��$	 Top=���  � ��$ Center=�.���  � RowDefinitions �� $
��Auto ��� $��* �.+�� $��10q�$��0��	 � ColumnDefinitions	 �� $��* ��� $
��Auto �.+��� $��0��$ 20�����#CCCCCC$��1q����#FFFFFF$��0,0,10,0q�._�� $��0,0,20,0q�	 �� $��* ��� $
��Auto �.+��� -   
 ��Name$
 	searchBox��#��[  $��Center=�$��0��$��2��$��10,10,10,10q�$��	0,0,-22,0q��� $��0��$�� ��$��	10,0,10,0q�$��Center=�
������� J� IsHitTestVisible$ False��$
��Left=�$y�Visible=���  �� Orientation$ 
Horizontal=�$��Right=�$��1��.+��� -   $��settingsButton��$��5q�
������ 
������ .���� $>��8https://img.icons8.com/m_rounded/200/FFFFFF/settings.png��$��22��$��22���� -    ��Name$ ReloadButton��$��5q�
������ 
���   �.���� $ 
Horizontal=�.+��� $9��3https://img.icons8.com/ios11/512/FFFFFF/refresh.png��$��24��$��24��$��0,0,5,0q��� -   $}��whttps://wikis.tid.es/gvp-public/images/thumb/9/9f/Infobox_info_icon_white.svg.png/600px-Infobox_info_icon_white.svg.png��$��24��$��24�� � Cursor$
 Hand�� � ToolTip$� �Para utilizar este programa, necesitas tener instalado VLC y Ace Stream.

Descargar VLC: https://www.videolan.org/vlc/
Descargar Ace Stream: https://download.acestream.media/products/acestream-full/win/latest���� $��10q�$��1��.���� ���#FFFFFF���#CCCCCC$��1q�$ 10�� �� Padding$ 10q�._�� -    �Name$ channelsPanel��$��Center=�