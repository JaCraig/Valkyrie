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
using Valkyrie.Enums;
using Valkyrie.ExtensionMethods;
using Valkyrie.Tests.BaseClasses;
using Xunit;

namespace Valkyrie.Tests.Rules
{
    public class CompareToClass
    {
        [CompareTo("ItemB", ComparisonType.Equal)]
        public int ItemA { get; set; }

        public int ItemB { get; set; }

        public object ObjectProp { get; set; }

        [CompareTo("ObjectProp", ComparisonType.Same)]
        public object ObjectProp2 { get; set; }
    }

    public class CompareToTests : TestingDirectoryFixture
    {
        [Fact]
        public void Test()
        {
            var Temp = new CompareToClass
            {
                ItemA = 1,
                ItemB = 1
            };
            Temp.ObjectProp = Temp.ObjectProp2 = new object();
            Temp.Validate();
            Temp.ObjectProp = Temp.ObjectProp2 = null;
            Temp.Validate();
            Temp.ObjectProp = new object();
            Assert.Throws<ValidationException>(() => Temp.Validate());
            Temp.ObjectProp = null;
            Temp.ItemA = 2;
            Assert.Throws<ValidationException>(() => Temp.Validate());
        }
    }
}