using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components.Forms;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using Syncfusion.Blazor.Inputs;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using SortOrder = Syncfusion.Blazor.Navigations.SortOrder;
using static Syncfusion.Blazor.Tests.DropDownTree.DropDownTree_self;

namespace Syncfusion.Blazor.Tests.DropDownTree
{
    public class DropDownTree_hierarchical : BunitTestContext
    {
        List<TreeData> TreeDataSource =
        [
            new TreeData
            {
                Code = "NA",
                Name = "North America",HtmlAttribute=new Dictionary<string, object>() { {"style", "background-color: yellow;"}   },
                Expanded = true, IsCheckedValue= true,
                Child =
                [
                    new TreeData { Code = "USA", Name = "United States of America" ,Icon="test" },
                    new TreeData { Code = "CUB", Name = "Cuba", Image="test" },
                    new TreeData { Code = "MEX", Name = "Mexico" }
                ]
            },
            new TreeData
            {
                Code = "AF",
                Name = "Africa",IsCheckedValue= true,
                Child =
                [
                    new TreeData { Code = "NGA", Name = "ćNigeria" },
                    new TreeData { Code = "EGY", Name = "Egypt" },
                    new TreeData { Code = "ZAF", Name = "South Africa" }
                ]
            },
            new TreeData
            {
                Code = "AS",
                Name = "Asia",
                Child =
                [
                    new TreeData { Code = "CHN", Name = "China" },
                    new TreeData { Code = "IND", Name = "India" },
                    new TreeData { Code = "JPN", Name = "Japan" }
                ]
            }
        ];
        List<TreeData> DynamicSource =
        [
            new TreeData
                {
                    Code = "NA",
                    Name = "North America",
                    Expanded = true,
                    Child =
                    [
                        new TreeData { Code = "USA", Name = "United States of America" },
                        new TreeData { Code = "CUB", Name = "Cuba" },
                        new TreeData { Code = "MEX", Name = "Mexico" }
                    ]
                },
                new TreeData
                {
                    Code = "AF",
                    Name = "Africa",
                    Child =
                    [
                        new TreeData { Code = "NGA", Name = "Nigeria" },
                        new TreeData { Code = "EGY", Name = "Egypt" },
                        new TreeData { Code = "ZAF", Name = "South Africa" }
                    ]
                }
        ];
        List<TreeData> MultiLevel =
        [
            new TreeData
            {
                Code = "NA",
                Name = "North America",
                Expanded = true, IsCheckedValue= true,
                Child =
                [
                    new TreeData { Code = "USA", Name = "United States of America" },
                    new TreeData { Code = "CUB", Name = "Cuba" },
                    new TreeData { Code = "MEX", Name = "Mexico",
                    Child =
                [
                    new TreeData { Code = "NGA1", Name = "ćNigeria1" },
                    new TreeData { Code = "EGY2", Name = "Egypt2",
                    Child =
                [
                    new TreeData { Code = "NGA11", Name = "ćNigeria11" },
                    new TreeData { Code = "EGY21", Name = "Egypt21" },
                    new TreeData { Code = "ZAF31", Name = "South Africa31" }
                ]},
                    new TreeData { Code = "ZAF3", Name = "South Africa3" }
                ]}
                ]
            },
            new TreeData
            {
                Code = "AF",
                Name = "Africa",IsCheckedValue= true,
                Child =
                [
                    new TreeData { Code = "NGA", Name = "ćNigeria" },
                    new TreeData { Code = "EGY", Name = "Egypt" },
                    new TreeData { Code = "ZAF", Name = "South Africa" }
                ]
            },
            new TreeData
            {
                Code = "AS",
                Name = "Asia",
                Child =
                [
                    new TreeData { Code = "CHN", Name = "China" },
                    new TreeData { Code = "IND", Name = "India" },
                    new TreeData { Code = "JPN", Name = "Japan" }
                ]
            }
        ];
        class TreeData
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public bool Expanded { get; set; }
            public bool IsCheckedValue { get; set; }
            public List<TreeData> Child { get; set; }
            public Dictionary<string, object> HtmlAttribute { get; set; }
            public string Icon { get; set; }
            public string Image { get; set; }

        }

