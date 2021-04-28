namespace DeprecatedClass
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class PrivateObject
    {
        private readonly object _object;
        private readonly TypeInfo _objectTypeInfo;
        
        public PrivateObject(object @object)
        {
            _object = @object;
            _objectTypeInfo = _object
                .GetType()
                .GetTypeInfo();
        }

        public object Invoke(string methodName, params object[] @params)
        {
            // The following should be done once since this does some reflection
            var method = _objectTypeInfo
                .GetDeclaredMethod(methodName);

            var result = method?.Invoke(_object, @params);
            return result;
        }
    }
}
