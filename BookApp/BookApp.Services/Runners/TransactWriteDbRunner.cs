namespace BookApp.Services.Runners;

using System;
using System.Collections.Generic;
using BookApp.Common;
using BookApp.Features;
using Microsoft.EntityFrameworkCore;

public class Transact3StepWriteDbRunner<TInput, TStep1TOutput, TStep2TOutput, TOutput>(
    DbContext dbContext,
    IBusinessAction<TInput, TStep1TOutput> step1Action,
    IBusinessAction<TStep1TOutput, TStep2TOutput> step2Action,
    IBusinessAction<TStep2TOutput, TOutput>? step3Action)
    : IRunner<TInput, TOutput>
{
    private readonly DbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly IBusinessAction<TInput, TStep1TOutput> _step1Action = step1Action ?? throw new ArgumentNullException(nameof(step1Action));
    private readonly IBusinessAction<TStep1TOutput, TStep2TOutput> _step2Action = step2Action ?? throw new ArgumentNullException(nameof(step2Action));
    private readonly IBusinessAction<TStep2TOutput, TOutput> _step3Action = step3Action ?? throw new ArgumentNullException(nameof(step3Action));

    public Result<TOutput, IEnumerable<Error>> Run(TInput input)
    {
        using var transaction = this._dbContext.Database.BeginTransaction();
        return RunStep(this._step1Action, input)
            .Then(step1Result => RunStep(this._step2Action, step1Result))
            .Then(step2Result => RunStep(this._step3Action, step2Result))
            .OnSuccess(_=> transaction.Commit())
            .OoFail(_=> transaction.Rollback());
    }

    private Result<TStepOutput, IEnumerable<Error>> RunStep<TStepInput, TStepOutput>(IBusinessAction<TStepInput, TStepOutput> action, TStepInput input)
    {
        return action.Action(input)
            .OnSuccess(_=>
            {
                _dbContext.SaveChanges();
            });
    }
}