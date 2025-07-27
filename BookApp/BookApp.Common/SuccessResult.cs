namespace BookApp.Common;

public class SuccessResult<TSuccess, TFault>(TSuccess value) : Result<TSuccess, TFault>
{
    TSuccess _value = value;
    public override TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TFault, TResult> onFault)
        => onSuccess(_value);
}

public class SuccessResult<TFault>() : Result<TFault>
{
    public override TResult Match<TResult>(Func<TResult> onSuccess, Func<TFault, TResult> onFault)
        => onSuccess();
}