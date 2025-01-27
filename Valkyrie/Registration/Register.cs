﻿/*
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

using BigBook.Registration;
using Canister.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Valkyrie.Registration
{
    /// <summary>
    /// Registration extension methods
    /// </summary>
    public static class Registration
    {
        /// <summary>
        /// Registers the library with the bootstrapper.
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        /// <returns>The bootstrapper</returns>
        public static ICanisterConfiguration? RegisterValkyrie(this ICanisterConfiguration? bootstrapper)
        {
            return bootstrapper?.AddAssembly(typeof(IsAttribute).GetTypeInfo().Assembly)
                               .RegisterBigBookOfDataTypes();
        }

        /// <summary>
        /// Registers the Valkyrie library with the service collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <returns>The service collection with Valkyrie registered.</returns>
        public static IServiceCollection? RegisterValkyrie(this IServiceCollection? services) => services?.RegisterBigBookOfDataTypes();
    }
}