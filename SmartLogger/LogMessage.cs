using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogger
{
    public class LogMessage
    {
        public LogType Type { get; set; } 
        public string Message { get; set; }
        public string MessageToLog { get { return DateTime.Now.ToShortDateString() + this.Message; } } 
    }
}
