using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Provides information about the clicked item in toolbar.
    /// </summary>
    public class ClickEventArgs
    {
        /// <summary>
        /// Gets or sets whether the item click action should be canceled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the item click action can be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the clicked <see cref="ItemModel"/> of toolbar.
        /// </summary>
        /// <value>
        /// A <see cref="ItemModel"/> object that represents the item that was clicked in the toolbar.
        /// </value>
        public ItemModel Item { get; internal set; }

        /// <summary>
        /// Gets name of the event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the mouse event informations.
        /// </summary>
        /// <value>
        /// A MouseEventArgs object that represents the mouse event information.
        /// </value>
        public MouseEventArgs OriginalEvent { get; internal set; }
    }

    /// <summary>
    /// Provides information about the toolbar item.
    /// </summary>
    public class ItemModel
    {
        /// <summary>
        /// Event triggers when `click` the toolbar item.
        /// </summary>
        [JsonIgnore]
        [JsonPropertyName("click")]
        public EventCallback<ClickEventArgs> Click { get; set; }

        /// <summary>
        /// Gets or sets the location for aligning toolbar items on the toolbar.
        /// </summary>
        [JsonPropertyName("align")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemAlign Align { get; set; }

        /// <summary>
        /// Gets or sets the classes for toolbar item to customize the toolbar item.
        /// </summary>
        [JsonPropertyName("cssClass")]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the toolbar item is disabled or not.
        /// </summary>
        [JsonPropertyName("disabled")]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the toolbar item element.
        /// </summary>
        [JsonPropertyName("htmlAttributes")]
        public Dictionary<string, object> HtmlAttributes { get; set; }

        /// <summary>
        /// Gets or sets the unique ID for toolbar button or input element.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates whether to display the toolbar item on toolbar or not, when the content is too large to fit available space.
        /// </summary>
        [JsonPropertyName("overflow")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OverflowOption Overflow { get; set; }

        /// <summary>
        /// Gets or sets the classes to display an icon for toolbar button item.
        /// </summary>
        [JsonPropertyName("prefixIcon")]
        public string PrefixIcon { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the toolbar items whether to display always in popup or not.
        /// </summary>
        [JsonPropertyName("showAlwaysInPopup")]
        public bool ShowAlwaysInPopup { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to display the button text on toolbar or popup.
        /// </summary>
        [JsonPropertyName("showTextOn")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DisplayMode ShowTextOn { get; set; }

        /// <summary>
        /// Gets or sets the classes to display an icon for toolbar button item.
        /// </summary>
        [JsonPropertyName("suffixIcon")]
        public string SuffixIcon { get; set; } = string.Empty;

        /// <summary> 
        /// Gets or sets the tab order of the Toolbar items. When positive values assigned, it allows to switch focus to the next/previous toolbar items with Tab/ShiftTab keys.
        /// </summary>
        [JsonPropertyName("tabIndex")]
        public int TabIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets the HTML element content for the toolbar item.
        /// </summary>
        [JsonIgnore]
        [JsonPropertyName("template")]
        public RenderFragment Template { get; set; }

        /// <summary>
        /// Gets or sets the text content for toolbar button item.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the tooltip text content to be displayed on hovering the toolbar button item.
        /// </summary>
        [JsonPropertyName("tooltipText")]
        public string TooltipText { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that indicates the type of toolbar item to be rendered in toolbar.
        /// </summary>
        [JsonPropertyName("type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ItemType Type { get; set; }

        /// <summary>
        /// Gets or sets whether the toolbar item is hidden or not.
        /// </summary>
        [JsonPropertyName("visible")]
        public bool Visible { get; set; } = true;

        /// <summary> 
        /// Gets or sets the width of the toolbar button item in pixels/number/percentage. 
        /// </summary>
        [JsonPropertyName("width")]
        public string Width { get; set; } = "auto";

        internal int Index { get; set; } = -1;
    }
}