using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Provides information about the adding or added item in tab.
    /// </summary>
    public class AddEventArgs
    {
        /// <summary>
        /// Gets the <see cref="TabItemModel"/> collection that is being adding or added.
        /// </summary>
        public List<TabItemModel> AddedItems { get; internal set; }

        /// <summary>
        /// Gets or sets whether to add the new tab item or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, to cancel adding new tab item. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets name of the event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; internal set; }
    }

    /// <summary>
    /// Provides information about the removing or removed item in tab.
    /// </summary>
    public class RemoveEventArgs
    {
        /// <summary>
        /// Gets or sets whether to remove the tab item or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the tab item should be removed. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets name of the event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the removed tab item index.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int RemovedIndex { get; internal set; }
    }

    /// <summary>
    /// Provides information about the dragged tab item.
    /// </summary>
    public class DragEventArgs
    {
        /// <summary>
        /// Gets or sets whether the drag action should be canceled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the drag action should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the index of tab item.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int Index { get; internal set; }

        /// <summary>
        /// Gets the dragged tab item index.
        /// </summary>
        /// <value>
        /// The <see cref="TabItem"/> object representing the dragged tab item.
        /// </value>
        public TabItem DraggedItem { get; internal set; }

        /// <summary>
        /// Gets the dropped tab item index.
        /// </summary>
        /// <value>
        /// The <see cref="TabItem"/> object representing the dropped tab item.
        /// </value>
        public TabItem DroppedItem { get; internal set; }

        /// <summary>
        /// Gets the Client X value of target element.
        /// </summary>
        /// <value>
        /// Accepts the double value.
        /// </value>
        public double Left { get; internal set; }

        /// <summary>
        /// Gets the Client Y value of target element.
        /// </summary>
        /// <value>
        /// Accepts the double value.
        /// </value>
        public double Top { get; internal set; }
    }

    /// <summary>
    /// Provides information about the selected tab item.
    /// </summary>
    public class SelectEventArgs
    {
        /// <summary>
        /// Gets whether the content selection is done through swiping or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the content selection is done through swiping. Otherwise, <c>false</c>.
        /// </value>
        public bool IsSwiped { get; internal set; }

        /// <summary>
        /// Gets whether the event is triggered via user interaction or programmatic way.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event triggered via user interaction. Otherwise, <c>false</c>.
        /// </value>
        public bool IsInteracted { get; internal set; }

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets or sets whether to prevent the focus of tab item or not when it is selected.
        /// </summary>
        /// <value>
        /// <c>true</c>, to prevent the focusing of tab item. Otherwise, <c>false</c>.
        /// </value>
        public bool PreventFocus { get; set; }

        /// <summary>
        /// Gets the index of previously selected tab item.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int PreviousIndex { get; internal set; }

        /// <summary>
        /// Gets the index of selected tab item.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int SelectedIndex { get; internal set; }
    }

    /// <summary>
    /// Provides information about the selecting tab item.
    /// </summary>
    public class SelectingEventArgs
    {
        /// <summary>
        /// Gets or sets whether the tab item selecting action should be canceled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, to cancel the tab item selected option. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets whether the content selection is done through swiping or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the content selection is done through swiping. Otherwise, <c>false</c>.
        /// </value>
        public bool IsSwiped { get; set; }

        /// <summary>
        /// Gets whether the event is triggered via user interaction or programmatic way.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event triggered via user interaction. Otherwise, <c>false</c>.
        /// </value>
        public bool IsInteracted { get; set; }

        /// <summary>
        /// Gets the name of the event.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the index of previously selected tab item.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int PreviousIndex { get; set; }

        /// <summary>
        /// Returns the index of the selected Tab item.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int SelectedIndex { get; set; }

        /// <summary>
        /// Gets the index of selecting tab item.
        /// </summary>
        /// <value>
        /// Accepts an integer value.
        /// </value>
        public int SelectingIndex { get; set; }
    }

    /// <summary>
    /// Provides information about the tab header.
    /// </summary>
    public class HeaderModel
    {
        /// <summary>
        /// Gets or sets a icon class to render an icon in tab header. 
        /// </summary>
        public string IconCss { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that specifies the icon positioning in tab header.
        /// The possible values are:
        /// <list type="bullet">
        /// <item><c>left</c></item>
        /// <item><c>top</c></item>
        /// <item><c>right</c></item>
        /// <item><c>bottom</c></item>
        /// </list>
        /// </summary>
        public string IconPosition { get; set; } = "left";

        /// <summary>
        /// Gets or sets the text content to display in tab header.
        /// </summary>
        public string Text { get; set; } = string.Empty;
    }

    /// <summary>
    /// Provides information about the tab item.
    /// </summary>
    public class TabItemModel
    {
        /// <summary>
        /// Gets or sets the text content to be displayed for tab item.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the classes for tab item to customize the tab header and content.
        /// </summary>
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the tab panel is disabled or not.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the header content of tab item.
        /// </summary>
        public HeaderModel Header { get; set; }

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of tab header.
        /// </summary>
        public RenderFragment HeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets whether the tab panel is hidden or not.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the unique ID for tab item.
        /// </summary>
        public string ID { get; set; }

        /// <summary> 
        /// Gets or sets the tab order of the tab items. When positive values assigned, it allows to switch focus to the next/previous tabs items with Tab/ShiftTab keys.
        /// </summary> 
        public int TabIndex { get; set; } = -1;

    }
}