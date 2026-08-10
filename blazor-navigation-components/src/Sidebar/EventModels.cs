using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Interface for open and close events.
    /// </summary>
    public class EventArgs
    {
        /// <summary>
        /// Determines whether the current action needs to be prevented or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the event should be canceled. Otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }

        /// <summary>
        /// Returns the element reference.
        /// </summary>
        /// <value>
        /// An ElementReference object representing the element reference for the Blazor Sidebar component.
        /// </value>
        public ElementReference Element { get; set; }

        /// <summary>
        /// Returns the event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Defines the boolean that returns true when the Sidebar is closed by user interaction, otherwise returns false.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the sidebar is closed by user interaction. Otherwise, <c>false</c>.
        /// </value>
        public bool IsInteracted { get; set; }

        /// <summary>
        /// Specifies the clientY position of the target element.
        /// </summary>
        /// <value>
        /// Accepts the double value.
        /// </value>
        public double? Top { get; set; }

        /// <summary>
        /// Specifies the clientX position of the target element.
        /// </summary>
        /// <value>
        /// Accepts the double value.
        /// </value>
        public double? Left { get; set; }

    }

    /// <summary>
    /// Defines the event arguments for the change event.
    /// </summary>
    public class ChangeEventArgs
    {
        /// <summary>
        /// Returns the element reference.
        /// </summary>
        /// <value>
        /// The ElementReference object that represents the reference to the component's root element.
        /// </value>
        public ElementReference Element { get; set; }

        /// <summary>
        /// Returns event name.
        /// </summary>
        /// <value>
        /// Accepts the string value.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Defines the boolean that returns true when the Sidebar is closed by user interaction, otherwise returns false.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the sidebar is closed by user interaction. Otherwise, <c>false</c>.
        /// </value>
        public bool IsInteracted { get; set; }
    }

    /// <summary>
    /// Interface for persistence values.
    /// </summary>
    internal sealed class PersistenceValues
    {
        /// <summary>
        /// Gets or sets the Sidebar component is open or close.
        /// </summary>
        public bool IsOpen { get; set; }
    }
}