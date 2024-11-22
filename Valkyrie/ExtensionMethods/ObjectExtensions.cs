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

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Valkyrie.ExtensionMethods
{
    /// <summary>
    /// Object extensions
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ObjectExtensions
    {
        /// <summary>
        /// Determines if the object is valid
        /// </summary>
        /// <typeparam name="TObjectType">Object type</typeparam>
        /// <param name="object">Object to validate</param>
        /// <param name="results">Results list</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool TryValidate<TObjectType>(this TObjectType @object, ICollection<ValidationResult> results)
        {
            if (@object is null)
                return true;
            return Validator.TryValidateObject(@object, new ValidationContext(@object, null, null), results, true);
        }

        /// <summary>
        /// Determines if the object is valid
        /// </summary>
        /// <typeparam name="TObjectType">Object type</typeparam>
        /// <param name="object">Object to validate</param>
        /// <exception cref="ValidationException"/>
        public static void Validate<TObjectType>(this TObjectType @object)
        {
            if (@object is null)
                return;
            Validator.ValidateObject(@object, new ValidationContext(@object, null, null), true);
        }
    }
}