namespace DeprecatedClass
{
    public class StartUp
    {
		//test
        public static void Main()
        {
            var summator = new Summator();
            var privateObject = new PrivateObject(summator);
            var result = privateObject.Invoke("GetSum", 12, 13);
        }
    }
}