using KabumCrawling.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Services.Notification
{
    public interface INotification
    {
        void Notificar(List<Produto> produtos, Destinario destinario);
        void Notificar<T>(List<Produto> produtos, List<Destinario> destinario);
    }
}
