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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Valkyrie
{
    /// <summary>
    /// Cascade attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class CascadeAttribute : ValidationAttribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ErrorMessage">Error message</param>
        public CascadeAttribute(string ErrorMessage = "")
            : base(string.IsNullOrEmpty(ErrorMessage) ? "The following errors have occurred on property {0}:" + System.Environment.NewLine + "{1}" : ErrorMessage)
        {
        }

        /// <summary>
        /// Determines if the property is valid
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <param name="validationContext">Validation context</param>
        /// <returns>The validation result</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Results = new List<ValidationResult>();
            if (!value.TryValidate(Results))
            {
                return new ValidationResult(string.Format(CultureInfo.InvariantCulture, ErrorMessageString, validationContext.DisplayName, Results.ForEach(x => x.ErrorMessage).ToString(x => x, System.Environment.NewLine)));
            }
            return ValidationResult.Success;
        }
    }
}