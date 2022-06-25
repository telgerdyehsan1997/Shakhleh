namespace Domain
{
    using System;
    using System.Web;
    using Olive;
    using Olive.Services;
    using Olive.Entities;
    using Olive.Web;
    using System.Threading.Tasks;
    using Olive.Security;

    /// <summary>
    /// Provides the functionality to reset a user's password.
    /// </summary>
    public class PasswordResetService
    {
        User User;
        PasswordResetTicket Ticket;
        static IDatabase Database => Context.Current.Database();

        PasswordResetService(User user) { User = user; }

        /// <summary>
        /// Creates a new Request New User Ticket for the specified user.
        /// </summary>
        public static async Task RequestNewUserTicket(User user)
        {
            var service = new PasswordResetService(user);

            using (var scope = Database.CreateTransactionScope())
            {
                await service.CreateTicket();
                await service.SendNewUserEmail();

                scope.Complete();
            }
        }

        /// <summary>
        /// Sends an email to the specified user using the New User email template.
        /// </summary>
        async Task SendNewUserEmail()
        {
            await EmailTemplate.WelcomeEmail.Send(User, new
            {
                UserName = User.Name,
                Link = $"<a href='{GetSetPasswordUrl()}'> Set Your Password </a>",
            });
        }


        /// <summary>
        /// Creates a new Password Reset Ticket for the specified user.
        /// </summary>
        public static async Task RequestTicket(User user)
        {
            var service = new PasswordResetService(user);

            using (var scope = Database.CreateTransactionScope())
            {
                await service.CreateTicket();
                await service.SendEmail();
                scope.Complete();
            }
        }

        /// <summary>
        /// Completes the password recovery process.
        /// </summary>
        public static async Task Complete(PasswordResetTicket ticket, string newPassword)
        {
            if (newPassword.IsEmpty()) throw new ArgumentNullException(nameof(newPassword));

            if (ticket.IsExpired)
                throw new ValidationException("This ticket has expired. Please request a new ticket.");

            if (ticket.IsUsed) throw new ValidationException("This ticket has been used once. Please request a new ticket.");

            var service = new PasswordResetService(ticket.User);

            using (var scope = Database.CreateTransactionScope())
            {
                await service.UpdatePassword(newPassword);
                await Database.Update(ticket, t => t.IsUsed = true);

                scope.Complete();
            }
        }

        Task CreateTicket() => Database.Save(Ticket = new PasswordResetTicket { User = User });

        /// <summary>
        /// Sends an email to the specified user using the Recover Email email template.
        /// </summary>
        async Task SendEmail()
        {
            await EmailTemplate.RecoverPassword.Send(User, new
            {
                USERNAME = User.Name,
                LINK = $"<a href='{GetResetPasswordUrl()}'> Reset Your Password </a>",
            });
        }

        string GetResetPasswordUrl() => Context.Current.Request().GetAbsoluteUrl("/password/reset/" + Ticket.ID);

        string GetSetPasswordUrl() => Context.Current.Request().GetAbsoluteUrl("/password/set/" + Ticket.ID);

        /// <summary>
        /// Saves and encrypts password provided by the user.
        /// </summary>
        Task UpdatePassword(string clearTextPassword)
        {
            var pass = SecurePassword.Create(clearTextPassword);
            return Database.Update(User, u => { u.Password = pass.Password; u.Salt = pass.Salt; });
        }
    }
}
