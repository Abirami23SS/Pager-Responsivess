using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents a collection of <see cref="AccordionItem"/>.
    /// </summary>
    /// <remarks>
    /// To generate dynamic <see cref="AccordionItem"/> based on collection, use <c>@foreach</c> within <see cref="AccordionItems"/> tag directive.
    /// </remarks>
    /// <example>
    /// In the below code example, a basic accordion has been rendered using <see cref="AccordionItems"/> tag directive.
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
    public partial class AccordionItems : SfOwningComponentBase
    {
        [CascadingParameter]
        private SfAccordion Parent { get; set; }

        /// <summary>
        /// Gets or sets the child content for the accordion items.
        /// </summary>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the list of accordion items to be rendered in accordion.
        /// </summary>
        internal List<AccordionItem> Items { get; set; } = new List<AccordionItem>();

        internal void UpdateChildProperty(AccordionItem item)
        {
            Items.Add(item);
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
                Items = null;
                Parent = null;
                ChildContent = null;
            }
        }
    }
}