namespace BookApp.Common;

public static class OnSuccessExtensions
{
    public static Result<TSuccess, TFault> OnSuccess<TSuccess, TFault>(this Result<TSuccess, TFault> result,
        Action<TSuccess> action)
        =>
            result.Match(
                success =>
                {
                    action(success);
                    return result;
                },
                _ => result);
    
    public static Result<TSuccess, TFault> OoFail<TSuccess, TFault>(this Result<TSuccess, TFault> result,
        Action<TFault> action)
        =>
            result.Match(
                _=> result,
                fault =>
                {
                    action(fault);
                    return result;
                });

}