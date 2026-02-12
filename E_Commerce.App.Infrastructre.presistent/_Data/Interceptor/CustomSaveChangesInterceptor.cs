using E_Commerce.App.Application.Abstruction;
using E_Commerce.App.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace E_Commerce.App.Infrastructre.presistent._Data.Interceptor
{
    internal class CustomSaveChangesInterceptor :SaveChangesInterceptor
    {
        public CustomSaveChangesInterceptor(ILoggedInUserService loggedInUserService)
        {
            LoggedInUserService = loggedInUserService;
        }

        public ILoggedInUserService LoggedInUserService { get; }

        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }



        private void UpdateEntities(DbContext? context)
        {
            if (context is null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseEntity<int>>()
                .Where((entity) => entity.State is EntityState.Added or EntityState.Modified))
            {
                if (entry.State is EntityState.Added)
                {
                    entry.Entity.CreatedBy = LoggedInUserService.UserId ?? "UserAdmin";
                    entry.Entity.CreatedOn = DateTime.UtcNow;
                }
                entry.Entity.lastModifiedBy = LoggedInUserService.UserId ?? "UserAdmin";
                entry.Entity.LastModifideOn = DateTime.UtcNow;

            }
        }
    }
}
