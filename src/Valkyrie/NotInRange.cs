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
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Valkyrie
{
    /// <summary>
    /// Not in range attribute
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments"), AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class NotInRangeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Max">Max value</param>
        /// <param name="Min">Min value</param>
        /// <param name="ErrorMessage">Error message</param>
        public NotInRangeAttribute(object Min, object Max, string ErrorMessage = "")
            : base(string.IsNullOrEmpty(ErrorMessage) ? "{0} is between {1} and {2}" : ErrorMessage)
        {
            this.Min = (IComparable)Min;
            this.Max = (IComparable)Max;
        }

        /// <summary>
        /// Max value to compare to
        /// </summary>
        public IComparable Max { get; private set; }

        /// <summary>
        /// Min value to compare to
        /// </summary>
        public IComparable Min { get; private set; }

        /// <summary>
        /// Formats the error message
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>The formatted string</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.InvariantCulture, ErrorMessageString, name, Min.ToString(), Max.ToString());
        }

        /// <summary>
        /// Determines if the property is valid
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns>The validation result</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Comparer = new GenericComparer<IComparable>();
            var MaxValue = (IComparable)Max.To<object>(value.GetType());
            var MinValue = (IComparable)Min.To<object>(value.GetType());
            var TempValue = value as IComparable;
            return (Comparer.Compare(MaxValue, TempValue) >= 0
                    && Comparer.Compare(TempValue, MinValue) >= 0) ?
                new ValidationResult(FormatErrorMessage(validationContext.DisplayName)) :
                ValidationResult.Success;
        }
    }
}