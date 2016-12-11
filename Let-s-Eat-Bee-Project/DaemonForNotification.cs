using System;
using System.Collections.Generic;
using System.Linq;
using Let_s_Eat_Bee_Project.Models;
using System.Threading;
using Microsoft.AspNet.Identity;

namespace Let_s_Eat_Bee_Project
{
    public class DaemonForNotification
    {
        ApplicationDbContext db = new ApplicationDbContext();
        string textOfNotification = "У вас есть заказ, который завершится менее чем через 30 минут. Подробно на сайте: http://localhost:1478/Account";
        string subjectOfNotification = "Let-s-Eat-Bee";
        EmailService eServise = new EmailService();

        public void StartDaemon()
        {
            DaemonForNotification daemon = new DaemonForNotification();
            Thread th = new Thread(daemon.Notification);
            th.Start();
        }

        private void Notification()
        {
            IdentityMessage toSend = new IdentityMessage();
            while (true)
            {
                Thread.Sleep(300000);
                DateTime now = DateTime.Now;
                DateTime start = now.AddMinutes(25);
                DateTime end = now.AddMinutes(30);
                List<Order> Orders = db.Orders.Where(x => x.CompletionDateTime >= start && x.CompletionDateTime <= end).ToList();
                foreach (var order in Orders)
                {
                    foreach (var join in order.Joinings)
                    {
                        if (join.User.GetType().BaseType.Name == typeof(AuthorizedUser).Name)
                        {
                            AuthorizedUser user = (AuthorizedUser)join.User;
                            toSend.Destination = user.AppUser.Email;
                            toSend.Body = textOfNotification;
                            toSend.Subject = subjectOfNotification;

                            eServise.Send(toSend);
                        }
                    }
                }
            }


        }
        
    }
}