using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabumCrawling.Repository.Interfaces
{
    public interface IEntityConfiguration
    {
         void ConfigurateFK();

         void ConfiguratePK();

         void ConfigurateFields();

         void ConfigurateTableName();
    }
}
