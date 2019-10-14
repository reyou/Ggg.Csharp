using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace intro1
{
    /*The new interception API in EF Core 3.0 allows providing custom
     logic to be invoked automatically whenever low-level database operations 
     occur as part of the normal operation of EF Core. 
     For example, when opening connections, committing transactions, or executing commands.*/
    /*Similarly to the interception features that existed in EF 6, interceptors
     allow you to intercept operations before or after they happen. 
     When you intercept them before they happen, you are allowed to by-pass execution 
     and supply alternate results from the interception logic.*/
    /// <summary>
    /// https://devblogs.microsoft.com/dotnet/announcing-ef-core-3-0-and-ef-6-3-general-availability/
    /// </summary>
    public class HintCommandInterceptor : DbCommandInterceptor
    {
        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            // Manipulate the command text, etc. here...
            command.CommandText += " OPTION (OPTIMIZE FOR UNKNOWN)";
            return result;
        }
    }
}