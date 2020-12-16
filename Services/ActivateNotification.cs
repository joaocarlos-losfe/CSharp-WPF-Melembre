using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Melembre.Source.Services
{
    public static class ActivateSystemNotification
    {
        public static void TaskAddshowSystemNotification(string title, string priority, string definido_para)
        {
            string toastXmlString =
            $@"<toast><visual>
            <binding template='ToastGeneric'>
            <text>{title}</text>
            <text>Prioridade {priority} definido para as {definido_para}</text>
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
    }
}
