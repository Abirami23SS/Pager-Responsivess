using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfCarousel : SfBaseComponent
    {

        /// <summary>
        /// Starts the transition of carousel items.
        /// </summary>
        /// <value> 
        /// It allows the carousel to starts the transition of items. 
        /// </value>
        public void Play()
        {
            AutoPlay = true;
            ApplySlideInterval();
        }

        /// <summary>
        /// Pauses the transition of carousel items.
        /// </summary>
        /// <value> 
        /// It allows the carousel to pauses the transition of items. 
        /// </value>
        public void Pause()
        {
            AutoPlay = false;
            ResetSlideInterval();
        }

        /// <summary>
        /// Navigates to previous carousel item.
        /// </summary>
        /// <value> 
        /// Move to previous carousel item. 
        /// </value>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task PreviousAsync()
        {
            int index = SelectedIndex == 0 ? Items.Count - 1 : SelectedIndex - 1;
            await SetActiveSlide(index, CarouselSlideDirection.Previous).ConfigureAwait(true);
        }

        /// <summary>
        /// Navigates to next carousel item.
        /// </summary>
        /// <value> 
        /// Move to next carousel item. 
        /// </value>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task NextAsync()
        {
            int index = SelectedIndex == Items.Count - 1 ? 0 : SelectedIndex + 1;
            await SetActiveSlide(index, CarouselSlideDirection.Next).ConfigureAwait(true);
        }

        /// <summary>
        /// Navigates to specific carousel item.
        /// </summary>
        /// <value> 
        /// Move to carousel item at desired index. 
        /// </value>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task MoveToAsync(int index)
        {
            CarouselSlideDirection direction = index > SelectedIndex ? CarouselSlideDirection.Next : CarouselSlideDirection.Previous;
            if (direction == CarouselSlideDirection.Previous && index < 0)
            {
                index = Items.Count - 1;
            }
            else if (direction == CarouselSlideDirection.Next && index > Items.Count - 1)
            {
                index = 0;
            }
            await SetActiveSlide(index, direction).ConfigureAwait(true);
        }

        /// <summary>
        /// Prevents the Carousel render. This method will internally sets value to be returned from ShouldRender method.
        /// </summary>
        /// <param name="preventRender">Default value is true. Toggles the ShouldRender method value.</param>
        public void PreventRender(bool preventRender = true) => shouldRender = !preventRender;

    }

}