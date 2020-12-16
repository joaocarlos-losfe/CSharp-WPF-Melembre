using Melembre.Source.Model;
using Melembre.Source.Services;
using Melembre_v2.Models;
using System.Windows;

namespace Melembre_v2
{
    /// <summary>
    /// Lógica interna para AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Window
    {

        string defalt_color_priority = "#FF4841";
        string defalt_text_priority = "Alta";

        Database database = new Database();

        public bool is_save = false;

        public Reminder reminder = new Reminder();

        public AddEdit()
        {
            InitializeComponent();
            initializeComboboxes();
        }

        
        private void save_buttom_Click(object sender, RoutedEventArgs e)
        {
            if(input_txt.Text == null || StringChecks.stringIsSpaces(input_txt.Text) == true)
            {
                MessageBox.Show("O lembrete esta vazio !!");
                return;
            }

            if(database.exists(hour_select.Text + ":" + minut_select.Text))
            {
                MessageBox.Show("Ja existe um lembrete definido para esse horario");
                return;
            }

            reminder.Reminder_text = input_txt.Text;
            reminder.Priority = defalt_text_priority;
            reminder.Priority_color = defalt_color_priority;
            reminder.Category = category_selected.Text;
            reminder.Frequency = frequency_select.Text;
            reminder.Is_concluded = false;
            reminder.Concluded_color = "#FEA224";
            reminder.Concluded_text = "---";
            reminder._Horario = hour_select.Text + ":" + minut_select.Text;

            database.save(reminder);

            is_save = true;

            this.Close();
           

        }

        private void high_btn_Click(object sender, RoutedEventArgs e)
        {
            defalt_color_priority = "#FF4841";
            defalt_text_priority = "Alta";

            selected_priority.Background = ColorStringConverter.stringToColor(defalt_color_priority);
            selected_text_priority.Text = defalt_text_priority;
        }

        private void average_button_Click(object sender, RoutedEventArgs e)
        {
            defalt_color_priority = "#FBB452";
            defalt_text_priority = "Média";

            selected_priority.Background = ColorStringConverter.stringToColor(defalt_color_priority);
            selected_text_priority.Text = defalt_text_priority;
        }

        private void low_button_Click(object sender, RoutedEventArgs e)
        {
            defalt_color_priority = "#99EFD5";
            defalt_text_priority = "Baixa";

            selected_priority.Background = ColorStringConverter.stringToColor(defalt_color_priority);
            selected_text_priority.Text = defalt_text_priority;
        }

        private void initializeComboboxes()
        {
            for (int i = 1; i < 24; i++)
                hour_select.Items.Add(i.ToString("00"));
            hour_select.SelectedIndex = 0;


            for (int i = 1; i <=59; i++)
                minut_select.Items.Add(i.ToString("00"));
            minut_select.SelectedIndex = 0;
        }

        public Reminder getReminder()
        {
            return reminder;
        }
    }
}
