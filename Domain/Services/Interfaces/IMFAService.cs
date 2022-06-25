using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IMFAService
    {
        Task<MFAStatus> ValidateMFA(User user, string key); 
        
        Task<(MFAStatus status, string key)> GenerateMFA(User user);
    }
}
