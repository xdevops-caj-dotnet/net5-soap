[TOC]

# Create SOAP web service in .NET 5

## Create SOAP web service via SoapCore

Tools:
- Visual Studio for Mac
- .NET 5

### Create project
Create a web project via Visual Studio and uncheck `https`.

### Add SoapCore NuGet package
Add `SoapCore` NuGet package.

### Create Model class
Create `Models` folder and create `MyCustomModel` class:

```c#
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace net5_soap.Models
{
    [DataContract]
    public class MyCustomModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
}
```

### Create service interface and class

Create `Services` folder and create `ISampleService` interface:

```c#
using System.ServiceModel;
using net5_soap.Models;

namespace net5_soap.Services
{
    [ServiceContract]
    public interface ISampleService
    {
        [OperationContract]
        string Test(string s);
        [OperationContract]
        void XmlMethod(System.Xml.Linq.XElement xml);
        [OperationContract]
        MyCustomModel TestCustomModel(MyCustomModel inputModel);
    }
}

```

Create the implementation class `SampleService`:

```c#
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
```

### Integrate SOAP in Startup class

Modify `Startup` class, add below statements in `ConfigureServices()` method:

```c#
services.AddSoapCore();
services.TryAddSingleton<SampleService>();
services.AddMvc();
```

Add below statements in `Configure()` method:

```c#
app.UseSoapEndpoint<SampleService>("/Service.asmx", new SoapEncoderOptions());
```



## Test SOAP web service

Run the applciation , and access <http://localhost:5000/Service.asmx> to view the WSDL of the SOAP web service.

Use [SoapUI](https://www.soapui.org/) or [Postman](https://www.postman.com/) to test SOAP web service.

Use Postman to test the SOAP web service:

```bash
POST http://localhost:5000/Service.asmx
```

### Test operation

Request body:

```xml
<?xml version="1.0" encoding="utf-8"?>
<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">
  <soap:Body>
    <Test xmlns="http://tempuri.org/">
      <s>Hello SOAP</s>
    </Test>
  </soap:Body>
</soap:Envelope>
```



Response body:

```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <s:Body>
    <TestResponse xmlns="http://tempuri.org/">
      <TestResult>Hello SOAP</TestResult>
    </TestResponse>
  </s:Body>
</s:Envelope>
```



### TestCustomModel operation

Request body:

```xml
<?xml version="1.0" encoding="utf-8"?>
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:tem="http://tempuri.org/" xmlns:net5="http://schemas.datacontract.org/2004/07/net5_soap.Models">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:TestCustomModel>
         <tem:inputModel>
            <!--Optional:-->
            <net5:Email>william@example.com</net5:Email>
            <!--Optional:-->
            <net5:Id>456</net5:Id>
            <!--Optional:-->
            <net5:Name>William</net5:Name>
         </tem:inputModel>
      </tem:TestCustomModel>
   </soapenv:Body>
</soapenv:Envelope>
```



Response body:

```xml
<s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <s:Body>
    <TestCustomModelResponse xmlns="http://tempuri.org/">
      <TestCustomModelResult xmlns:d4p1="http://schemas.datacontract.org/2004/07/net5_soap.Models" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
        <d4p1:Email>william@example.com</d4p1:Email>
        <d4p1:Id>456</d4p1:Id>
        <d4p1:Name>William</d4p1:Name>
      </TestCustomModelResult>
    </TestCustomModelResponse>
  </s:Body>
</s:Envelope>
```





## References

- [HOW TO CREATE SOAP WEB SERVICES IN DOTNET CORE](https://dottutorials.net/creating-soap-web-services-dot-net-core-tutorial/)
- [SoapCore](https://github.com/DigDes/SoapCore)
- [SoapUI | SOAP Test](https://www.soapui.org/getting-started/soap-test/)

