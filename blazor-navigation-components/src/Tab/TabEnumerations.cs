using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Enum for the different positions where a tab header can be placed.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum HeaderPosition
    {
        /// <summary>
        /// Places the tab header at the top.
        /// </summary>
        [EnumMember(Value = "Top")]
        Top,

        /// <summary>
        /// Places the tab header at the bottom.
        /// </summary>
        [EnumMember(Value = "Bottom")]
        Bottom,

        /// <summary>
        /// Places the tab header on the left.
        /// </summary>
        [EnumMember(Value = "Left")]
        Left,

        /// <summary>
        /// Places the tab header on the right.
        /// </summary>
        [EnumMember(Value = "Right")]
        Right
    }

    /// <summary>
    /// Enum for the different options for displaying tab content.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContentLoad
    {
        /// <summary>
        /// Only the content of the selected tab is loaded and available in the DOM, and it will be replaced with the corresponding content if the tab is selected dynamically.
        /// </summary>
        [EnumMember(Value = "Dynamic")]
        Dynamic,

        /// <summary>
        /// The content of all tabs is rendered on the initial load and maintained in the DOM.
        /// </summary>
        [EnumMember(Value = "Init")]
        Init,

        /// <summary>
        /// Only the content of the selected tab is loaded initially. The content of tabs that have been loaded once will be maintained in the DOM.
        /// </summary>
        [EnumMember(Value = "Demand")]
        Demand
    }

    /// <summary>
    /// Enables or disables the slide swiping action through Touch and Mouse.
    /// </summary>
    /// <remarks>
    /// The slide swiping is enabled or disabled using bitwise operators. The swiping is disabled using '~' bitwise operator.
    /// </remarks>
    /// <example>
    /// <code lang="Razor">
    /// <![CDATA[
    /// <SfTab SwipeMode="TabSwipeMode.Touch & TabSwipeMode.Mouse">
    /// </SfTab>
    /// ]]>
    /// </code>
    /// </example> 

    [Flags]
    public enum TabSwipeMode
    {
        /// <summary> 
        /// Enables or disables Touch swiping. 
        /// </summary> 
        Touch = 1 << 0,

        /// <summary> 
        /// Enables or disables swiping through Mouse. 
        /// </summary> 
        Mouse = 1 << 1,
    }
}