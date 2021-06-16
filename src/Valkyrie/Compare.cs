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
    /// Compare attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class CompareAttribute : ValidationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Value">Value to compare to</param>
        /// <param name="Type">Comparison type to use</param>
        /// <param name="ErrorMessage">Error message</param>
        public CompareAttribute(object Value, ComparisonType Type, string ErrorMessage = "")
            : base(string.IsNullOrEmpty(ErrorMessage) ? "{0} is not {1} {2}" : ErrorMessage)
        {
            this.Value = Value;
            this.Type = Type;
        }

        /// <summary>
        /// Comparison type
        /// </summary>
        public ComparisonType Type { get; }

        /// <summary>
        /// Value to compare to
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// Formats the error message
        /// </summary>
        /// <param name="name">Property name</param>
        /// <returns>The formatted string</returns>
        public override string FormatErrorMessage(string name)
        {
            string ComparisonTypeString = "";
            switch (Type)
            {
                case ComparisonType.Equal:
                    ComparisonTypeString = "equal";
                    break;

                case ComparisonType.GreaterThan:
                    ComparisonTypeString = "greater than";
                    break;

                case ComparisonType.GreaterThanOrEqual:
                    ComparisonTypeString = "greater than or equal";
                    break;

                case ComparisonType.LessThan:
                    ComparisonTypeString = "less than";
                    break;

                case ComparisonType.LessThanOrEqual:
                    ComparisonTypeString = "less than or equal";
                    break;

                case ComparisonType.NotEqual:
                    ComparisonTypeString = "not equal";
                    break;
            }

            return string.Format(CultureInfo.InvariantCulture, ErrorMessageString, name, ComparisonTypeString, Value.ToString());
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
            var Value2 = Value.To(value?.GetType() ?? typeof(object), null) as IComparable;
            var TempValue = value as IComparable;
            return Type switch
            {
                ComparisonType.Same => ReferenceEquals(value, Value) ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                ComparisonType.Equal => Comparer.Compare(TempValue!, Value2!) == 0 ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                ComparisonType.NotEqual => Comparer.Compare(TempValue!, Value2!) != 0 ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                ComparisonType.GreaterThan => Comparer.Compare(TempValue!, Value2!) > 0 ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                ComparisonType.GreaterThanOrEqual => Comparer.Compare(TempValue!, Value2!) >= 0 ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                ComparisonType.LessThan => Comparer.Compare(TempValue!, Value2!) < 0 ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                ComparisonType.LessThanOrEqual => Comparer.Compare(TempValue!, Value2!) <= 0 ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext?.DisplayName ?? "")),

                _ => ValidationResult.Success,
            };
        }
    }
}