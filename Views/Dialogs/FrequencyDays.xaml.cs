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

namespace Melembre_v2.Views.Dialogs
{
    /// <summary>
    /// Lógica interna para FrequencyDays.xaml
    /// </summary>
    public partial class FrequencyDays : Window
    {
        public List<string> MarquedDays = new List<string>();

        public FrequencyDays()
        {
            InitializeComponent();
        }

        private void Seg_Click(object sender, RoutedEventArgs e)
        {
            if (Seg.IsChecked == true)
                MarquedDays.Add("seg");
            else
                MarquedDays.Remove("seg");
                   
        }

        private void Terc_Click(object sender, RoutedEventArgs e)
        {
            if (Terc.IsChecked == true)
                MarquedDays.Add("ter");
            else
                MarquedDays.Remove("ter");
        }

        private void Qua_Click(object sender, RoutedEventArgs e)
        {
            if (Qua.IsChecked == true)
                MarquedDays.Add("qua");
            else
                MarquedDays.Remove("qua");
        }

        private void Qui_Click(object sender, RoutedEventArgs e)
        {
            if (Qui.IsChecked == true)
                MarquedDays.Add("qui");
            else
                MarquedDays.Remove("qui");

        }

        private void Sex_Click(object sender, RoutedEventArgs e)
        {
            if (Sex.IsChecked == true)
                MarquedDays.Add("sex");
            else
                MarquedDays.Remove("sex");
        }

        private void Sab_Click(object sender, RoutedEventArgs e)
        {
            if (Sab.IsChecked == true)
                MarquedDays.Add("sáb");
            else
                MarquedDays.Remove("sáb");
        }

        private void Dom_Click(object sender, RoutedEventArgs e)
        {
            if (Dom.IsChecked == true)
                MarquedDays.Add("dom");
            else
                MarquedDays.Remove("dom");
        }

        private void btn_salvar_Click(object sender, RoutedEventArgs e)
        {
            if (MarquedDays == null || MarquedDays.Count == 0)
            {
                MessageBox.Show("Nenhum dia foi definido...");
                MarquedDays = null;
            }  
            else
            {
                this.Close();
            } 
        }
    }
}
