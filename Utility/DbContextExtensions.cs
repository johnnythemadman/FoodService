using Microsoft.EntityFrameworkCore;
using System;

namespace Utility
{
    public static class DbContextExtensions
    {
        public static void WithTransaction(this DbContext db, Action action)
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
        }
    }
}
