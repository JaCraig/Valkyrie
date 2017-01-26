# Valkyrie

[![Build status](https://ci.appveyor.com/api/projects/status/tscmnl7u4gw19iad?svg=true)](https://ci.appveyor.com/project/JaCraig/valkyrie)

Valkyrie is a validation library for .Net. It supports .Net Core, .Net 4.6, etc.

## Basic Usage

The system relies on an IoC wrapper called [Canister](https://github.com/JaCraig/Canister). While Canister has a built in IoC container, it's purpose is to actually wrap your container of choice in a way that simplifies setup and usage for other libraries that don't want to be tied to a specific IoC container. As such you must set up Canister in order to use Valkyrie:

    Canister.Builder.CreateContainer(new List<ServiceDescriptor>())
                    .RegisterValkyrie()
                    .Build();
					
For the most part it's a collection of DataAnnotations and as such you just need to add them to your properties like this:

    public class MyClass
    {
        [Cascade]
        public MyOtherClass ItemA { get; set; }
		
		[Between("1/1/1900", "1/1/2100")]
        public DateTime ItemB { get; set; }
		
		[Contains("A")]
        public List<string> ItemC { get; set; }
    }
	
The System.ComponentModel.DataAnnotations.Validator class will automatically pick them up when validating your object. Also there are a couple of extension methods that are added to simplify validating an object:

    var Results = new List<ValidationResult>();
    bool DidItWork = MyObject.TryValidate(Results);
	
and:

    MyObject.Validate();
	
TryValidate will return the list of validation results while Validate will simply throw a ValidationException if there is an issue with validating your object.

## Installation

The library is available via Nuget with the package name "Valkyrie". To install it run the following command in the Package Manager Console:

Install-Package Valkyrie

## Build Process

In order to build the library you will require the following as a minimum:

1. Visual Studio 2015 with Update 3
2. .Net Core 1.0 SDK

Other than that, just clone the project and you should be able to load the solution and build without too much effort.