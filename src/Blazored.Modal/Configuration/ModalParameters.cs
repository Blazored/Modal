using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blazored.Modal
{
    public class ModalParameters
    {
        internal Dictionary<string, object> _parameters;

        public ModalParameters()
        {
            _parameters = new Dictionary<string, object>();
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

    public class ModalParameters<TComponent> : ModalParameters, IEnumerable<KeyValuePair<Expression<Func<TComponent, object>>, object>>
    {
        public void Add<TParameter>(Expression<Func<TComponent, TParameter>> parameterExpression, TParameter value)
        {
            if (parameterExpression.Body is MemberExpression memberExpression)
            {
                var property = typeof(TComponent).GetProperty(memberExpression.Member.Name);
                if (!property.CustomAttributes.Any(a => a.AttributeType == typeof(ParameterAttribute)))
                {
                    throw new InvalidOperationException($"Property {memberExpression.Member.Name} is not a component parameter.");
                }
                Add(memberExpression.Member.Name, value);
            }
            else
            {
                throw new InvalidOperationException("Expression is not a member");
            }
        }

        public IEnumerator<KeyValuePair<Expression<Func<TComponent, object>>, object>> GetEnumerator()
        {
            var parameter = Expression.Parameter(typeof(TComponent));

            return _parameters?.ToDictionary(kvp => (Expression<Func<TComponent, object>>)Expression.Lambda(
                    Expression.Convert(Expression.Property(parameter, kvp.Key), typeof(object)), parameter),
                kvp => kvp.Value)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
