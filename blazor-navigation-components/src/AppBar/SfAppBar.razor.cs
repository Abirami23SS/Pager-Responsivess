using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The AppBar displays the information and actions related to the current application screen. It is used to show branding, screen titles, navigation, and actions.
    /// </summary>
    /// <remarks>
    /// AppBar component can be populated by specifying the child components within <see cref="SfAppBar"/> tag directive.
    /// Support to inherit colors from AppBar provided to <c>SfButton</c>, <c>SfDropDownButton</c>, <c>SfMenu</c> and <c>SfTextBox</c>. 
    /// Set <c>CssClass</c> property with <code>e-inherit</code> CSS class to inherit the background and color from AppBar. 
    /// </remarks>
    /// <example>
    /// The below example shows AppBar with Primary Button.
    /// <code><![CDATA[
    /// <SfAppBar> 
    ///     <SfButton IsPrimary="true">Primary</SfButton>
    /// </SfAppBar>
    /// ]]></code>
    /// The below example shows AppBar with Buttons which inherits colors from AppBar.
    /// <code><![CDATA[
    /// <SfAppBar>
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-home"></SfButton>
    /// <AppBarSeparator />
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-pan"></SfButton>
    /// </SfAppBar>
    /// ]]></code>
    /// The below example, AppBarSpacer component used to align the Buttons on left and right.
    /// <code><![CDATA[
    /// <SfAppBar>
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-home"></SfButton>
    /// <AppBarSpacer />
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-pan"></SfButton>
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-close"></SfButton>
    /// </SfAppBar>
    /// ]]></code>
    /// </example>
    public partial class SfAppBar : SfBaseComponent
    {
        private const string CLS_HORIZONTAL_BOTTOM = "e-horizontal-bottom";
        private const string CLS_STICKY = "e-sticky";
        private const string CLS_PROMINENT = "e-prominent";
        private const string CLS_DENSE = "e-dense";
        private const string CLS_RTL = "e-rtl";
        private const string CLS_LIGHT = "e-light";
        private const string CLS_DARK = "e-dark";
        private const string CLS_PRIMARY = "e-primary";
        private const string CLS_INHERIT = "e-inherit";
        private string AppBarClass { get; set; } = string.Empty;
        private AppBarPosition position;
        private bool isSticky;
        private AppBarMode mode;
        private AppBarColor colorMode;
        private Dictionary<string, object> htmlAttributes;
        private bool isDestroyed;

        /// <summary>
        /// Gets or sets mode of the AppBar that defines the AppBar height. 
        /// </summary>
        /// <value>
        /// One of the <see cref="AppBarMode"/> enumeration. The default value is <see cref="AppBarMode.Regular"/>
        /// </value>
        [Parameter]
        public AppBarMode Mode { get; set; } = AppBarMode.Regular;

        /// <summary>
        /// Gets or sets position of the AppBar.
        /// </summary>
        /// <value>
        /// One of the <see cref="AppBarPosition"/> enumeration. The default value is <see cref="AppBarPosition.Top"/>
        /// </value>
        [Parameter]
        public AppBarPosition Position { get; set; } = AppBarPosition.Top;

        /// <summary> 
        /// Gets or sets the custom classes to customize the AppBar component.  
        /// </summary>
        /// <remarks> 
        /// Accepts single/multiple classes (separated by a space) to be used for AppBar customization. 
        /// </remarks>
        /// <value> 
        /// If we set the css class, then the custom class is applied for AppBar. The default value is <c>string.Empty</c>. 
        /// </value>
        /// <example>
        /// In the below example AppBar background and color is customized using <c>CssClass</c> property.
        /// <code><![CDATA[ 
        /// <SfAppBar CssClass="custom-appbar">
        /// <SfButton CssClass="e-inherit" IconCss="e-icons e-menu"></SfButton>
        /// </SfAppBar>
        /// <style>
        /// .custom-appbar {
        /// background: #adadb1;
        /// color: #fff;
        /// }
        /// </style>
        /// ]]></code> 
        /// </example>
        [Parameter]
        public string CssClass { get; set; }

        /// <summary> 
        /// Gets or sets whether the AppBar position is fixed or not while scrolling the page. 
        /// </summary> 
        /// <value> 
        /// <c>true</c>, The AppBar will be sticky while scrolling. The default value is <c>false</c>. 
        /// </value>
        [Parameter]
        public bool IsSticky { get; set; }

        /// <summary> 
        /// Gets or sets a collection of additional attributes that will be applied to the AppBar element. 
        /// </summary> 
        /// <remarks> 
        /// Additional attributes can be added by specifying as inline attributes or by specifying <c>@attributes</c> directive. 
        /// </remarks> 
        /// <value> 
        /// It allows the AppBar component to render non-declared attributes. The default value is `null`. 
        /// </value>
        /// <example>
        /// In the below code example, Elevation of the AppBar customized using <c>@attributes</c> directive.
        /// <code><![CDATA[ 
        /// <SfAppBar @attributes="customAttribute">
        /// <SfButton CssClass="e-inherit" IconCss="e-icons e-home"></SfButton>
        /// </SfAppBar> 
        /// @code{ 
        ///    Dictionary<string, object> customAttribute = new Dictionary<string, object>() 
        ///    { 
        ///        { "style", "box-shadow: 0 5px 5px -3px rgba(0,0,0,.06), 0 8px 10px 1px rgba(0,0,0,.042), 0 3px 14px 2px rgba(0,0,0,.036)" } 
        ///    }; 
        /// } 
        /// ]]></code> 
        /// </example> 
        [Parameter(CaptureUnmatchedValues = true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<string, object> HtmlAttributes
        { 
            get => htmlAttributes;
            set => htmlAttributes = SfBaseUtils.SanitizeHtmlAttributes(value);
        }

        /// <summary>
        /// Gets or sets the color mode that defines the color of AppBar component.
        /// </summary>
        /// <value>
        /// One of the <see cref="AppBarColor"/> enumeration. The default value is <see cref="AppBarColor.Light"/>
        /// </value>
        [Parameter]
        public AppBarColor ColorMode { get; set; } = AppBarColor.Light;

        /// <summary>
        /// Gets or sets the child content of AppBar component.
        /// </summary>
        /// <value>
        /// The value used to build the content.
        /// </value>
        /// <example>
        /// The below example, AppBarSpacer component used to align the Buttons on left and right.
        /// <code><![CDATA[
        /// <SfAppBar>
        /// <SfButton CssClass="e-inherit" IconCss="e-icons e-home"></SfButton>
        /// <AppBarSpacer />
        /// <SfButton CssClass="e-inherit" IconCss="e-icons e-pan"></SfButton>
        /// <SfButton CssClass="e-inherit" IconCss="e-icons e-close"></SfButton>
        /// </SfAppBar>
        /// ]]></code>
        /// </example>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary> 
        /// Triggers when the component is created.  
        /// </summary> 
        /// <value> 
        /// Fired when AppBar created. 
        /// </value>
        [Parameter]
        public EventCallback<object> Created { get; set; }

        /// <summary>
        /// Triggers when the component is destroyed.
        /// </summary>
        /// <value> 
        /// Fired when AppBar destroyed. 
        /// </value>
        [Parameter]
        public EventCallback<object> Destroyed { get; set; }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            AppBarClass = "e-appbar e-control e-lib";
            if (Position == AppBarPosition.Bottom)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_HORIZONTAL_BOTTOM);
            }
            if (IsSticky)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_STICKY);
            }
            SetHeightMode();
            SetColorMode();
            if (SyncfusionService.options.EnableRtl)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_RTL);
            }
            UpdateHtmlAttributes();
            isSticky = IsSticky;
            position = Position;
            mode = Mode;
            colorMode = ColorMode;
        }

        /// <inheritdoc/>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (Position != position)
            {
                position = Position;
                AppBarClass = Position == AppBarPosition.Bottom ? SfBaseUtils.AddClass(AppBarClass, CLS_HORIZONTAL_BOTTOM) : SfBaseUtils.RemoveClass(AppBarClass, CLS_HORIZONTAL_BOTTOM);
            }
            if (IsSticky != isSticky)
            {
                isSticky = IsSticky;
                AppBarClass = IsSticky ? SfBaseUtils.AddClass(AppBarClass, CLS_STICKY) : SfBaseUtils.RemoveClass(AppBarClass, CLS_STICKY);
            }
            if (Mode != mode)
            {
                mode = Mode;
                AppBarClass = SfBaseUtils.RemoveClass(AppBarClass, CLS_DENSE);
                AppBarClass = SfBaseUtils.RemoveClass(AppBarClass, CLS_PROMINENT);
                SetHeightMode();
            }
            if (ColorMode != colorMode)
            {
                colorMode = ColorMode;
                AppBarClass = SfBaseUtils.RemoveClass(AppBarClass, CLS_DARK);
                AppBarClass = SfBaseUtils.RemoveClass(AppBarClass, CLS_PRIMARY);
                AppBarClass = SfBaseUtils.RemoveClass(AppBarClass, CLS_INHERIT);
                AppBarClass = SfBaseUtils.RemoveClass(AppBarClass, CLS_LIGHT);
                SetColorMode();
            }
        }

        /// <inheritdoc/>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
            if (firstRender && Created.HasDelegate)
            {
                await Created.InvokeAsync(null).ConfigureAwait(true);
            }
        }

        internal override void ComponentDispose()
        {
            if (IsRendered && !isDestroyed)
            {
                if (Destroyed.HasDelegate == true)
                {
                    Task.Yield().GetAwaiter().OnCompleted(async () =>
                    {
                        await Destroyed.InvokeAsync(null).ConfigureAwait(true);
                    });
                }
                isDestroyed = true;
                htmlAttributes = null;
            }
        }

        private void UpdateHtmlAttributes()
        {
            if (htmlAttributes != null && htmlAttributes.TryGetValue("class", out object clsValue))
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, clsValue as string);
                htmlAttributes.Remove("class");
            }
        }

        private void SetHeightMode()
        {
            if (Mode == AppBarMode.Prominent)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_PROMINENT);
            }
            else if (Mode == AppBarMode.Dense)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_DENSE);
            }
        }

        private void SetColorMode()
        {
            if (ColorMode == AppBarColor.Light)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_LIGHT);
            }
            else if (ColorMode == AppBarColor.Dark)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_DARK);
            }
            else if (ColorMode == AppBarColor.Primary)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_PRIMARY);
            }
            else if (ColorMode == AppBarColor.Inherit)
            {
                AppBarClass = SfBaseUtils.AddClass(AppBarClass, CLS_INHERIT);
            }
        }
    }
}