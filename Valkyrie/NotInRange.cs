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
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Valkyrie
{
    /// <summary>
    /// Not in range attribute
    /// </summary>
    /// <remarks>Constructor</remarks>
    /// <param name="min">Min value</param>
    /// <param name="max">Max value</param>
    /// <param name="errorMessage">Error message</param>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class NotInRangeAttribute(object min, object max, string errorMessage = "")
        : ValidationAttribute(string.IsNullOrEmpty(errorMessage) ? "{0} is between {1} and {2}" : errorMessage)
    {
        /// <summary>
        /// Max value to compare to
        /// </summary>
        public IComparable Max { get; } = (IComparable)max;

        /// <summary>
        /// Min value to compare to
        /// </summary>
        public IComparable Min { get; } = (IComparable)min;

        /// <summary>
        /// Formats the error message
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>The formatted string</returns>
        public override string FormatErrorMessage(string name) => string.Format(CultureInfo.InvariantCulture, ErrorMessageString, name, Min.ToString(), Max.ToString());

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
            var Comparer = new GenericComparer<IComparable>();
            var MaxValue = Max.To(value.GetType(), null) as IComparable;
            var MinValue = Min.To(value.GetType(), null) as IComparable;
            var TempValue = value as IComparable;
            return (Comparer.Compare(MaxValue!, TempValue!) >= 0
                    && Comparer.Compare(TempValue!, MinValue!) >= 0) ?
                new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")) :
                ValidationResult.Success;
        }
    }
}