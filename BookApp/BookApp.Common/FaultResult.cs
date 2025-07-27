namespace BookApp.Common;

public class FaultResult<TSuccess, TFault>(TFault value) : Result<TSuccess, TFault>
{
    private TFault _value = value;
    
    public override TResult Match<TResult>(Func<TSuccess, TResult> onSuccess, Func<TFault, TResult> onFault) 
        => onFault(_value);
}

public class FaultResult<TFault>(TFault value) : Result<TFault>
{
    private TFault _value = value;
    
    public override TResult Match<TResult>(Func<TResult> onSuccess, Func<TFault, TResult> onFault) 
        => onFault(_value);
}