using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
#nullable enable
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using Syncfusion.Blazor.Navigations;

namespace Syncfusion.Blazor.Tests.Navigations
{
    internal class PagerReflection
    {
        /// <summary>
        /// Invokes an internal method on the Pager component that returns void.
        /// </summary>
        /// <param name="pagerInstance">The SfPager component instance</param>
        /// <param name="methodName">Name of the internal method to invoke</param>
        /// <param name="parameters">Optional parameters to pass to the method</param>
        /// <remarks>
        internal static void InvokeVoidMethod(object pagerInstance, string methodName, params object?[] parameters)
        {
            if (pagerInstance == null)
                throw new ArgumentNullException(nameof(pagerInstance), "Pager instance cannot be null");

            var method = GetMethodInfo(pagerInstance.GetType(), methodName, parameters);
            if (method == null)
                throw new ArgumentException($"Internal method '{methodName}' not found on type '{pagerInstance.GetType().Name}'");

            method.Invoke(pagerInstance, parameters);
        }

        /// <summary>
        /// Invokes an internal method that returns a value (non-async).
        /// </summary>
        /// <typeparam name="T">Return type of the method</typeparam>
        /// <param name="pagerInstance">The SfPager component instance</param>
        /// <param name="methodName">Name of the internal method to invoke</param>
        /// <param name="parameters">Optional parameters to pass to the method</param>
        /// <returns>The return value of the method</returns>
        internal static T InvokeMethod<T>(object pagerInstance, string methodName, params object?[] parameters)
        {
            if (pagerInstance == null)
                throw new ArgumentNullException(nameof(pagerInstance), "Pager instance cannot be null");

            var method = GetMethodInfo(pagerInstance.GetType(), methodName, parameters);
            if (method == null)
                throw new ArgumentException($"Internal method '{methodName}' not found on type '{pagerInstance.GetType().Name}'");

            var result = method.Invoke(pagerInstance, parameters);
            return result != null ? (T)result : default!;
        }

        /// <summary>
        /// Invokes an internal async method that returns Task.
        /// </summary>
        /// <param name="pagerInstance">The SfPager component instance</param>
        /// <param name="methodName">Name of the internal async method to invoke</param>
        /// <param name="parameters">Optional parameters to pass to the method</param>
        /// <returns>Task that can be awaited</returns>
        internal static async Task InvokeAsyncMethod(object pagerInstance, string methodName, params object?[] parameters)
        {
            if (pagerInstance == null)
                throw new ArgumentNullException(nameof(pagerInstance), "Pager instance cannot be null");

            var method = GetMethodInfo(pagerInstance.GetType(), methodName, parameters);
            if (method == null)
                throw new ArgumentException($"Internal async method '{methodName}' not found on type '{pagerInstance.GetType().Name}'");

            var result = method.Invoke(pagerInstance, parameters);
            if (result is Task task)
            {
                await task.ConfigureAwait(false);
            }
            else
            {
                throw new InvalidOperationException($"Method '{methodName}' is not async or does not return a Task");
            }
        }

        /// <summary>
        /// Sets the value of an internal property on the Pager component.
        /// </summary>
        /// <param name="pagerInstance">The SfPager component instance</param>
        /// <param name="propertyName">Name of the internal property</param>
        /// <param name="value">The value to set</param>
        /// <remarks>
        internal static void SetInternalProperty(object pagerInstance, string propertyName, object? value)
        {
            if (pagerInstance == null)
                throw new ArgumentNullException(nameof(pagerInstance), "Pager instance cannot be null");

            var property = GetPropertyInfo(pagerInstance.GetType(), propertyName);
            if (property == null)
                throw new ArgumentException($"Internal property '{propertyName}' not found on type '{pagerInstance.GetType().Name}'");

            if (!property.CanWrite)
            {
                SetReadOnlyProperty(pagerInstance, property, value);
            }
            else
            {
                property.SetValue(pagerInstance, value);
            }
        }

        /// <summary>
        /// Gets method info for an internal method with optional parameter matching.
        /// </summary>
        /// <param name="type">The type containing the method</param>
        /// <param name="methodName">Name of the internal method</param>
        /// <param name="parameters">Optional parameters to match method signature</param>
        /// <returns>MethodInfo or null if not found</returns>
        private static MethodInfo? GetMethodInfo(Type type, string methodName, object?[] parameters)
        {
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;

            // Try to find exact method match with parameters
            if (parameters.Length > 0)
            {
                var paramTypes = parameters.Select(p => p?.GetType() ?? typeof(object)).ToArray();
                var method = type.GetMethod(methodName, flags, null, paramTypes, null);
                if (method != null)
                    return method;
            }

            // Fallback to finding method by name only
            var methods = type.GetMethods(flags).Where(m => m.Name.Equals(methodName, StringComparison.OrdinalIgnoreCase)).ToArray();
            return methods.FirstOrDefault();
        }

        /// <summary>
        /// Gets property info for an internal property.
        /// </summary>
        /// <param name="type">The type containing the property</param>
        /// <param name="propertyName">Name of the internal property</param>
        /// <returns>PropertyInfo or null if not found</returns>
        private static PropertyInfo? GetPropertyInfo(Type type, string propertyName)
        {
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;
            return type.GetProperty(propertyName, flags);
        }

        /// <summary>
        /// Sets a read-only property by finding and setting its backing field.
        /// </summary>
        /// <param name="obj">The object instance</param>
        /// <param name="property">The property info</param>
        /// <param name="value">The value to set</param>
        private static void SetReadOnlyProperty(object obj, PropertyInfo property, object? value)
        {
            var type = obj.GetType();
            const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;

            // Try common backing field patterns
            var backingFieldPatterns = new[]
            {
                $"<{property.Name}>k__BackingField",  // Auto-property backing field
                $"_{property.Name}",                    // _PropertyName
                $"m_{property.Name}",                   // m_PropertyName
                $"__{property.Name}",                   // __PropertyName
            };

            foreach (var fieldName in backingFieldPatterns)
            {
                var field = type.GetField(fieldName, flags);
                if (field != null)
                {
                    field.SetValue(obj, value);
                    return;
                }
            }

            // If no backing field found, try using reflection to bypass setter
            var setMethod = property.GetSetMethod(true);
            if (setMethod != null)
            {
                setMethod.Invoke(obj, new[] { value });
            }
            else
            {
                throw new InvalidOperationException($"Cannot set read-only property '{property.Name}' on type '{type.Name}'");
            }
        }
    }
}