using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ILrnToDriverService
    {
        Task<LrnToDriverStatus> GenerateMessage(LrnToDriverMessage message);
    }
}
