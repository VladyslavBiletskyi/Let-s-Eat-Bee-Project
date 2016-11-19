using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Let_s_Eat_Bee_Project.Models;
using Let_s_Eat_Bee_Project.Controllers;
using System.Data.Entity;

namespace Let_s_Eat_Bee_Project
{
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Hello()
        {
            Clients.All.hello();
        }
        public void Send(string name, string text, int OrderId )
        {
            // Call the addNewMessageToPage method to update clients.
            Order order = db.Orders.FirstOrDefault(x => x.Id == OrderId);
            if (order == null)
            {
                return;
            }
            AbstractUser user = db.AllUsers.OfType<AuthorizedUser>().FirstOrDefault(x => x.AppUser.UserName == name);
            if (user != null)
            {
                name = user.LastName + " " + user.FirstName;
            }
            else
            {
                user = new UnauthorizedUser();
                user.FirstName = name;
                user.LastName = "√";
                db.AllUsers.Add(user);
                db.SaveChanges();
            }

            Message message = new Message();
            message.Text = text;
            message.OrderId = OrderId;
            message.Order = order;
            message.CreationDateTime = DateTime.Now;
   
            message.User = user;
            message.UserId = user.Id;
            user.Messages.Add(message);
            order.Messages.Add(message);
            db.Messages.Add(message);
            db.SaveChanges();

            Clients.All.addNewMessageToPage(name, text);
        }
    }
}