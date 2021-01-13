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

namespace Melembre_v2.Views.Dialogs
{
    /// <summary>
    /// Lógica interna para NewCategory.xaml
    /// </summary>
    public partial class NewCategory : Window
    {
        public string newCategoryTxt = null;

        public NewCategory()
        {
            InitializeComponent();
        }

        private void btn_salvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(input_txt.Text))
                return;
            else
            {
                newCategoryTxt = input_txt.Text;
                this.Close();
            }

        }
    }
}
