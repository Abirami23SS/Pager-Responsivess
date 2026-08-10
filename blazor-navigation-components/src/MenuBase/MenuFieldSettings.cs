using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Configures the field options of the Menu.
    /// </summary>
    public class MenuFieldSettings: SfOwningComponentBase
    {
        [CascadingParameter]
        private IMenu Parent { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the children field for Menu item.
        /// </summary>
        [Parameter]
        public string Children { get; set; } = "Items";

        /// <summary>
        /// Gets or sets a value that indicates the CSS icon field for Menu item.
        /// </summary>
        [Parameter]
        public string IconCss { get; set; } = "IconCss";

        /// <summary>
        /// Gets or sets a value that indicates the itemId field for Menu item.
        /// </summary>
        [Parameter]
        public string ItemId { get; set; } = "Id";

        /// <summary>
        /// Gets or sets a value that indicates the parentId field for Menu item.
        /// </summary>
        [Parameter]
        public string ParentId { get; set; } = "ParentId";

        /// <summary>
        /// Gets or sets a value that indicates the separator field for Menu item.
        /// </summary>
        [Parameter]
        public string Separator { get; set; } = "Separator";

        /// <summary>
        /// Gets or sets a value that indicates the disabled field for Menu item.
        /// </summary>
        [Parameter]
        public string Disabled { get; set; } = "Disabled";

        /// <summary>
        /// Gets or sets a value that indicates the hidden field for Menu item.
        /// </summary>
        [Parameter]
        public string Hidden { get; set; } = "Hidden";

        /// <summary>
        /// Gets or sets a value that indicates the text field for Menu item.
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "Text";

        /// <summary>
        /// Gets or sets a value that indicates the URL field for Menu item.
        /// </summary>
        [Parameter]
        public string Url { get; set; } = "Url";

        /// <summary>
        /// Gets or sets a value that indicates the @attributes (additional attributes) field for Menu item.
        /// </summary>
        [Parameter]
        public string HtmlAttributes { get; set; } = "HtmlAttributes";

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent.UpdateChildProperties("Fields", this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                Parent = null;
            }
        }
    }
}