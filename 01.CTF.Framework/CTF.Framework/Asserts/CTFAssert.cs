namespace CTF.Framework.Asserts
{
    using CTF.Framework.Exceptions;
    using CTF.Framework.Extensions;
    using System;
    using System.Linq;
    using System.Reflection;

    // ReSharper disable once InconsistentNaming
    public abstract class CTFAssert
    {
        public static void AreEqual(object a, object b)
        {
            if (a?.GetType() != b?.GetType()
                || (a != null && b == null) || (a == null && b != null))
            {
                throw new TestException();
            }

            if (ReferenceEquals(a, b) || (a == null && b == null))
            {
                return;
            }

            if (a.IsPrimitiveType())
            {
                if (a.Equals(b))
                {
                    return;
                }

                throw new TestException();
            }

            var propertiesOfA = a
                .GetType()
                .GetProperties();
            var propertiesOfB = b
                .GetType()
                .GetProperties();

            //Primitive properties comparisons
            var primitivePropertiesFromA = propertiesOfA.GetPrimitiveProperties();
            var primitivePropertiesFromB = propertiesOfB.GetPrimitiveProperties();

            foreach (var propertyFromA in primitivePropertiesFromA)
            {
                var propertyFromB = primitivePropertiesFromB
                    .First(prop => prop.Name == propertyFromA.Name);

                var valueFromA = propertyFromA.GetValue(a);
                var valueFromB = propertyFromB.GetValue(b);

                if (!valueFromA.Equals(valueFromB))
                {
                    throw new TestException();
                }
            }

            if (propertiesOfA.Length == primitivePropertiesFromA.Count())
            {
                return;
            }

            var nonPrimitivePropertiesFromA = propertiesOfA.GetNonPrimitiveProperties();
            var nonPrimitivePropertiesFromB = propertiesOfB.GetNonPrimitiveProperties();

            foreach (var propertyA in nonPrimitivePropertiesFromB)
            {
                var propertyFromB = primitivePropertiesFromB
                    .First(prop => prop.Name == propertyA.Name);

                var valueFromA = propertyA.GetValue(a);
                var valueFromB = propertyFromB.GetValue(b);

                AreEqual(valueFromA, valueFromB);
            }
        }

        public static void AreNotEqual(object a, object b)
        {
            if (a?.GetType() != b?.GetType()
                || (a != null && b == null) || (a == null && b != null))
            {
                return;
            }

            if (ReferenceEquals(a, b) || (a == null && b == null))
            {
                throw new TestException();
            }

            if (a.IsPrimitiveType())
            {
                if (!a.Equals(b))
                {
                    return;
                }

                throw new TestException();
            }

            var propertiesOfA = a
                .GetType()
                .GetProperties();
            var propertiesOfB = b
                .GetType()
                .GetProperties();

            //Primitive properties comparisons
            var primitivePropertiesFromA = propertiesOfA.GetPrimitiveProperties();
            var primitivePropertiesFromB = propertiesOfB.GetPrimitiveProperties();

            foreach (var propertyFromA in primitivePropertiesFromA)
            {
                var propertyFromB = primitivePropertiesFromB
                    .First(prop => prop.Name == propertyFromA.Name);

                var valueFromA = propertyFromA.GetValue(a);
                var valueFromB = propertyFromB.GetValue(b);

                if (valueFromA.Equals(valueFromB))
                {
                    throw new TestException();
                }
            }

            if (propertiesOfA.Length == primitivePropertiesFromA.Count())
            {
                return;
            }

            var nonPrimitivePropertiesFromA = propertiesOfA.GetNonPrimitiveProperties();
            var nonPrimitivePropertiesFromB = propertiesOfB.GetNonPrimitiveProperties();

            foreach (var propertyA in nonPrimitivePropertiesFromB)
            {
                var propertyFromB = primitivePropertiesFromB
                    .First(prop => prop.Name == propertyA.Name);

                var valueFromA = propertyA.GetValue(a);
                var valueFromB = propertyFromB.GetValue(b);

                AreNotEqual(valueFromA, valueFromB);
            }
        }

        public static void Throws<T>(Func<bool> condition)
            where T: Exception
        {
            try
            {
                condition.Invoke();
            }
            catch (T)
            {
                return;
            }
            catch(Exception)
            {
                throw new TestException();
            }
        }
    }
}