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

using System.ComponentModel.DataAnnotations;
using Valkyrie.ExtensionMethods;
using Valkyrie.Tests.BaseClasses;
using Xunit;

namespace Valkyrie.Tests.Rules
{
    public class IsClass
    {

/* Unmerged change from project 'Valkyrie.Tests (net9.0)'
Before:
        [Is(Valkyrie.IsValid.CreditCard)]
        public string ItemA { get; set; }
After:
        [Is(IsValid.CreditCard)]
        public string ItemA { get; set; }
*/
        [Is(Enums.IsValid.CreditCard)]
        public string ItemA { get; set; }


/* Unmerged change from project 'Valkyrie.Tests (net9.0)'
Before:
        [Is(Valkyrie.IsValid.Decimal)]
        public string ItemB { get; set; }
After:
        [Is(IsValid.Decimal)]
        public string ItemB { get; set; }
*/
        [Is(Enums.IsValid.Decimal)]
        public string ItemB { get; set; }


/* Unmerged change from project 'Valkyrie.Tests (net9.0)'
Before:
        [Is(Valkyrie.IsValid.Domain)]
        public string ItemC { get; set; }
After:
        [Is(IsValid.Domain)]
        public string ItemC { get; set; }
*/
        [Is(Enums.IsValid.Domain)]
        public string ItemC { get; set; }


/* Unmerged change from project 'Valkyrie.Tests (net9.0)'
Before:
        [Is(Valkyrie.IsValid.Integer)]
        public string ItemD { get; set; }
After:
        [Is(IsValid.Integer)]
        public string ItemD { get; set; }
*/
        [Is(Enums.IsValid.Integer)]
        public string ItemD { get; set; }
    }

    public class IsTests : TestingDirectoryFixture
    {
        [Fact]
        public void Test()
        {
            var Temp = new IsClass
            {
                ItemA = "4012888888881881",
                ItemB = "1234.123",
                ItemC = "http://www.google.com",
                ItemD = "1234"
            };
            Temp.Validate();
            Temp.ItemA = "1234567890123";
            Temp.ItemB = "ASD1234";
            Temp.ItemC = "google@somewhere.com";
            Temp.ItemD = "123.4313";
            Assert.Throws<ValidationException>(() => Temp.Validate());
        }
    }
}