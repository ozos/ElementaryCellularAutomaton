using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Input;


namespace automaton
{
    /// <summary>
    /// Visualize elementary celullar automatons 
    /// </summary>
    public partial class MainWindow : Window
    {
        const int MaxGridSize = 90;
        CellCollection _cells = new CellCollection(MaxGridSize);

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Window_Loaded);
            InitializeGrid();

        }
        private void RuleNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return || e.Key == Key.Enter)
            {
                startButton_Click(this, new RoutedEventArgs());
            }
        }
        void Window_Loaded(object sender, RoutedEventArgs e)
        {

            startButton.Click += new RoutedEventHandler(startButton_Click);
            RuleNumber.KeyDown += new KeyEventHandler(RuleNumber_KeyDown);

        }


        void InitializeGrid()
        {

            for (int i = 0; i < MaxGridSize; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition());
                Grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int row = 0; row < MaxGridSize; row++)
            {
                for (int column = 0; column < MaxGridSize; column++)
                {
                    Ellipse ellipse = new Ellipse();
                    Grid.SetColumn(ellipse, column);
                    Grid.SetRow(ellipse, row);
                    ellipse.DataContext = _cells[row, column];
                    Grid.Children.Add(ellipse);
                    ellipse.Style = Resources["lifeStyle"] as Style;
                }
            }
        }
        void startButton_Click(object sender, RoutedEventArgs e)
        {
            _cells.kill();

            try
            {
                _cells.UpdateLife(Convert.ToInt32(RuleNumber.Text));

            }
            catch (FormatException)
            {
                MessageBox.Show("Please, Introduce a rule", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }





}
