using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Provides event handlers for the <see cref="SfAccordion"/> component.
    /// </summary>
    public partial class AccordionEvents : ComponentBase
    {
        [CascadingParameter]
        private SfAccordion Parent { get; set; }

        /// <summary>
        /// Triggers an event when clicking anywhere within the <see cref="SfAccordion"/> component.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <remarks>
        /// The event arguments contain information about the clicked item, including the <see cref="AccordionItemModel"/>.
        /// </remarks>
        [Parameter]
        public EventCallback<AccordionClickArgs> Clicked { get; set; }

        /// <summary>
        /// Triggers after the <see cref="SfAccordion"/> component is created.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// Triggers after the <see cref="SfAccordion"/> component is destroyed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// Triggers after the <see cref="AccordionItem"/> is expanded.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<ExpandedEventArgs> Expanded { get; set; }

        /// <summary>
        /// Triggers before the <see cref="AccordionItem"/> is expanded.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<ExpandEventArgs> Expanding { get; set; }

        /// <summary>
        /// Triggers after the <see cref="AccordionItem"/> is collapsed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<CollapsedEventArgs> Collapsed { get; set; }

        /// <summary>
        /// Triggers before the <see cref="AccordionItem"/> is collapsed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        [Parameter]
        public EventCallback<CollapseEventArgs> Collapsing { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.Delegates = this;
        }
    }
}