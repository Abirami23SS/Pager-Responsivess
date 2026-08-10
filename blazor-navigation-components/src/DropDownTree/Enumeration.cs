using System.Text.Json.Serialization;
using System.Runtime.Serialization;


namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents the visual mode options for the <see cref="SfDropDownTree{TValue,TItem}"/> component.
    /// </summary>
    /// <value>
    /// The mode to display the selected items. The default value is <c>Default</c>
    /// </value>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DdtVisualMode
    {
        /// <summary>
        /// Defines the Default visual mode.
        /// </summary>
        /// <value>
        ///  When focused, the component will act in the box mode. When blurred, the component will act in the delimiter mode.
        /// </value>
        [EnumMember(Value = "Default")]
        Default,

        /// <summary>
        /// Defines the Delimiter visual mode.
        /// </summary>
        /// <value>
        /// Selected items will be visualized in the text content.
        /// </value>
        [EnumMember(Value = "Delimiter")]
        Delimiter,

        /// <summary>
        /// Defines the Box visual mode.
        /// </summary>
        /// <value>
        /// Selected items will be visualized in chip format.
        /// </value>
        [EnumMember(Value = "Box")]
        Box,

        /// <summary>
        /// Defines the Custom visual mode.
        /// </summary>
        /// <value>
        /// Selected items will be visualized with the given custom template value. The given custom template is added to the input instead of the selected item text.
        /// </value>
        [EnumMember(Value = "Custom")]
        Custom
    }

    /// <summary>
    /// Specifies the change event action of the <see cref="SfDropDownTree{TValue,TItem}"/> component.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DdtAction
    {
        /// <summary>
        /// Specifies the item selection in <see cref="SfDropDownTree{TValue,TItem}"/> component.
        /// </summary>
        /// <value>
        /// “Select” if an item is selected.
        /// </value>
        [EnumMember(Value = "select")]
        Select,

        /// <summary>
        /// Specifies the item deselection in <see cref="SfDropDownTree{TValue,TItem}"/> component.
        /// </summary>
        /// <value>
        /// “Unselect” if an item is deselected.
        /// </value> 
        [EnumMember(Value = "unselect")]
        Unselect
    }
}
