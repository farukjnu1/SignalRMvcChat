using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRMvcChat.ViewModels
{
    public class VmUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string GroupName { get; set; }
        public string Checkbox { get; set; }
    }
}