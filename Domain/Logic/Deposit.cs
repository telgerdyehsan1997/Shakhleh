namespace Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;
    using Olive.Entities;

    partial class Deposit
    {
        public static async Task AddPending(Consignment consignment, decimal revenue)
        {
            try
            {
                var declarent = consignment.GetDefermentNumber();
                if (declarent.Company != null)
                {
                    var deposit = (await Database.Of<Deposit>().Where(x => x.ConsignmentId == consignment).FirstOrDefault())?.Clone() ?? new Deposit();

                    deposit.Value = revenue;
                    deposit.TransactionType = TransactionType.Pending;
                    deposit.DateAdded = LocalTime.Now;
                    deposit.Company = declarent.Company;
                    deposit.Consignment = consignment;
                    await Database.Save(deposit);
                }

            }
            catch (ValidationException ex)
            {
                if (ex.Message != "There is an existing Deposit with the same Consignment and Transaction type in the database already.")
                    throw;
            }
        }

        public static async Task UpdatePendingToWithdrawl(Consignment consignment, decimal revenue = 0)
        {
            var deposit = await Database.Of<Deposit>()
                .Where(x => x.ConsignmentId == consignment)
                .FirstOrDefault() ?? new Deposit();

            if (!deposit.IsNew)
                await Database.Update(deposit, x => x.TransactionType = TransactionType.Withdrawal);
            else
            {
                var declarent = consignment.GetDefermentNumber();

                if (declarent.Company != null)
                {
                    deposit.Value = revenue;
                    deposit.TransactionType = TransactionType.Withdrawal;
                    deposit.DateAdded = LocalTime.Now;
                    deposit.Company = declarent.Company;
                    deposit.Consignment = consignment;
                    await Database.Save(deposit);
                }
            }
        }

        public static async Task AddDefaultPending(Consignment consignment)
        {
            try
            {
                var company = consignment.GetDefermentNumber().Company;
                if (company != null)
                {
                    var deposit = new Deposit
                    {
                        Value = 0,
                        TransactionType = TransactionType.Pending,
                        DateAdded = LocalTime.Now,
                        Company = company,
                        Consignment = consignment
                    };
                    await Database.Save(deposit);
                }

            }
            catch (ValidationException ex)
            {
                if (ex.Message != "There is an existing Deposit with the same Consignment and Transaction type in the database already.")
                    throw;
            }

        }
        public static async Task<IEnumerable<Deposit>> GetDepositList()
        {
            var deposits = await Database.Of<Deposit>()
                .Where(x => x.TransactionTypeId == TransactionType.Pending && x.Value > 0)
                 .GetList()
                 .ToList();
            return deposits.OrderByDescending(x => x.Consignment?.ConsignmentNumber);
        }

        public static async Task<IEnumerable<Deposit>> GetDeactivatedDepositList(Company company)
        {
            if (company != null)
            {
                return await Database.Of<Deposit>()
                .Where(x => x.CompanyId == company)
                .Where(x => !x.IsDeactivated && x.TransactionTypeId == TransactionType.Pending && x.Value > 0)
                .GetList()
                .ToList();
            }

            return new List<Deposit>();
        }


        public static async Task<bool> HasCompanyRemainingBalancePositive(Company company)
        {
            if (company != null) 
            {
                var overdraft = company.OverdraftAmount.HasValue ? company.OverdraftAmount.Value : 0;
                return (await company.GetRemainingBalanceAfterPending())+overdraft > 0;
            }

            return false;
        }

        public static async Task<bool> HasCompanyRemainingBalanceZeroOrPositive(Company company)
        {
            if (company != null)
            {
                var overdraft = company.OverdraftAmount.HasValue ? company.OverdraftAmount.Value : 0;
                return (await company.GetRemainingBalanceAfterPending())+overdraft >= 0;
            }

            return false;
        }
    }
}