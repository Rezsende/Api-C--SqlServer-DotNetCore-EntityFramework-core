using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c_.Entities
{
    public class UsersPlanos
    {
        public int id {get; set;}        
        public User User { get; set; }
        public int UserId { get; set; }

        public Planos planos { get; set; }
        public int planosId { get; set; }
       
    }
}