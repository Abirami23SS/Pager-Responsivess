using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Defines the change event of the DropDownTree component.
    /// </summary>
    public class DdtChangeEventArgs<T>
    {
        /// <summary>
        /// Gets whether the action is select or unselect in the Dropdown Tree component
        /// </summary>
        /// <value>
        /// An enumeration value of type <see cref="DdtAction"/> representing the action, which can be either select or unselect.
        /// </value>
        public DdtAction Action { get; internal set; }

        /// <summary>
        /// Gets or sets whether the current action should be cancelled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the current action can be cancelled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets a value indicating whether the event was triggered by user interaction.
        /// </summary> 
        /// <value>
        /// <c>true</c> if the event was triggered by user interaction; otherwise, <c>false</c>.
        /// </value> 
        public bool IsInteracted { get; internal set; }

        /// <summary>
        /// Gets the previous selected values of the component.
        /// </summary>
        /// <value>
        /// The previous selected values.
        /// </value>
        public List<T> PreviousValue { get; internal set; }

        /// <summary>
        /// Gets the current updated value of the component.
        /// </summary>
        /// <value>
        /// The current selected value.
        /// </value>
        public T CurrentValue { get; internal set; }

        /// <summary>
        /// Gets the selected item as a list from the data source.
        /// </summary>
        /// <value>
        /// An object of type <c>NodeData</c> representing the selected item from the data source.
        /// </value>
        public NodeData NodeData { get; internal set; }
    }

    /// <summary>
    /// Provides information about an <see cref="SfDropDownTree{TValue, TItem}.Filtering"/> event being raised.
    /// </summary>
    public class DdtFilteringEventArgs
    {
        /// <summary>
        /// Gets or sets whether the current filter action should be canceled or not.
        /// </summary>
        /// <value>
        /// <c>true</c> to cancel the current filter action; otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets the current typed text value.
        /// </summary>
        /// <value>
        /// A string representing the current typed text value.
        /// </value>
        public string Text { get; internal set; }
    }

    /// <summary>
    /// Provides information about current item.
    /// </summary> 
    public class SelectedItemTemplate<T>
    {
        /// <summary>
        /// Gets the text of the current item to <see cref="SfDropDownTree{TValue, TItem}.SelectedItemTemplate"/>
        /// </summary>
        /// <remarks>
        /// This property stores the selected text value(s).
        /// </remarks>
        public string Text { get; internal set; }

        /// <summary>
        /// Gets the value of the selected item in the Dropdown Tree component.
        /// </summary>
        public List<T> Value { get; internal set; }
    }

    internal class ChipItems
    {
        internal string? Value { get; set; }
        internal string? Text { get; set; }
    }

    internal class TreeData<T>
    {
        internal string? Text { get; set; }
        internal List<T>? Child { get; set; }
    }
}