using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents a component that introduce spacing between the AppBar contents which gives an additional space on content layout.
    /// </summary>
    /// <remarks>
    /// <c>AppBarSpacer</c> provides the space between the components. 
    /// So, user can tune the spacing between the content like right or left.
    /// </remarks>
    /// <value> 
    /// It provides space between the Appbar contents. 
    /// </value>
    /// <example> 
    /// <code><![CDATA[
    /// <SfAppBar>
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-home"></SfButton>
    /// <AppBarSpacer />
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-pan"></SfButton>
    /// </SfAppBar>
    /// ]]></code> 
    /// </example>
    public partial class AppBarSpacer : SfOwningComponentBase
    {
        private const string CLS_APPBAR_SPACER = "e-appbar-spacer";
    }
}