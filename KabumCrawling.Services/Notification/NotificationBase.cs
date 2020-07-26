using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Notification
{
    public abstract class NotificationBase : INotification
    {
        protected string _smtp = ConfigurationManager.AppSettings["smtp"];
        protected int _smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
        protected string _email = ConfigurationManager.AppSettings["email"];
        protected string _emailPassword = ConfigurationManager.AppSettings["senhaEmail"];
        
        public abstract void Notificar(List<Produto> produtos, Destinario destinario);

        public abstract void Notificar(List<Destinario> destinario);
    }
}
