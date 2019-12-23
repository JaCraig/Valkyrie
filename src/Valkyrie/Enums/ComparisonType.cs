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

namespace Valkyrie
{
    /// <summary>
    /// Comparison types
    /// </summary>
    public enum ComparisonType
    {
        /// <summary>
        /// Equal
        /// </summary>
        Equal,

        /// <summary>
        /// Not equal
        /// </summary>
        NotEqual,

        /// <summary>
        /// Greater than
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Greater than or equal
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Less than
        /// </summary>
        LessThan,

        /// <summary>
        /// Less than or equal
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        /// The same reference
        /// </summary>
        Same
    }
}