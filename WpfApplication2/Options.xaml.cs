using System;
using SnakeGame;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Snake_TZWKTT
{



    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {


        public Options()
        {
            InitializeComponent();
            //ComboBoxPace.ItemsSource = Enum.GetValues(typeof(PACE)).Cast<PACE>();
        }

        
        int thickness=2;

        public int Thickness
        {
            get { return thickness; }

        }
        int pace=1;

        public int Pace
        {
            get { return pace; }
            set { pace = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RButton_Thin.IsChecked == true)
            {
                thickness = 1;
            }
            else if (RButton_Normal.IsChecked == true)
            {
                thickness = 2;   
            }
            else
            {
                thickness = 3;
            }
            pace = ComboBoxPace.SelectedIndex;
            SnakeGame.Window1 Jatek = new SnakeGame.Window1(thickness, pace);
            Jatek.ShowDialog();
            if (!DialogResult.HasValue || !DialogResult.Value)
            {
                Jatek.Close();
                Jatek.Timer.Stop();
            }
            //this.Close();
            DialogResult = true;
            
        }
    }
}
