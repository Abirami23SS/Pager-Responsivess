using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Specifies the option to expand single or multiple panel at a time in the <see cref="SfAccordion"/> component.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ExpandMode
    {
        /// <summary>
        /// Allows expanding only a single panel.
        /// </summary>
        [EnumMember(Value = "Single")]
        Single,

        /// <summary>
        /// Allows expanding multiple panels.
        /// </summary>
        [EnumMember(Value = "Multiple")]
        Multiple
    }
}