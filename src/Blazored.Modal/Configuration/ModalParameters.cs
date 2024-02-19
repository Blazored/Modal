using System.Collections;

namespace Blazored.Modal;

public class ModalParameters : IEnumerable<KeyValuePair<string, object?>>
{
    internal readonly Dictionary<string, object?> Parameters;

    public ModalParameters()
    {
        Parameters = new Dictionary<string, object?>();
    }

    public ModalParameters Add(string parameterName, object? value)
    {
        Parameters[parameterName] = value;
        return this;
    }

    public T Get<T>(string parameterName)
    {
        if (!Parameters.TryGetValue(parameterName, out var value))
        {
            throw new KeyNotFoundException($"{parameterName} does not exist in modal parameters");
        }

        if (value is not T typedValue)
        {
            throw new InvalidOperationException($"The value for parameter '{parameterName}' is not of the expected type {typeof(T)}.");
        }

        return typedValue;
    }

    public T? TryGet<T>(string parameterName) where T : class
    {
        return Parameters.TryGetValue(parameterName, out var objValue) && objValue is T typedValue
            ? typedValue
            : null;
    }

    public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        => Parameters.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => Parameters.GetEnumerator();
}
