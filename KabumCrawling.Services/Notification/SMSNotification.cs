using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Notification
{
    public class SMSNotification : NotificationBase
    {
        public override void Notificar(List<Produto> produtos, Destinario destinario)
        {
            throw new NotImplementedException();
        }

        public override void Notificar(List<Destinario> destinario)
        {
            throw new NotImplementedException();
        }
    }
}
