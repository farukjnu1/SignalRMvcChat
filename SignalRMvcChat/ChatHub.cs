using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRMvcChat.Models;

namespace SignalRMvcChat
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string fromUser, string toUser, string message, string path, string time)
        {
            var db = new ChatDbContext();
            var oChatMessage = new ChatMessage();
            oChatMessage.Sender = fromUser;
            oChatMessage.Receiver = toUser;
            oChatMessage.GroupName = toUser;
            oChatMessage.FilePath = path;
            oChatMessage.Text = message;
            oChatMessage.MessageTime = DateTime.Now;
            db.ChatMessages.Add(oChatMessage);
            db.SaveChanges();
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(fromUser, toUser, message, path, oChatMessage.MessageTime.ToString());
        }

    }
}