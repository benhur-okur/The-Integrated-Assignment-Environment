   M S B A M L   `   `   `  ����)  %The-Integrated-Assignment-Environment@    :The_Integrated_Assignment_Environment.OpenAssignmentWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Open Assignment��5   	   $	��720��5   	   $
��1080��6   ���#f5f7fa � WindowStartupLocation$ CenterScreen=�5   	    � Icon$ Resources/logo.ico��5   	    � 	Resources 5
       �� 
TargetType   SoftButtonStyle �� Property �� Value �� 
TargetType �� CornerRadius  ProjectCardTemplate	 �� Padding
 �� StringFormat��%�  &	        &	     �� 5   
   " ��6   .�5      ��  � �� �#2C3E506+   �� 5       � ��
 �����6+   �� 5       � ��$ 14(�6)   �� 5       � ��$ 35��6'   �� 5       � �� �#2C3E506,   �� 5       � ��$ 0q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5   !   #��z��5   !   $ 6��5   !   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      5      �� 5!   
   .R�5"      �� ���#ccc$��1q�6(   $ 8��6<   
�������$��5q�6`   $	 10q�6k   ._�5#      �� .+�5$      �� $��18(�6=   $
��Bold"�6K   ���#2C3E50��6    �� *ProjectName+6]   �� 5%      $��0,4,0,0q�6h   ��6    �� *Configuration.LanguageName+$
 Language: {0}��6h   �� 5&      ��6    �� *Results.Count+$
 Submissions: {0}��5'      5(      5)      5*      .��5,      � $��20q�6    � RowDefinitions 5-   
   �� 5.      $
��Auto �6   �� 5/      $��* �6   �� 50      $
��Auto �6   .+�53   
   �� $"��Select an Assignment to Open��6   $��26(�54      $
��Bold"�55      $��0,0,0,10q�56      �� 58   
   $��1��6   .��59      � .+�5:      �� -   5         ��Name$ lstProjects��5:      #��[ 5;      
������ $��0q�5>      $��Stretch=�5?      �� 5B      -   5         ��Name$ txtPlaceholder��5B      $��There is nothing to show.��5C      $��16(�5D      
�������$��Center=�5F      $��Center=�5G      $y�	Collapsed=�5H      $��Italic$�5I      5J      5K      �� 5N   
    �� Orientation$ 
Horizontal=�6   $��2��6.   $��Right=�6;   $��0,20,0,0q�6W   .+�5O      �� -   5        $
��Open��5O      $	��120��6$     GraySoftButtonStyle#��[ 6F   $��5q�6s   �� 5P      -   5        $
��Back��5P      $	��120��6$   #��[ 6F   $��5q�6s   �� 5Q      -   5        $��Delete��5Q      $	��120��6&   #��[  6J   $��5q�6s   5R      5T      5U      