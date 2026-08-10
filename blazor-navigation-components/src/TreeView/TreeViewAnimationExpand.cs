using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Specifies the animation to appear while expanding the TreeView item.
    /// </summary>
    public partial class TreeViewAnimationExpand : SfOwningComponentBase
    {
        [CascadingParameter]
        private TreeViewNodeAnimationSettings? Parent { get; set; }

        /// <summary>
        /// Specifies the time duration to transform content.
        /// </summary>
        [Parameter]
        public int Duration { get; set; } = 400;

        /// <summary>
        /// Specifies the easing effect applied when transforming the content.
        /// </summary>
        [Parameter]
        public string Easing { get; set; } = "linear";

        /// <summary>
        /// Specifies the animation effect for expanding the TreeView node.
        /// Default animation is given as SlideDown. You can also disable the animation by setting the animation effect as None.
        /// </summary>
        [Parameter]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AnimationEffect Effect { get; set; } = AnimationEffect.SlideDown;
        
        internal void SetDuration(int duration)
        {
            Duration = duration;
        }

        /// <summary>
        ///  Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>"Task".</returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            Parent?.UpdateExpandProperties(this);
        }

        /// <summary>
        /// Dispose the Expand animation value.
        /// </summary>
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