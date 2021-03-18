# FileDownloader

Test project which experiments with an ASP.NET Core API capable of returning multiple content types on the 
same route and the associated Swagger support for these scenarios. Uses:

- `Swashbuckle.AspNetCore`
- `Swashbuckle.AspNetCore.Annotations`

Interesting discussion on this subject:

https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1691

The philosophy of swashbuckle is that their aim is to generate API 
descriptions according to how the application *actually* behaves in reality. 
In other words, the implementation drives the documentation.

PR which will add some interesting functionality to Swashbuckle.AspNetCore.Annotations:

https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/1956

## application/octet-stream

By default Swagger will not list `application/octet-stream` as a possible content type. I therefore
added 

```csharp
[Produces("application/octet-stream")]
```

To my action, but in the case where I returned JSON (serialization of error details), there was an error
as ASP.NET couldn't find the appropriate serializer. I therefore extended the attribute to :

```csharp
[Produces("application/octet-stream", "application/json","text/plain", "text/json")] 
```
This then covers all the bases. It should be stressed that I did this only to have a nice swagger. If you
omit completely the `Produces` attribute the `application/octet-stream` is handled perfectly.