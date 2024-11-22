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

using BigBook;
using BigBook.Comparison;
using ObjectCartographer;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Valkyrie
{
    /// <summary>
    /// Does not contain attribute
    /// </summary>
    /// <remarks>Constructor</remarks>
    /// <param name="value">Value to check for</param>
    /// <param name="errorMessage">Error message</param>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class DoesNotContainAttribute(object value, string errorMessage = "")
        : ValidationAttribute(string.IsNullOrEmpty(errorMessage) ? "{0} contains {1}" : errorMessage)
    {
        /// <summary>
        /// Value to compare to
        /// </summary>
        public IComparable Value { get; } = (IComparable)value;

        /// <summary>
        /// Formats the error message
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>The formatted string</returns>
        public override string FormatErrorMessage(string name) => string.Format(CultureInfo.InvariantCulture, ErrorMessageString, name, Value.ToString());

        /// <summary>
        /// Determines if the property is valid
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns>The validation result</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;
            var Comparer = new GenericEqualityComparer<IComparable>();
            if (value is not IEnumerable ValueList)
                return ValidationResult.Success;
            IComparable? ValueTemp = 0;
            foreach (IComparable Item in ValueList)
            {
                ValueTemp = Value.To(Item.GetType(), null) as IComparable;
                break;
            }
            foreach (IComparable Item in ValueList)
            {
                if (Comparer.Equals(Item, ValueTemp!))
                    return new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? ""));
            }
            return ValidationResult.Success;
        }
    }
}