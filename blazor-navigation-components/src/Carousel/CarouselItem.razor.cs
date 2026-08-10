using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// A class that represents the Carousel item of <see cref="SfCarousel"/> component.
    /// </summary>
    /// <example> 
    /// In the below code example, a basic Carousel has been rendered using <see cref="CarouselItem"/> tag directive. 
    /// <code><![CDATA[ 
    /// <SfCarousel>
    ///     <CarouselItem><div>Slide 1</div></CarouselItem>
    ///     <CarouselItem><div>Slide 2</div></CarouselItem>
    ///     <CarouselItem><div>Slide 3</div></CarouselItem>
    /// </SfCarousel>
    /// ]]></code> 
    /// </example> 
    public partial class CarouselItem : SfOwningComponentBase
    {
        [CascadingParameter]
        private SfCarousel ItemParent { get; set; }

        internal string? SlideClass { get; set; }

        internal Dictionary<string, object> itemHtmlAttributes = new Dictionary<string, object>();

        /// <summary>
        /// Child Content for the Carousel item.
        /// </summary>
        /// <value>
        /// The value used to build the content.
        /// </value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Gets or sets the classes for carousel item to customize the carousel item.
        /// </summary>
        /// <value> 
        /// If we set the css class, then the custom class is applied for carousel item. The default value is `null`. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel> 
        ///     <CarouselItem CssClass="item1"><div>Slide 1</div></CarouselItem>
        ///     <CarouselItem CssClass="item2"><div>Slide 2</div></CarouselItem>
        ///     <CarouselItem CssClass="item3"><div>Slide 3</div></CarouselItem>
        /// </SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public string CssClass { get; set; }

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the carousel item element.
        /// </summary>
        /// <value> 
        /// It allows the carousel item element to render non-declared attributes. The default value is `null`. 
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel>
        ///     <CarouselItem HtmlAttributes="customAttribute"><div>Slide 1</div></CarouselItem>
        ///     <CarouselItem><div>Slide 2</div></CarouselItem>
        ///     <CarouselItem><div>Slide 3</div></CarouselItem>
        /// </SfCarousel>
        /// @code{ 
        ///    Dictionary<string, object> customAttribute = new Dictionary<string, object>() 
        ///    { 
        ///        { "aria-label", "slide1" } 
        ///    }; 
        /// } 
        /// ]]></code> 
        /// </example> 
        [Parameter(CaptureUnmatchedValues = true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, object> HtmlAttributes { get { return itemHtmlAttributes; } set { itemHtmlAttributes = value; } }

        /// <summary>
        /// Gets or sets the auto transition time in milliseconds for individual carousel items. 
        /// </summary>
        /// <value> 
        /// If we set the interval value, then the slide transition begins after the specified time interval for individual carousel items otherwise the default interval value 5000 is set.  
        /// </value>
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel> 
        ///     <CarouselItem Interval="3000"><div>Slide 1</div></CarouselItem>
        ///     <CarouselItem><div>Slide 2</div></CarouselItem>
        ///     <CarouselItem><div>Slide 3</div></CarouselItem>
        /// </SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public int Interval { get; set; } = 5000;

        /// <summary>
        /// Gets or sets template to customize the carousel item.
        /// </summary>
        /// <value>
        /// A template content that specifies the visualization of carousel item. The default value is <c>null</c>.
        /// </value> 
        /// <example> 
        /// <code><![CDATA[ 
        /// <SfCarousel> 
        ///     <CarouselItem><Template><div>Slide 1</div></Template></CarouselItem>
        ///     <CarouselItem><Template><div>Slide 2</div></Template></CarouselItem>
        ///     <CarouselItem><Template><div>Slide 3</div></Template></CarouselItem>
        /// </SfCarousel> 
        /// ]]></code> 
        /// </example> 
        [Parameter]
        public RenderFragment Template { get; set; }

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ItemParent.UpdateItemProperties(this, false);
            if (ItemParent.IsStaticServerRendering())
            {
                ItemParent.NotifyItemsInitialized();
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
                ItemParent?.UpdateItemProperties(this, true);
                ItemParent = null;
                itemHtmlAttributes = null;
                ChildContent = null;
            }
        }

    }
}

