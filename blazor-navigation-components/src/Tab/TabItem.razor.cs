using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents tab component item of <see cref="SfTab"/> component.
    /// </summary>
    /// <remarks>
    /// You can render header and content of tab by specifying value to corresponding property.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic tab item has been added using <see cref="TabItem"/> tag directive.
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
    public partial class TabItem : SfOwningComponentBase
    {
        private const string ITEMS = "Items";
        private string? content;
        private string? cssClass;
        private bool disabled;
        private bool visible;
        private int tabIndex;
        private TabHeader? header { get; set; }

        [CascadingParameter]
        internal TabItems Parent { get; set; }

        [CascadingParameter]
        internal SfTab BaseParent { get; set; }

        /// <summary>
        /// Gets or sets the child content for the tab item.
        /// </summary>
        /// <value>
        /// The value used to build the content.
        /// </value>
        [Parameter]
        [JsonIgnore]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of tab content.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of tab content. The default value is <c>null</c>.
        /// </value>        
        /// <example>
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
        ///     </TabItems>
        /// </SfTab>
        /// ]]></code>
        /// </example>
        [Parameter]
        [JsonIgnore]
        public RenderFragment ContentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the text content to be displayed for tab item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>string.Empty</c>.
        /// </value>
        [Parameter]
        public string Content { get; set; } = string.Empty;

        /// <summary> 
        /// Gets or sets the tab order of the tab items. When positive values assigned, it allows to switch focus to the next/previous tabs items with Tab/ShiftTab keys.
        /// </summary> 
        /// <value> 
        /// Tab index of tabs item. The default value is `-1`.
        /// </value> 
        /// <remarks>
        /// By default, user can able to switch between items only via arrow keys.
        /// If the value is set to 0 for all tool bar items, then tab switches based on element order.
        /// </remarks>
        [Parameter]
        public int TabIndex { get; set; } = -1;

        /// <summary>
        /// Gets or sets the classes for tab item to customize the tab header and content.
        /// </summary>
        /// <value> 
        /// If we set the css class, then the custom class is applied for tab item. The default value is <c>string.Empty</c>. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfTab>
        ///     <TabItems>
        ///         <TabItem CssClass="item1">
        ///             <ChildContent>
        ///                 <TabHeader Text="Tab 1"></TabHeader>
        ///             </ChildContent>
        ///             <ContentTemplate>
        ///                 <div>Content of tab 1</div>
        ///             </ContentTemplate>
        ///         </TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public string CssClass { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the tab panel is disabled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, to disable the tab panel. The default value is <c>false</c>.
        /// </value>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets the header content of tab item.
        /// </summary>
        /// <value>
        /// If we set the header, then the provided <see cref="TabHeader"/> value is rendered, otherwise the default `null` value is set.
        /// </value>
        [Parameter]
        public TabHeader Header { get; set; }

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of tab header.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of tab header. The default value is <c>null</c>.
        /// </value>        
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///     <TabItems>
        ///         <TabItem Content="Content of tab 1">
        ///             <HeaderTemplate>Tab 1</HeaderTemplate>
        ///         </TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// ]]></code>
        /// </example>
        [Parameter]
        [JsonIgnore]
        public RenderFragment HeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets whether the tab panel is hidden or not.
        /// </summary>
        /// <value>
        /// <c>false</c>, to hide the tab panel. The default value is <c>true</c>.
        /// </value>
        [Parameter]
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the unique ID for tab item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>null</c>.
        /// </value>
        [Parameter]
        public string ID { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        internal void UpdateHeaderProperties(TabHeader tabHeader)
        {
            var headerCnt = tabHeader == null ? new TabHeader() : tabHeader;
            Header = header = headerCnt;
        }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            await Parent.UpdateChildProperty(this).ConfigureAwait(true);
            BaseParent.IsTabItemChanged = true;
            content = Content;
            cssClass = CssClass;
            disabled = Disabled;
            visible = Visible;
            tabIndex = TabIndex;
            UpdateHeaderProperties(Header);
            if (BaseParent.IsStaticServerRendering())
            {
                await BaseParent.UpdateToolbarItems().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            if (Content != content || CssClass != cssClass || Disabled != disabled || Header != header || Visible != visible  || TabIndex != tabIndex)
            {
                content = Content;
                cssClass = CssClass;
                disabled = Disabled;
                header = Header;
                visible = Visible;
                tabIndex = TabIndex;
                BaseParent.IsTabItemChanged = true;
            }
        }

        internal void EnableItem(bool isDisabled)
        {
            Disabled = disabled = isDisabled;
            BaseParent.IsTabItemChanged = true;
        }

        internal void SetVisible(bool isVisible)
        {
            Visible = visible = isVisible;
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
                if (Parent.Items != null && Parent.Items.Contains(this))
                {
                    Parent.Items.Remove(this);
                    SfBaseUtils.UpdateDictionary(ITEMS, Parent.Items, BaseParent.PropertyChanges);
                }

                Parent = null;
                BaseParent = null;
                ChildContent = null;
            }
        }

    }
}