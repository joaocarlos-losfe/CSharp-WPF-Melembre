using Melembre.Source.Model;
using Melembre.Source.Services;
using Melembre_v2.Models;
using Melembre_v2.Views.Dialogs;
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

namespace Melembre_v2.Views
{
    /// <summary>
    /// Lógica interna para Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        string defalt_color_priority = "";
        string defalt_text_priority = "";
        string selectedCategory = null;
        public bool is_edit = false;

        public Reminder reminder = new Reminder();

        Database database = new Database();

        string temp_hour = "";

        bool temp_is_concluded = false;
        string temp_Concluded_text = "";
        string temp_color = "";


        public Edit(Reminder reminder)
        {
            InitializeComponent();
            initializeComboboxes();
            this.reminder = reminder;

            input_txt.Text = reminder.Reminder_text;
            selected_color_priority.Background = ColorStringConverter.stringToColor(reminder.Priority_color);
            selected_text_priority.Text = reminder.Priority;

            defalt_color_priority = reminder.Priority_color;
            defalt_text_priority = reminder.Priority;

            category_selected.Items.Add(reminder.Category);
            category_selected.Text = reminder.Category;

            frequency_select.Items.Add(reminder.Frequency);
            frequency_select.Text = reminder.Frequency;

            string[] hour_minut = reminder._Horario.Split(':');

            hour_select.Text = hour_minut[0];
            minut_select.Text = hour_minut[1];

            this.temp_hour = reminder._Horario;
            this.temp_is_concluded = reminder.Is_concluded;
            this.temp_Concluded_text = reminder.Concluded_text;
            this.temp_color = reminder.Concluded_color;

        }

        private void save_buttom_Click(object sender, RoutedEventArgs e)
        {
            if (input_txt.Text == null || StringChecks.stringIsSpaces(input_txt.Text) == true)
            {
                MessageBox.Show("O lembrete esta vazio !!");
                return;
            }

            if(database.exists(hour_select.Text + ":" + minut_select.Text) && hour_select.Text + ":" + minut_select.Text != temp_hour)
            {
                MessageBox.Show("Ja existe um lembrete definido para esse hora");
                return;
            }
            else
            {
                is_edit = true;
                //corrigir o modo edit
                reminder.Reminder_text = input_txt.Text;
                reminder.Priority = defalt_text_priority;
                reminder.Priority_color = defalt_color_priority;
                reminder.Category = category_selected.Text;
                reminder.Frequency = frequency_select.Text;

                reminder.Is_concluded = temp_is_concluded;
                reminder.Concluded_color = "#FEA224";
                reminder.Concluded_text = "---";

                reminder._Horario = hour_select.Text + ":" + minut_select.Text;
                database.update(reminder, temp_hour);
            }

            this.Close();
        }

        private void high_btn_Click(object sender, RoutedEventArgs e)
        {
            defalt_color_priority = "#FF4841";
            defalt_text_priority = "Alta";

            selected_color_priority.Background = ColorStringConverter.stringToColor(defalt_color_priority);
            selected_text_priority.Text = defalt_text_priority;
        }

        private void average_button_Click(object sender, RoutedEventArgs e)
        {
            defalt_color_priority = "#FBB452";
            defalt_text_priority = "Média";

            selected_color_priority.Background = ColorStringConverter.stringToColor(defalt_color_priority);
            selected_text_priority.Text = defalt_text_priority;
        }

        private void low_button_Click(object sender, RoutedEventArgs e)
        {
            defalt_color_priority = "#99EFD5";
            defalt_text_priority = "Baixa";

            selected_color_priority.Background = ColorStringConverter.stringToColor(defalt_color_priority);
            selected_text_priority.Text = defalt_text_priority;
        }

        private void initializeComboboxes()
        {
            for (int i = 1; i < 24; i++)
                hour_select.Items.Add(i.ToString("00"));
            hour_select.SelectedIndex = 0;


            for (int i = 1; i <= 59; i++)
                minut_select.Items.Add(i.ToString("00"));
            minut_select.SelectedIndex = 0;
        }

        private void OutroBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCategory newCategory = new NewCategory();

            newCategory.Title = "Nova categoria";

            newCategory.ShowDialog();

            if (newCategory.newCategoryTxt != null)
            {
                selectedCategory = newCategory.newCategoryTxt;
                category_selected.Items.Add(selectedCategory);
                category_selected.Text = selectedCategory;
            }
        }

        private void SelectDaysBtn_Click(object sender, RoutedEventArgs e)
        {
            string days = null;

            FrequencyDays frequencyDays = new FrequencyDays();
            frequencyDays.ShowDialog();

            if (frequencyDays.MarquedDays == null || frequencyDays.MarquedDays.Count == 0)
            {
                frequency_select.Text = "Uma vez";
            }
            else
            {
                List<string> selected_days = frequencyDays.MarquedDays;

                foreach (var day in selected_days)
                    days += day + " ";

                frequency_select.Items.Add(days);
                frequency_select.Text = days;
                return;
            }
        }
    }
}
