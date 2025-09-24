using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SimpleExample.Infrastructure.Interceptors
{
    public class AuditInterceptor(ILogger<AuditInterceptor> logger) : SaveChangesInterceptor
    {
        private readonly ILogger<AuditInterceptor> _logger = logger;
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                    _logger.LogInformation($"Added entity: {entry.Entity.GetType().Name}, Status: {entry.State}");
                else if (entry.State == EntityState.Modified)
                    _logger.LogInformation($"Modified entity: {entry.Entity.GetType().Name}, Status: {entry.State}");
                else if (entry.State == EntityState.Deleted)
                    _logger.LogInformation($"Deleted entity: {entry.Entity.GetType().Name}, Status: {entry.State}");
            }
            return base.SavingChanges(eventData, result);
        }
    }
}