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

namespace Valkyrie
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
        /// <typeparam name="ObjectType">Object type</typeparam>
        /// <param name="Object">Object to validate</param>
        /// <param name="Results">Results list</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool TryValidate<ObjectType>(this ObjectType Object, ICollection<ValidationResult> Results)
        {
            if (ReferenceEquals(Object, null))
                return true;
            return Validator.TryValidateObject(Object, new ValidationContext(Object, null, null), Results, true);
        }

        /// <summary>
        /// Determines if the object is valid
        /// </summary>
        /// <typeparam name="ObjectType">Object type</typeparam>
        /// <param name="Object">Object to validate</param>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException"/>
        public static void Validate<ObjectType>(this ObjectType Object)
        {
            if (ReferenceEquals(Object, null))
                return;
            Validator.ValidateObject(Object, new ValidationContext(Object, null, null), true);
        }
    }
}