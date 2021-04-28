using CTF.Framework.TestRunner;
using System;

namespace SandBox
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string assemblyPath = @"C:\Personal Projects\C# masterclass course\Previous Exam\01.CTF.Framework\Calculator.Tests\bin\Debug\netcoreapp3.0\Calculator.Tests.dll";
            Runner runner = new Runner();
            string result = runner.Run(assemblyPath);
            Console.WriteLine(result);
        }
    }
}
