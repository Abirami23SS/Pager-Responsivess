using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Represents a component which displays a line to visually group or separate the AppBar contents.
    /// </summary>
    /// <remarks>
    /// <c>AppBarSeparator</c> shows a vertical line which used to group AppBar contents 
    /// by separating single or group of AppBar contents.
    /// </remarks>
    /// <value> 
    /// It allows to group the Appbar contents. 
    /// </value>
    /// <example> 
    /// <code><![CDATA[ 
    /// <SfAppBar>
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-home"></SfButton>
    /// <AppBarSeparator />
    /// <SfButton CssClass="e-inherit" IconCss="e-icons e-pan"></SfButton>
    /// </SfAppBar>
    /// ]]></code> 
    /// </example>
    public partial class AppBarSeparator : SfOwningComponentBase
    {
        private const string CLS_APPBAR_SEPARATOR = "e-appbar-separator";
    }
}