using System;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;
using System.Collections.Generic;

namespace Syncfusion.Blazor.Navigations
{
    /// <summary>
    /// Provides the information about OnOpen/OnClose event.
    /// </summary>
    public class BeforeOpenCloseMenuEventArgs<T>
    {
        /// <summary>
        /// Gets or sets a value that indicates whether to allow or prevent the open/close action of menu bar.
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the menu container element.
        /// </summary>
        public ElementReference Element { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the current menu items.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the parent item.
        /// </summary>
        public T ParentItem { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the menu container height to show the scrollable menu.
        /// It is applicable only when the EnableScrolling property is enabled.
        /// </summary>
        public double ScrollHeight { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the clientY position of the menu.
        /// </summary>
        public double? Top { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the clientX position of the menu.
        /// </summary>
        public double? Left { get; set; }

        /// <summary>
        /// Gets the ID of the element on which the user right click their mouse button (or finger, on touch devices) to open the context menu.
        /// </summary>
        public string TargetId { get; set;  }

        /// <summary>
        /// Gets or sets a value that indicates whether the menu is opened by hovering on the parent item.
        /// </summary>
        internal bool IsOpenHover { get; set; }
    }

    /// <summary>
    /// Provides the information about the OnItemRender/ItemSelected event.
    /// </summary>
    public class MenuEventArgs<T>
    {
        /// <summary>
        /// Gets or sets a value that indicates the menu container element.
        /// </summary>
        public ElementReference Element { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the item select event.
        /// </summary>
        public System.EventArgs Event { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the selected item.
        /// </summary>
        public T Item { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the name of the event.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Provides the information about the Opened/Closed event.
    /// </summary>
    public class OpenCloseMenuEventArgs<T>
    {
        /// <summary>
        /// Gets or sets a value that indicates the menu container element.
        /// </summary>
        public ElementReference Element { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the current menu items.
        /// </summary>
        public List<T> Items { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the parent item.
        /// </summary>
        public T ParentItem { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the Navigation Index.
        /// <exclude/>
        /// </summary>
        public int NavigationIndex { get; set; }

        /// <summary>
        /// Gets the ID of the element on which the user right click their mouse button (or finger, on touch devices) to open the context menu.
        /// </summary>
        public string TargetId { get; set;  }
    }

    /// <summary>
    /// Provides the information about MenuItem.
    /// </summary>
    public class MenuItemModel : ItemModelBase
    {
        /// <summary>
        /// Gets or sets a value that indicates the list of menu item model.
        /// </summary>
        public List<MenuItemModel> Items { get; set; }
    }
}

namespace Syncfusion.Blazor.Navigations.Internal
{
    public class ItemModelBase
    {
        /// <summary>
        /// Gets or sets a value that indicates the class to include icons.
        /// </summary>
        public string IconCss { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the menu item id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to enble or disable the separator.
        /// The separator is either horizontal or vertical lines used to group menu items.
        /// </summary>
        public bool Separator { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the menu item disable state.
        /// </summary>
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the menu item hidden state.
        /// </summary>
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the text of the menu item.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates the URL of the menu item.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates additional attributes to the menu item.
        /// </summary>
        public Dictionary<string, object> HtmlAttributes { get; set; }
    }

    public class ItemModel<T> : ItemModelBase
    {
        public List<T> Items { get; set; }

        public string ParentId { get; set; }
    }

    public class ClassCollection
    {
        public string ItemClass { get; set; }

        public List<ClassCollection> ClassList { get; set; }
    }

    public class MenuOptions
    {
        public string dataId { get; set; }

        public string popupDataId { get; set; }

        public ElementReference? Element { get; set; }

        public ElementReference? Popup { get; set; }

        public int ItemIndex { get; set; }

        public double ScrollHeight { get; set; }

        public bool IsRtl { get; set; }

        public bool IsVertical { get; set; }

        public bool ShowItemOnClick { get; set; }

        public bool EnableScrolling { get; set; }

        public List<int> NavigationIndex { get; set; }

        public Orientation Orientation { get; set; }

        public Dictionary<string, object> AnimationSettings { get; set; }

}

    public class CurrentNavProps
    {
        public int ItemIndex { get; set; }

        public List<ClassCollection> ItemClasses { get; set; }

        public int UlIndex { get; set; }
    }
}