        [Fact(Timeout = 10000, DisplayName = "Empty Initialization")]
        public void DefaultInitialize()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")));
            var inputEle = dropdownTree.Find("input");
            Assert.Contains("e-dropdowntree", inputEle.ClassName);
            Assert.Contains("e-control-container", inputEle.ParentElement.ClassName);
            Assert.Contains("e-ddt", inputEle.ParentElement.ClassName);
            Assert.Contains("e-input-group", inputEle.ParentElement.ClassName);
            Assert.True(inputEle.ParentElement.HasChildNodes);
            Assert.True(inputEle.ParentElement.NodeName == "SPAN");
            Assert.Equal("0", inputEle.ParentElement.GetAttribute("tabindex"));
        }

        [Fact(Timeout = 10000, DisplayName = "DefaultInitialize with properties")]
        public void DefaultInitialize_with_properties()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")));
            Assert.False(dropdownTree.Instance.EnablePersistence);
            Assert.False(dropdownTree.Instance.AllowFiltering);
            Assert.False(dropdownTree.Instance.AllowMultiSelection);
            Assert.False(dropdownTree.Instance.ShowCheckBox);
            Assert.False(dropdownTree.Instance.ShowSelectAll);
            Assert.False(dropdownTree.Instance.AutoUpdateCheckState);
            Assert.False(dropdownTree.Instance.TextWrap);
            Assert.False(dropdownTree.Instance.IgnoreAccent);
            Assert.True(dropdownTree.Instance.IgnoreCase);
            Assert.False(dropdownTree.Instance.Disabled);
            Assert.False(dropdownTree.Instance.LoadOnDemand);
            Assert.Null(dropdownTree.Instance.ActionFailureTemplate);
            Assert.Null(dropdownTree.Instance.FooterTemplate);
            Assert.Null(dropdownTree.Instance.HeaderTemplate);
            Assert.Null(dropdownTree.Instance.ID);
            Assert.Null(dropdownTree.Instance.ItemTemplate);
            Assert.Null(dropdownTree.Instance.SelectedItemTemplate);
            Assert.Null(dropdownTree.Instance.NoRecordsTemplate);
            //Assert.Null(dropdownTree.Instance.HtmlAttributes);
            Assert.Null(dropdownTree.Instance.FilterBarPlaceholder);
            Assert.Null(dropdownTree.Instance.Placeholder);
            Assert.Equal("", dropdownTree.Instance.CssClass);
            Assert.Equal(",", dropdownTree.Instance.DelimiterChar);
            Assert.Equal(ExpandAction.DoubleClick, dropdownTree.Instance.ExpandOn);
            Assert.Equal(FilterType.StartsWith, dropdownTree.Instance.FilterType);
            Assert.Equal(FloatLabelType.Never, dropdownTree.Instance.FloatLabelType);
            Assert.Equal(DdtVisualMode.Default, dropdownTree.Instance.Mode);
            Assert.Equal("300px", dropdownTree.Instance.PopupHeight);
            Assert.Equal("100%", dropdownTree.Instance.PopupWidth);
            Assert.Equal(SortOrder.None, dropdownTree.Instance.SortOrder);
            Assert.Equal("100%", dropdownTree.Instance.Width);
            Assert.Equal(1000, dropdownTree.Instance.ZIndex);
        }

        [Fact(Timeout = 10000, DisplayName = "Dynamic property change and property testing for CSS class")]
        public void CssClass()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.CssClass, "Custom"));
            var containerEle = dropdownTree.Find("input").ParentElement;
            Assert.Contains("Custom", containerEle.ClassName);
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            Assert.Contains("Custom", popupEle.ClassName);
            dropdownTree.SetParametersAndRender(("CssClass", "NewCustom"));
            containerEle = dropdownTree.Find("input").ParentElement;
            Assert.Contains("NewCustom", containerEle.ClassName);
            popupEle = dropdownTree.Find(".e-popup");
            Assert.Contains("NewCustom", popupEle.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Dynamic property change and property testing for ID")]
        public void ID()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ID, "custom").Add(p => p.AllowFiltering, true));
            var inputEle = dropdownTree.Find("input");
            Assert.Contains("custom", inputEle.Id);
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            Assert.Contains("custom_options_", popupEle.Id);
            var filterWrap = popupEle.QuerySelector(".e-filter-wrap");
            Assert.Contains("custom_filter_wrap_", filterWrap.Id);
            var filterInput = popupEle.QuerySelector("input");
            Assert.Contains("custom_filter_", filterInput.Id);
            dropdownTree.SetParametersAndRender(("ID", "custom1"));
            containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            Assert.Contains("custom1", inputEle.Id);
            Assert.Contains("custom1_options_", popupEle.Id);
            filterWrap = popupEle.QuerySelector(".e-filter-wrap");
            Assert.Contains("custom1_filter_wrap_", filterWrap.Id);
            filterInput = popupEle.QuerySelector("input");
            Assert.Contains("custom1_filter_", filterInput.Id);
        }


        [Fact(Timeout = 10000, DisplayName = "Property testing for default AllowFiltering")]
        public void AllowFiltering()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ID, "custom").Add(p => p.AllowFiltering, true));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(12, liCollection.Length);
            var filterEle = popupEle.QuerySelector(".e-filter-wrap");
            Assert.NotNull(filterEle);
            var filterInput = filterEle.QuerySelector("input");
            Assert.NotNull(filterInput);
            filterInput.NodeValue = "a";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "a" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(2, liCollection.Length);
            Assert.Equal("Africa", liCollection[0].QuerySelector(".e-list-text").TextContent);
            Assert.Equal("Asia", liCollection[1].QuerySelector(".e-list-text").TextContent);
            filterInput.NodeValue = "";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(12, liCollection.Length);
            filterInput.NodeValue = "c";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "c" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(4, liCollection.Length);
            Assert.Equal("China", liCollection.LastOrDefault().QuerySelector(".e-list-text").TextContent);
            filterInput.NodeValue = "cu";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "cu" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(2, liCollection.Length);
            Assert.Equal("Cuba", liCollection.LastOrDefault().QuerySelector(".e-list-text").TextContent);
            dropdownTree.SetParametersAndRender(("AllowFiltering", false));
            containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            popupEle = dropdownTree.Find(".e-popup");
            filterEle = popupEle.QuerySelector(".e-filter-wrap");
            Assert.Null(filterEle);
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for default AllowFiltering and FilterType")]
        public void FilteringWithFilterType()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ID, "custom").Add(p => p.AllowFiltering, true).Add(p => p.FilterType, FilterType.StartsWith));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(12, liCollection.Length);
            var filterInput = popupEle.QuerySelector("input");
            Assert.NotNull(filterInput);
            filterInput.Focus();
            filterInput.NodeValue = "Sa";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "Sa" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(0, liCollection.Length);
            dropdownTree.SetParametersAndRender(("FilterType", FilterType.EndsWith));
            filterInput.NodeValue = "a";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "a" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(9, liCollection.Length);
            Assert.Equal("North America", liCollection.FirstOrDefault().QuerySelector(".e-list-text").TextContent);
            Assert.Equal("India", liCollection.LastOrDefault().QuerySelector(".e-list-text").TextContent);
            dropdownTree.SetParametersAndRender(("FilterType", FilterType.Contains));
            filterInput.NodeValue = "p";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "p" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(4, liCollection.Length);
            Assert.Equal("Japan", liCollection.LastOrDefault().QuerySelector(".e-list-text").TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for default AllowFiltering, IgnoreCase and IgnoreAccent")]
        public void FilteringWithIgnoreCase()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ID, "custom").Add(p => p.AllowFiltering, true).Add(p => p.IgnoreCase, false).Add(p => p.IgnoreAccent, false).Add(p => p.FilterBarPlaceholder, "Search items"));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(12, liCollection.Length);
            var filterInput = popupEle.QuerySelector("input");
            Assert.NotNull(filterInput);
            filterInput.NodeValue = "c";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "c" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(0, liCollection.Length);
            filterInput.NodeValue = "C";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "C" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(4, liCollection.Length);
            dropdownTree.SetParametersAndRender(("IgnoreCase", true));
            filterInput.NodeValue = "c";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "c" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(4, liCollection.Length);
            filterInput.NodeValue = "ć";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "ć" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(2, liCollection.Length);
            dropdownTree.SetParametersAndRender(("IgnoreAccent", true));
            filterInput.NodeValue = "ć";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "ć" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(6, liCollection.Length);
            Assert.Contains("Search items", filterInput.GetAttribute("placeholder"));
            dropdownTree.SetParametersAndRender(("FilterBarPlaceholder", "Enter Value"));
            filterInput = popupEle.QuerySelector("input");
            Assert.Contains("Enter Value", filterInput.GetAttribute("placeholder"));
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for DelimiterChar")]
        public void DelimiterChar()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ID, "custom").Add(p => p.Value, ["NA", "USA"]).Add(p => p.AllowMultiSelection, true).Add(p => p.DelimiterChar, "-").Add(p => p.FloatLabelType, FloatLabelType.Always));
            var inputEle = dropdownTree.Find("input");
            Assert.Contains("-", inputEle.GetAttribute("value"));
            dropdownTree.SetParametersAndRender(("DelimiterChar", "=>"));
            inputEle = dropdownTree.Find("input");
            Assert.Contains("=>", inputEle.GetAttribute("value"));
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for EnablePersistence")]
        public void EnablePersistence()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.Value, ["NA", "USA"]).Add(p => p.AllowMultiSelection, true).Add(p => p.ID, "tree").Add(p => p.EnablePersistence, false).Add(p => p.Placeholder, "select item").Add(p => p.PopupHeight, "100px").Add(p => p.PopupWidth, "50%"));
            var inputEle = dropdownTree.Find("input");
            Assert.Contains("North America, United States of America", inputEle.GetAttribute("value"));
            dropdownTree.SetParametersAndRender(("EnablePersistence", true));
            dropdownTree.Render();
            inputEle = dropdownTree.Find("input");
            Assert.Contains("North America, United States of America", inputEle.GetAttribute("value"));
            Assert.Contains("select item", inputEle.GetAttribute("placeholder"));
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for Disabled")]
        public void Disabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.Disabled, true));
            var inputEle = dropdownTree.Find("input");
            Assert.Contains("e-disabled", inputEle.ClassName);
            Assert.Contains("true", inputEle.GetAttribute("aria-disabled"));
            Assert.Contains("disabled", inputEle.GetAttribute("disabled"));
            var containerEle = inputEle.ParentElement;
            Assert.Contains("e-disabled", containerEle.ClassName);
            Assert.Contains("true", containerEle.GetAttribute("aria-disabled"));
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for Width")]
        public void Width()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.Width, "50%").Add(p => p.ZIndex, 1001).Add(p => p.ExpandOn, ExpandAction.Click));
            var containerEle = dropdownTree.Find("input").ParentElement;
            var containerStyle = containerEle.GetAttribute("data-sf-style");
            Assert.Contains("50%", containerStyle);
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for HtmlAttributes")]
        public void HtmlAttributes()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.HtmlAttributes, new() { { "custom-attr", "test" } }));
            var inputEle = dropdownTree.Find("input");
            Assert.Equal("test", inputEle.GetAttribute("custom-attr"));
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for checkbox")]
        public void CheckBox()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.ShowSelectAll, true).Add(p => p.AutoUpdateCheckState, true).Add(p => p.EnablePersistence, true).Add(p => p.ID, "custom"));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var checkBoxCollection = popupEle.QuerySelectorAll(".e-frame");
            Assert.Equal(12 + 1, checkBoxCollection.Length);
            dropdownTree.SetParametersAndRender(("ShowSelectAll", false));
            checkBoxCollection = popupEle.QuerySelectorAll(".e-frame");
            Assert.Equal(12, checkBoxCollection.Length);
            dropdownTree.SetParametersAndRender(("ShowSelectAll", true));
            Assert.Contains("Select All", popupEle.QuerySelector(".e-selectall-parent").TextContent);
            dropdownTree.SetParametersAndRender(("Value", new List<string>() { "NA", "AS", "AF" }));
            dropdownTree.Render();
            var checkedItems = popupEle.QuerySelectorAll(".e-check");
            Assert.Equal(12 + 1, checkedItems.Length);
            dropdownTree.SetParametersAndRender(("Value", new List<string>() { }));
            dropdownTree.Render();
            checkedItems = popupEle.QuerySelectorAll(".e-check");
            Assert.Equal(0, checkedItems.Length);
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for TextWrap")]
        public void TextWrap()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.Value, ["NA", "AS", "AF"]).Add(p => p.AutoUpdateCheckState, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.TextWrap, true));
            var containerEle = dropdownTree.Find("input").ParentElement;
            Assert.Equal(12, containerEle.QuerySelectorAll(".e-chips").Length);
            dropdownTree.SetParametersAndRender(("Mode", DdtVisualMode.Delimiter));
            containerEle = dropdownTree.Find("input").ParentElement;
            Assert.Equal(0, containerEle.QuerySelectorAll(".e-chips").Length);
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing for LoadOnDemand")]
        public void LoadOnDemand()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.LoadOnDemand, true).Add(p => p.SortOrder, SortOrder.None).Add(p => p.ShowClearButton, false));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var liCollection = popupEle.QuerySelectorAll(".e-list-item");
            Assert.Equal(6, liCollection.Length);
            dropdownTree.SetParametersAndRender(("LoadOnDemand", false));
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll(".e-list-item");
            Assert.Equal(12, liCollection.Length);
            Assert.Contains(TreeDataSource[0].Name, liCollection[0].QuerySelector(".e-list-text").TextContent);
            Assert.Null(containerEle.QuerySelector(".e-clear-icon"));
            dropdownTree.SetParametersAndRender(("ShowClearButton", true));
            containerEle = dropdownTree.Find("input").ParentElement;
            Assert.NotNull(containerEle.QuerySelector(".e-clear-icon"));
        }

        [Fact(Timeout = 10000, DisplayName = "Event testing for Created and Destroyed")]
        public void Created()
        {
            int i = 0;
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.LoadOnDemand, true).Add(p => p.SortOrder, SortOrder.None).Add(p => p.ShowClearButton, false)
            .Add(p => p.Created, (object args) =>
            {
                i++;
                Assert.Equal(1, i);
            }).Add(p => p.Destroyed, (object args) =>
            {
                i++;
                Assert.Equal(2, i);
            }));
            dropdownTree.Dispose();
            Assert.True(dropdownTree.IsDisposed);
        }

        [Fact(Timeout = 10000, DisplayName = "Event testing for ValueChanging and ValueChanged")]
        public async void ValueEvents()
        {
            int i = 0;
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.ShowSelectAll, true).Add(p => p.LoadOnDemand, true).Add(p => p.SortOrder, SortOrder.None).Add(p => p.ShowClearButton, false)
            .Add(p => p.ValueChanging, (DdtChangeEventArgs<string> args) =>
            {
                i++;
                Assert.Equal(1, i);
                Assert.False(args.IsInteracted);
                Assert.NotNull(args.NodeData);
                Assert.False(args.Cancel);
                Assert.True(args.Action == DdtAction.Select);
                Assert.Null(args.PreviousValue);
                args.Cancel = true;
            }).Add(p => p.ValueChanged, (List<string> args) =>
            {
                i++;
                Assert.Equal(1, i);
            }));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            await dropdownTree.Instance.SelectAllAsync();
        }

        [Fact(Timeout = 10000, DisplayName = "Public Method Testing SelectAllAsync")]
        public async void SelectAll()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true));
            await dropdownTree.Instance.SelectAllAsync();
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            Assert.NotNull(popupEle);
            var liColl = popupEle.QuerySelectorAll(".e-check");
            Assert.Equal(12, liColl.Length);
            await dropdownTree.Instance.RefreshAsync();
        }

        [Fact(Timeout = 10000, DisplayName = "Public Method Testing SelectAllAsync")]
        public async void SelectAllWithMultiSelection()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.AllowMultiSelection, true));
            await dropdownTree.Instance.SelectAllAsync();
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            Assert.NotNull(popupEle);
            var liColl = popupEle.QuerySelectorAll(".e-active");
            Assert.Equal(12, liColl.Length);
        }

        [Fact(Timeout = 10000, DisplayName = "Public Method Testing GetTreeViewData")]
        public void GetData()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            .Add(p => p.IsChecked, "IsCheckedValue")).Add(p => p.ShowCheckBox, true).Add(p => p.Value, ["NA", "AF"]));
            List<TreeData> treeData = dropdownTree.Instance.GetTreeViewData();
            Assert.Equal(3, treeData.Count);
            Assert.True(treeData[0].IsCheckedValue);
            treeData = dropdownTree.Instance.GetTreeViewData("AF");
            Assert.Single(treeData);
            Assert.True(treeData[0].IsCheckedValue);
        }

        [Fact(Timeout = 10000, DisplayName = "Event testing for Filtering")]
        public void FilteringEvent()
        {
            int i = 0;
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.AllowFiltering, true)
            .Add(p => p.Filtering, (DdtFilteringEventArgs args) =>
            {
                i++;
                Assert.Equal(1, i);
                Assert.Equal("sa", args.Text);
                args.Cancel = true;
            }));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(12, liCollection.Length);
            var filterInput = popupEle.QuerySelector("input");
            Assert.NotNull(filterInput);
            filterInput.NodeValue = "sa";
            filterInput.Input(new Microsoft.AspNetCore.Components.ChangeEventArgs() { Value = "sa" });
            popupEle = dropdownTree.Find(".e-popup");
            liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(12, liCollection.Length);
        }

        [Fact(Timeout = 10000, DisplayName = "Open Popup")]
        public void ShowPopUp()
        {
            int i = 0;
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ParentID, "Pid").Add(p => p.ID, "Code").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.OnPopupOpen, (PopupEventArgs args) =>
            {
                i++;
                Assert.Equal(1, i);
            }));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
        }

        [Fact(Timeout = 10000, DisplayName = "Hide Popup")]
        public void ClosePopup()
        {
            int i = 0;
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ParentID, "Pid").Add(p => p.ID, "Code").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.OnPopupClose, (PopupEventArgs args) =>
            {
                i++;
                Assert.Equal(1, i);
            }));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
        }

        [Fact(Timeout = 10000, DisplayName = "Default Checked Items")]
        public void DefaultCheckedItems()
        {
            int i = 0;
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.OnPopupClose, (PopupEventArgs args) =>
            {
                i++;
                Assert.Equal(1, i);
            }).Add(p => p.ShowCheckBox, true).Add(p => p.Value, ["NA", "USA"]));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var liColl = popupEle.QuerySelectorAll(".e-check");
            Assert.Equal(2, liColl.Length);
        }

        [Fact(Timeout = 10000, DisplayName = "Public Method Testing ClearAll")]
        public async void ClearAll()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.Value, ["NA", "USA"]));
            Assert.Equal(["NA", "USA"], dropdownTree.Instance.Value);
            await dropdownTree.Instance.ClearAsync();
            Assert.Equal([], dropdownTree.Instance.Value);

        }

        [Fact(Timeout = 10000, DisplayName = "Default text without multi selection checkbox")]
        public void DefaultTextWithoutMultiSelectionAndCheckbox()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.Text, "North America"));
            Assert.Equal(["NA"], dropdownTree.Instance.Value);
            dropdownTree.SetParametersAndRender(("ShowCheckBox", true), ("Value", new List<string>() { }));
            Assert.Equal([], dropdownTree.Instance.Value);

        }
        [Fact(Timeout = 10000, DisplayName = "Default value without multi selection checkbox")]
        public void DefaultValueWithoutMultiSelectionAndCheckbox()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.Value, ["NA"]));
            Assert.Equal(["NA"], dropdownTree.Instance.Value);
            dropdownTree.SetParametersAndRender(("ShowCheckBox", true), ("Value", new List<string>() { "TEST" }));
            Assert.Equal(["NA"], dropdownTree.Instance.Value);
            dropdownTree.SetParametersAndRender(("Value", new List<string>() { }));
            Assert.Equal([], dropdownTree.Instance.Value);
            dropdownTree.SetParametersAndRender(("Value", new List<string>() { "AF" }));
            Assert.Equal(["AF"], dropdownTree.Instance.Value);
            dropdownTree.SetParametersAndRender(("ShowCheckBox", false), ("Value", new List<string>() { "TEST" }));
            Assert.Equal(["AF"], dropdownTree.Instance.Value);
            dropdownTree.SetParametersAndRender(("Value", new List<string>() { }));
            Assert.Equal([], dropdownTree.Instance.Value);
        }

        [Fact(Timeout = 10000, DisplayName = "Dynamic Property Testing DataSource")]
        public void Dynamic_Property_DataSource()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal(12, liCollection.Length);
            var field = dropdownTree.FindComponent<DropDownTreeField<TreeData>>();
            field.SetParametersAndRender(("DataSource", DynamicSource));
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing ShowSelectAll")]
        public void SelectAllButton()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.ShowSelectAll, true));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var selectAllButton = dropdownTree.Find(".e-selectall-parent");
            selectAllButton.Click();
            var checkedNodes = dropdownTree.FindAll(".e-list-item .e-check");
            Assert.Equal(12, checkedNodes.Count);
            selectAllButton.Click();
            dropdownTree.Render();
            checkedNodes = dropdownTree.FindAll(".e-list-item .e-check");
            Assert.Equal(0, checkedNodes.Count);
        }

        [Fact(Timeout = 10000, DisplayName = "chip delete with show check box")]
        public void ChipDelete()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.Value, ["NA"]));

            var chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(1, chipCollection.Count);
            var clearButton = dropdownTree.Find(".e-chips-close");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
        }

        [Fact(Timeout = 10000, DisplayName = "chip delete with ShowCheckBox and AutoUpdateCheckState")]
        public async void ChipDeleteWithAutoUpdateCheck()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.Value, ["NA"]).Add(p => p.AutoUpdateCheckState, true));
            var chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(4, chipCollection.Count);
            var clearButton = dropdownTree.Find(".e-chips-close");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(12, chipCollection.Count);
            chipCollection[0].MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(12 - 4, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync(false);
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync();
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            clearButton = dropdownTree.Find(".e-chips-close");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(12 - 4, chipCollection.Count);
        }

        [Fact(Timeout = 10000, DisplayName = "chip delete with AllowMultiSelection")]
        public async void ChipDeleteWithAllowMultiSelection()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.AllowMultiSelection, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.Value, ["NA"]));
            var chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(1, chipCollection.Count);
            var clearButton = dropdownTree.Find(".e-chips-close");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(12, chipCollection.Count);
            chipCollection[0].MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(12 - 1, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync(false);
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            await dropdownTree.Instance.SelectAllAsync();
            clearButton = dropdownTree.Find(".e-chips-close");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(12 - 1, chipCollection.Count);
        }

        [Fact(Timeout = 10000, DisplayName = "clear button with ShowCheckBox and AutoUpdateCheckState")]
        public async void ClearButtonWithAutoUpdateCheck()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.ShowCheckBox, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.Value, ["NA"]).Add(p => p.AutoUpdateCheckState, true));
            var chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(4, chipCollection.Count);
            var clearButton = dropdownTree.Find(".e-clear-icon");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(12, chipCollection.Count);
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync(false);
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
        }

        [Fact(Timeout = 10000, DisplayName = "clear button with AllowMultiSelection")]
        public async void ClearButtonWithAllowMultiSelection()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.AllowMultiSelection, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.Value, ["NA"]));
            var chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(1, chipCollection.Count);
            var clearButton = dropdownTree.Find(".e-clear-icon");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(12, chipCollection.Count);
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
            await dropdownTree.Instance.SelectAllAsync(false);
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
        }

        [Fact(Timeout = 10000, DisplayName = "chip delete with Disabled")]
        public void ChipDeleteWithDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            ).Add(p => p.AllowMultiSelection, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.Value, ["NA"]).Add(p => p.Disabled, true));
            var chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(1, chipCollection.Count);
            var clearButton = dropdownTree.Find(".e-chips-close");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(1, chipCollection.Count);
            var inputEle = dropdownTree.Find("input");
            Assert.Contains("e-disabled", inputEle.ClassName);
            Assert.Contains("true", inputEle.GetAttribute("aria-disabled"));
            Assert.Contains("disabled", inputEle.GetAttribute("disabled"));
            var containerEle = inputEle.ParentElement;
            Assert.Contains("e-disabled", containerEle.ClassName);
            Assert.Contains("true", containerEle.GetAttribute("aria-disabled"));
        }

        [Fact(Timeout = 10000, DisplayName = "Public Method Testing GetTreeViewData with incorrect parameter")]
        public void GetDataWithIncorrectParameter()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
           .Add(p => p.IsChecked, "IsCheckedValue")).Add(p => p.ShowCheckBox, true).Add(p => p.Value, ["NA", "AF"]));
            List<TreeData> treeData = dropdownTree.Instance.GetTreeViewData("USA");
            Assert.Single(treeData);
            treeData = dropdownTree.Instance.GetTreeViewData("TEST");
            Assert.Empty(treeData);
            var field = dropdownTree.FindComponent<DropDownTreeField<TreeData>>();
            field.SetParametersAndRender(("DataSource", new List<TreeData>() { }));
            treeData = dropdownTree.Instance.GetTreeViewData("NA");
            Assert.Empty(treeData);
        }

        [Fact(Timeout = 10000, DisplayName = "Remove Auto check third level")]
        public void AutoCheckMultiLevel()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
            .Add(p => p.ShowCheckBox, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.AutoUpdateCheckState, true).Add(p => p.Value, ["ZAF31"]));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var checkedNodes = popupEle.QuerySelectorAll(".e-check");
            Assert.Equal(1, checkedNodes.Length);
            var chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(1, chipCollection.Count);
            var clearButton = dropdownTree.Find(".e-chips-close");
            clearButton.MouseDown();
            chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(0, chipCollection.Count);
        }

        [Fact(Timeout = 10000, DisplayName = "Auto check with all child checked")]
        public void AutoCheckWithAllChildChecked()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
            .Add(p => p.ShowCheckBox, true).Add(p => p.Mode, DdtVisualMode.Box).Add(p => p.AutoUpdateCheckState, true).Add(p => p.Value, ["ZAF31", "NGA11", "EGY21"]));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var checkedNodes = popupEle.QuerySelectorAll(".e-check");
            Assert.Equal(4, checkedNodes.Length);
            var chipCollection = dropdownTree.FindAll(".e-chips-close");
            Assert.Equal(4, chipCollection.Count);
        }

        [Fact(Timeout = 10000, DisplayName = "HtmlAttributes on each nodes")]
        public void HtmlAttributesOnNodes()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, TreeDataSource).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded")
            .Add(p => p.HtmlAttributes, "HtmlAttribute").Add(p => p.IconCss, "Icon").Add(p => p.ImageUrl, "Image")));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var popupEle = dropdownTree.Find(".e-popup");
            var liCollection = popupEle.QuerySelectorAll("li.e-list-item");
            Assert.Equal("North America", liCollection[0].QuerySelector(".e-list-text").TextContent);
            var nodeStyle = liCollection[0].GetAttribute("data-sf-style");
            Assert.Contains("yellow", nodeStyle);
            var icon = popupEle.QuerySelector(".e-list-icon.test");
            Assert.NotNull(icon);
            var image = popupEle.QuerySelectorAll(".e-list-img");
            Assert.Equal(1, image.Length);
            Assert.Contains("test", image[0].GetAttribute("src"));
        }

        [Fact(Timeout = 10000, DisplayName = "Dynamic SortOrder")]
        public void DynamicSortOrder()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
            .Add(p => p.SortOrder, SortOrder.Ascending));
            var containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            var treeEle = dropdownTree.Find(".e-treeview");
            var liElements = dropdownTree.FindAll("li");
            Assert.Equal("Africa", liElements[0].QuerySelector(".e-list-text").TextContent);
            dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
            .Add(p => p.SortOrder, SortOrder.Descending));
            containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            treeEle = dropdownTree.Find(".e-treeview");
            liElements = dropdownTree.FindAll("li");
            Assert.Equal("North America", liElements[0].QuerySelector(".e-list-text").TextContent);
            dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
            .Add(p => p.SortOrder, SortOrder.None));
            containerEle = dropdownTree.Find("input").ParentElement;
            containerEle.MouseDown();
            treeEle = dropdownTree.Find(".e-treeview");
            liElements = dropdownTree.FindAll("li");
            Assert.Equal("North America", liElements[0].QuerySelector(".e-list-text").TextContent);
        }

        //[Fact(Timeout = 10000, DisplayName = "RTL")]
        //public void RTL()
        //{
        //    var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
        //    .Add(p => p.ShowCheckBox, true).Add(p => p.AllowMultiSelection, true).Add(p => p.AllowFiltering, true).Add(p => p.ShowSelectAll, true));
        //    dropdownTree.Instance.SyncfusionService.EnableRtl();
        //    dropdownTree.Render();
        //    var containerEle = dropdownTree.Find("input").ParentElement;
        //    containerEle.MouseDown();
        //    var treeEle = dropdownTree.Find(".e-treeview");
        //    Assert.Contains("e-rtl", treeEle.ClassName);
        //    Assert.Contains("e-rtl", containerEle.ClassName);
        //    var filterWrap = dropdownTree.Find(".e-filter-wrap .e-input-group");
        //    Assert.Contains("e-rtl", filterWrap.ClassName);
        //}

        //[Fact(Timeout = 10000, DisplayName = "RTL with value")]
        //public void RTLWithValue()
        //{
        //    var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
        //    .Add(p => p.ShowCheckBox, true).Add(p => p.AllowMultiSelection, true).Add(p => p.AllowFiltering, true).Add(p => p.ShowSelectAll, true).Add(p => p.Value, ["NA"]).Add(p => p.AutoUpdateCheckState, true));
        //    dropdownTree.Instance.SyncfusionService.EnableRtl();
        //    dropdownTree.Render();
        //    var containerEle = dropdownTree.Find("input").ParentElement;
        //    containerEle.MouseDown();
        //    var treeEle = dropdownTree.Find(".e-treeview");
        //    var checkCollection = treeEle.QuerySelectorAll(".e-check");
        //    Assert.Equal(10, checkCollection.Length);
        //    Assert.Contains("e-rtl", treeEle.ClassName);
        //    Assert.Contains("e-rtl", containerEle.ClassName);
        //    var filterWrap = dropdownTree.Find(".e-filter-wrap .e-input-group");
        //    Assert.Contains("e-rtl", filterWrap.ClassName);
        //}

        [Fact(Timeout = 10000, DisplayName = "ShowPopup with method")]
        public async void ShowPopupAPI()
        {
            var i = 0;
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
            .Add(p => p.OnPopupOpen, (PopupEventArgs args) =>
            {
                i++;
                Assert.Equal(1, i);
            }).Add(p => p.OnPopupClose, (PopupEventArgs args) =>
            {
                i++;
                Assert.Equal(2, i);
            }));
            await dropdownTree.Instance.ShowPopupAsync();
            await dropdownTree.Instance.HidePopupAsync();
        }

        [Fact(Timeout = 10000, DisplayName = "ShowPopup with method with Disabled")]
        public async void ShowPopupAPWithDisabledI()
        {
            var i = 0;
            var dropdownTree = RenderComponent<SfDropDownTree<string, TreeData>>(Parameter => Parameter.AddChildContent<DropDownTreeField<TreeData>>(field => field.Add(p => p.DataSource, MultiLevel).Add(p => p.ID, "Code").Add(p => p.Child, "Child").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded"))
            .Add(p => p.Disabled, true).Add(p => p.OnPopupOpen, (PopupEventArgs args) =>
            {
                i++;
                Assert.Equal(1, i);
            }).Add(p => p.OnPopupClose, (PopupEventArgs args) =>
            {
                i++;
                Assert.Equal(2, i);
            }));
            await dropdownTree.Instance.ShowPopupAsync();
            await dropdownTree.Instance.HidePopupAsync();
            Assert.Equal(0, i);
        }
        [Fact(DisplayName = "Test NodeData Property")]
        public void TestNodeDataProperty()
        {
            var nodeData = new NodeData { Id = "1", Text = "Node1" };
            //var args = new DdtChangeEventArgs<string>
            //{
            //    NodeData = nodeData
            //};

            //Assert.Equal(nodeData, args.NodeData);
        }

        [Fact(DisplayName = "Test Action Property")]
        public void TestActionProperty()
        {
            //var args = new DdtChangeEventArgs<string>
            //{
            //    Action = DdtAction.Select
            //};

            //Assert.Equal(DdtAction.Select, args.Action);
        }
        [Fact(DisplayName = "Test IsInteracted Property")]
        public void TestIsInteractedProperty()
        {
            //var args = new DdtChangeEventArgs<string>
            //{
            //    IsInteracted = true
            //};

            //Assert.True(args.IsInteracted);
        }
        [Fact(DisplayName = "Test PreviousValue Property")]
        public void TestPreviousValueProperty()
        {
            var previousValues = new List<string> { "Value1", "Value2" };
            //var args = new DdtChangeEventArgs<string>
            //{
            //    PreviousValue = previousValues
            //};

            //Assert.Equal(previousValues, args.PreviousValue);
        }
        [Fact(DisplayName = "Test CurrentValue Property")]
        public void TestCurrentValueProperty()
        {
            var currentValue = "Value3";
            //var args = new DdtChangeEventArgs<string>
            //{
            //    CurrentValue = currentValue
            //};

            //Assert.Equal(currentValue, args.CurrentValue);
        }

    }
}
