namespace Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Security;

    /// <summary> 
    /// Provides the business logic for ChannelPortsUser class.
    /// </summary>
    partial class ChannelPortsUser : IArchivable
    {
        /// <summary> 
        /// Gets the roles of this user.
        /// </summary>
        public override IEnumerable<string> GetRoles()
        {
            var result = base.GetRoles().Concat("Admin");
            if (IsAdmin == true)
                result = result.Concat("Super Admin");

            return result;
        }

        public override Task Validate()
        {
            base.Validate();

            FirstName = FirstName.CapitaliseFirstLetters();
            LastName = LastName.CapitaliseFirstLetters();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates/updates a ChannelPortsUser record.
        /// If an existing record is being updated, it validates that there is at least one user in the Admin role.
        /// </summary>
        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);

            if (e.Mode == SaveMode.Update && (await Database.None<ChannelPortsUser>(a => a.IsAdmin == true)))
                throw new ValidationException("At least one of the ChannelPorts users should be in Admin role.");
        }
    }
}