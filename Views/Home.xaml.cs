using Melembre.Source.Model;
using Melembre.Source.Services;
using Melembre_v2.Models;
using Melembre_v2.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;

using System.Windows.Forms;

using System.Windows.Threading;


namespace Melembre_v2
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// Funcionalidade principais inseridas
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Reminder> reminders = new List<Reminder>();
        List<string> timers = new List<string>(); // carregar depois do app

        Database database = new Database();
        string aplication_root_directory = "";

        NotifyIcon notifyIcon = new NotifyIcon();

        string current_process_id = "";

        public MainWindow()
        {
            InitializeComponent();
            initSystemApp();

            this.notifyIcon.Click += new EventHandler(notifyIcon_Click);
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            AddEdit add = new AddEdit();
            add.ShowDialog();

            if (add.is_save == true)
            {
                reminders_list_view.Items.Add(add.reminder);
                reminders.Add(add.reminder);
                timers.Add(add.reminder._Horario + ":00");
                //o save no database esta direto na classe "Add";
                ActivateSystemNotification.ReminderAdd(add.reminder);
            }

        }

        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            if(getIndex() != -1)
            {
                int temp_index = getIndex();
                Edit edit = new Edit(reminders[getIndex()]);
                edit.ShowDialog();
                
                if (edit.is_edit)
                {
                    reminders.Insert(getIndex(), edit.reminder);
                    reminders.RemoveAt(getIndex());

                    reminders_list_view.Items.Insert(getIndex(), edit.reminder);
                    reminders_list_view.Items.RemoveAt(getIndex());


                    Debug.WriteLine(temp_index);

                    timers.Insert(temp_index, edit.reminder._Horario + ":00");
                    timers.RemoveAt(temp_index + 1);
                    
                } 
            }
            else
            {
                return;
            }
        }

        private int getIndex()
        {
            return reminders_list_view.SelectedIndex;
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            if (getIndex() != -1)
            {
                int temp_index = getIndex();
                database.remove(reminders[getIndex()]);
                reminders.RemoveAt(getIndex());
                reminders_list_view.Items.RemoveAt(getIndex());

                timers.RemoveAt(temp_index);
                reminders_list_view.Items.Refresh();    
            }
            else
            {
                return;
            }
        }

        private void initSystemApp()
        {
            aplication_root_directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MelembreApp";
           
            if (!Directory.Exists(aplication_root_directory))
            {
                Directory.CreateDirectory(aplication_root_directory);

                using(File.Create(aplication_root_directory + "\\process.dat")){}

                database.config();
            }
            else
            {
                reminders = database.loadAll();

                if (reminders.Count > 0)
                {
                    foreach (var reminder in reminders)
                    {
                        reminders_list_view.Items.Add(reminder);
                        timers.Add(reminder._Horario + ":00");
                    }
                }
            }

            string process = File.ReadAllText(aplication_root_directory + "\\process.dat");
            Debug.WriteLine(process);


            // adcionar > recuperar todos os processos associados e eliminalo, exeto o atual.

            if (process != null || process != "")
            {
                try
                {
                    Process process_kill = Process.GetProcessById(int.Parse(process));
                    process_kill.Kill();
                }
                catch
                {
                    //ok
                }
            }

            // getAssosiatesProcess();

        }

        //private void getAssosiatesProcess()
        //{
        //    Process[] localByName = Process.GetProcessesByName("Melembre");

        //    if(localByName != null )
        //    {

        //        if(localByName.Length > 1)
        //        {
        //            foreach (var pr in localByName)
        //            {
        //                if(Process.GetCurrentProcess().ToString() != pr.Id.ToString())
        //                {
        //                    Process process_kill = Process.GetProcessById(int.Parse(pr.Id.ToString()));
        //                    process_kill.Kill();

        //                }
        //            }
        //        }
        //    }   
        //}

        private void conclude_button_Click(object sender, RoutedEventArgs e)
        {
            Reminder reminder = new Reminder();

            if (getIndex() != -1)
            {
                Debug.WriteLine(getIndex());
                reminder = reminders[getIndex()];
                reminder.Concluded_color = "#49BABA";
                reminder.Concluded_text = "✔";
                reminder.Is_concluded = true;

                reminders.Insert(getIndex(), reminder);
                reminders.RemoveAt(getIndex());
               
                reminders_list_view.Items.Insert(getIndex(), reminder);
                reminders_list_view.Items.RemoveAt( getIndex() );

                database.update(reminder, reminder._Horario);
            }
            else
            {
                return;
            }

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

            if(timers.Contains(DateTime.Now.ToString("T")))
            {
                int index = timers.IndexOf(DateTime.Now.ToString("T"));
                string temp_frequency = "";
                temp_frequency = reminders[index].Frequency;

                Debug.WriteLine("era pra alarmar");
                Debug.WriteLine(reminders[index].Is_already_alarmed + " " + reminders[index].Frequency);


                if ( (reminders[index].Frequency == "Uma vez" && !(reminders[index].Is_already_alarmed)) || reminders[index].Frequency == "Todo dia")
                {
                    ReminderAlert reminderAlert = new ReminderAlert(reminders[index]);
                    reminderAlert.ShowDialog();

                    this.Visibility = Visibility.Visible;
                    Reminder reminder = new Reminder();

                    reminder = reminders[index];
                    reminder.Is_already_alarmed = true;

                    database.update(reminder, reminder._Horario);

                    reminders.RemoveAt(index);
                    reminders.Insert(index, reminder);
                    

                    reminders_list_view.SelectedItem = reminders[index];
                }
                else if(reminders[index].Frequency != "Uma vez" || reminders[index].Frequency != "Todo dia")
                {
                    Debug.WriteLine("Freqeuncia personalizada");
                    string[] frequency = reminders[index].Frequency.Split(' ');

                    List<string> days = new List<string>();

                    foreach (var day in frequency)
                        days.Add(day);

                    string abrevDay = DateTime.Now.ToString("ddd");

                    if(days.Contains(abrevDay))
                    {
                        ReminderAlert reminderAlert = new ReminderAlert(reminders[index]);
                        reminderAlert.ShowDialog();

                        this.Visibility = Visibility.Visible;
                        Reminder reminder = new Reminder();

                        reminder = reminders[index];
                        reminder.Is_already_alarmed = true;

                        database.update(reminder, reminder._Horario);

                        reminders.RemoveAt(index);
                        reminders.Insert(index, reminder);

                        reminders_list_view.SelectedItem = reminders[index];
                    }
                }

                
            }
               
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            current_process_id = Process.GetCurrentProcess().Id.ToString();
            File.WriteAllText(aplication_root_directory + "\\process.dat", current_process_id);
            this.Visibility = Visibility.Hidden; 
        }

        
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;  
        }

    }
}
