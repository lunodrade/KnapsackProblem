using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KnapsackProblem
{
    /// <summary>
    /// Interaction logic for KnapsackWindow.xaml
    /// </summary>
    public partial class KnapsackWindow : Window
    {
        public KnapsackWindow()
        {
            InitializeComponent();
        }

        private void button0_Click(object sender, RoutedEventArgs e)
        {
            if (button0.IsChecked == false)
                grid1.ColumnDefinitions[0].Width = new GridLength(0);
            else
                grid1.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (button1.IsChecked == false)
                grid1.ColumnDefinitions[1].Width = new GridLength(0);
            else
                grid1.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (button2.IsChecked == false)
                grid1.ColumnDefinitions[2].Width = new GridLength(0);
            else
                grid1.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
        }
    }
}
