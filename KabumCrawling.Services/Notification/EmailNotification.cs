using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Notification
{
    public sealed class EmailNotification : INotification
    {
        public void Notificatar(string mensagem, string destinario)
        {
            throw new NotImplementedException();
        }

        public void Notificatar<T>(string mensagem, List<T> destinario) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
