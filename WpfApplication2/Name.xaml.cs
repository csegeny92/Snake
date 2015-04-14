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

namespace Snake_TZWKTT
{
    /// <summary>
    /// Interaction logic for Name.xaml
    /// </summary>
    public partial class Name : Window
    {
        public Name()
        {
            InitializeComponent();
            btnOK.Focus();
            player = new ScoreLists();
        }
        ScoreLists player;

        public ScoreLists PlayerName
        {
            get { return player; }
            set { player = value; }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PlayerName.Name= TextBox_Name.Text;
            player.Name = TextBox_Name.Text;
            this.DialogResult = true;
            this.Close();

        }
    }
}
