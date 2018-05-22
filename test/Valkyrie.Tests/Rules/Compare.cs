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
using Valkyrie;

using Xunit;

namespace Valkyrie.Tests.Rules
{
    public class CompareClass
    {
        [Valkyrie.Compare(1, ComparisonType.Equal)]
        public int ItemA { get; set; }

        [Valkyrie.Compare(2.0f, ComparisonType.GreaterThan)]
        public float ItemB { get; set; }

        [Valkyrie.Compare("1/1/1900", ComparisonType.GreaterThanOrEqual)]
        public DateTime ItemC { get; set; }

        [Valkyrie.Compare("A", ComparisonType.LessThan)]
        public string ItemD { get; set; }

        [Valkyrie.Compare(0, ComparisonType.LessThanOrEqual)]
        public long ItemE { get; set; }

        [Valkyrie.Compare("1/1/2100", ComparisonType.NotEqual)]
        public DateTime ItemF { get; set; }

        [Valkyrie.Compare(double.NaN, ComparisonType.NotEqual)]
        public double NaNTest { get; set; }
    }

    public class CompareTests
    {
        [Fact]
        public void Test()
        {
            var Temp = new CompareClass
            {
                ItemA = 1,
                ItemB = 2.1f,
                ItemC = new DateTime(1900, 1, 1),
                ItemD = "a",
                ItemE = -1,
                ItemF = DateTime.Now,
                NaNTest = 1
            };
            Temp.Validate();
            Temp.ItemA = 2;
            Temp.NaNTest = double.NaN;
            Assert.Throws<ValidationException>(() => Temp.Validate());
        }
    }
}