using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Melembre.Source.Model
{
    public class Reminder
    {
        
        public string Reminder_text   { get; set; }
        public string Priority        { get; set; }
        public string Priority_color  { get; set; }
        public string Category        { get; set; }
        public string Frequency       { get; set; }
        public bool   Is_concluded    { get; set; }
        public string Concluded_color { get; set; }
        public string Concluded_text  { get; set; }
        public string _Horario        { get; set; }

        public Reminder()
        {
            this.Reminder_text = "";
            this.Priority = "Alta";
            this.Priority_color = "#FF4841";
            this.Category = "";
            this.Frequency = "Uma vez";
            this.Is_concluded = false;
            this.Concluded_color = "#FBB452";
            this.Concluded_text = "---";
            this._Horario = "";

        }
    }

    
}
