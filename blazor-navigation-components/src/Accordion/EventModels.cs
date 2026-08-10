using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Provides the information about the accordion click action.
    /// </summary>
    public class AccordionClickArgs
    {
        /// <summary>
        /// Gets the accordion item that is being clicked. 
        /// </summary>
        /// <value>
        /// An <see cref="AccordionItemModel"/> object that represents the clicked accordion item.
        /// </value>
        public AccordionItemModel Item { get; internal set; }

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; internal set; }

        /// <summary>
        ///  Gets the mouse event informations.
        /// </summary>
        /// <value>
        /// The MouseEventArgs object that contains the mouse event data.
        /// </value>
        public MouseEventArgs OriginalEvent { get; internal set; }
    }

    /// <summary>
    /// Provides the information about the accordion item expanding action.
    /// </summary>
    public class ExpandEventArgs
    {
        /// <summary>
        /// Gets or sets whether the prevent the expanding action.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets the accordion item index.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets the expand or collapse state of accordion item.
        /// </summary>
        public bool IsExpanded { get; set; }

        /// <summary>
        /// Gets the accordion item that is being expand or collapse. 
        /// </summary>
        public AccordionItemModel Item { get; set; }

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Provides the information about the accordion item collapse action.
    /// </summary>
    public class CollapseEventArgs : ExpandEventArgs
    {
    }

    /// <summary>
    /// Provides the information about the accordion item expanded action.
    /// </summary>
    public class ExpandedEventArgs
    {
        /// <summary>
        /// Gets or sets the accordion item index.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int Index { get; internal set; }

        /// <summary>
        /// Gets the expanded or collapsed state of accordion item.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the accordion item is expanded. Otherwise, <c>false</c>.
        /// </value>
        public bool IsExpanded { get; internal set; }

        /// <summary>
        /// Gets the accordion item that is being expanded or collapsed. 
        /// </summary>
        /// <value>
        /// An AccordionItemModel object that represents the accordion item being expanded or collapsed.
        /// </value>
        public AccordionItemModel Item { get; internal set; }

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; internal set; }
    }

    /// <summary>
    /// Provides the information about the accordion item collapsed action.
    /// </summary>
    public class CollapsedEventArgs : ExpandedEventArgs
    {
    }

    /// <summary>
    /// Provides information about the accordion item.
    /// </summary>
    public class AccordionItemModel
    {
        /// <summary>
        /// Gets or sets the template as <see cref="RenderFragment"/>, that defines custom appearance of accordion header.
        /// </summary>
        public RenderFragment HeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets the template as <see cref="RenderFragment"/>, that defines custom appearance of accordion content.
        /// </summary>
        public RenderFragment ContentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the text content to be displayed for accordion item.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the classes for accordion item to customize the accordion header and content.
        /// </summary>
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets whether the accordion panel is disabled or not.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the accordion panel is expanded or not.
        /// </summary>
        public bool Expanded { get; set; }

        /// <summary>
        /// Gets or sets the header text to be displayed for accordion item.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets a CSS class string to include an icon or image for accordion header. 
        /// </summary>
        public string IconCss { get; set; }

        /// <summary>
        /// Gets or sets whether the accordion panel is hidden or not.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the unique ID for accordion item.
        /// </summary>
        public string Id { get; set; }
    }
}