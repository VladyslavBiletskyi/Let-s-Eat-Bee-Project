using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Let_s_Eat_Bee_Project.Models
{
    public class DBDump
    {
        public List<Order> Orders { get; set; }
        public List<AbstractUser> Users { get; set; }

    }
}