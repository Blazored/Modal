using System.Collections.Generic;

namespace Blazored.Modal
{
    public class ModalParameters
    {
        internal Dictionary<string, object> _parameters;

        public ModalParameters()
        {
            _parameters = new Dictionary<string, object>();
        }

        public ModalParameters(IEnumerable<KeyValuePair<string, object>> parameters)
        {
            _parameters = new Dictionary<string, object>(parameters);
        }

        public object this[string parameterName]
        {
            get => _parameters[parameterName];
            set => _parameters[parameterName] = value;
        }

        public void Add(string parameterName, object value)
        {
            _parameters[parameterName] = value;
        }

        public T Get<T>(string parameterName)
        {
            if (_parameters.TryGetValue(parameterName, out var value))
            {
                return (T)value;
            }

            throw new KeyNotFoundException($"{parameterName} does not exist in modal parameters");
        }

        public T TryGet<T>(string parameterName)
        {
            if (_parameters.TryGetValue(parameterName, out var value))
            {
                return (T)value;
            }

            return default;
        }
    }
}