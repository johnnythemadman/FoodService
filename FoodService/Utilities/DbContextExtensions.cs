using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FoodService.Utilities
{
    public static class DbContextExtensions
    {
        public static async Task WithTransaction(this DbContext db, Action action)
        {
            await Task.Run(() =>
            {
                using (var context = db)
                {
                    try
                    {
                        Transaction.TransactionBeginLog();
                        context.Database.BeginTransaction();
                        Transaction.TransactionActionStartLog();
                        action();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Transaction interrupted at {DateTime.UtcNow}");
                        Transaction.TransactionRollbackLog();

                        Transaction.PrintExceptionInfo(e);
                        context.Database.RollbackTransaction();
                        throw e;
                    }
                    finally
                    {
                        Transaction.TransationCommitLog();
                        context.Database.CommitTransaction();
                    }
                }
            });

        }
    }
}
