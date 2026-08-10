using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Partial Class SfSidebar.
    /// </summary>
    public partial class SfSidebar 
    {
        /// <summary>
        /// Triggers when the state(expand/collapse) of the component is changed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<ChangeEventArgs> Changed { get; set; }

        /// <summary>
        /// Triggers when the component is ready to close.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<EventArgs> OnClose { get; set; }

        /// <summary>
        /// Triggers when the component is created.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// Triggers when the component gets destroyed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// Triggers when the component is ready to open.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<EventArgs> OnOpen { get; set; }
    }
}