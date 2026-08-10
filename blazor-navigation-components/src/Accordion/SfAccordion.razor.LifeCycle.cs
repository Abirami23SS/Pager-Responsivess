using Syncfusion.Blazor.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfAccordion : SfBaseComponent
    {
        private bool enableRtl;
        private ExpandMode expandMode;
        private int[] expandedIndices;
        private string width;
        private string height;

        /// <summary>
        /// Method invoked when the component is ready to start.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnInitializedAsync()
        {
            if (string.IsNullOrEmpty(ID))
            {
                if (EnablePersistence)
                {
                    throw new InvalidOperationException("The ID property of Accordion must not be null or Empty when using EnablePersistance.");
                }
                ID = SfBaseUtils.GenerateID(ACCORDIONPREFIX);
            }

            ScriptModules = SfScriptModules.SfAccordion;
            UpdateLocalProperties();
            await base.OnInitializedAsync().ConfigureAwait(true);
            UpdateAnimationProperties(AnimationSettings);
            enableRtl = EnableRtl;
            expandMode = ExpandMode;
            expandedIndices = ExpandedIndices;
            width = Width;
            height = Height;
        }

        /// <summary>
        /// Method invoked when the component has received parameters from its parent in the render tree,
        /// and the incoming values have been assigned to properties.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            enableRtl = NotifyPropertyChanges(nameof(EnableRtl), EnableRtl, enableRtl);
            expandMode = NotifyPropertyChanges(nameof(ExpandMode), ExpandMode, expandMode);
            expandedIndices = NotifyPropertyChanges(nameof(ExpandedIndices), ExpandedIndices, expandedIndices);
            width = NotifyPropertyChanges(nameof(Width), Width, width);
            height = NotifyPropertyChanges(nameof(Height), Height, height);
            if (PropertyChanges.Count > 0)
            {
                await OnPropertyChangeHandler().ConfigureAwait(true);
            }
        }

        /// <summary>
        /// Method invoked after each time the component has been rendered.
        /// </summary>
        /// <param name="firstRender">Set to true for the first time component rendering; otherwise gets false.</param>
        /// <returns>A System.Threading.Tasks.Task representing any asynchronous operation.</returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && EnablePersistence)
            {
                var localStorage = await InvokeMethod<string>("window.localStorage.getItem", false, new object[] { $"accordion{ID}" }).ConfigureAwait(true);
                if (!string.IsNullOrEmpty(localStorage))
                {
                    int[] persistExpandedIndices = new int[Items.Count];
                    persistExpandedIndices = Array.ConvertAll<string, int>(localStorage.Split(','), Convert.ToInt32);
                    ExpandedIndices = expandedIndices = persistExpandedIndices;
                }

                UpdateExpandedIndices();
            }

            if (firstRender || IsItemChanged)
            {
                if (IsItemChanged)
                {
                    StateHasChanged();
                    IsItemChanged = false;
                    await InvokeMethod("sfBlazor.Accordion.itemChanged", new object[] { dataId }).ConfigureAwait(true);
                }
            }

            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
        }

        internal override async Task OnAfterScriptRendered()
        {
            await InvokeMethod("sfBlazor.Accordion.initialize", new object[] { dataId, Element, GetInstance(), DotnetObjectReference }).ConfigureAwait(true);
        }

        protected override bool ShouldRender()
        {
            bool isPreventRender = shouldRender;
            shouldRender = true;
            return isPreventRender;
        }

        internal override async void ComponentDispose()
        {
            if (IsRendered)
            {
                if (!string.IsNullOrEmpty(dataId))
                {
                    _ = InvokeMethod("sfBlazor.Accordion.destroy", new object[] { dataId });
                    await WindowInstanceDispose(dataId).ConfigureAwait(false);
                }
                if (Delegates?.Destroyed.HasDelegate == true)
                {
                    await Delegates.Destroyed.InvokeAsync(null).ConfigureAwait(true);
                }
            }
            Items = null;
            Delegates = null;
            rootAttributes = null;
            ExpandedItem = null;
            AnimationSettings = null;
        }
    }
}
