using Melembre.Source.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Melembre.Source.Services
{
    public static class ActivateSystemNotification
    {
        public static void ReminderAdd(Reminder reminder)
        {
            string toastXmlString =
            $@"<toast><visual>
            <binding template='ToastGeneric'>
            <text>Novo lembrete Definido</text>
            <text>{reminder.Reminder_text} para as {reminder._Horario} Prioridade {reminder.Priority}</text>
            </binding>
            </visual></toast>";
            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(toastXmlString);

            var toastNotification = new Windows.UI.Notifications.ToastNotification(xmlDoc);
            var toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
            
            toastNotification.Priority = ToastNotificationPriority.High;
            toastNotification.ExpiresOnReboot = false;
            toastNotifier.Show(toastNotification);
            
        }

        public static void appClosing()
        {
            string toastXmlString =
            $@"<toast><visual>
            <binding template='ToastGeneric'>
            <text>O melembre está sendo executado em segundo plano</text>
            </binding>
            </visual></toast>";
            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(toastXmlString);

            var toastNotification = new Windows.UI.Notifications.ToastNotification(xmlDoc);
            var toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
            toastNotification.ExpiresOnReboot = true;
            toastNotification.Priority = ToastNotificationPriority.Default;
            toastNotifier.Show(toastNotification);           

        }

        public static void remembering(Reminder reminder)
        {
            string toastXmlString =
            $@"<toast><visual>
            <binding template='ToastGeneric'>
            <text>{reminder.Reminder_text}</text>
            <text>Prioridade {reminder.Priority}</text>
            </binding>
            </visual></toast>";
            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(toastXmlString);

            var toastNotification = new Windows.UI.Notifications.ToastNotification(xmlDoc);
            var toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
            toastNotification.ExpiresOnReboot = false;
            toastNotification.Priority = ToastNotificationPriority.High;
            toastNotifier.Show(toastNotification);

        }

        public static void rememberingDelay(Reminder reminder)
        {
            string toastXmlString =
            $@"<toast><visual>
            <binding template='ToastGeneric'>
            <text>{reminder.Reminder_text}</text>
            <text>Em 1 hora</text>
            </binding>
            </visual></toast>";
            var xmlDoc = new Windows.Data.Xml.Dom.XmlDocument();
            xmlDoc.LoadXml(toastXmlString);

            var toastNotification = new Windows.UI.Notifications.ToastNotification(xmlDoc);
            var toastNotifier = Windows.UI.Notifications.ToastNotificationManager.CreateToastNotifier();
            toastNotification.ExpiresOnReboot = false;
            toastNotification.Priority = ToastNotificationPriority.High;
            toastNotifier.Show(toastNotification);

        }
    }
}
