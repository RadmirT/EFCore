namespace BookApp.Services.Runners;

using System;
using System.Collections.Generic;
using BookApp.Common;
using BookApp.Features;
using BookApp.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

public class WriteDbWithValidationRunner<TInput, TOutput>(
    DbContext dbContext,
    IBusinessAction<TInput, TOutput> action) : IRunner<TInput, TOutput>
{
    private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    private readonly IBusinessAction<TInput, TOutput> _action =
        action ?? throw new ArgumentNullException(nameof(action));

    public Result<TOutput, IEnumerable<Error>> Run(TInput input)
    {
        return _action.Action(input)
            .Match(
                result =>
                {
                    return _dbContext.SaveChangesWithValidation()
                        .Match(
                            () =>
                                Result<TOutput, IEnumerable<Error>>.Success(result),
                            Result<TOutput, IEnumerable<Error>>.Fail);
                },
                Result<TOutput, IEnumerable<Error>>.Fail);
    }
}