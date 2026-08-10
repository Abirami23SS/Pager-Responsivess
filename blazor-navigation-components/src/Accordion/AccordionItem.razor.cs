using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents accordion panels of <see cref="SfAccordion"/> component.
    /// </summary>
    /// <remarks>
    /// You can render header and content of accordion by specifying value to corresponding property.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic accordion panel has been added using <see cref="AccordionItem"/> tag directive.
    /// <code><![CDATA[
    /// <SfAccordion>
    ///     <AccordionItems>
    ///         <AccordionItem Header="ASP.NET">
    ///             <ContentTemplate>
    ///                 Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services.
    ///             </ContentTemplate>
    ///         </AccordionItem>
    ///         <AccordionItem Header="ASP.NET MVC">
    ///             <ContentTemplate>
    ///                 The Model-View-Controller (MVC) architectural pattern separates an application into three main components: the model, the view, and the controller.
    ///             </ContentTemplate>
    ///         </AccordionItem>
    ///         <AccordionItem Header="JavaScript">
    ///             <ContentTemplate>
    ///                 JavaScript (JS) is an interpreted computer programming language. It was originally implemented as part of web browsers so that client-side scripts could interact with the user, control the browser, communicate asynchronously, and alter the document content that was displayed.
    ///             </ContentTemplate>
    ///         </AccordionItem>
    ///     </AccordionItems>
    /// </SfAccordion>
    /// ]]></code>
    /// </example>
    public partial class AccordionItem : SfOwningComponentBase
    {
        [CascadingParameter]
        internal AccordionItems ItemParent { get; set; }

        [CascadingParameter]
        internal SfAccordion BaseParent { get; set; }

        /// <summary>
        /// Gets or sets the child content for the accordion item.
        /// </summary>
        /// <value>
        /// The value used to build the content.
        /// </value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of accordion header.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of accordion header. The default value is <c>null</c>.
        /// </value>        
        /// <example>
        /// <code><![CDATA[
        /// <SfAccordion>
        ///     <AccordionItems>
        ///         <AccordionItem Content="C# is intended to be a simple, modern, general-purpose, object-oriented programming language. Its development team is led by Anders Hejlsberg. The most recent version is C# 5.0, which was released on August 15, 2012.">
        ///             <HeaderTemplate>
        ///                 <div class="header-text">C Sharp(C#)</div>
        ///             </HeaderTemplate>
        ///         </AccordionItem>
        ///     </AccordionItems>
        /// </SfAccordion>
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment HeaderTemplate { get; set; }

        /// <summary>
        /// Gets or sets template as <see cref="RenderFragment"/>, that defines custom appearance of accordion content.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of accordion content. The default value is <c>null</c>.
        /// </value>        
        /// <example>
        /// <code><![CDATA[
        /// <SfAccordion>
        ///     <AccordionItems>
        ///         <AccordionItem Header="C Sharp(C#)">
        ///             <ContentTemplate>
        ///                 <div class="content-text">C# is intended to be a simple, modern, general-purpose, object-oriented programming language. Its development team is led by Anders Hejlsberg. The most recent version is C# 5.0, which was released on August 15, 2012.</div>
        ///             </ContentTemplate>     
        ///         </AccordionItem>
        ///     </AccordionItems>
        /// </SfAccordion>
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment ContentTemplate { get; set; }

        /// <summary>
        /// Gets or sets the text content to be displayed for accordion item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>null</c>.
        /// </value>
        [Parameter]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the classes for accordion item to customize the accordion header and content.
        /// </summary>
        /// <value> 
        /// If we set the css class, then the custom class is applied for accordion item. The default value is <c>null</c>. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfAccordion>
        ///     <AccordionItems>
        ///         <AccordionItem CssClass="item1">
        ///             <HeaderTemplate>
        ///                 Margeret Peacock
        ///             </HeaderTemplate>
        ///             <ContentTemplate>
        ///                 Microsoft ASP.NET is a set of technologies in the Microsoft .NET Framework for building Web applications and XML Web services.
        ///             </ContentTemplate>
        ///         </AccordionItem>
        ///     </AccordionItems>
        /// </SfAccordion>
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets whether the accordion panel is disabled or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, to disable the accordion panel. The default value is <c>false</c>.
        /// </value>
        [Parameter]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the accordion panel is expanded or not.
        /// </summary>
        /// <value>
        /// <c>true</c>, to expand the accordion panel. The default value is <c>false</c>.
        /// </value>
        [Parameter]
        public bool Expanded { get; set; }

        internal bool IsExpanded { get; set; }

        /// <summary>
        /// Gets or sets the header text to be displayed for accordion item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>null</c>.
        /// </value>
        [Parameter]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets a CSS class string to include an icon or image for accordion header. 
        /// </summary>
        /// <value>
        /// Accepts a CSS class string separated by space to include an icon or image for the accordion item. The default value is <c>null</c>.
        /// </value>
        /// <remarks>
        /// This property value is only applied for accordion header. 
        /// </remarks>
        /// <example>
        /// <code><![CDATA[
        /// <SfAccordion>
        ///     <AccordionItems>
        ///         <AccordionItem  IconCss="e-icons e-home" Content="Home icon rendered in header"></AccordionItem>
        ///     </AccordionItems>
        /// </SfAccordion>
        /// ]]></code>
        /// </example>
        [Parameter]
        public string IconCss { get; set; }

        /// <summary>
        /// Gets or sets whether the accordion panel is hidden or not.
        /// </summary>
        /// <value>
        /// <c>false</c>, to hide the accordion panel. The default value is <c>true</c>.
        /// </value>
        [Parameter]
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the unique ID for accordion item.
        /// </summary>
        /// <value>
        /// Accepts a string value. The default value is <c>null</c>.
        /// </value>
        [Parameter]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a callback when <see cref="Expanded"/> property changed.
        /// </summary>
        [Parameter]
        public EventCallback<bool> ExpandedChanged { get; set; }

        internal bool IsExpandedFromIndex { get; set; }

        internal bool IsContentRendered { get; set; }

        internal int InsertAt { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            ItemParent.UpdateChildProperty(this);
            SetItems();
        }

        internal void VisibleItem(bool isVisible)
        {
            Visible = isVisible;
        }

        internal async Task UpdateExpandedValue(bool isExpanded)
        {
            Expanded = IsExpanded = await SfBaseUtils.UpdateProperty(isExpanded, IsExpanded, ExpandedChanged).ConfigureAwait(true);
        }

        private void SetItems()
        {
            if (!BaseParent.LoadOnDemand)
            {
                IsContentRendered = true;
            }

            if (BaseParent.ExpandedIndices != null && BaseParent.ExpandedIndices.Contains(ItemParent.Items.Count - 1))
            {
                IsExpandedFromIndex = true;
                IsContentRendered = true;
            }

            if ((Expanded || IsExpandedFromIndex) && (!string.IsNullOrEmpty(Content) || ContentTemplate != null))
            {
                BaseParent.ExpandedItem.Add(this);
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
                if (ItemParent != null && ItemParent.Items != null && ItemParent.Items.Contains(this))
                {
                    ItemParent.Items.Remove(this);
                    SfBaseUtils.UpdateDictionary(nameof(BaseParent.Items), ItemParent.Items, BaseParent.PropertyChanges);
                    BaseParent.IsItemChanged = true;
                }

                ItemParent = null;
                BaseParent = null;
                ChildContent = null;
            }
        }
    }
}