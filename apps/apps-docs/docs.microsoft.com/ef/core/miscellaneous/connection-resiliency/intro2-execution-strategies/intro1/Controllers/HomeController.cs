using System;
using intro1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Transactions;

namespace intro1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency#execution-strategies-and-transactions
        /// The solution is to manually invoke the execution strategy with a delegate representing everything that needs to be executed. If a transient failure occurs, the execution strategy will invoke the delegate again.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            using (BloggingContext db = new BloggingContext())
            {
                // A strategy that is used to execute a command or query against the database,
                // possibly with logic to retry when a failure occurs.
                IExecutionStrategy strategy = db.Database.CreateExecutionStrategy();
                // <summary>Executes the specified operation.</summary>
                strategy.Execute(() =>
                {
                    using (BloggingContext context = new BloggingContext())
                    {
                        /*A transaction against the database.
                         Instances of this class are typically obtained from Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade.BeginTransaction and it is not designed to be directly constructed in your application code.*/
                        // Starts a new transaction.
                        using (IDbContextTransaction transaction = context.Database.BeginTransaction())
                        {
                            context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });
                            /*Saves all changes made in this context to the database. 
                            This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges 
                            to discover any changes to entity instances before saving to the underlying database. 
                            This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.*/
                            context.SaveChanges();

                            context.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/visualstudio" });
                            context.SaveChanges();

                            transaction.Commit();
                        }
                    }
                });
            }

            return View();
        }

        /// <summary>
        /// This approach can also be used with ambient transactions.
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            using (BloggingContext context1 = new BloggingContext())
            {
                context1.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/visualstudio" });

                IExecutionStrategy strategy = context1.Database.CreateExecutionStrategy();

                strategy.Execute(() =>
                {
                    using (BloggingContext context2 = new BloggingContext())
                    {
                        // Makes a code block transactional. This class cannot be inherited.
                        using (TransactionScope transaction = new TransactionScope())
                        {
                            context2.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });
                            context2.SaveChanges();

                            context1.SaveChanges();

                            transaction.Complete();
                        }
                    }
                });
            }
            return View();
        } 
        
        public IActionResult AddStateVerification()
        {
            using (BloggingContext db = new BloggingContext())
            {
                // A strategy that is used to execute a command or query against the database, possibly with logic to retry when a failure occurs.
                IExecutionStrategy strategy = db.Database.CreateExecutionStrategy();

                Blog blogToAdd = new Blog { Url = "http://blogs.msdn.com/dotnet" };
                db.Blogs.Add(blogToAdd);

                // Executes the specified operation in a transaction.
                // Allows to check whether the transaction has been rolled back if an error occurs during commit.
                strategy.ExecuteInTransaction(db,
                    operation: context =>
                    {
                        context.SaveChanges(acceptAllChangesOnSuccess: false);
                    },
                    /*AsNoTracking: Returns a new query where the change tracker will not track any of the entities that are returned. If the entity instances are modified, this will not be detected by the change tracker and Microsoft.EntityFrameworkCore.DbContext.SaveChanges will not persist those changes to the database. 
 Disabling change tracking is useful for read-only scenarios because it avoids the overhead of setting up change tracking for each entity instance. You should not disable change tracking if you want to manipulate entity instances and persist those changes to the database using Microsoft.EntityFrameworkCore.DbContext.SaveChanges. 
 The default tracking behavior for queries can be controlled by Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.QueryTrackingBehavior.*/
                    verifySucceeded: context => context.Blogs.AsNoTracking().Any(b => b.BlogId == blogToAdd.BlogId));
                // Provides access to information and operations for entity instances this context is tracking.
                // Accepts all changes made to entities in the context.
                // It will be assumed that the tracked entities represent the current state of the database.
                // This method is typically called by Microsoft.EntityFrameworkCore.DbContext.SaveChanges after changes
                // have been successfully saved to the database.
                db.ChangeTracker.AcceptAllChanges();
            }
            return View();
        } 
        
        public IActionResult ManuallyTrackTheTransaction()
        {
            using (BloggingContext db = new BloggingContext())
            {
                IExecutionStrategy strategy = db.Database.CreateExecutionStrategy();

                db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/dotnet" });

                TransactionRow transaction = new TransactionRow { Id = Guid.NewGuid() };
                db.Transactions.Add(transaction);

                strategy.ExecuteInTransaction(db,
                    operation: context =>
                    {
                        context.SaveChanges(acceptAllChangesOnSuccess: false);
                    },
                    verifySucceeded: context => context.Transactions.AsNoTracking().Any(t => t.Id == transaction.Id));

                db.ChangeTracker.AcceptAllChanges();
                db.Transactions.Remove(transaction);
                db.SaveChanges();
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
