using System;
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
using WpfApplication1;


namespace Snake_TZWKTT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Options Options;

        public Options Options1
        {
            get { return Options; }

        }
        
        int pace=2;

        public int Pace
        {
            get { return pace; }
            set { pace = value; }
        }
        int thickness=2;

        public int Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }
        public MainWindow()
        {
            InitializeComponent();

        }
        public MainWindow(int thickness,int pace)
        {
            this.thickness = thickness;
            this.pace = pace;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WpfApplication1.Window1 Jatek = new Window1(thickness, pace);
            //this.Close();
           
            Jatek.ShowDialog();
            if (!DialogResult.HasValue || !DialogResult.Value)
            {
                Jatek.Close();
                Jatek.Timer.Stop();
            }
        }

        private void Button_Option_Click(object sender, RoutedEventArgs e)
        {
            Options = new Options();
            
            Options.ShowDialog();
        }
        ScoreLists sl;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sl = new ScoreLists();
            sl.ShowDialog();
        }


    }
}
