namespace BookApp.Services.Runners;

using System;
using System.Collections.Generic;
using BookApp.Common;
using BookApp.Features;
using Microsoft.EntityFrameworkCore;

public class WriteDbRunner<TInput, TOutput>(DbContext dbContext, IBusinessAction<TInput, TOutput> action) : IRunner<TInput, TOutput>
{
    private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly IBusinessAction<TInput, TOutput> _action = action ?? throw new ArgumentNullException(nameof(action));

    public Result<TOutput, IEnumerable<Error>> Run(TInput input)
    {
        return _action.Action(input)
            .OnSuccess(_=>
            {
                _dbContext.SaveChanges();
            });
    }
}