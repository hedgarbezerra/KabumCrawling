using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Notification
{
    interface INotification
    {
        void Notificatar(string mensagem, string destinario);
        void Notificatar<T>(string mensagem, List<T> destinario) where T : class;
    }
}
