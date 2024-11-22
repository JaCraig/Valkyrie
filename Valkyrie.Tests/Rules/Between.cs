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

using System;
using System.ComponentModel.DataAnnotations;
using Valkyrie.ExtensionMethods;
using Valkyrie.Tests.BaseClasses;
using Xunit;

namespace Valkyrie.Tests.Rules
{
    public class BetweenTests : TestingDirectoryFixture
    {
        [Fact]
        public void Test()
        {
            var Temp = new ClassA
            {
                ItemA = 1,
                ItemB = DateTime.Now
            };
            Temp.Validate();
            Temp.ItemA = 0;
            Temp.ItemB = new DateTime(1800, 1, 1);
            Assert.Throws<ValidationException>(() => Temp.Validate());
        }
    }

    public class ClassA
    {
        [Between(1, 5)]
        public int ItemA { get; set; }

        [Between("1/1/1900", "1/1/2100")]
        public DateTime ItemB { get; set; }
    }
}