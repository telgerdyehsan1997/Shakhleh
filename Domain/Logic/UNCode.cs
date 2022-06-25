namespace Domain
{
   using System;
   using System.Linq;
   using System.Threading.Tasks;
   using System.Collections.Generic;
   using Olive;
   using Olive.Entities;
   
   partial class UNCode
   {
        public static Task<UNCode> FindByUnNumber(string unNumber)
        {
            return Database.FirstOrDefault<UNCode>(c => c.UNNo == unNumber && !c.IsDeactivated);
        }
    }
}