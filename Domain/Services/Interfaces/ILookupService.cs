﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ILookupService
    {
        Task<IEnumerable<Company>> GetActiveCompanyList();
    }
}
