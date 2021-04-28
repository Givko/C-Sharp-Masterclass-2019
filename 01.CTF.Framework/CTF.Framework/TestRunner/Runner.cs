namespace CTF.Framework.TestRunner
{
    using CTF.Framework.Attributes;
    using CTF.Framework.Exceptions;
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public class Runner
    {
        private readonly StringBuilder stringBuilder;
        private const string TEST_PASSED_MESSAGE = "Class: {testClassName} Method: {testMethodName} - passed!";
        private const string TEST_FAILED_MESSAGE = "Class: {testClassName} Method: {testMethodName} - failed!";
        private const string CLASS_NAME_PLACEHOLDER = "{testClassName}";
        private const string METHOD_NAME_PLACEHOLDER = "{testMethodName}";

        public Runner()
        {
            stringBuilder = new StringBuilder();
        }

        public string Run(string assemblyPath)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var classes = assembly.GetTypes();
            var testClasses = classes
                .Where(c => c?.GetCustomAttribute<CTFTestClassAttribute>() != null);
            var testMethodsResults = testClasses
                .SelectMany(c =>
                {
                    var classInstance = Activator.CreateInstance(c);
                    var testMethodsInvokations = c
                    .GetMethods()
                    .Where(m => m?.GetCustomAttribute<CTFTestMethodAttribute>() != null)
                    .Select(testMethod =>
                    {
                        try
                        {
                            testMethod?.Invoke(classInstance, Type.EmptyTypes);
                            return TEST_PASSED_MESSAGE
                            .Replace(CLASS_NAME_PLACEHOLDER, c.Name)
                            .Replace(METHOD_NAME_PLACEHOLDER, testMethod.Name);
                        }
                        catch (Exception e)
                        {
                            return TEST_FAILED_MESSAGE
                            .Replace(CLASS_NAME_PLACEHOLDER, c.Name)
                            .Replace(METHOD_NAME_PLACEHOLDER, testMethod.Name);
                        }
                    });

                    return testMethodsInvokations;
                });

            foreach (var result in testMethodsResults)
            {
                stringBuilder.AppendLine(result);
            }

            return stringBuilder.ToString();
        }
    }
}