# The SMITE Toolkit

Available at http://thesmitetoolkit.com

The SMITE Toolkit was created by Christopher Newport University student, Andrew Bell, for his senior capstone project.<br />
The website was created with one simple goal -- provide the SMITE gaming community with tools and data that help players improve their own gameplay!  

<hr />

## Files that have been omitted:

* ~/Models/Credentials.cs
  * This file contains the credentials for the SMITE developer API. File should be in the format:

```csharp
namespace NewSmiteToolkit.Models
{
    public static class Credentials
    {
        public static string DevId => "####";
        public static string AuthKey => "################################";
    }
}
```

* ~/Models/Credentials.cs
  * This file contains the connection strings for datastores. File should be in the format:
  
```xml
<connectionStrings>
  <clear />
  <add name="name" connectionString="connectionstring" providerName="provider" />
  ...
</connectionStrings>
```
