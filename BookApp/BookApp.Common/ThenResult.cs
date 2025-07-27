namespace BookApp.Common;

public static class ThenResult
{
    public static Result<TOut, TFault> Then<TValue, TOut, TFault>(this Result<TValue, TFault> result,
        Func<TValue, Result<TOut, TFault>> onSuccess)
    {
        return result.Match(onSuccess, Result<TOut,TFault>.Fail);
    }
}