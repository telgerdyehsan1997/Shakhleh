namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Olive;
    using Olive.Entities;
    using Olive.Entities.Data;

    public partial class ShopUser : User
    {
        /// <summary> 
        /// Gets the roles of this user.
        /// </summary>
        public override IEnumerable<string> GetRoles()
        {
#pragma warning disable GCop177 // Variable declaration is unnecessary due to it being used only for return statement
            var result = base.GetRoles().Concat("ShopUser");
#pragma warning restore GCop177 // Variable declaration is unnecessary due to it being used only for return statement
                               //if (IsAdmin == true)
                               //    result = result.Concat("Customer Admin");

            return result;
        }
    }
}