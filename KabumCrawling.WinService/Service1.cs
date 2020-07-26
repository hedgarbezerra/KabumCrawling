using FluentScheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.WinService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            var schedule = new Schedule(
                   () => ConsumingService.EnviarEmailPrecos()
               );

            schedule.ToRunEvery(5).Minutes();
        }

        protected override void OnStop()
        {
        }

    }
}
