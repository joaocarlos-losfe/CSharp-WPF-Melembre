using Melembre.Services;
using Melembre.Source.Model;
using Melembre.Source.Services;
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
using System.Windows.Threading;

namespace Melembre_v2.Views
{
    /// <summary>
    /// Lógica interna para ReminderAlert.xaml
    /// </summary>
    public partial class ReminderAlert : Window
    {
        public bool is_conclude = false;

        int time_tick = 0;

        public ReminderAlert(Reminder reminder)
        {
            InitializeComponent();

            display_reminder.Text = reminder.Reminder_text;
            reminder_color_priority.Fill = ColorStringConverter.stringToColor(reminder.Priority_color);
            reminder_text_priority.Text = "Prioridade " + reminder.Priority;
            ExecuteSound.play_sound(@"C:\Windows\Media\Ring02.wav");
            ActivateSystemNotification.remembering(reminder);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            get_current_time();
        }

        private void get_current_time()
        {
            var Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(updateTime_tick);
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Start();
        }

        //obtem a hora atual
        private void updateTime_tick(object sender, EventArgs e)
        {
            date_time_app.Text = DateTime.Now.ToString("F");
            time_tick++;

            if(time_tick == 58)
            {
                ExecuteSound.stop_sound();
                this.Close();
            }
        }

        private void mute_btn_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSound.stop_sound();
        }

        private void stop_button_Click(object sender, RoutedEventArgs e)
        {
            ExecuteSound.stop_sound();
            this.Close();
        }

       
    }
}
