using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Melembre.Source.Model;
using Melembre.Source.Services;
using Microsoft.Data.Sqlite;

namespace Melembre_v2.Models
{
    class Database
    {
        private string aplication_root_directory = "Data Source = " + Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\MelembreApp\\database.db";
        SqliteConnection connection;
        SqliteCommand command = new SqliteCommand();
        

        public void config()
        {
            connection = new SqliteConnection(aplication_root_directory);
            
            using (connection)
            {
                connection.Open();

                command.CommandText = $@"CREATE TABLE Reminders
                (
                    Reminder_text VARCHAR(200) NOT NULL,
                    Priority VARCHAR(10) NOT NULL,      
                    Priority_color VARCHAR(8) NOT NULL,
                    Category VARCHAR(15) NOT NULL,
                    Frequency VARCHAR(8) NOT NULL,
                    Is_concluded VARCHAR NOT NULL,
                    Concluded_color VARCHAR NOT NULL,
                    Concluded_text VARCHAR NOT NULL,
                    _Horario VARCHAR NOT NULL,
                    PRIMARY KEY (_Horario)
                )";

                command.Connection = connection;

                command.ExecuteNonQueryAsync();

                connection.Close();
            }

        }

        public async void save(Reminder reminder)
        {
            connection = new SqliteConnection(aplication_root_directory);

            using (connection)
            {
                connection.Open();

                command.CommandText = $@"INSERT INTO Reminders
                VALUES
                (
                    '{reminder.Reminder_text}',
                    '{reminder.Priority}',
                    '{reminder.Priority_color}',
                    '{reminder.Category}',
                    '{reminder.Frequency}',
                    '{reminder.Is_concluded.ToString()}',
                    '{reminder.Concluded_color}',
                    '{reminder.Concluded_text}',
                    '{reminder._Horario}'
                    
                )";

                command.Connection = connection;

               
                await command.ExecuteNonQueryAsync();
               

                connection.Close();
            }
        }

        public List<Reminder> loadAll()
        {
            List<Reminder> reminders = new List<Reminder>();
            connection = new SqliteConnection(aplication_root_directory);

            SqliteDataReader dataReader;

            using (connection)
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM Reminders";
                dataReader = command.ExecuteReader();
                
                while(dataReader.Read())
                {
                    Reminder reminder = new Reminder();

                    reminder.Reminder_text = dataReader["Reminder_text"].ToString();
                    reminder.Priority = dataReader["Priority"].ToString();
                    reminder.Priority_color = dataReader["Priority_color"].ToString();
                    reminder.Category = dataReader["Category"].ToString();
                    reminder.Frequency = dataReader["Frequency"].ToString();
                    reminder.Is_concluded = bool.Parse(dataReader["Is_concluded"].ToString());
                    reminder.Concluded_color = dataReader["Concluded_color"].ToString();
                    reminder.Concluded_text = dataReader["Concluded_text"].ToString();
                    reminder._Horario = dataReader["_Horario"].ToString();

                    if (reminder != null)
                        reminders.Add(reminder);

                }

                connection.Close();

            }

            return reminders;
        }

        public void alterDataBase(Reminder reminder, string reminder_hour)
        {
           
        }

        public void remove(Reminder reminder)
        {
            connection = new SqliteConnection(aplication_root_directory);

            using (connection)
            {
                connection.Open();

                command.CommandText = $@"DELETE FROM Reminders WHERE _Horario = '{reminder._Horario}'"; 

                command.Connection = connection;

                 command.ExecuteNonQueryAsync();

                connection.Close();
            }
        }

        public bool exists(string horario)
        {
            connection = new SqliteConnection(aplication_root_directory);
            SqliteDataReader dataReader;
            Reminder reminder = new Reminder();

            using (connection)
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from Reminders";
                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    reminder = new Reminder();
                    reminder._Horario = dataReader["_Horario"].ToString();

                    if (reminder._Horario == horario)
                        return true;
                }
                connection.Close();
            }

            return false;
        }


    }
}
