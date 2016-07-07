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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            sliderPesoMaxMochila.Value = 28;
            sliderPesoObj.LowerValue = 1.7;
            sliderPesoObj.HigherValue = 4.2;
            sliderValorObj.LowerValue = 4.7;
            sliderValorObj.HigherValue = 7.4;
            numNumMochila.Value = 32;
            numTxCruzamento.Value = 256;
            numIntGeracao.Value = 10;
            numIntGeracao.Maximum = (int)Math.Floor(((decimal)numNumMochila.Value) / 2);
            numTxMutacao.Value = 10;
            numLimiar.Value = 70;
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
            string numMin = sliderPesoObj.LowerValue.ToString();
            string numMax = sliderPesoObj.HigherValue.ToString();

            String min = numMin.Substring(0, Math.Min(3, numMin.Length));
            String max = numMax.Substring(0, Math.Min(3, numMax.Length));

            labelNumPesoObj.Content = min + " - " + max + " (Kg)";
        }

        private void labelNumPesoObj_Loaded(object sender, RoutedEventArgs e)
        {
            if (labelNumPesoObj != null)
                setNumPesoObj();
        }

        private void sliderValorObj_HigherValueChanged(object sender, RoutedEventArgs e)
        {
            if (labelNumValorObj != null)
                setNumValorObj();
        }

        private void sliderValorObj_LowerValueChanged(object sender, RoutedEventArgs e)
        {
            if (labelNumValorObj != null)
                setNumValorObj();
        }

        private void setNumValorObj()
        {
            string numMin = sliderValorObj.LowerValue.ToString();
            string numMax = sliderValorObj.HigherValue.ToString();

            String min = numMin.Substring(0, Math.Min(3, numMin.Length));
            String max = numMax.Substring(0, Math.Min(3, numMax.Length));

            labelNumValorObj.Content = min + " - " + max + " (R$)";
        }

        private void labelNumValorObj_Loaded(object sender, RoutedEventArgs e)
        {
            setNumValorObj();
        }

        private void sliderPesoMaxMochila_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (numPesoMaxMochila != null)
                numPesoMaxMochila.Value = (int)sliderPesoMaxMochila.Value;
        }

        private void numPesoMaxMochila_Loaded(object sender, RoutedEventArgs e)
        {
            numPesoMaxMochila.Value = (int)sliderPesoMaxMochila.Value;
            numPesoMaxMochila.Increment = (int)sliderPesoMaxMochila.TickFrequency;
            numPesoMaxMochila.Minimum = (int)sliderPesoMaxMochila.Minimum;
            numPesoMaxMochila.Maximum = (int)sliderPesoMaxMochila.Maximum;
        }

        private void numPesoMaxMochila_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sliderPesoMaxMochila != null)
                sliderPesoMaxMochila.Value = (double)numPesoMaxMochila.Value;
        }

        private void buttonResetaDados_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(
                "Tem certeza que deseja resetar os dados?", "Resetar dados", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
                Grid_Loaded(sender, e);
        }

        private void buttonProcessar_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>()
            {
                {"pesoMaxMochila", sliderPesoMaxMochila.Value.ToString()},
                {"pesoMaxObjeto", sliderPesoObj.HigherValue.ToString()},
                {"pesoMinObjeto", sliderPesoObj.LowerValue.ToString()},
                {"valorMaxObjeto", sliderValorObj.HigherValue.ToString()},
                {"valorMinObjeto", sliderValorObj.LowerValue.ToString()},
                {"numMochila", numNumMochila.Value.ToString()},
                {"numTxCruzamento", numTxCruzamento.Value.ToString()},
                {"numIntGeracao", numIntGeracao.Value.ToString()},
                {"numTxMutacao", numTxMutacao.Value.ToString()},
                {"numLimiar", numLimiar.Value.ToString()}
            };
            KnapsackWindow window = new KnapsackWindow(dict);
            window.ShowDialog();         //Permite só uma instância da janela resultado
            //window.Show();               //Permite várias instâncias da janela resultado
        }

        private void numNumMochila_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
        }

        private void numNumMochila_LostFocus(object sender, RoutedEventArgs e)
        {
            if (numIntGeracao != null)
            {
                numIntGeracao.Maximum = (int)Math.Floor(((decimal)numNumMochila.Value) / 2);
                if (numIntGeracao.Value > numIntGeracao.Maximum)
                    numIntGeracao.Value = numIntGeracao.Maximum;
            }
        }
    }
}
