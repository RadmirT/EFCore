namespace BookApp.Features;

using BookApp.Common;

public interface IBusinessAction<in TInput, TOutput>
{
    Result<TOutput, IEnumerable<Error>> Action(TInput input);
}