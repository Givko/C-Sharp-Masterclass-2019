using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CTF.Framework.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<PropertyInfo> GetPrimitiveProperties(this PropertyInfo[] properties)
        {
            var primitiveProperties = properties
                .Where(p =>
                {
                    var type = p.GetType();

                    return type.IsPrimitive
                    || type == typeof(string)
                    || type == typeof(DateTime)
                    || type == typeof(TimeSpan);
                });

            return primitiveProperties;
        }

        public static IEnumerable<PropertyInfo> GetNonPrimitiveProperties(this PropertyInfo[] properties)
        {
            var primitiveProperties = properties
                .Where(p =>
                {
                    var type = p.GetType();

                    return !type.IsPrimitive
                    && type != typeof(string)
                    && type != typeof(DateTime)
                    && type != typeof(TimeSpan);
                });

            return primitiveProperties;
        }


        public static bool IsPrimitiveType(this object @object)
        {
            var type = @object.GetType();

            return type.IsPrimitive
            || type == typeof(string)
            || type == typeof(DateTime)
            || type == typeof(TimeSpan);
        }
    }
}
