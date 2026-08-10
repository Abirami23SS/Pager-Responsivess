using System.Threading.Tasks;
using System.ComponentModel;
using System;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfAccordion : SfBaseComponent
    {
        /// <summary>
        /// Sets focus to the specified index item header in Accordion.
        /// </summary>
        /// <param name="index">Number value that determines which item should be focused.</param>
        /// <returns><see cref="System.Threading.Tasks.Task"/>.</returns>
        public async Task SelectAsync(int index)
        {
            await InvokeMethod("sfBlazor.Accordion.select", new object[] { dataId, index }).ConfigureAwait(true);
        }

        /// <summary>
        /// Prevents the Accordion render. This method will internally sets value to be returned from ShouldRender method.
        /// </summary>
        /// <param name="preventRender">Default value is true. Toggles the ShouldRender method value.</param>
        public void PreventRender(bool preventRender = true) => shouldRender = !preventRender;
    }
}
