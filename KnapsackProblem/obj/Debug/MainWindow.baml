   M S B A M L   `   `   `  ����J  FKnapsackProblem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:KnapsackProblemKnapsackProblem  5   	   X TXceed.Wpf.Toolkit, Version=2.9.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4P:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.ToolkitXceed.Wpf.Toolkit 5   	        KnapsackProblem.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:KnapsackProblem     5   	   NwpfTool:clr-namespace:Xceed.Wpf.Toolkit;assembly=Xceed.Wpf.Toolkit     5   	     � Title$  Problema da Mochila��5	   	   $	��400��6%   $	��525��62    � 
ResizeMode$ NoResize=�6>    � WindowStartupLocation$ CenterScreen=�6T   .��5
      � -   5        .+�5   
   �� -   5         ��Name$ labelPesoMaxMochila��5      $��Peso máximo da mochila:��6-   $
��Left=�6O   $��	28,39,0,0q�6j   $	��Top=�6}   �� 5   
   -   5         ��Name$ sliderPesoMaxMochila��5      $��
177,44,0,0q�6/   $
��Left=�6C   $	��202��6^   $��24��6j   $	��Top=�6v    �� TickPlacement$ BottomRight=�6�    �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q�5       � Minimum$ 15Q�5      	 � Maximum$	 40Q�5      %  Xceed.Wpf.Toolkit.IntegerUpDown  5   
   -   5        
  Name$
 numPesoMaxMochila��5       �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ N0��6;   $��397,38,60.4,0q�6M   $��24��6d   $	��Top=�6p   6�   �� 5   
   -   5        $ label4��5      $��Kg��6    $
��Left=�6-   $��
450,39,0,0q�6H   $	��Top=�6\   �� 5   
   -   5        $ labelPesoObj��5      $��Peso objeto:��6&   $
��Left=�6=   $��	28,86,0,0q�6X   $	��Top=�6k   #  Xceed.Wpf.Toolkit.RangeSlider  5   
   -   5          Name$ sliderPesoObj��5      $��108,91,122.4,0q�65   $��32��6M   $	��Top=�6Y      Step$	 0.1Q�5         Minimum$	 1.0Q�5         Maximum$	 5.0Q�5      �� 5   
   -   5        $��labelNumPesoObj��5      $��min - max (Kg)��6(   $
��Left=�6A   $��
396,86,0,0q�6\   $	��Top=�6p   6�   �� 5   
   -	   5        $ labelValorObj��5      $��Valor objeto:��6'   $
��Left=�6?   $��
28,112,0,0q�6Z   $	��Top=�6n     5   
   -
   5        $ sliderValorObj��5      $��108,117,122.4,0q�66   $��32��6O   $	��Top=�6[   $	 0.1Q�5      $	 1.0Q�5      $
 10.0Q�5      �� 5   
   -   5        $��labelNumValorObj��5      $��min - max (R$)��6)   $
��Left=�6B   $��396,112,0,0q�6]   $	��Top=�6r   6�   �� 5!   
   -   5        $ labelNumMochila��5!      $��Número de mochilas:��6)   $
��Left=�6G   $��
28,175,0,0q�6b   $	��Top=�6v     5"   
   -   5        $
 numNumMochila��5"       $ N0��67   $��156,175,301.4,0q�6I   $��24��6b   $	��Top=�6n      	Increment$ 2X�5#      �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 16X�5$         Maximum$	 512X�5%      6I   �� 5&   
   -   5        $ label1��5&      $	��un.��6    $
��Left=�6.   $��211,175,0,0q�6I   $	��Top=�6^   �� 5(   
   -   5        $ labelTxCruzamento��5(      $��Taxa de cruzamentos:��6+   $
��Left=�6J   $��
28,201,0,0q�6e   $	��Top=�6y     5)   
   -   5        $
 numTxCruzamento��5)       $ N0��69   $��156,203,301.4,0q�6K   $��24��6d   $	��Top=�6p   $ 1X�5*      $ 64X�5+      $
 4096X�5,      �� 5-   
   -   5        $ label��5-      $��un��6   $
��Left=�6,   $��211,201,0,0q�6G   $	��Top=�6\   �� 5/   
   -   5        $ labelIntGeracao��5/      $��Intervalo de geração:��6)   $
��Left=�6I   $��276,175,0,0q�6d   $	��Top=�6y     50   
   -   5        $
 numIntGeracao��50       $ N0��67   $��397,175,60.4,0q�6I   $��24��6a   $	��Top=�6m   $ 1X�51      $ 2X�52      �� 53   
   -   5        $ label2��53      $	��un.��6    $
��Left=�6.   $��451,175,0,0q�6I   $	��Top=�6^   �� 55   
   -   5        $ labelTxMutacao��55      $��Taxa de mutação:��6(   $
��Left=�6C   $��276,201,0,0q�6^   $	��Top=�6s   %  Xceed.Wpf.Toolkit.DecimalUpDown  56   
   -   5          Name$ numTxMutacao��56       �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ N0��66   $��397,203,60.4,0q�6H   $��24��6`   $	��Top=�6l      	Increment$ 1X�57      �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Decimal, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 1X�58         Maximum$ 20X�59      �� 5:   
   -   5        $ label3��5:      $��%��6    $
��Left=�6,   $��452,201,0,0q�6G   $	��Top=�6\   �� 5<   
   -   5        $ labelLimiar��5<      $��Limiar:��6%   $
��Left=�67   $��370,267,0,0q�6R   $	��Top=�6g     5=   
   -   5        $
 	numLimiar��5=       $��413,268,53.4,0q�63   $��24��6K   $	��Top=�6W   $ 1X�5>      $ 1X�5?      $	 100X�5@      �� 5A   
   -   5        $ labelPercLimiar��5A      $��%��6)   $
��Left=�65   $��456,267,0,0q�6P   $	��Top=�6e   $��24��6}   �� 5C   
   -   5         ��Name$ buttonResetaDados��5C      $��Resetar dados��6,   $
��Left=�6D   $��
28,318,0,0q�6_   $	��Top=�6s   $	��123��6�   $��32��6�   ���	#FFEDEDED6�   �� 5D   
   -   5        $ buttonProcessar��5D      $��	Processar��6*   $��0,318,28.4,0q�6>   $	��Top=�6T   $��32��6l   $��Right=�6x   $	��123��6�   ���	#FFA5EDBA6�   5F      5G      