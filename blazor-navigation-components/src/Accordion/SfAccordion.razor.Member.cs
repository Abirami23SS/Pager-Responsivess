using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfAccordion : SfBaseComponent
    {
        /// <summary>
        /// Gets or sets the unique Id value for accordion component.
        /// </summary>
        /// <value>
        /// If we set the id, then the id value set for accordion element. The default value is `null`.
        /// </value>
        [Parameter]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the child content of accordion component.
        /// </summary>
        /// <value>
        /// Accepts a RenderFragment that defines the content of the accordion element.
        /// </value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the list of accordion items that will be populated using the <see cref="AccordionItems"/> tag directive. 
        /// </summary>
        /// <value>
        /// <see cref="Navigations.AccordionItems"/>
        /// </value>
        public List<AccordionItem> Items { get; set; }

        /// <summary>
        /// Gets or sets whether to persist component's state between page reloads. When set to <c>true</c>, the <see cref="ExpandedIndices" /> property is persisted.
        /// </summary>
        /// <value>
        /// <c>true</c>, if the component's state persistence is enabled. The default value is <c>false</c>.
        /// </value>
        /// <remarks>
        /// Component's <see cref="ExpandedIndices"/> property will be stored in browser local storage to persist component's state when page reloads.
        /// It is mandatory to provide <see cref="ID"/> to persist <c>ExpandedIndices</c> property.
        /// </remarks>
        [Parameter]
        public bool EnablePersistence { get; set; }

        /// <summary>
        /// Gets or sets whether to render all the accordion content on initial load or not.
        /// </summary>
        /// <value>
        /// If we set <c>false</c>, then all the contents are rendered on initial load, The default value is <c>true</c>.
        /// </value>
        [Parameter]
        public bool LoadOnDemand { get; set; } = true;

        /// <summary>
        /// Gets or sets whether the right to left direction is enabled for accordion component.
        /// </summary>
        /// <value> 
        /// true, the right to left direction is enabled for accordion component. The default value is `false`. 
        /// </value> 
        [Parameter]
        public bool EnableRtl { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates options to expand a single or multiple panels at a time.
        /// </summary>
        /// <value>
        /// One of the <see cref="Navigations.ExpandMode"/> enumeration. The default value is <see cref="ExpandMode.Multiple"/>
        /// </value>
        /// <remarks>
        /// If the <c>ExpandMode</c> is <c>Single</c>, only one <see cref="AccordionItem"/> will expand at a time.
        /// If the <c>ExpandMode</c> is <c>Multiple</c>, more than one <see cref="AccordionItem"/> will expand at a time.
        /// </remarks>
        [Parameter]
        public ExpandMode ExpandMode { get; set; } = ExpandMode.Multiple;

        /// <summary> 
        /// Gets or sets the index of items that is expanded on the initial load. 
        /// </summary> 
        /// <value> 
        /// If we set the index value, then specified index items were expanded otherwise the default <c>null</c> value is set.  
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfAccordion @bind-ExpandedIndices="@ExpandedIndices">
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
        /// @code{
        ///     int[] ExpandedIndices = new int[] { 0, 1 };
        /// }
        /// ]]></code>
        /// </example> 
        [Parameter]
        public int[] ExpandedIndices { get; set; }

        /// <summary>
        /// Invokes when index of expanded items were changed.
        /// </summary>
        /// <value> 
        /// Fired when expanded item index changes.
        /// </value>
        [Parameter]
        public EventCallback<int[]> ExpandedIndicesChanged { get; set; }

        /// <summary> 
        /// Gets or sets the height of the accordion element in pixels/number/percentage. 
        /// </summary> 
        /// <value> 
        /// If we set the height value, then the accordion will render based on specified height otherwise the default height value `auto` is set.  
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfAccordion Height="500px"></SfAccordion> 
        /// ]]></code>
        /// </example> 
        /// <remarks>
        /// If we set number values, then it is considered as pixels.
        /// </remarks>
        [Parameter]
        public string Height { get; set; } = "auto";

        /// <summary> 
        /// Gets or sets the width of the accordion element in pixels/number/percentage. 
        /// </summary> 
        /// <value> 
        /// If we set the width value, then the accordion will render based on specified width otherwise the default width value `100%` is set.  
        /// </value>
        /// <example>
        /// <code><![CDATA[
        /// <SfAccordion Width="500px"></SfAccordion>
        /// ]]></code>
        /// </example>
        /// <remarks>
        /// If we set number values, then it is considered as pixels.
        /// </remarks>
        [Parameter]
        public string Width { get; set; } = "100%";

        /// <summary> 
        /// Gets or sets a collection of additional attributes that will applied to the accordion element. 
        /// </summary> 
        /// <remarks>
        /// Additional attributes can be added by specifying as in-line attributes or by specifying <c>@attributes</c> directive.
        /// </remarks> 
        /// <value> 
        /// It allows the accordion component to render non-declared attributes. The default value is `null`. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfAccordion name="accordion"></SfAccordion>
        /// ]]></code>
        /// </example>
        [Parameter(CaptureUnmatchedValues = true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, object> HtmlAttributes { get; set; }
        
        internal AccordionAnimationSettings? AnimationSettings { get; set; }

        internal bool IsExpandIndicesChanged { get; set; }

        internal void UpdateItemProperties(List<AccordionItem> items)
        {
            Items = items;
        }

        internal void UpdateAnimationProperties(AccordionAnimationSettings animationSettings)
        {
            AccordionAnimationSettings? animation = null;
            if ((SyncfusionService.options.Animation == GlobalAnimationMode.Default) || (SyncfusionService.options.Animation == GlobalAnimationMode.Enable))
            {
                animation = animationSettings;
            }
            if (animation == null)
            {
                animation = new AccordionAnimationSettings();
                animation.UpdateExpandProperties(animation.Expand);
                animation.UpdateCollapseProperties(animation.Collapse);
            }

            AnimationSettings = animation;
        }
    }
}