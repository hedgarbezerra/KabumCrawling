using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outlook = Microsoft.Office.Interop.Outlook;
namespace KabumCrawling.Services.Email
{
    public class EmailSender
    {
        public void SendEmail(string emailPara, string assunto, string mensagem, string anexo)
        {
            Outlook.Application app = new Outlook.Application();
            Outlook.MailItem mailItem = app.CreateItem(Outlook.OlItemType.olMailItem);
            mailItem.Subject = assunto;
            mailItem.To = emailPara;
            mailItem.Body = mensagem;
            mailItem.Attachments.Add(anexo);
            mailItem.Importance = Outlook.OlImportance.olImportanceHigh;
            mailItem.Display(false);
            mailItem.Send();

        }
    }
}
