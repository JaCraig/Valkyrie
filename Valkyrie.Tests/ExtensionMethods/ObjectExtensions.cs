/*
Copyright 2017 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Valkyrie.ExtensionMethods;
using Valkyrie.Tests.BaseClasses;
using Xunit;

namespace Valkyrie.Tests.Extensions
{
    public class ObjectExtensions : TestingDirectoryFixture
    {
        [Fact]
        public void ObjectValidationTest()
        {
            var dog = new Dog { Age = -1, Name = "Jim" };
            var validationResults = new List<ValidationResult>();
            Assert.False(dog.TryValidate(validationResults));
            Assert.Single(validationResults);
            Assert.Throws<ValidationException>(() => dog.Validate());
        }

        public class Dog
        {
            [Range(0, 20)]
            public int Age { get; set; }

            [Required]
            public string Name { get; set; }
        }
    }
}