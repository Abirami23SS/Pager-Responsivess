using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Provides event handlers for the <see cref="SfTab"/> component.
    /// </summary>
    public partial class TabEvents : ComponentBase
    {
        [CascadingParameter]
        internal SfTab Parent { get; set; }

        /// <summary>
        /// Triggers after adding an <see cref="TabItem"/> to the Tabs.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Added="OnTabAdded"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabAdded(AddEventArgs args) {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<AddEventArgs> Added { get; set; }

        /// <summary>
        /// Triggers before adding a <see cref="TabItem"/> to the Tabs.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Adding="OnTabAdding"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabAdding(AddEventArgs args) {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<AddEventArgs> Adding { get; set; }

        /// <summary>
        /// Triggers after the <see cref="SfTab"/> component is created.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Created="OnTabCreated"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabCreated() {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// Triggers after the <see cref="SfTab"/> component is destroyed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Destroyed="OnTabDestroyed"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabDestroyed() {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <summary>
        /// Triggers after the <see cref="TabItem"/> is removed.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Removed="OnTabRemoved"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabRemoved(RemoveEventArgs args) {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<RemoveEventArgs> Removed { get; set; }

        /// <summary>
        /// Triggers before removing the <see cref="TabItem"/>.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Removing="OnTabRemoving"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabRemoving(RemoveEventArgs args) {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<RemoveEventArgs> Removing { get; set; }

        /// <summary>
        /// Triggers after the <see cref="TabItem"/> is selected.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Selected="OnTabSelected"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabSelected(SelectEventArgs args) {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<SelectEventArgs> Selected { get; set; }

        /// <summary>
        /// Triggers before selecting the <see cref="TabItem"/>.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Selecting="OnTabSelecting"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabSelecting(SelectingEventArgs args) {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<SelectingEventArgs> Selecting { get; set; }

        /// <summary>
        /// Triggers when a drag action starts on a <see cref="TabItem"/>.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents OnDragStart="OnTabDragStart"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabDragStart(DragEventArgs args) {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<DragEventArgs> OnDragStart { get; set; }

        /// <summary>
        /// Triggers when a <see cref="TabItem"/> is dropped.
        /// </summary>
        /// <value>
        /// An event callback function.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///   <TabEvents Dragged="OnTabDragged"></TabEvents>
        ///     <TabItems>
        ///         <TabItem Header="Tab1" Content="Content of Tab1"></TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// @code {
        ///     public void OnTabDragged(DragEventArgs args) {
        ///     }
        /// }
        /// ]]></code>
        /// </example>
        [Parameter]
        public EventCallback<DragEventArgs> Dragged { get; set; }

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