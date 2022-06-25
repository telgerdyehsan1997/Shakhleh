using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Domain
{
    public class ArchiveLogService : IArchiveLogService
    {
        IDatabase Database;

        public ArchiveLogService(IDatabase database)
        {
            Database = database;
        }

        public async Task<ArchiveLog> CreateArchiveLog(string reason, User currentUser, string ip, GuidEntity entity, string trackingNumber = null)
        {
            var result = new ArchiveLog
            {
                Reason = reason,
                LoggedInUser = currentUser,
                UserIp = ip,
                DateAndTime = LocalTime.Now,
                EntityId = entity.ID,
                TrackingNumber = trackingNumber
            };

            return await Database.Save(result);
        }

        public async Task<ArchiveLog> CreateUnArchiveLog(string reason, User currentUser, string ip, GuidEntity entity)
        {
            var oldLog = await Database.Of<ArchiveLog>().Where(x => x.EntityId == entity).OrderByDescending(x => x.DateAndTime).FirstOrDefault();

            if (oldLog != null)
            {
                var result = new ArchiveLog
                {
                    Reason = reason,
                    LoggedInUser = currentUser,
                    UserIp = ip,
                    DateAndTime = LocalTime.Now,
                    EntityId = entity.ID
                };

                return await Database.Save(result);
            }
            return null;
        }

        public async Task<string> GetIPAddress()
        {
            const int port = 65530;
            string localIP;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", port);
                var endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address.ToString();
            }
            return localIP;
        }
    }
}
