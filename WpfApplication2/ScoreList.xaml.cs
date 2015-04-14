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
using System.IO;
using System.ComponentModel;



namespace Snake_TZWKTT
{
    /// <summary>
    /// Interaction logic for ScoreList.xaml
    /// </summary>
    public partial class ScoreLists : Window
    {


        List<ScoreLists> scorelist;

        public List<ScoreLists> Scorelist
        {
            get { return scorelist; }
            set { scorelist = value; }
        }
        
        
        public ScoreLists()
        {
            InitializeComponent();
       

        }






        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            scorelist = new List<ScoreLists>();
            
            StreamReader sr = new StreamReader("scorelist.txt");
            string str;
            string[] scores;
            

            while (!sr.EndOfStream)
            {
                scores = new string[2];
                str = sr.ReadLine();
                scores = str.Split(' ');
                string name = scores[0];
                string nev = Name;
                int score = int.Parse(scores[1]);
                ScoreLists sl = new ScoreLists(name,score);
                scorelist.Add(sl);
            }

            List<ScoreLists> SortedList = scorelist.OrderByDescending(o => o.score).ToList();
            
            
            
            foreach (ScoreLists sc in SortedList)
            {
                ListBox1.Items.Add(sc.ToString());
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
