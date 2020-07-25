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
        public override void Notificar(string mensagem, Destinario destinario)
        {
            throw new NotImplementedException();
        }

        public override void Notificar<T>(string mensagem, List<Destinario> destinario)
        {
            throw new NotImplementedException();
        }
    }
}
