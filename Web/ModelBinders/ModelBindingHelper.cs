using System;
using System.Web.Mvc;

namespace Web.ModelBinders
{
    /// <summary>
    /// Aids model binding.
    /// </summary>
    public class ModelBindingHelper
    {
        /// <summary>
        /// Gets a strongly-typed value from the binding context using the model name.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="bindingContext">The binding context.</param>
        /// <param name="value">The value, if found.</param>
        /// <returns>Whether the value was found.</returns>
        public virtual bool TryGetValueFromBindingContext<T>(ModelBindingContext bindingContext, out T value)
        {
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext");

            return TryGetValueFromBindingContext(bindingContext, bindingContext.ModelName, out value);
        }

        /// <summary>
        /// Gets a strongly-typed value from the binding context.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="bindingContext">The binding context.</param>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <param name="value">The value, if found.</param>
        /// <returns>Whether the value was found.</returns>
        public virtual bool TryGetValueFromBindingContext<T>(ModelBindingContext bindingContext, string key, out T value)
        {
            if (bindingContext == null)
                throw new ArgumentNullException("bindingContext");

            // Set the default out value in case we return early.
            value = default(T);

            // If we have no key, then just stop now.
            if (string.IsNullOrEmpty(key))
                return false;

            // Try to get the value using the key with and without the model name prefix.
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);
            if (valueResult == null
                && (bindingContext.FallbackToEmptyPrefix || bindingContext.ModelName.Equals(key, StringComparison.OrdinalIgnoreCase)))
                valueResult = bindingContext.ValueProvider.GetValue(key);

            // If we didn't find the value, then stop now.
            if (valueResult == null)
                return false;

            // Try to convert the value that we found to the output type.
            try
            {
                value = (T)valueResult.ConvertTo(typeof(T));
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);
                return false;
            }

            return true;
        }
    }

}