# Valkyrie

[![.NET Publish](https://github.com/JaCraig/Valkyrie/actions/workflows/dotnet-publish.yml/badge.svg)](https://github.com/JaCraig/Valkyrie/actions/workflows/dotnet-publish.yml)

Valkyrie is a simple set of data annotations and extension methods to help with validating your objects in .NET.

## Basic Usage

For the most part, Valkyrie consists of DataAnnotations. To use Valkyrie, simply add the annotations to your properties as shown below:

```csharp
public class MyClass
{
    [Cascade]
    public MyOtherClass ItemA { get; set; }
	
    [Between("1/1/1900", "1/1/2100")]
    public DateTime ItemB { get; set; }
	
    [Contains("A")]
    public List<string> ItemC { get; set; }
}
```

The `System.ComponentModel.DataAnnotations.Validator` class will automatically pick up these annotations when validating your object. Additionally, there are a couple of extension methods added to simplify object validation:

```csharp
var results = new List<ValidationResult>();
bool didItWork = MyObject.TryValidate(results);
```

And:

```csharp
MyObject.Validate();
```

The `TryValidate` method returns a list of validation results, while the `Validate` method throws a `ValidationException` if there are any issues with validating your object.

## Installation

The library is available on NuGet with the package name "Valkyrie." To install it, run the following command in the Package Manager Console:

```
Install-Package Valkyrie
```

## Build Process

To build the library, make sure you have the following minimum requirements:

- Visual Studio 2022

Simply clone the project and load the solution in Visual Studio. You should be able to build it without much effort.

## Contributing

We welcome contributions to Valkyrie. If you find any issues or have suggestions for improvements, please open an issue or submit a pull request. We appreciate your support!
