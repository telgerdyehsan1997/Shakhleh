using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ISmsService
    {
        Task<MFAStatus> Dispatch(MFAMessage sms);
    }
}
