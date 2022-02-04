using System;
using System.Xml.Linq;
using net5_soap.Models;

namespace net5_soap.Services
{
    public class SampleService : ISampleService
    {
        public string Test(string s)
        {
            Console.WriteLine("Test Method Executed!");
            return s;
        }
        public void XmlMethod(XElement xml)
        {
            Console.WriteLine(xml.ToString());
        }
        public MyCustomModel TestCustomModel(MyCustomModel customModel)
        {
            return customModel;
        }
    }
}
