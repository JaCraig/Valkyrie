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
using BigBook.ExtensionMethods;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;
using Valkyrie.Enums;

namespace Valkyrie
{
    /// <summary>
    /// Is attribute
    /// </summary>
    /// <remarks>Constructor</remarks>
    /// <param name="type">Validation type enum</param>
    /// <param name="errorMessage">Error message</param>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed partial class IsAttribute(IsValid type, string errorMessage = "")
        : ValidationAttribute(string.IsNullOrEmpty(errorMessage) ? "{0} is not {1}" : errorMessage)
    {
        /// <summary>
        /// Gets the type of validation to do.
        /// </summary>
        public IsValid Type { get; } = type;

        /// <summary>
        /// Gets the decimal regex.
        /// </summary>
        private static readonly Regex _DecimalRegex = CreateDecimalRegex();

        /// <summary>
        /// Gets the domain regex.
        /// </summary>
        private static readonly Regex _DomainRegex = CreateDomainRegex();

        /// <summary>
        /// Gets the integer regex.
        /// </summary>
        private static readonly Regex _IntegerRegex = CreateIntegerRegex();

        /// <summary>
        /// Formats the error message.
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>The formatted string</returns>
        public override string FormatErrorMessage(string name)
        {
            var ComparisonString = "";
            switch (Type)
            {
                case Enums.IsValid.CreditCard:
                    ComparisonString = "a credit card";
                    break;

                case Enums.IsValid.Decimal:
                    ComparisonString = "a decimal";
                    break;

                case Enums.IsValid.Domain:
                    ComparisonString = "a domain";
                    break;

                case Enums.IsValid.Integer:
                    ComparisonString = "an integer";
                    break;
            }

            return string.Format(CultureInfo.InvariantCulture, ErrorMessageString, name, ComparisonString);
        }

        /// <summary>
        /// Determines if the property is valid.
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns>The validation result</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var Tempvalue = value as string;
            if (string.IsNullOrEmpty(Tempvalue))
                return new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? ""));
            return Type switch
            {
                Enums.IsValid.CreditCard => Tempvalue.Is(StringCompare.CreditCard) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                Enums.IsValid.Decimal => _DecimalRegex.IsMatch(Tempvalue) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                Enums.IsValid.Domain => _DomainRegex.IsMatch(Tempvalue) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                Enums.IsValid.Integer => _IntegerRegex.IsMatch(Tempvalue) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                _ => ValidationResult.Success,
            };
        }

        [GeneratedRegex(@"^(\d+)+(\.\d+)?$|^(\d+)?(\.\d+)+$")]
        private static partial Regex CreateDecimalRegex();

        [GeneratedRegex(@"^(http|https|ftp)://([a-zA-Z0-9_-]*(?:\.[a-zA-Z0-9_-]*)+):?([0-9]+)?/?")]
        private static partial Regex CreateDomainRegex();

        [GeneratedRegex(@"^\d+$")]
        private static partial Regex CreateIntegerRegex();
    }
}