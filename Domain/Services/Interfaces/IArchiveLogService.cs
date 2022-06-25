using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IArchiveLogService
    {
        Task<ArchiveLog> CreateArchiveLog(string reason, User currentUser, string ip, GuidEntity entity, string trackingNumber = null);

        Task<ArchiveLog> CreateUnArchiveLog(string reason, User currentUser, string ip, GuidEntity entity);

        Task<string> GetIPAddress();
    }
}
