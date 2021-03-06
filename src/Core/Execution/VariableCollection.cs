using System;
using System.Collections.Generic;
using HotChocolate.Types;

namespace HotChocolate.Execution
{
    internal sealed class VariableCollection
    {
        private readonly Dictionary<string, object> _variables;

        public VariableCollection(Dictionary<string, object> variables)
        {
            if (variables == null)
            {
                throw new ArgumentNullException(nameof(variables));
            }

            _variables = variables;
        }

        public T GetVariable<T>(string variableName)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentNullException(nameof(variableName));
            }

            if (!_variables.TryGetValue(variableName,
                out object variableValue))
            {
                throw new QueryException(new VariableError(
                    "The specified variable was not declared.", variableName));
            }

            // TODO : integrate converters
            return (T)variableValue;
        }

        public bool TryGetVariable<T>(string variableName, out T variableValue)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentNullException(nameof(variableName));
            }

            if (_variables.TryGetValue(variableName,
                out object value))
            {
                // TODO : integrate converters
                variableValue = (T)value;
                return true;
            }

            variableValue = default(T);
            return false;
        }
    }
}
