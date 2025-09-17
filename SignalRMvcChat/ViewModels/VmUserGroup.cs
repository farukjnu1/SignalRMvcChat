using SignalRMvcChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRMvcChat.ViewModels
{
    public class VmUserGroup
    {
        public int ID { get; set; }
        public string UserGroupName { get; set; }
        public List<ChatUserGroup> listUserGroup { get; set; }
    }
}