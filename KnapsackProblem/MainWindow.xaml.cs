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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KnapsackProblem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sliderPesoObj_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            if (labelNumPesoObj != null)
                setNumPesoObj();
        }

        private void sliderPesoObj_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            if (labelNumPesoObj != null)
                setNumPesoObj();
        }

        private void setNumPesoObj()
        {
            Double numMin = sliderPesoObj.LowerValue;
            Double numMax = sliderPesoObj.HigherValue;

            String min = numMin.ToString();
            try {
                min = numMin.ToString().Substring(0, 3);
            } catch { }

            String max = numMax.ToString();
            try {
                max = numMax.ToString().Substring(0, 3);
            } catch { }

            labelNumPesoObj.Content = min + " - " + max;
        }

        private void labelNumPesoObj_Loaded(object sender, RoutedEventArgs e)
        {
            setNumPesoObj();
        }
    }
}
