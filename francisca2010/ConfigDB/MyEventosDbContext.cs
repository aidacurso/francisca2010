using francisca2010.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace francisca2010.ConfigDB
{
    class MyEventosDbContext:DbContext
    {
        
        DbSet<Evento> Eventos { get; set; }
        DbSet<Status> Statuses { get; set; }
    }
}
