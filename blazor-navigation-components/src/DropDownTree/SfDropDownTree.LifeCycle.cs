using System.Collections.Generic;
using System.Threading.Tasks;
using Syncfusion.Blazor.Internal;
using Syncfusion.Blazor.Navigations.Internal;
using System;
using System.Linq;
using Syncfusion.Blazor.Data;
using System.Collections;
using System.Text.Json;

namespace Syncfusion.Blazor.Navigations
{
    public partial class SfDropDownTree<TValue, TItem>
    {
        /// <inheritdoc/>
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync().ConfigureAwait(true);
            if (EnablePersistence && string.IsNullOrEmpty(ID))
                throw new InvalidOperationException($"The {nameof(ID)} property of Dropdown Tree must not be null or empty when using EnablePersistence.");
            ScriptModules = SfScriptModules.SfDropDownTree;
            value = previousValue = currentValue = Value;
            showCheckBox = ShowCheckBox;
            showClearButton = ShowClearButton;
            showSelectAll = ShowSelectAll;
            popupWidth = PopupWidth;
            popupHeight = PopupHeight;
            zIndex = ZIndex;
            allowFiltering = AllowFiltering;
            allowMultiSelection = AllowMultiSelection;
            disabled = Disabled;
            text = Text;
            mode = Mode;
            delimiterChar = DelimiterChar;
            textWrap = TextWrap;
            SetAttributes();
        }

