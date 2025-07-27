namespace BookApp.Common;

/// <summary>
/// Результат выполнения операции.
/// </summary>
public abstract class Result<TSuccess, TFault>
{
    public bool IsSuccess => Match(_ => true, f => false);
    public bool IsFault => Match(_ => false, f => true);
    
    public static Result<TSuccess, TFault> Success(TSuccess value) => new SuccessResult<TSuccess, TFault>(value);
    public static Result<TSuccess, TFault> Fail(TFault value) => new FaultResult<TSuccess, TFault>(value);

    public abstract TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TFault, TResult> onFault);
    
    public TResult Match<TResult>(TResult successValue, TResult faultValue)
        => Match(_ => successValue, _ => faultValue);
    
    public TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, TResult onFaultValue)
        => Match(onSuccess, (TFault _) => onFaultValue);
 
    public TResult Match<TResult>(TResult successValue, Func<TFault, TResult> onFault)
        => Match((TSuccess _) => successValue, onFault);

}

public abstract class Result<TFault>
{
public bool IsSuccess => Match(() => true, f => false);
public bool IsFault => Match(() => false, f => true);
    
public static Result<TFault> Success() => new SuccessResult<TFault>();
public static Result<TFault> Fail(TFault value) => new FaultResult<TFault>(value);

public abstract TResult Match<TResult>(Func<TResult> onSuccess, Func<TFault, TResult> onFault);
    
public TResult Match<TResult>(TResult successValue, TResult faultValue)
    => Match(() => successValue, _ => faultValue);
    
public TResult Match<TResult>(Func<TResult> onSuccess, TResult onFaultValue)
    => Match(onSuccess, (TFault _) => onFaultValue);
 
public TResult Match<TResult>(TResult successValue, Func<TFault, TResult> onFault)
    => Match(() => successValue, onFault);


}