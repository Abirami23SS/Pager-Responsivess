using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Specifies the display mode for the Toolbar component when the content exceeds the available space.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OverflowMode
    {
        /// <summary>
        ///  Displays all the elements in a single line with horizontal scrolling enabled.
        /// </summary>
        [EnumMember(Value = "Scrollable")]
        Scrollable,

        /// <summary>
        /// Displays prioritized elements on the Toolbar and moves the rest of the elements to a popup.
        /// </summary>
        [EnumMember(Value = "Popup")]
        Popup,

        /// <summary>
        ///  Displays the overflow toolbar items as an in-line of the toolbar.
        /// </summary>
        [EnumMember(Value = "MultiRow")]
        MultiRow,

        /// <summary>
        /// Hides the overflowing toolbar items in the next row and shows them when clicking the expand icons. If the popup content overflows the height of the page, the rest of the elements will be hidden.
        /// </summary>
        [EnumMember(Value = "Extended")]
        Extended
    }

    /// <summary>
    /// Specifies where the text is displayed in the popup mode of the Toolbar.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DisplayMode
    {
        /// <summary>
        /// Displays the text on the Toolbar and popup.
        /// </summary>
        [EnumMember(Value = "Both")]
        Both,

        /// <summary>
        /// Displays the text only when the content overflows to the popup.
        /// </summary>
        [EnumMember(Value = "Overflow")]
        Overflow,

        /// <summary>
        ///  Displays the text only on the Toolbar.
        /// </summary>
        [EnumMember(Value = "Toolbar")]
        Toolbar
    }

    /// <summary>
    /// Specifies the alignment of the Toolbar items.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ItemAlign
    {
        /// <summary>
        ///  Aligns the commands to the left side of the Toolbar.
        /// </summary>
        [EnumMember(Value = "Left")]
        Left,

        /// <summary>
        ///  Aligns the commands at the center of the Toolbar.
        /// </summary>
        [EnumMember(Value = "Center")]
        Center,

        /// <summary>
        ///  Aligns the commands to the right side of the Toolbar.
        /// </summary>
        [EnumMember(Value = "Right")]
        Right
    }

    /// <summary>
    /// Specifies the element types supported by the Toolbar component.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ItemType
    {
        /// <summary>
        /// Creates a Button control with its given properties like text, prefixIcon, etc.
        /// </summary>
        [EnumMember(Value = "Button")]
        Button,

        /// <summary>
        /// Adds a horizontal line that separates the Toolbar commands.
        /// </summary>
        [EnumMember(Value = "Separator")]
        Separator,

        /// <summary>
        /// Adds a space between the Toolbar items. This can be achieved by using the flex-grow property as 1.
        /// </summary>
        [EnumMember(Value = "Spacer")]
        Spacer,

        /// <summary>
        /// Creates an input element that is applicable to template rendering with Syncfusion controls like DropDownList, AutoComplete, etc.
        /// </summary>
        [EnumMember(Value = "Input")]
        Input
    }

    /// <summary>
    /// Specifies the display area of the Toolbar item when the Toolbar content overflows the available space. This option is applicable in the `Popup` mode.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OverflowOption
    {
        /// <summary>
        /// No priority for display, and as per the normal order, moves to the popup when the content exceeds.
        /// </summary>
        [EnumMember(Value = "None")]
        None,

        /// <summary>
        /// Always shows the item as the primary priority on the Toolbar.
        /// </summary>
        [EnumMember(Value = "Show")]
        Show,

        /// <summary>
        /// Always shows the item as the secondary priority on the popup.
        /// </summary>
        [EnumMember(Value = "Hide")]
        Hide
    }
}