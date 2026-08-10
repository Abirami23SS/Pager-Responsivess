using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents a collection of <see cref="TabItem"/>.
    /// </summary>
    /// <remarks>
    /// To generate dynamic <see cref="TabItem"/> based on collection, use <c>@foreach</c> within <see cref="TabItems"/> tag directive.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic tab has been rendered using <see cref="TabItems"/> tag directive.
    /// <code><![CDATA[
    /// <SfTab>
    ///     <TabItems>
    ///         <TabItem>
    ///             <ChildContent>
    ///                 <TabHeader Text="Tab 1"></TabHeader>
    ///             </ChildContent>
    ///             <ContentTemplate>
    ///                 <div>Content of tab 1</div>
    ///             </ContentTemplate>
    ///         </TabItem>
    ///         <TabItem>
    ///             <ChildContent>
    ///                 <TabHeader Text="Tab 2"></TabHeader>
    ///             </ChildContent>
    ///             <ContentTemplate>
    ///                 <div>Content of tab 2</div>
    ///             </ContentTemplate>
    ///         </TabItem>
    ///         <TabItem>
    ///             <ChildContent>
    ///                 <TabHeader Text="Tab 3"></TabHeader>
    ///             </ChildContent>
    ///             <ContentTemplate>
    ///                 <div>Content of tab 3</div>
    ///             </ContentTemplate>
    ///         </TabItem>
    ///     </TabItems>
    /// </SfTab>
    /// ]]></code>
    /// </example>
    public partial class TabItems : SfOwningComponentBase
    {
        [CascadingParameter]
        internal SfTab Parent { get; set; }

        /// <summary>
        /// Gets or sets the child content for the tab items.
        /// </summary>
        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the list of tab items to be rendered in tab panel.
        /// </summary>
        public List<TabItem> Items { get; set; } = new List<TabItem>();

        internal async Task UpdateChildProperty(TabItem item)
        {
            Items.Add(item);
            if (Parent.ShouldReinitialize)
            {
                await Parent.UpdateToolbarItems().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.UpdateItemProperties(Items);
        }

        /// <summary>
        /// Dispose unmanaged resources in the Syncfusion Blazor component.
        /// </summary>
        /// <param name="disposing">Boolean value to dispose the object.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent = null;
                ChildContent = null;
            }
        }
    }
}
