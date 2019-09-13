using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodService.Utilities
{
    public static class Transaction
    {
        public static void WithRollback(DbContext db, Action action)
        {
            using (var context = db)
            {
                try
                {
                    TransactionBeginLog();
                    context.Database.BeginTransaction();
                    TransactionActionStartLog();
                    action();
                }
                catch (Exception e)
                {
                    PrintExceptionInfo(e);
                    throw e;
                }
                finally
                {
                    TransactionRollbackLog();
                    context.Database.RollbackTransaction();
                }
            }
        }



        public static void TransactionBeginLog()
        {
            Console.WriteLine($"Transaction begin: {DateTime.UtcNow}");
        }

        public static void TransactionActionStartLog()
        {
            Console.WriteLine($"Action started: {DateTime.UtcNow}");
        }

        public static void PrintExceptionInfo(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }

        public static void TransactionRollbackLog()
        {
            Console.WriteLine($"Transaction rollbacked at: {DateTime.UtcNow}");
        }

        public static void TransationCommitLog()
        {
            Console.WriteLine($"Transaction commited at: {DateTime.UtcNow}");
        }
    }
}
