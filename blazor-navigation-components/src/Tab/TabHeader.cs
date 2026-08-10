using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents the tab header of <see cref="SfTab"/> component.
    /// </summary>
    /// <remarks>
    /// You can render text and icon of tab header by specifying value to corresponding property.
    /// </remarks>
    /// <example>
    /// <code><![CDATA[
    /// <SfTab>
    ///     <TabItems>
    ///         <TabItem>
    ///             <ChildContent>
    ///                 <TabHeader IconCss="e-icons e-home" Text="Home"></TabHeader>
    ///             </ChildContent>
    ///             <ContentTemplate>
    ///                 <div>Tab content</div>
    ///             </ContentTemplate>
    ///         </TabItem>
    ///     </TabItems>
    /// </SfTab>
    /// ]]></code>
    /// </example>
    public partial class TabHeader : SfOwningComponentBase
    {
        private string? iconCss;
        private string? iconPosition;
        private string? text;

        [CascadingParameter]
        internal TabItem Parent { get; set; }

        [CascadingParameter]
        internal SfTab BaseParent { get; set; }

        /// <summary>
        /// Gets or sets a icon class to render an icon in tab header. 
        /// </summary>
        /// <value>
        /// Accepts a icon class string separated by space to render an icon in tab header. The default value is <c>string.Empty</c>.
        /// </value>
        /// <remarks>
        /// This property value is only applied for tab header. 
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///     <TabItems>
        ///         <TabItem>
        ///             <ChildContent>
        ///                 <TabHeader IconCss="e-icons e-home"></TabHeader>
        ///             </ChildContent>
        ///             <ContentTemplate>
        ///                 <div>Home icon rendered in header</div>
        ///             </ContentTemplate>
        ///         </TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string IconCss { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value that specifies the icon positioning in tab header.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>left</c>
        /// </value>
        /// <remarks>
        /// If the value is <c>left</c>, Places the icon to the `left` of the item.
        /// If the value is <c>top</c>, Places the icon on the `top` of the item.
        /// If the value is <c>right</c>, Places the icon to the `right` end of the item.
        /// If the value is <c>bottom</c>, Places the icon at the `bottom` of the item.
        /// This property depends on the <see cref="IconCss"/> property.
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///     <TabItems>
        ///         <TabItem>
        ///             <ChildContent>
        ///                 <TabHeader Text="Home" IconCss="e-icons e-home" IconPosition="right"></TabHeader>
        ///             </ChildContent>
        ///             <ContentTemplate>
        ///                 <div>Home icon rendered in header</div>
        ///             </ContentTemplate>
        ///         </TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string IconPosition { get; set; } = "left";

        /// <summary>
        /// Gets or sets the text content to display in tab header.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>String.Empty</c>.
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfTab>
        ///     <TabItems>
        ///         <TabItem>
        ///             <ChildContent>
        ///                 <TabHeader Text="Home"></TabHeader>
        ///             </ChildContent>
        ///             <ContentTemplate>
        ///                 <div>Home icon rendered in header</div>
        ///             </ContentTemplate>
        ///         </TabItem>
        ///     </TabItems>
        /// </SfTab>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.UpdateHeaderProperties(this);
            iconCss = IconCss;
            iconPosition = IconPosition;
            text = Text;
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            if (IconCss != iconCss || IconPosition != iconPosition || Text != text)
            {
                iconCss = IconCss;
                iconPosition = IconPosition;
                text = Text;
                BaseParent.IsTabItemChanged = true;
            }
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
                BaseParent = null;
            }
        }
    }
}