using Olive;
using Olive.Entities;
using Olive.Entities.Data;
using Olive.Security;
using System.Threading.Tasks;

namespace Domain
{
    public class ReferenceData : IReferenceData
    {
        static IDatabase Database => Context.Current.Database();

        async Task<T> Create<T>(T item) where T : IEntity
        {
            await Context.Current.Database().Save(item);
            return item;
        }

        public async Task Create()
        {
            await Create(new Settings { Name = "Current", PasswordResetTicketExpiryMinutes = 2 });

            await CreateAdmins();
        }

        async Task CreateAdmins()
        {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
            await AddAdmin("test", "testington", "admin@uat.co");
        }


        private Task<Administrator> AddAdmin(string firstName, string lastName, string email)
        {
            var pass = SecurePassword.Create("test");
            return Create(new Administrator
            {
#pragma warning disable GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                Email = email,
#pragma warning restore GCop646 // Email addresses should not be hard-coded. Move this to Settings table or Config file.
                FirstName = firstName,
                LastName = lastName,
                Password = pass.Password,
                Salt = pass.Salt
            });
        }
    }
}
