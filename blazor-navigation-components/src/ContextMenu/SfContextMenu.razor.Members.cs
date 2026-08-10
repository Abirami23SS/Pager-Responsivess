using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// ContextMenu is a graphical user interface that appears on the user right click/touch hold operation.
    /// </summary>
    /// <example>
    /// In the below code example, showcases a basic context menu.
    /// <code><![CDATA[
    /// <SfContextMenu Target="#contextmenutarget" TValue="MenuItem">
    /// <MenuItems>
    /// <MenuItem Text="Cut" IconCss="e-cm-icons e-cut"></MenuItem>
    /// <MenuItem Text="Copy" IconCss="e-cm-icons e-copy"></MenuItem>
    /// <MenuItem Text="Paste" IconCss="e-cm-icons e-paste"></MenuItem>
    /// </MenuItems>
    /// </SfContextMenu>
    /// ]]></code>
    /// </example>
    public partial class SfContextMenu<TValue> : SfMenuBase<TValue>
    {
        /// <summary>
        /// Gets or sets a value that indicates the filter selector in which element the context menu should be opened inside the sepcified target.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is empty.
        /// </value>
        [Parameter]
        public string Filter { get; set; } = string.Empty;

        private string? filter;

        /// <summary>
        /// Gets or sets a value that indicates the target element selector in which the context menu should be opened.
        /// </summary>
        /// <value>
        /// Accepts the string value. The default value is empty.
        /// </value>
        [Parameter]
        public string Target { get; set; } = string.Empty;

        private string? target;

        /// <summary>
        /// Gets or sets a value that indicates an event to open the context menu.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>contextmenu</c>.
        /// </value>
        /// <remarks>
        /// The list of events supports to open the context menu are contextmenu, mousedown, mouseup, touchstart, mouseleave etc. The event must be different from <c> CloseActionEvents. </c>
        /// </remarks>
        /// <example>
        /// In the below code example, <c>contextmenu</c> event is set to open the context menu.
        /// <code><![CDATA[
        /// <div id="target">Right click or touch hold to open the context menu.</div>
        /// <SfContextMenu Target="#target" TValue="MenuItem" OpenActionEvents=”OpenAction”>
        /// <MenuItems>
        ///     <MenuItem Text="Cut"></MenuItem>
        ///     <MenuItem Text="Copy"></MenuItem>
        ///     <MenuItem Text="Paste"></MenuItem>
        /// </MenuItems>
        /// </SfContextMenu>
        /// @code {
        ///     private string OpenAction = “contextmenu”;
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public string OpenActionEvents { get; set; } = "contextmenu";

        private string? showOn;

        /// <summary>
        /// Gets or sets a value that indicates an event to close the context menu.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>mousedown, touchstart</c>.
        /// </value>
        /// <remarks>
        /// The list of events supports to close the context menu are mousedown, mouseup, click, touchstart, mouseleave, touchend. The event must be different from <c> OpenActionEvents. </c>
        /// </remarks>
        /// <example>
        /// In the below code example, <c>contextmenu</c> event is set to close the context menu.
        /// <code><![CDATA[
        /// <div id="target">Right click or touch hold to open the context menu.</div>
        /// <SfContextMenu Target="#target" TValue="MenuItem" CloseActionEvents=”CloseAction”>
        /// <MenuItems>
        ///     <MenuItem Text="Cut"></MenuItem>
        ///     <MenuItem Text="Copy"></MenuItem>
        ///     <MenuItem Text="Paste"></MenuItem>
        /// </MenuItems>
        /// </SfContextMenu>
        /// @code {
        ///     private string CloseAction = “mousedown touchstart”;
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public string CloseActionEvents { get; set; } = "mousedown touchstart";

        private string? closeOn;

        /// <summary>
        /// Gets or sets a value that indicates the additional HTML attributes such as style, title etc., to the context menu.
        /// </summary>
        private Dictionary<string, object> _htmlAttributes;

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> HtmlAttributes
        {
            get => _htmlAttributes;
            set => _htmlAttributes = SfBaseUtils.SanitizeHtmlAttributes(value);
        }
    }
}
