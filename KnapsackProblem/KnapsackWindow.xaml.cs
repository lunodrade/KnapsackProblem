using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        List<Item> itemsList = new List<Item>();
        List<Bag> bagsList = new List<Bag>();
        List<Bag> bagsInitialList = new List<Bag>();
        Dictionary<string, string> configs;
        Random rnd = new Random();
        int loopCount = 0;
        Stopwatch stopwatch = new Stopwatch();

        public KnapsackWindow(Dictionary<string, string> dict)
        {
            InitializeComponent();

            this.configs = dict;
            this.configs["numItems"] = 10.ToString();
            generateRandomItems();
            generateRandomBags();
            addItemsToView();
            bagsInitialList = bagsInitialList.OrderBy(x => x.overflow).ThenByDescending(x => x.fitness).ToList();
            addBagsInitialToView();
            stopwatch.Start();
            nextGeneration();
            stopwatch.Stop();
            bagsList = bagsList.OrderBy(x => x.overflow).ThenByDescending(x => x.fitness).ToList();
            addBagsToView();

            infoExec.Text = "Loops executados: " + loopCount + "\n" + 
                            "Tempo gasto: " + stopwatch.Elapsed.TotalSeconds;

            Button btn = BagsListInitialPanel.Children.OfType<Button>().FirstOrDefault();
            btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            btn = BagsListPanel.Children.OfType<Button>().FirstOrDefault();
            btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void nextGeneration()
        {
            for (int i = 0; i < Int16.Parse(configs["numTxCruzamento"]); i++)
            {
                if (thereValidItem())
                    break;

                if ((i % 128) == 0 && i > 127)
                    tryMoreOneTime();

                crossoverAndMutation();
                loopCount++;
                bagsList = bagsList.OrderBy(x => x.overflow).ThenByDescending(x => x.fitness).ToList();
            }
        }

        private void tryMoreOneTime()
        {
            int bfull = bagsList.Count;
            int bhalf = (int)Math.Floor((decimal.Parse(bfull.ToString()) / 2));

            for (int i = 0; i < bhalf; i++)
            {
                string chromosomes = "";
                decimal fitness = 0, peso = 0, valor = 0;

                foreach (Item item in itemsList)
                {
                    if (rnd.Next(0, 2) == 1)
                    {
                        chromosomes += "1";
                        peso += item.weight;
                        valor += item.value;
                    }
                    else
                    {
                        chromosomes += "0";
                    }
                }

                if (valor > 0)
                {
                    decimal diff = decimal.Parse(configs["valorMaxObjeto"]) - decimal.Parse(configs["valorMinObjeto"]);
                    decimal perc = 100 / (decimal.Parse(configs["valorMaxObjeto"]) * Int16.Parse(configs["numItems"]));
                    fitness = valor * perc;
                }

                bool overflow = peso > decimal.Parse(configs["pesoMaxMochila"]);

                int ind = bfull - bhalf + i;
                bagsList.RemoveAt(ind);
                bagsList.Add(new Bag(chromosomes, fitness, peso, overflow));
            }
        }

        private void crossoverAndMutation()
        {
            int cfull = bagsList.ElementAt(0).chromosomes.Length;
            int chalf = (int)Math.Floor((decimal.Parse(cfull.ToString()) / 2));
            int cthird = (int)Math.Floor((decimal.Parse(cfull.ToString()) / 3));

            int bfull = bagsList.Count;
            int bhalf = (int)Math.Floor((decimal.Parse(bfull.ToString()) / 2));

            for (int i = 0; i < Int16.Parse(configs["numIntGeracao"]); i++)
            {
                int frnd = rnd.Next(0+i, bhalf+i);
                int f2rnd = rnd.Next(0+i, bhalf+i);
                string father1 = bagsList.ElementAt(frnd).chromosomes;
                string father2 = bagsList.ElementAt(f2rnd).chromosomes;
                
                string child1 = father1.Substring(0, chalf) + father2.Substring(chalf, cfull - chalf);
                string child2 = father2.Substring(0, chalf) + father1.Substring(chalf, cfull - chalf);
                
                if (rnd.Next(0, 100) <= Int16.Parse(configs["numTxMutacao"]))
                {
                    int chromoRnd = rnd.Next(0, cfull);
                    StringBuilder sb = new StringBuilder(child2);
                    sb[chromoRnd] = child2[chromoRnd] == '1' ? '0' : '1';
                    child2 = sb.ToString();
                    if (hasChromo(sb.ToString()) == false)
                        child2 = sb.ToString();
                }

                Bag childBag1 = createBag(child1);
                int rnd1 = rnd.Next(0, bfull);
                if (hasChromo(childBag1.chromosomes) == false
                   &&
                   (
                       bagsList.ElementAt(rnd1).peso > decimal.Parse(configs["pesoMaxMochila"])
                       ||
                       (
                            (childBag1.peso < decimal.Parse(configs["pesoMaxMochila"]))
                            &&
                            (childBag1.fitness > bagsList.ElementAt(rnd1).fitness)
                       )
                   )
                   )
                {
                    bagsList.RemoveAt(rnd1);
                    bagsList.Add(childBag1);
                }

                Bag childBag2 = createBag(child1);
                int rnd2 = rnd.Next(0, bfull);
                if (hasChromo(childBag2.chromosomes) == false
                   &&
                   (
                       bagsList.ElementAt(rnd2).peso > decimal.Parse(configs["pesoMaxMochila"])
                       ||
                       (
                            (childBag2.peso < decimal.Parse(configs["pesoMaxMochila"]))
                            &&
                            (childBag2.fitness > bagsList.ElementAt(rnd2).fitness)
                       )
                   )
                   )
                {
                    bagsList.RemoveAt(rnd2);
                    bagsList.Add(childBag2);
                }
            }
        }

        private bool hasChromo(string chromosome)
        {
            bool result = false;
            foreach (Bag bag in bagsList)
            {
                if (bag.chromosomes.Equals(chromosome))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private Bag createBag(string child)
        {
            decimal fitness = 0, peso = 0, valor = 0;

            for (int i = 0; i < child.Length; i++)
            {
                if (child.ElementAt(i).Equals('1'))
                {
                    peso += itemsList.ElementAt(i).weight;
                    valor += itemsList.ElementAt(i).value;
                }
            }

            if (valor > 0)
            {
                decimal diff = decimal.Parse(configs["valorMaxObjeto"]) - decimal.Parse(configs["valorMinObjeto"]);
                decimal perc = 100 / (decimal.Parse(configs["valorMaxObjeto"]) * Int16.Parse(configs["numItems"]));
                fitness = valor * perc;
            }
            bool overflow = peso > decimal.Parse(configs["pesoMaxMochila"]);

            return new Bag(child, fitness, peso, overflow);
        }

        private bool thereValidItem()
        {
            bool response = false;

            foreach (Bag bag in bagsList)
            {
                if (bag.peso < decimal.Parse(configs["pesoMaxMochila"]) && bag.fitness >= decimal.Parse(configs["numLimiar"]))
                {
                    response = true;
                    break;
                }
            }
            return response;
        }

        private void generateRandomItems()
        {
            string speso, svalor;
            double pesoMinObjeto = Double.Parse(configs["pesoMinObjeto"]);
            double pesoMaxObjeto = Double.Parse(configs["pesoMaxObjeto"]);
            double valorMinObjeto = Double.Parse(configs["valorMinObjeto"]);
            double valorMaxObjeto = Double.Parse(configs["valorMaxObjeto"]);

            for (int i = 0; i < Int16.Parse(configs["numItems"]); i++)
            {
                speso = (rnd.NextDouble() * (pesoMaxObjeto - pesoMinObjeto) + pesoMinObjeto).ToString("#.##");
                svalor = (rnd.NextDouble() * (valorMaxObjeto - valorMinObjeto) + valorMinObjeto).ToString("#.##");

                decimal peso = Decimal.Parse(speso);
                decimal valor = Decimal.Parse(svalor);

                itemsList.Add(new Item(peso, valor));
            }
        }

        private void generateRandomBags()
        {
            for (int i = 0; i < Int16.Parse(configs["numMochila"]); i++)
            {
                string chromosomes = "";
                decimal fitness = 0, peso = 0, valor = 0;

                foreach (Item item in itemsList)
                {
                    if (rnd.Next(0, 2) == 1)
                    {
                        chromosomes += "1";
                        peso += item.weight;
                        valor += item.value;
                    }
                    else
                    {
                        chromosomes += "0";
                    }
                }

                if (valor > 0)
                {
                    decimal diff = decimal.Parse(configs["valorMaxObjeto"]) - decimal.Parse(configs["valorMinObjeto"]);
                    decimal perc = 100 / (decimal.Parse(configs["valorMaxObjeto"]) * Int16.Parse(configs["numItems"]));
                    fitness = valor * perc;
                }

                bool overflow = peso > decimal.Parse(configs["pesoMaxMochila"]);
                bagsInitialList.Add(new Bag(chromosomes, fitness, peso, overflow));
                bagsList.Add(new Bag(chromosomes, fitness, peso, overflow));
            }
        }

        private void addItemsToView()
        {
            for (int i = 0; i < itemsList.Count; i++)
            {
                StackPanel stackPanel = ItemsListPanel;
                Button button = new Button();
                TextBlock txtBlock = new TextBlock();
                txtBlock.Text = "#" + (i + 1) + " - Peso: " +
                    itemsList.ElementAt(i).weight + " Kg | Valor: " +
                    itemsList.ElementAt(i).value + " $";
                button.Content = txtBlock;
                stackPanel.Children.Add(button);
            }
        }

        private void addBagsInitialToView()
        {
            for (int i = 0; i < bagsInitialList.Count; i++)
            {
                WrapPanel wrapPanel = BagsListInitialPanel;
                Button button = new Button();
                button.Tag = i;
                button.Content = "Bag #" + (i + 1);
                wrapPanel.Children.Add(button);
            }
        }

        private void addBagsToView()
        {
            for (int i = 0; i < bagsList.Count; i++)
            {
                WrapPanel wrapPanel = BagsListPanel;
                Button button = new Button();
                button.Tag = i;
                button.Content = "Bag #" + (i + 1);
                wrapPanel.Children.Add(button);
            }
        }

        public void BagInitialHandler(object sender, RoutedEventArgs e)
        {
            Button b = e.OriginalSource as Button;
            WrapPanel wrapPanel = BagsInitialContainer;
            wrapPanel.Children.Clear();
            int index = Int16.Parse(b.Tag.ToString());
            Bag bag = bagsInitialList.ElementAt(index);

            decimal peso = 0, valor = 0;

            for (int i = 0; i < bag.chromosomes.Length; i++)
            {
                if (bag.chromosomes.ElementAt(i).ToString().Equals("1"))
                {
                    Item item = itemsList.ElementAt(i);
                    Button button = new Button();
                    button.Content = "Item #" + (i + 1);
                    peso += item.weight;
                    valor += item.value;
                    button.Tag = i + "," + item.weight + "," + item.value;
                    button.ToolTip = "Item #" + (i + 1) + "\n" +
                                     "Peso: " + item.weight + "\n" +
                                     "Valor: " + item.value;
                    wrapPanel.Children.Add(button);
                }
            }

            stackCircularInitial.Visibility = Visibility.Visible;
            double value = Double.Parse(bag.fitness.ToString("#.#"));
            if (peso > decimal.Parse(configs["pesoMaxMochila"]))
                CircularInitial.SegmentColor = new SolidColorBrush(Color.FromRgb(192, 57, 43));
            else if (value < double.Parse(configs["numLimiar"]))
                CircularInitial.SegmentColor = new SolidColorBrush(Color.FromRgb(41, 128, 185));
            else
                CircularInitial.SegmentColor = new SolidColorBrush(Color.FromRgb(22, 160, 133));
            sliderInitial.Value = value;
            pesoTBinitial.Text = "Peso: " + peso.ToString("#.##");
            valorTBinitial.Text = "Valor: " + valor.ToString("#.##");
        }

        public void BagFinalHandler(object sender, RoutedEventArgs e)
        {
            Button b = e.OriginalSource as Button;
            WrapPanel wrapPanel = BagsFinalContainer;
            wrapPanel.Children.Clear();
            int index = Int16.Parse(b.Tag.ToString());
            Bag bag = bagsList.ElementAt(index);

            decimal peso = 0, valor = 0;

            for (int i = 0; i < bag.chromosomes.Length; i++)
            {
                if (bag.chromosomes.ElementAt(i).ToString().Equals("1"))
                {
                    Item item = itemsList.ElementAt(i);
                    Button button = new Button();
                    button.Content = "Item #" + (i + 1);
                    peso += item.weight;
                    valor += item.value;
                    button.Tag = i + "," + item.weight + "," + item.value;
                    button.ToolTip = " Item #" + (i + 1) + "\n\n" +
                                     " Peso: " + item.weight + "\n" +
                                     " Valor: " + item.value;
                    wrapPanel.Children.Add(button);
                }
            }
            stackCircularFinal.Visibility = Visibility.Visible;
            double value = Double.Parse(bag.fitness.ToString("#.#"));
            if (peso > decimal.Parse(configs["pesoMaxMochila"]))
                CircularFinal.SegmentColor = new SolidColorBrush(Color.FromRgb(192, 57, 43));
            else if (value < double.Parse(configs["numLimiar"]))
                CircularFinal.SegmentColor = new SolidColorBrush(Color.FromRgb(41, 128, 185));
            else
                CircularFinal.SegmentColor = new SolidColorBrush(Color.FromRgb(22, 160, 133));
            sliderFinal.Value = value;
            pesoTBfinal.Text = "Peso: " + peso.ToString("#.##");
            valorTBfinal.Text = "Valor: " + valor.ToString("#.##");
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
