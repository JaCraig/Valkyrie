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
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Valkyrie
{
    /// <summary>
    /// Is attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class IsAttribute : ValidationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Type">Validation type enum</param>
        /// <param name="ErrorMessage">Error message</param>
        public IsAttribute(IsValid Type, string ErrorMessage = "")
            : base(string.IsNullOrEmpty(ErrorMessage) ? "{0} is not {1}" : ErrorMessage)
        {
            this.Type = Type;
        }

        /// <summary>
        /// Type of validation to do
        /// </summary>
        public IsValid Type { get; private set; }

        /// <summary>
        /// Formats the error message
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>The formatted string</returns>
        public override string FormatErrorMessage(string name)
        {
            string ComparisonString = "";
            switch (Type)
            {
                case Valkyrie.IsValid.CreditCard:
                    ComparisonString = "a credit card";
                    break;

                case Valkyrie.IsValid.Decimal:
                    ComparisonString = "a decimal";
                    break;

                case Valkyrie.IsValid.Domain:
                    ComparisonString = "a domain";
                    break;

                case Valkyrie.IsValid.Integer:
                    ComparisonString = "an integer";
                    break;
            }

            return string.Format(CultureInfo.InvariantCulture, ErrorMessageString, name, ComparisonString);
        }

        /// <summary>
        /// Determines if the property is valid
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns>The validation result</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Tempvalue = value as string;
            switch (Type)
            {
                case Valkyrie.IsValid.CreditCard:
                    return Tempvalue.Is(StringCompare.CreditCard) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

                case Valkyrie.IsValid.Decimal:
                    return Regex.IsMatch(Tempvalue, @"^(\d+)+(\.\d+)?$|^(\d+)?(\.\d+)+$") ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

                case Valkyrie.IsValid.Domain:
                    return Regex.IsMatch(Tempvalue, @"^(http|https|ftp)://([a-zA-Z0-9_-]*(?:\.[a-zA-Z0-9_-]*)+):?([0-9]+)?/?") ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

                case Valkyrie.IsValid.Integer:
                    return Regex.IsMatch(Tempvalue, @"^\d+$") ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}