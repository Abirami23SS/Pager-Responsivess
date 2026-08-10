using System.Reflection;

namespace Syncfusion.Blazor.Navigations.Internal
{
    internal static class Utils
    {
        public static T GetItemProperties<T, TItem>(TItem item, string propName)
        {
            if (ReflectionExtension.TryGetValue(item, propName, false, out object value))
                return (T)value;
            return default;
        }
    }
}
