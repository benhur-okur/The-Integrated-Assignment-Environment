   M S B A M L   `   `   `  ���� )  %The-Integrated-Assignment-EnvironmentB    <The_Integrated_Assignment_Environment.CreateAssignmentWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Create Assignment��$	��600��$
��1000�����#f5f7fa � WindowStartupLocation$ CenterScreen=� � Icon$ Resources/logo.ico�� � 	Resources    SidebarButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 �� Triggers
 P� Property P� Value  SoftTextBoxStyle ��Name  SoftButtonStyle  GraySoftButtonStyle ��Name �� Triggers �� 
TargetName��%�  &	        &	 �    &	 �    &	 �    �� " ��.���  � ��
 �������  � ��
 ���� ��  � ��
 ���� ��  � ��$ 16(���  � ��$
 10,5q���  � ��$ 10q���  � ��$
 Left=���  �@�Cursor$
 Hand����  � �� �� " ��.R��� #��z��#��z��$��0q�$ 5��._��� $
��Left=�$��Center=�$
��10,0q�	 P� 
 �@J�IsMouseOver$
 True��. ���  � �� �#3c5978�� " ��.���  � ��
 �������  � ��
 �   ���  � �� �#ccc��  � ��$ 1q���  � ��$ 14(���  � ��$ 35����  � ��$ Center=���  � �� �� " ��.R��� #��z��#��z��#��z��$ 6��._��� $ PART_ContentHost��$��0q��� " ��.���  � �� �#2C3E50��  � ��
 �������  � ��$ 14(���  � ��$ 35����  � �� �#2C3E50��  � ��$ 0q���  �@�Cursor$
 Hand����  � �� �� " ��.R��� #��z��#��z��#��z��$ 6��._��� $��Center=�$��Center=��� " ��.���  � �� �#dddddd��  � ��
 �   ���  � ��$ 14(���  � ��$ 35����  � ��
 ���� ��  � ��$ 0q���  � ��$ Center=���  � ��$
 10,0q���  �@�Cursor$
 Hand����  � �� �� " ��.R��� $ border��#��z��#��z��#��z��$ 6��._��� #��z��$��Center=� P� 
 �@J�IsMouseOver$
 True��. ��� $ border�� � �� �#c0c0c0.���  � ColumnDefinitions �� $	��220 ��� $��* �.+��� ���#2d3e50$��0��.+��� -    ��Name$ btnBack��  AnimatedBackButtonStyle#��[ $
��Left=�$	��Top=�$��10q� � ToolTip$ Go Back��.���� $��Resources/back.png���� -   $��Configurations��#��[  �� $��10q��� $��Open Assignment��#��[  �� $��Manage Assignments��#��[  �� $��10q��� $
��Help��#��[  �� $��1��$��30q�.���� $	��600��$
��Left=�.+��� $��Create New Assignment��$��28(�$
��Bold"�$��0,0,0,20q��� $��0,0,0,15q�.+��� $��Assignment Name:��$��14(�$��0,0,0,5q��� -    ��Name$ txtAssignmentName��$��35��#��[ �� $��0,0,0,15q�.+��� $��Select Configuration:��$��14(�$��0,0,0,5q��� -    ��Name$ cmbConfiguration��$��35�� �� DisplayMemberPath$ LanguageName���� $��0,0,0,15q� �� Orientation$ 
Horizontal=�$��Center=�.+��� .+��� $ ��Source Folder (ZIP Files):��$��14(�$��0,0,0,5q��� -   $��Select Folder��$��35��$	��180��#��[ $
��Left=��� -    ��Name$ lblSelectedFolderPath��$��Selected Folder Path:��$��20,0,0,0q�$��Center=�$	��300��$
��Left=�$��Center=�$��5q��� $��0,0,0,15q�$ 
Horizontal=�$��Center=�.+��� .+��� $��Expected Output File:��$��14(�$��0,0,0,5q��� -   $��Select File��$��35��$	��180��#��[ $
��Left=��� -   $ lblExpectedOutputPath��$��Selected Output File:��$��20,0,0,0q�$��Center=�$	��300��$
��Left=�$��Center=�$��5q��� $��0,0,0,15q�.+��� $��Arguments to Main:��$��14(�$��0,0,0,5q��� -	   $ txtArguments��$��35��#��[ �� -
   $��Save Assignment��$	��200��$��40��#��[ 