using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{

    public class ASMApiService : IASMApiService
    {
        private readonly IDatabase Database = Olive.Context.Current.Database();
    }
}
