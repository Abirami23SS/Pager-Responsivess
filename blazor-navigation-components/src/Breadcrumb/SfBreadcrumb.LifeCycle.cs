using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfBreadcrumb
    {

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            activeItem = ActiveItem;
            EnableRtl = EnableRtl || SyncfusionService.options.EnableRtl;
            _url = Url;
            ScriptModules = SfScriptModules.SfBreadcrumb;
            breadcrumbItems = Items;
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync().ConfigureAwait(true);
            _maxItems = MaxItems;
            BreadcrumbClass = "e-breadcrumb e-control e-lib";
            if (Items != breadcrumbItems)
            {
                PopupItems = new List<BreadcrumbItem>();
                breadcrumbItems = Items;
            }
            if (OverflowMode == BreadcrumbOverflowMode.Wrap)
            {
                BreadcrumbClass = SfBaseUtils.AddClass(BreadcrumbClass, "e-breadcrumb-wrap-mode");
            }
            else if (OverflowMode == BreadcrumbOverflowMode.Scroll)
            {
                BreadcrumbClass = SfBaseUtils.AddClass(BreadcrumbClass, "e-breadcrumb-scroll-mode");
            }
            if (htmlAttributes != null)
            {
                if (htmlAttributes.TryGetValue("class", out var classValue))
                {
                    BreadcrumbClass = SfBaseUtils.AddClass(BreadcrumbClass, classValue.ToString());
                    htmlAttributes.Remove("class");
                }
                if (htmlAttributes.TryGetValue("id", out var idValue))
                {
                    IdValue = idValue.ToString()!;
                }
            }
            if (EnableRtl || SyncfusionService.options.EnableRtl)
            {
                BreadcrumbClass = SfBaseUtils.AddClass(BreadcrumbClass, "e-rtl");
            }
            if (Disabled)
            {
                BreadcrumbClass = SfBaseUtils.AddClass(BreadcrumbClass, "e-disabled");
            }
            if (_url != Url)
            {
                UpdateItemsFromUrl();
                _url = Url;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (Items == null)
                {
                    UpdateItemsFromUrl();
                }
                if (EnablePersistence)
                {
                    var localStorageValue = await InvokeMethod<string>("window.localStorage.getItem", false, new object[] { IdValue! }).ConfigureAwait(true);
                    localStorageValue = string.IsNullOrEmpty(localStorageValue) ? null : localStorageValue;
                    if (localStorageValue != null && localStorageValue != "null")
                    {
                        var persistValue = (string)SfBaseUtils.ChangeType(localStorageValue, typeof(string));
                        if (persistValue != null)
                        {
                            ActiveItem = persistValue;
                            StateHasChanged();
                        }
                    }
                }
            }
            else if (PropertyChanges.Count > 0)
            {
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
        }

        protected override bool ShouldRender()
        {
            if(!_shouldRender)
            {
                _shouldRender = true;
                return false;
            }
            return _shouldRender;
        }

        private void UpdateItemsFromUrl()
        {
            try
            {
                string prevUri;
                string[] uri;
                if (Url != null)
                {
                    Uri givenUri = new Uri(Url);
                    prevUri = givenUri.GetLeftPart(System.UriPartial.Authority) + "/";
                    uri = givenUri.AbsoluteUri.Split(prevUri)[1].Split("/");
                }
                else
                {
                    prevUri = navigationManager!.BaseUri;
                    uri = navigationManager.Uri.Split(prevUri)[1].Split("/");
                }
                List<BreadcrumbItem> items = new List<BreadcrumbItem>();
                BreadcrumbItem item = new BreadcrumbItem();
                item.UpdateChildProperties("iconCss", "e-icons e-home");
                item.UpdateChildProperties("url", prevUri);
                items.Add(item);
                for (int i = 0; i < uri.Length; i++)
                {
                    if (uri[i].Length > 0)
                    {
                        item = new BreadcrumbItem();
                        item.UpdateChildProperties("text", uri[i]);
                        item.UpdateChildProperties("url", prevUri + uri[i]);
                        items.Add(item);
                        prevUri += uri[i] + "/";
                    }
                }
                Items = items;
                ActiveItem = activeItem = items[items.Count - 1].Url;
                StateHasChanged();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        internal override async Task OnAfterScriptRendered()
        {
            await InvokeMethod("sfBlazor.Breadcrumb.initialize", new object[] { dataId, Element, DotnetObjectReference, OverflowMode.ToString(), MaxItems }).ConfigureAwait(true);
        }

        internal override void ComponentDispose()
        {
            if (IsRendered)
            {
                InvokeMethod("sfBlazor.Breadcrumb.destroy", new object[] { dataId }).ContinueWith(t => { }, TaskScheduler.Current);
                WindowInstanceDispose(dataId).ConfigureAwait(false);
            }
        }
    }
}
