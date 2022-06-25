using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface ILoggable
    {
        Task Log(string str, LogType logType);
    }
}
