namespace BookApp.Persistence.Extensions;

using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using BookApp.Common;
using Microsoft.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static async Task<Result<Error[]>> SaveChangesWithValidation(this DbContext context, CancellationToken cancellationToken = default)
    {
        var result = context.ExecuteValidation();
        if (result.Length != 0)
        {
            return Result<Error[]>.Fail(result);
        }
        context.ChangeTracker.AutoDetectChangesEnabled = false;
        try
        {
            await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        finally
        {
            context.ChangeTracker.AutoDetectChangesEnabled = true;
        }   
        return Result<Error[]>.Success();
    }

    public static Result<Error[]> SaveChangesWithValidation(this DbContext context)
    {
        var result = context.ExecuteValidation();
        if (result.Length != 0)
        {
            return Result<Error[]>.Fail(result);
        }
        context.ChangeTracker.AutoDetectChangesEnabled = false;
        try
        {
            context.SaveChanges();
        }
        finally
        {
            context.ChangeTracker.AutoDetectChangesEnabled = true;
        }   
        return Result<Error[]>.Success();
    }

    private static Error[]
        ExecuteValidation(this DbContext context)
    {
        var result = new List<Error>();
        foreach (var entry in
                 context.ChangeTracker.Entries() 
                     .Where(e =>
                         e.State is EntityState.Added or EntityState.Modified))
        {
            var entity = entry.Entity;
            var valContext = new ValidationContext(entity);
            var entityErrors = new List<ValidationResult>();
            if (!Validator.TryValidateObject(entity, valContext, entityErrors, true))
            {
                result.AddRange(entityErrors.Select(e=> new Error(e.ErrorMessage)));
            }
        }

        return result.ToArray();
    }
}