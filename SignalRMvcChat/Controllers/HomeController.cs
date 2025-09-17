using SignalRMvcChat.Models;
using SignalRMvcChat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRMvcChat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chat()
        {
            return View();
        }

        public ActionResult UploadFile()
        {
            object result = null;
            var files = Request.Files;
            if (files != null && files.Count > 0)
            {
                var file = files[0];
                var filePath = Server.MapPath("~/MyFiles/" + file.FileName);
                file.SaveAs(filePath);
                result = new { message = "file saved successfully." };
            }
            return Json(result);
        }

        public ActionResult Login(string username)
        {
            object result = null; string message = "";
            if (!string.IsNullOrEmpty(username))
            {
                var isLogin = LoginUser(username);
                message = isLogin == true ? "SUCCESS" : "FAIL";
                result = new { message };
            }
            return Json(result);
        }

        public ActionResult Group()
        {
            return View();
        }

        public ActionResult GetGroup()
        {
            object result = null;
            result = new { data = GetGroups() };
            return Json(result);
        }

        private List<VmUserGroup> GetGroups()
        {
            var listVmUserGroup = new List<VmUserGroup>(); int id = 0;
            var db = new ChatDbContext();
            var listChatGroup = db.ChatGroups.ToList();
            foreach (var oChatGroup in listChatGroup)
            {
                var oVmUserGroup = new VmUserGroup();
                oVmUserGroup.ID = ++id;
                oVmUserGroup.UserGroupName = oChatGroup.GroupName;
                oVmUserGroup.listUserGroup = db.ChatUserGroups.Where(x => x.GroupName == oChatGroup.GroupName).ToList();
                if (oVmUserGroup.listUserGroup.Count > 0)
                {
                    listVmUserGroup.Add(oVmUserGroup);
                }
            }
            return listVmUserGroup;
        }

        public ActionResult GetUser(string groupName)
        {
            object result = null;
            result = new { data = GetUsers(groupName) };
            return Json(result);
        }

        private List<VmUser> GetUsers(string groupName)
        {
            var listVmUser = new List<VmUser>(); int id = 0;
            var db = new ChatDbContext();
            var listUser = db.ChatUsers.ToList();
            var listUserGroup = db.ChatUserGroups.Where(x => x.GroupName == groupName).ToList();
            foreach (var oUser in listUser)
            {
                var oUserGroup = (from x in listUserGroup where x.Username == oUser.Username select x).FirstOrDefault();
                var oVmUser = new VmUser();
                oVmUser.ID = ++id;
                oVmUser.Username = oUser.Username;
                oVmUser.GroupName = oUserGroup == null ? "" : oUserGroup.GroupName;
                oVmUser.Checkbox = oUserGroup == null ? "" : "checked";
                listVmUser.Add(oVmUser);
            }
            return listVmUser;
        }

        public ActionResult CreateGroup(string groupName, string csvUser)
        {
            object result = null;
            result = new { data = CreateGroups(groupName,csvUser) };
            return Json(result);
        }

        private bool CreateGroups(string groupName, string csvUser)
        {
            var isSave = false;
            try
            {
                var db = new ChatDbContext();
                var oGroup = (from x in db.ChatGroups where x.GroupName == groupName select x).FirstOrDefault();
                if (oGroup == null)
                {
                    oGroup = new ChatGroup();
                    oGroup.GroupName = groupName;
                    db.ChatGroups.Add(oGroup);
                }
                var listUserGroupRem = db.ChatUserGroups.Where(x => x.GroupName == groupName).ToList();
                db.ChatUserGroups.RemoveRange(listUserGroupRem);
                var arrUser = csvUser.Split(',');
                var listUserGroup = new List<ChatUserGroup>();
                foreach (var username in arrUser)
                {
                    var oUserGroup = new ChatUserGroup();
                    oUserGroup.GroupName = groupName;
                    oUserGroup.Username = username;
                    listUserGroup.Add(oUserGroup);
                }
                db.ChatUserGroups.AddRange(listUserGroup);
                db.SaveChanges();
                isSave = true;
            }
            catch { }
            return isSave;
        }

        public ActionResult RemoveGroup(string groupName)
        {
            object result = null;
            result = new { data = RemoveGroups(groupName) };
            return Json(result);
        }

        private bool RemoveGroups(string groupName)
        {
            var isSave = false;
            try
            {
                var db = new ChatDbContext();
                var oGroup = (from x in db.ChatGroups where x.GroupName == groupName select x).FirstOrDefault();
                if (oGroup != null)
                {
                    var listUserGroupRem = db.ChatUserGroups.Where(x => x.GroupName == groupName).ToList();
                    db.ChatUserGroups.RemoveRange(listUserGroupRem);
                    db.ChatGroups.Remove(oGroup);
                }
                db.SaveChanges();
                isSave = true;
            }
            catch { }
            return isSave;
        }

        private bool LoginUser(string username)
        {
            bool isLogin = false;
            var db = new Models.ChatDbContext();
            var oChatUser = db.ChatUsers.Where(x => x.Username == username).FirstOrDefault();
            if (oChatUser != null)
            {
                isLogin = true;
            }
            return isLogin;
        }

        public ActionResult GetUserGroup(string username)
        {
            object result = null;
            result = new { data = GetUserOrGroup(username) };
            return Json(result);
        }

        private List<VmUserGroup> GetUserOrGroup(string username)
        {
            var listVmUserGroup = new List<VmUserGroup>(); int id = 0;
            var db = new Models.ChatDbContext();
            var listChatGroup = db.ChatGroups.ToList();
            var listChatUser = db.ChatUsers.ToList();
            foreach (var oChatGroup in listChatGroup)
            {
                var oVmUserGroup = new VmUserGroup();
                oVmUserGroup.ID = ++id;
                oVmUserGroup.UserGroupName = oChatGroup.GroupName;
                oVmUserGroup.listUserGroup = db.ChatUserGroups.Where(x => x.GroupName == oChatGroup.GroupName && x.Username == username).ToList();
                if (oVmUserGroup.listUserGroup.Count > 0)
                {
                    listVmUserGroup.Add(oVmUserGroup);
                }
            }
            foreach (var oChatUser in listChatUser)
            {
                var oVmUserGroup = new VmUserGroup();
                oVmUserGroup.ID = ++id;
                oVmUserGroup.UserGroupName = oChatUser.Username;
                listVmUserGroup.Add(oVmUserGroup);
            }
            return listVmUserGroup;
        }

        public ActionResult GetMessage(string fromUser, string toUser)
        {
            object result = null;
            result = new { data = GetChatMessage(fromUser, toUser) };
            return Json(result);
        }

        private List<ChatMessage> GetChatMessage(string fromUser, string toUser)
        {
            var db = new ChatDbContext();
            var listChatMessage = (from x in db.ChatMessages
                                   where (x.Sender == fromUser && x.Receiver == toUser)
                                   || (x.Sender == toUser && x.Receiver == fromUser)
                                   || x.Receiver == toUser
                                   select x).ToList();
            return listChatMessage;
        }

    }
}