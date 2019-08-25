using FoodService.DAO.Database;
using System;

namespace FoodService.DataAccess.DAO.DataAccess
{
    public static class Transaction
    {
        public static void WithRollback(FoodServiceContext db, Action action)
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

        private static void TransactionBeginLog()
        {
            Console.WriteLine($"Transaction begin: {DateTime.UtcNow}");
        }

        private static void TransactionActionStartLog()
        {
            Console.WriteLine($"Action started: {DateTime.UtcNow}");
        }

        private static void PrintExceptionInfo(Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(e.StackTrace);
        }

        private static void TransactionRollbackLog()
        {
            Console.WriteLine($"Transaction rollbacked at: {DateTime.UtcNow}");
        }
    }
}
