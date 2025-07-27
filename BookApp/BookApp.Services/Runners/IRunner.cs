namespace BookApp.Services.Runners;

using System.Collections.Generic;
using BookApp.Common;

public interface IRunner<in TInput, TOutput>
{
    Result<TOutput, IEnumerable<Error>> Run(TInput input);
}