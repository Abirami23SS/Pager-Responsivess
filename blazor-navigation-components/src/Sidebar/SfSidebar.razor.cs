using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Internal;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// The SfSidebar component is an expandable and collapsible component that typically acts as a side container to place primary or secondary content alongside the main content.
    /// </summary>
    public partial class SfSidebar : SfBaseComponent
    {
        internal const string DOCKER = "e-dock e-blazor-dock";
        internal const string SPACE = " ";
        internal const string RTL = "e-rtl";
        internal const string VISIBILITY = "e-visibility";
        internal const string LEFT = "e-left";
        internal const string RIGHT = "e-right";
        internal const string OPEN = "e-open";
        internal const string TRANSITION = "e-transition";
        internal const string CLOSE = "e-close";
        internal const string SLIDE = "e-slide";
        internal const string ANIMATION = "e-disable-animation";
        internal const string ABSOLUTE = "e-sidebar-absolute";
        internal const string OVER = "e-over";
        internal const string PUSH = "e-push";
        internal const string OVERFLOW = " e-sidebar-overflow";
        internal const string IDPREFIX = "sidebar-";
        internal string dataId = "sfSidebar-" + Guid.NewGuid().ToString();

        private string sidebarClass = string.Empty;

        private string styles = string.Empty;

        private ElementReference element;

        private bool isDeviceMode;

        private bool isMediaQueryOpen;

        private bool isDestroyed;

        private bool isInteracted;

        private bool openState;

        private bool isVisible;

        private Dictionary<string, object> attributes = new Dictionary<string, object>();

        private double LeftPosition { get; set; }

        private double TopPosition { get; set; }

        private List<KeyValuePair<string, object>>? propertyKeys;

        [CascadingParameter]
        SfSidebarContainer? SfSidebarContainer { get; set; }

        // Returns the valid dimension for given width value.
        private static string SetDimension(string width)
        {
            return SfBaseUtils.FormatUnit(width);
        }

        // Initial updates with properties like setting Dock width and media query values.
        internal async Task SidebarInitRender()
        {
            if (SfSidebarContainer != null && IsOpen && !openState)
            {
                SfSidebarContainer.SetWidth(SidebarWidth == "auto" ? "240px" : SidebarWidth!);
                openState = true;
                await SidebarShow().ConfigureAwait(true);
            }
            if (SfSidebarContainer == null && isMediaQueryOpen && ((Type == SidebarType.Auto && !isDeviceMode) || (Type != SidebarType.Auto && IsOpen)))
            {
                openState = true;
                await SidebarShow().ConfigureAwait(true);
            }
            else if (!IsOpen && !sidebarClass.Contains(CLOSE, StringComparison.Ordinal))
            {
                sidebarClass = SfBaseUtils.AddClass(sidebarClass, CLOSE);
            }

            if (EnableDock && !IsOpen)
            {
                SfSidebarContainer?.SetWidth(DockSize);
                SetDock();
            }
        }

        // Returns the style properties of sidebar component by adding default Z-index value.
        private void GetStyle()
        {
            styles = string.Empty;
            styles += " z-index: " + ZIndex + ";";
            if (!openState)
            {
                styles += SPACE + "width: " + SetDimension(EnableDock ? DockSize : Width) + ";";
            }
            else
            {
                styles += SPACE + "width: " + SetDimension(Width) + ";";
                if (EnableDock)
                {
                    styles += SPACE + "transform: none;";
                }
            }
        }

        // Returns the basic root classes to be added for the sidebar component.
        private void GetClass()
        {
            string classNames = "e-control e-sidebar e-lib";
            if (EnableRtl || (SyncfusionService != null && SyncfusionService.options.EnableRtl))
            {
                classNames = SfBaseUtils.AddClass(classNames, RTL);
            }

            if (EnableDock)
            {
                classNames = SfBaseUtils.AddClass(classNames, DOCKER);
            }
            else if (Type != SidebarType.Auto)
            {
                classNames = SfBaseUtils.AddClass(classNames, VISIBILITY);
            }

            classNames = SfBaseUtils.AddClass(classNames, Position == SidebarPosition.Left ? LEFT : RIGHT);
            if (!((Animate && SyncfusionService != null && SyncfusionService.options.Animation == GlobalAnimationMode.Default) || (SyncfusionService != null && SyncfusionService.options.Animation == GlobalAnimationMode.Enable)))
            {
                classNames = SfBaseUtils.AddClass(classNames, ANIMATION);
                classNames = SfBaseUtils.RemoveClass(classNames, "e-blazor-dock");
            }

            if (!string.IsNullOrEmpty(Target))
            {
                classNames = SfBaseUtils.AddClass(classNames, ABSOLUTE);
            }

            classNames = SfBaseUtils.AddClass(classNames, TRANSITION);
            classNames = SfBaseUtils.AddClass(classNames, !openState ? CLOSE : OPEN);
            switch (Type)
            {
                case SidebarType.Push:
                    classNames = SfBaseUtils.AddClass(classNames, PUSH);
                    break;
                case SidebarType.Slide:
                    classNames = SfBaseUtils.AddClass(classNames, openState ? SLIDE + OVERFLOW : SLIDE);
                    break;
                case SidebarType.Over:
                    classNames = SfBaseUtils.AddClass(classNames, OVER);
                    break;
                default:
                    classNames = SfBaseUtils.AddClass(classNames, isDeviceMode ? OVER : PUSH);
                    break;
            }
            sidebarClass = classNames;
        }

        private void UpdateClass()
        {
            GetClass();
            GetStyle();
            UpdateAttributes();
            StateHasChanged();
        }

        /// <summary>
        /// Update the Persistence value to local storage.
        /// </summary>
        private async Task SetLocalStorage(string persistId, string dataValue)
        {
            await InvokeMethod("window.localStorage.setItem", new object[] { persistId, dataValue }).ConfigureAwait(true);
        }

        /// <summary>
        /// Updating the persisting values to our component properties.
        /// </summary>
        private string SerializeModel()
        {
            return JsonSerializer.Serialize(new PersistenceValues { IsOpen = IsOpen });
        }

        /// <summay>
        /// Updates attributes added for the Sidebar component.
        /// </summay>
        private void UpdateAttributes()
        {
            attributes = new Dictionary<string, object>();
            SfBaseUtils.UpdateDictionary("id", ID, attributes);
            SfBaseUtils.UpdateDictionary("tabindex", "0", attributes);
            SfBaseUtils.UpdateDictionary("class", sidebarClass, attributes);
            SfBaseUtils.UpdateDictionary("data-sf-style", styles, attributes);
            if (SidebarHtmlAttributes != null)
            {
                foreach (string key in SidebarHtmlAttributes.Keys)
                {
                    if (key.Equals("style", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!attributes.ContainsKey("data-sf-style"))
                            attributes["data-sf-style"] = SidebarHtmlAttributes[key];
                        continue;
                    }
                    if (key == "class")
                    {
                        SfBaseUtils.UpdateDictionary("class", SfBaseUtils.AddClass(attributes["class"].ToString(), SidebarHtmlAttributes[key].ToString()), attributes);
                    }
                    else
                    {
                        attributes[key] = SidebarHtmlAttributes[key];
                    }
                }
            }
        }

        /// <summary>
        ///   Updates the dock styles and classes for the sidebar element.
        /// </summary>
        /// <exclude/>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetDock()
        {
            GetStyle();
            if (EnableDock && !openState)
            {
                int dimension = Position == SidebarPosition.Left ? -100 : 100;
                string transform = Position == SidebarPosition.Left ? SetDimension(DockSize) : "-" + SetDimension(DockSize);
                styles += " transform: translateX(" + dimension + "%) translateX(" + transform + ")";
            }

            UpdateAttributes();
        }

        /// <summary>
        /// Triggers change event.
        /// </summary>
        /// <exclude/>
        /// <param name="visible">visibles.</param>
        /// <param name="argsvalue">argsvalue.</param>
        /// <returns>"Task".</returns>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerChange(bool visible, ChangeEventArgs argsvalue = null)
        {
            if (isVisible != visible)
            {
                if (Changed.HasDelegate)
                {
                    ChangeEventArgs eventArgs = new ChangeEventArgs
                    {
                        Element = element,
                        Name = "Changed",
                        IsInteracted = argsvalue != null,
                    };
                    await Changed.InvokeAsync(eventArgs).ConfigureAwait(true);
                }
                isVisible = visible;
            }
        }

        // Updates the type of the sidebar component.
        private async Task SetSidebarType()
        {
            if (SfSidebarContainer == null)
                await InvokeMethod("sfBlazor.Sidebar.setType", new object[] { dataId, GetProperties() }).ConfigureAwait(true);
        }

        // Dynamic porperty changes handler
        internal async Task SidebarPropertyChange(Dictionary<string, object> propertyChanges)
        {
            propertyKeys = propertyChanges.ToList();

            List<KeyValuePair<string, object>> localKeys = propertyChanges.ToList();
            foreach (var property in localKeys)
            {
                if (property.Key == nameof(Position))
                {
                    SetDock();
                    GetClass();
                    await SetSidebarType().ConfigureAwait(true);
                    UpdateAttributes();
                }

                if (property.Key == nameof(EnableDock))
                {
                    GetClass();
                    await SetSidebarType().ConfigureAwait(true);
                    SetDock();
                }

                if (property.Key == nameof(Width))
                {
                    SetDock();
                }

                if (property.Key == nameof(Type))
                {
                    GetClass();
                    await CheckType().ConfigureAwait(true);
                    UpdateAttributes();
                }

                if (property.Key == nameof(IsOpen))
                {
                    if ((bool)property.Value != openState)
                    {
                        if ((bool)property.Value)
                        {
                            isInteracted = true;
                            await SidebarShow().ConfigureAwait(true);
                            if (SfSidebarContainer != null && EnableDock)
                            {
                                SfSidebarContainer.SetWidth(SidebarWidth == "auto" ? "240px" : SidebarWidth!);
                            }
                        }
                        else
                        {
                            isInteracted = true;
                            await SidebarHide().ConfigureAwait(true);
                            if (SfSidebarContainer != null && EnableDock)
                            {
                                SfSidebarContainer.SetWidth(DockSize);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  Invoke show method from client.
        /// </summary>
        /// <exclude/>
        /// <param name="args">args.</param>
        /// <returns>"Task".</returns>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerShow(EventArgs args)
        {
            isMediaQueryOpen = true;
            isInteracted = args != null;
            await SidebarShow().ConfigureAwait(true);
        }

        /// <summary>
        ///  Invoke hide method from client.
        ///  </summary>
        /// <exclude/>
        /// <param name="args">args.</param>
        /// <returns>"Task".</returns>
        [JSInvokable]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public async Task TriggerHide(EventArgs args)
        {
            isMediaQueryOpen = false;
            isInteracted = args != null;
            if(args != null)
            {
                LeftPosition = (double)args.Left!;
                TopPosition = (double)args.Top!;
            }
            await SidebarHide().ConfigureAwait(true);
        }

        // Returns event argument for the sidebar open/close events.
        private EventArgs SidebarEvent(string eventValue)
        {
            EventArgs eventArgs = new EventArgs
            {
                Cancel = false,
                Element = element,
                Name = eventValue,
                Left = LeftPosition,
                Top = TopPosition,
                IsInteracted = isInteracted
            };
            isInteracted = false;
            return eventArgs;
        }

        // Specifies the sidebar component dispose method.
        internal async override void ComponentDispose()
        {
            if (IsRendered && !isDestroyed)
            {
                try
                {
                    isDestroyed = true;
                    await InvokeMethod("sfBlazor.Sidebar.destroy", dataId).ConfigureAwait(true);
                    propertyKeys?.Clear();
                    propertyKeys = null;
                    if (Destroyed.HasDelegate)
                        await Destroyed.InvokeAsync(null).ConfigureAwait(true);
                    await WindowInstanceDispose(dataId).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    if (Destroyed.HasDelegate)
                        await Destroyed.InvokeAsync(e).ConfigureAwait(true);
                    throw new InvalidOperationException(e.Message);
                }
            }
        }

        // Updates sidebar element state based in the type of the sidebar component.
        private async Task CheckType()
        {
            if (Type == SidebarType.Auto)
            {
                await SidebarShow().ConfigureAwait(true);
            }
            else if (!sidebarClass.Contains(CLOSE, StringComparison.Ordinal))
            {
                await SidebarHide().ConfigureAwait(true);
            }
        }

        // Returns the properties of the sidebar component to be sent to the client side handling.

        /// <summary>
        ///  Method to Get Properties.
        /// </summary>
        /// <returns>properties.</returns>
        protected Dictionary<string, object> GetProperties()
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            properties.Add("DockSize", DockSize);
            properties.Add("Animate", Animate);
            properties.Add("IsOpen", IsOpen);
            properties.Add("EnableDock", EnableDock);
            properties.Add("MediaQuery", MediaQuery);
            properties.Add("Position", Position.ToString());
            properties.Add("Type", Type.ToString());
            properties.Add("CloseOnDocumentClick", CloseOnDocumentClick);
            properties.Add("Width", Width);
            properties.Add("EnableGestures", EnableGestures);
            properties.Add("ShowBackdrop", ShowBackdrop);
            properties.Add("Target", Target);
            return properties;
        }
    }
}
