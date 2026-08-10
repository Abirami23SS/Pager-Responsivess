using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfSidebarContainer
    {
        /// <summary>
        /// Specifies the child content.
        /// </summary>
        /// <value>
        /// Accepts a RenderFragment that defines the child elements of the SfSidebarContainer. This is typically used to include the SfSidebar component within the SfSidebarContainer component.
        /// </value>
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        private string? Width { get; set; }

        internal void SetWidth( string sidebarWidth)
        {
            if (Width != sidebarWidth)
            {
                Width = sidebarWidth;
                StateHasChanged();
            }
        }
    }
}
