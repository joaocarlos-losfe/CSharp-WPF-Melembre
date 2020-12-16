using Melembre.Source.Model;
using Melembre.Source.Services;
using Melembre_v2.Models;
using Melembre_v2.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        Database database = new Database();
        

        public MainWindow()
        {
            InitializeComponent();
            initSystemApp();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            AddEdit add = new AddEdit();
            add.ShowDialog();

            if (add.is_save == true)
            {
                reminders_list_view.Items.Add(add.reminder);
                reminders.Add(add.reminder);
                //o save no database esta direto na classe "Add";
            }

        }

        private void edit_button_Click(object sender, RoutedEventArgs e)
        {
            if(getIndex() != -1)
            {
                Edit edit = new Edit(reminders[getIndex()]);
                edit.ShowDialog();
                try
                {
                    if (edit.is_edit)
                    {
                        reminders.Insert(getIndex(), edit.reminder);
                        reminders.RemoveAt(getIndex());

                        reminders_list_view.Items.Insert(getIndex(), edit.reminder);
                        reminders_list_view.Items.RemoveAt(getIndex());
                    } 
                }     
                catch
                {
                    
                    return;
                }
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
                try
                {
                    database.remove(reminders[getIndex()]);
                    reminders.RemoveAt(getIndex());
                    reminders_list_view.Items.RemoveAt(getIndex());
                    reminders_list_view.Items.Refresh();
                }
                catch
                {
                    reminders_list_view.Items.Refresh();
                    return;
                }
            }
            else
            {
                return;
            }
        }

        private void initSystemApp()
        {
            string aplication_root_directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MelembreApp";

            if (!Directory.Exists(aplication_root_directory))
            {
                Directory.CreateDirectory(aplication_root_directory);
                File.Create(aplication_root_directory + "\\init.ini");
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
                    }
                }
            }
        }

        private void conclude_button_Click(object sender, RoutedEventArgs e)
        {
            Reminder reminder = new Reminder();
            if (getIndex() != -1)
            {
                try
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
                catch
                {
                    return;
                }
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
        }

    }
}
