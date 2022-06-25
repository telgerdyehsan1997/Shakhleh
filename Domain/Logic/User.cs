namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;
    using Olive.Security;
    using Olive.Web;

    /// <summary>
    /// Provides the business logic for User class.
    /// </summary>
    partial class User : ILoginInfo
    {
        string ILoginInfo.DisplayName => Name;

        string ILoginInfo.ID => ID.ToString();

        TimeSpan? ILoginInfo.Timeout => Config.Get("Authentication:Timeout", defaultValue: 20).Minutes();

        /// <summary>
        /// Gets the roles of this user.
        /// </summary>
        public virtual IEnumerable<string> GetRoles()
        {
            if (Context.Current.Request().IsLocal()) yield return "Local.Request";
            yield return "User";
        }

        /// <summary>
        /// Specifies whether or not this user has a specified role.
        /// </summary>
        public bool IsInRole(string role) => GetRoles().Contains(role);

        /// <summary>
        /// Creates a New User Ticket for the specified User if they are creating a new account.
        /// </summary>
        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);

            if (e.Mode == SaveMode.Insert)
                await PasswordResetService.RequestNewUserTicket(this);
        }

        /// <summary>
        /// Validates this instance to ensure it can be saved in a data repository.
        /// Checks that no existing User has the same email address.
        /// </summary>
        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            if (await Database.Any<User>(u => u.Email == Email && u != this))
                throw new ValidationException("Email must be unique. There is an existing User record with the provided Email.");
        }

        /// <summary>
        /// Checks for an existing user with the matching email address.
        /// </summary>
        public static Task<User> FindByEmail(string email)
        {
            return Database.FirstOrDefault<User>(u => u.Email == email);
        }
    }
}
