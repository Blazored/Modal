namespace Blazored.Modal.Services;

public class ModalResult
{
    public object? Data { get; }
    public Type? DataType { get; }
    public bool Cancelled { get; }
    public bool Confirmed => !Cancelled;

    private ModalResult(object? data, Type? resultType, bool cancelled)
    {
        Data = data;
        DataType = resultType;
        Cancelled = cancelled;
    }

    public static ModalResult Ok<T>(T result) 
        => Ok(result, typeof(T));

    public static ModalResult Ok<T>(T result, Type? dataType) 
        => new(result, dataType, false);

    public static ModalResult Ok() 
        => new(null, null, false);
    
    public static ModalResult Cancel() 
        => new(null, null, true);
    
    public static ModalResult Cancel<T>(T payload) 
        => new(payload, null, true);
}