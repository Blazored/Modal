using System.Collections;

namespace Blazored.Modal;

public class ModalParameters : IEnumerable<KeyValuePair<string, object>>
{
    internal readonly Dictionary<string, object> Parameters;

    public ModalParameters()
    {
        Parameters = new Dictionary<string, object>();
    }

    public ModalParameters Add(string parameterName, object value)
    {
        Parameters[parameterName] = value;
        return this;
    }

    public T Get<T>(string parameterName)
    {
        if (Parameters.TryGetValue(parameterName, out var value))
        {
            return (T)value;
        }
            
        throw new KeyNotFoundException($"{parameterName} does not exist in modal parameters");
    }

    public T? TryGet<T>(string parameterName)
    {
        if (Parameters.TryGetValue(parameterName, out var value))
        {
            return (T)value;
        }

        return default;
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() 
        => Parameters.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() 
        => Parameters.GetEnumerator();
}