        /// <inheritdoc/>
        protected override async Task OnParametersSetAsync()
        {
            try
            {
                await base.OnParametersSetAsync().ConfigureAwait(true);
                Dictionary<string, object> changedProperties = new Dictionary<string, object>();
                if (!SfBaseUtils.Equals(value, Value))
                {
                    if (!SfBaseUtils.Equals(Value, currentValue))
                    {
                        currentValue = Value;
                        await UpdateValue(Value).ConfigureAwait(true);
                    }
                    value = Value;
                }
                if (showCheckBox != ShowCheckBox)
                {
                    showCheckBox = ShowCheckBox;
                    await UpdateSelectedValues(true).ConfigureAwait(true);
                    changedProperties.Add("showCheckBox", ShowCheckBox);
                }
                if (showSelectAll != ShowSelectAll)
                {
                    showSelectAll = ShowSelectAll;
                    changedProperties.Add("showSelectAll", ShowSelectAll);
                }
                if (showClearButton != ShowClearButton)
                {
                    showClearButton = ShowClearButton;
                    changedProperties.Add("showClearButton", ShowClearButton);
                }
                if (!string.Equals(popupWidth, PopupWidth, StringComparison.Ordinal))
                {
                    popupWidth = PopupWidth;
                    changedProperties.Add("popupWidth", PopupWidth);
                }
                if (!string.Equals(popupHeight, PopupHeight, StringComparison.Ordinal))
                {
                    popupWidth = PopupHeight;
                    changedProperties.Add("popupHeight", PopupHeight);
                }
                if (!Equals(zIndex, ZIndex))
                {
                    zIndex = ZIndex;
                    changedProperties.Add("zIndex", ZIndex);
                }
                if (allowFiltering != AllowFiltering)
                {
                    allowFiltering = AllowFiltering;
                    changedProperties.Add("allowFiltering", AllowFiltering);
                }
                if (allowMultiSelection != AllowMultiSelection)
                {
                    allowMultiSelection = AllowMultiSelection;
                    await UpdateSelectedValues(true).ConfigureAwait(true);
                    changedProperties.Add("allowMultiSelection", AllowMultiSelection);
                }
                if (disabled != Disabled)
                {
                    disabled = Disabled;
                    changedProperties.Add("disabled", Disabled);
                }
                if (textWrap != TextWrap)
                {
                    textWrap = TextWrap;
                    changedProperties.Add("textWrap", TextWrap);
                }
                if (!string.Equals(text, Text, StringComparison.Ordinal) && !isInternalChange)
                {
                    text = Text;
                    changedProperties.Add("text", Text);
                }
                if (!SfBaseUtils.Equals(mode, Mode))
                {
                    mode = Mode;
                    changedProperties.Add("mode", Mode);
                }
                if (!string.Equals(delimiterChar, DelimiterChar, StringComparison.Ordinal))
                {
                    delimiterChar = DelimiterChar;
                    await UpdateSelectedValues().ConfigureAwait(true);
                }

                if (changedProperties.Count > 0)
                {
                    if (changedProperties.ContainsKey("mode") || changedProperties.ContainsKey("textWrap"))
                    {
                        bool validMode = AllowMultiSelection || ShowCheckBox;
                        if (!validMode)
                            return;
                        await SetMultiSelect().ConfigureAwait(true);
                    }
                    await InvokeMethod("sfBlazor.DropDownTree.updateProperties", new object[] { dataId, changedProperties }).ConfigureAwait(true);

                    if (changedProperties.ContainsKey("text") && !isDestroyed)
                    {
                        if (string.IsNullOrEmpty(Text))
                            await ResetValue().ConfigureAwait(true);
                        else
                            await SetTreeText(true).ConfigureAwait(true);
                    }
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <inheritdoc/>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            try
            {
                await base.OnAfterRenderAsync(firstRender).ConfigureAwait(true);
                if (firstRender)
                {
                    dataType = (!string.IsNullOrEmpty(DropDownTreeFields?.ParentID) || (!string.IsNullOrEmpty(DropDownTreeFields?.HasChildren) && string.IsNullOrEmpty(DropDownTreeFields?.Child))) ? TreeViewDataType.SelfReferential : TreeViewDataType.Hierarchical;
                    if (InputBaseObj != null)
                    {
                        InputBaseObj.ClearIconClass = "e-clear-icon e-icons e-icon-hide";
                    }
                    if (EnablePersistence)
                    {
                        List<TValue> localStorageValue = await InvokeMethod<List<TValue>>("window.localStorage.getItem", true, new object[] { ID }).ConfigureAwait(true);
                        if (localStorageValue == null)
                        {
                            await SetLocalStorage(ID, SerializeModel()).ConfigureAwait(true);
                        }
                        else
                        {
                            currentValue = Value = localStorageValue;
                        }
                    }
                    if (DropDownTreeFields?.DataManager != null)
                    {
                        try
                        {
                            DropDownTreeField<TItem> tempField = DropDownTreeFields;
                            object itemsData = await tempField.DataManager.ExecuteQuery<TItem>(tempField.Query).ConfigureAwait(true);
                            IEnumerable? nodeData = tempField.Query != null && tempField.Query.IsCountRequired ? ((DataResult)itemsData).Result ?? new List<object>() : itemsData as IEnumerable;
                            List<TItem>? remoteData = nodeData?.Cast<TItem>().ToList();
                            DataSource = remoteData?.ToList()!;
                        }
                        catch (Exception exception)
                        {
                            OnFailure(exception);
                            return;
                            throw;
                        }
                    }
                    UpdateAllData(DataSource!.ToList(), expandedNodes?.ToList() ?? new(), checkedNodes?.ToList() ?? new(), selectedNodes?.ToList() ?? new());
                    overAllLiItems = AllData.Count;
                    await SetTreeValue(false).ConfigureAwait(true);
                    await SetTreeText(false).ConfigureAwait(true);
                    if (Created.HasDelegate)
                        await Created.InvokeAsync().ConfigureAwait(true);
                }
            }
            catch
            {
                if (!IsDisposed)
                    throw;
            }
        }

        /// <inheritdoc/>
        internal override async Task OnAfterScriptRendered()
        {
            if (InputBaseObj != null)
            {
                await InvokeMethod("sfBlazor.DropDownTree.initialize", new object[] { dataId, InputBaseObj.ContainerElement, DotnetObjectReference, GetInstance(), uniqueID }).ConfigureAwait(true);
            }
        }

        /// <inheritdoc/>
        protected override bool ShouldRender()
        {
            bool renderState = shouldRender;
            shouldRender = true;
            return renderState;
        }

        private void PreventRender(bool preventRender = true) => shouldRender = !preventRender;

        /// <inheritdoc/>
        internal async override void ComponentDispose()
        {
            if (!isDestroyed)
            {
                base.ComponentDispose();
                if (Destroyed.HasDelegate)
                    await Destroyed.InvokeAsync().ConfigureAwait(true);
                isDestroyed = true;
                TreeObj = null!;
                InputBaseObj = null!;
                DropDownTreeFields = null!;
                selectedNodes = null!;
                Accessor.Dispose();
                Accessor = null!;
                if (IsRendered)
                {
                    await InvokeMethod("sfBlazor.DropDownTree.destroy", new object[] { dataId }).ConfigureAwait(true);
                    await WindowInstanceDispose(dataId).ConfigureAwait(false);
                }
            }
        }
    }
}
