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
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Valkyrie
{
    /// <summary>
    /// Not empty attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class NotEmptyAttribute : ValidationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ErrorMessage">Error message</param>
        public NotEmptyAttribute(string ErrorMessage = "")
            : base(string.IsNullOrEmpty(ErrorMessage) ? "{0} is not empty" : ErrorMessage)
        {
        }

        /// <summary>
        /// Formats the error message
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>The formatted string</returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.InvariantCulture, ErrorMessageString, name);
        }

        /// <summary>
        /// Determines if the property is valid
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns>The validation result</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? ""));
            var ValueList = value as IEnumerable;
            return ValueList?.GetEnumerator().MoveNext() == true ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? ""));
        }
    }
}