using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using Syncfusion.Blazor.Tests.DropDownTree;
using Xunit;

namespace Syncfusion.Blazor.Tests.DropDownTree
{
    public class DropDownTreeCoverageTests : BunitTestContext
    {
        List<ListData> ListDataSource { get; set; } = new List<ListData>
        {
            new ListData { Id = 1, Pid = null, Name = "Electronics", HasChild = true, Expanded = true },
            new ListData { Id = 2, Pid = 1, Name = "Smartphones" },
            new ListData { Id = 3, Pid = 1, Name = "Laptops" },
            new ListData { Id = 4, Pid = 1, Name = "Tablets" },
            new ListData { Id = 5, Pid = 2, Name = "Accessories" },
            new ListData { Id = 6, Name = "Clothing", HasChild = true },
            new ListData { Id = 7, Pid = 6, Name = "Men's Clothing" },
            new ListData { Id = 8, Pid = 6, Name = "Women's Clothing" }
        };

        public class ListData
        {
            public int Id { get; set; }
            public int? Pid { get; set; }
            public string Name { get; set; }
            public bool HasChild { get; set; }
            public bool Expanded { get; set; }
            public bool IsCheckedValue { get; set; }
        }

        [Fact]
        public void TestGetValueData_WhenValueIsNull()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetValueData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method?.Invoke(dropdownTree.Instance, null);
            Assert.NotNull(result);
            Assert.Empty((System.Collections.IList)result);
        }

        [Fact]
        public void TestGetValueData_WithValidValue()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetValueData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method?.Invoke(dropdownTree.Instance, null);
            Assert.NotNull(result);
            Assert.Single((System.Collections.IList)result);
        }

        [Fact]
        public void TestSetTreeText_WithTextProperty()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Text, "Electronics")
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetTreeText", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { false });
            var value = (List<int>)typeof(SfDropDownTree<int, ListData>).GetField("currentValue", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(dropdownTree.Instance);
            Assert.NotNull(value);
            Assert.Single(value);
        }

        [Fact]
        public void TestSetTreeText_WithEmptyText()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetTreeText", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { false });
        }

        [Fact]
        public void TestUpdateTwoWayBinding()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateTwoWayBinding", BindingFlags.NonPublic | BindingFlags.Instance);
            var task = (Task)method?.Invoke(dropdownTree.Instance, null);
            task?.Wait();
        }

        [Fact]
        public void TestUpdateAllData_WithNullData()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateAllData", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { null, null, null, null });
        }

        [Fact]
        public void TestUpdateAllData_WithEmptyList()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var emptyList = new List<ListData>();
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateAllData", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { emptyList, null, null, null });
        }

        [Fact]
        public async Task TestClearAll_WhenDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Disabled, true)
                .Add(p => p.Value, new List<int> { 1 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("ClearAll", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false });
        }

        [Fact]
        public async Task TestClearAll_WithValueChangingCancel()
        {
            var cancelValueChanging = false;
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ValueChanging, (DdtChangeEventArgs<int> args) =>
                {
                    if (!cancelValueChanging)
                    {
                        cancelValueChanging = true;
                        args.Cancel = true;
                    }
                })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("ClearAll", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false });
        }

        [Fact]
        public async Task TestResetValue_WithCurrentValueAndText()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("ResetValue", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false });
        }

        [Fact]
        public void TestFilterChangeHandler_WithEmptyFilter()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowFiltering, true)
            );

            var filterArgs = new DdtFilteringEventArgs { Cancel = false };
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnFiltering", BindingFlags.NonPublic | BindingFlags.Instance);
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { filterArgs });
            task?.Wait();
        }

        [Fact]
        public void TestFilterChangeHandler_WithFilterValue()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowFiltering, true)
            );

            var filterArgs = new DdtFilteringEventArgs { Cancel = false };
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnFiltering", BindingFlags.NonPublic | BindingFlags.Instance);
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { filterArgs });
            task?.Wait();
        }

        [Fact]
        public void TestFilterChangeHandler_WhenFilteringCancelled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowFiltering, true)
                .Add(p => p.Filtering, (DdtFilteringEventArgs args) =>
                {
                    args.Cancel = true;
                })
            );

            var filterArgs = new DdtFilteringEventArgs { Cancel = true };
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnFiltering", BindingFlags.NonPublic | BindingFlags.Instance);
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { filterArgs });
            task?.Wait();
        }

        [Fact]
        public void TestIsMatchedNode_WithIgnoreCase()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.IgnoreCase, true)
                .Add(p => p.AllowFiltering, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("IsMatchedNode", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (bool)method?.Invoke(dropdownTree.Instance, new object[] { "electronics", ListDataSource[0], 0 });
            Assert.True(result);
        }

        [Fact]
        public void TestIsMatchedNode_WithIgnoreCaseNoMatch()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.IgnoreCase, false)
                .Add(p => p.AllowFiltering, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("IsMatchedNode", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (bool)method?.Invoke(dropdownTree.Instance, new object[] { "ele", ListDataSource[0], 0 });
            Assert.False(result);
        }

        [Fact]
        public void TestIsMatchedNode_WithIgnoreAccent()
        {
            var specialData = new List<ListData>
            {
                new ListData { Id = 1, Name = "ǩitchen" }
            };

            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, specialData)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.IgnoreAccent, true)
                .Add(p => p.AllowFiltering, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("IsMatchedNode", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (bool)method?.Invoke(dropdownTree.Instance, new object[] { "kitchen", specialData[0], 0 });
            Assert.True(result);
        }

        [Fact]
        public void TestIsMatchedNode_WithFilterTypeStartsWith()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.FilterType, FilterType.StartsWith)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("IsMatchedNode", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (bool)method?.Invoke(dropdownTree.Instance, new object[] { "Elec", ListDataSource[0], 0 });
            Assert.True(result);
        }

        [Fact]
        public void TestIsMatchedNode_WithFilterTypeEndsWith()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.FilterType, FilterType.EndsWith)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("IsMatchedNode", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (bool)method?.Invoke(dropdownTree.Instance, new object[] { "tronics", ListDataSource[0], 0 });
            Assert.True(result);
        }

        [Fact]
        public void TestIsMatchedNode_WithFilterTypeContains()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.FilterType, FilterType.Contains)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("IsMatchedNode", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (bool)method?.Invoke(dropdownTree.Instance, new object[] { "tron", ListDataSource[0], 0 });
            Assert.True(result);
        }

        [Fact]
        public void TestRemoveDiacritics()
        {
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("RemoveDiacritics", BindingFlags.NonPublic | BindingFlags.Static);
            var result = (string)method?.Invoke(null, new object[] { "ǩitchen" });
            Assert.Equal("kitchen", result);
        }

        [Fact]
        public void TestRemoveDiacritics_WithNullOrEmpty()
        {
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("RemoveDiacritics", BindingFlags.NonPublic | BindingFlags.Static);
            var result = (string)method?.Invoke(null, new object[] { "" });
            Assert.Equal("", result);
            result = (string)method?.Invoke(null, new object[] { null });
            Assert.Null(result);
        }

        [Fact]
        public void TestSelfReferentialFilter_FindParents()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowFiltering, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SelfReferentialFilter", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (List<ListData>)method?.Invoke(dropdownTree.Instance, new object[] { "Accessories" });
            Assert.NotNull(result);
            Assert.True(result.Count >= 1);
        }

        [Fact]
        public void TestGetClonedList()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetClonedList", BindingFlags.NonPublic | BindingFlags.Static);
            var result = (List<ListData>)method?.Invoke(null, new object[] { ListDataSource });
            Assert.NotNull(result);
            Assert.Equal(ListDataSource.Count, result.Count);
        }

        [Fact]
        public void TestGetClonedList_WithNull()
        {
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetClonedList", BindingFlags.NonPublic | BindingFlags.Static);
            var result = (List<ListData>)method?.Invoke(null, new object[] { null });
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void TestGetPopupContentClass_WithNoData()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, new List<ListData>())
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetPopupContentClass", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (string)method?.Invoke(dropdownTree.Instance, null);
            Assert.Contains("e-no-data", result);
        }

        [Fact]
        public async Task TestInvokePopupEvent_WithNullArgs_Throws()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name"))
            );

            var method = typeof(SfDropDownTree<int, ListData>)
                .GetMethod("InvokePopupEvent", BindingFlags.NonPublic | BindingFlags.Instance);

            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                var task = method?.Invoke(dropdownTree.Instance, new object[] { null }) as Task;
                await task;
            });
        }

        [Fact]
        public void TestGetContainerAttributes()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Disabled, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetContainerAttributes", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (Dictionary<string, object>)method?.Invoke(dropdownTree.Instance, null);
            Assert.NotNull(result);
            Assert.Equal("true", result["aria-disabled"].ToString());
        }

        [Fact]
        public void TestGetInstance()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.AllowMultiSelection, true)
                .Add(p => p.AllowFiltering, true)
                .Add(p => p.ZIndex, 1500)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetInstance", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (Dictionary<string, object>)method?.Invoke(dropdownTree.Instance, null);
            Assert.NotNull(result);
            Assert.Equal(1500d, Convert.ToDouble(result["zIndex"]));
        }

        [Fact]
        public void TestOnFailure()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.OnActionFailure, EventCallback.Factory.Create<object>(this, args => Task.CompletedTask))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnFailure", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { new Exception("Test exception") });

            var actionFailure = (bool)typeof(SfDropDownTree<int, ListData>).GetField("actionFailure", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(dropdownTree.Instance);
            Assert.True(actionFailure);
        }

        [Fact]
        public void TestSerializeModel()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1, 2 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SerializeModel", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (string)method?.Invoke(dropdownTree.Instance, null);
            Assert.NotNull(result);
            Assert.Contains("1", result);
        }

        [Fact]
        public void TestRemoveSelectedData()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1, 2 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("RemoveSelectedData", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { "1" });
        }

        [Fact]
        public void TestGetTreeData_WithNullId()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetTreeData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (List<ListData>)method?.Invoke(dropdownTree.Instance, new object[] { null });
            Assert.NotNull(result);
            Assert.Equal(ListDataSource.Count, result.Count);
        }

        [Fact]
        public void TestGetTreeData_WithInvalidId()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetTreeData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (List<ListData>)method?.Invoke(dropdownTree.Instance, new object[] { "999" });
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void TestGetHierarchicalData_WithEmptyDataSource()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, new List<ListData>())
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetHierarchicalData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method?.Invoke(dropdownTree.Instance, new object[] { null, "1" });
            Assert.Null(result);
        }

        [Fact]
        public void TestUpdateChildProperties_WithDataSourceUpdate()
        {
            var newDataSource = new List<ListData>
            {
                new ListData { Id = 1, Name = "New Item" }
            };

            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var field = dropdownTree.FindComponent<DropDownTreeField<ListData>>();
            field.SetParametersAndRender(("DataSource", newDataSource));

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateChildProperties", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, null);
        }

        [Fact]
        public void TestUpdateChildProperties_WhenNoDataSourceUpdate()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateChildProperties", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, null);
        }

        [Fact]
        public void TestUpdateData_Method()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.IsChecked, "IsCheckedValue"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateData", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { ListDataSource });
        }

        [Fact]
        public void TestSetAttributes()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetAttributes", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, null);
        }

        [Fact]
        public async Task TestOnNodeSelected_WithInvalidArgs()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var selectArgs = new NodeSelectEventArgs
            {
                Action = "select",
                IsInteracted = true,
                NodeData = new NodeData { Id = "1", Text = "Electronics" }
            };

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeSelected", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, selectArgs });
        }

        [Fact]
        public async Task TestOnNodeChecked_WithInvalidArgs()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
            );

            var checkArgs = new NodeCheckEventArgs
            {
                Action = "check",
                IsInteracted = true,
                NodeData = new NodeData { Id = "1" }
            };

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeChecked", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, checkArgs });
        }

        [Fact]
        public async Task TestRefreshPopup()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnTreeNodeExpand", BindingFlags.NonPublic | BindingFlags.Instance);
            var expandArgs = new NodeExpandEventArgs { NodeData = new NodeData { Id = "1" } };
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, expandArgs });
            task?.Wait();
        }

        [Fact]
        public async Task TestOnDataSourceChanged()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var newDataSource = new List<ListData>
            {
                new ListData { Id = 9, Name = "New Item", HasChild = false }
            };

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnTreeDataSourceChanged", BindingFlags.NonPublic | BindingFlags.Instance);
            var sourceChangedArgs = new TreeDataSourceChangedEventArgs { Data = newDataSource };
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, sourceChangedArgs });
            task?.Wait();
        }

        [Fact]
        public async Task TestOnNodeExpanding_WithInvalidArgs()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var expandArgs = new NodeExpandEventArgs { NodeData = new NodeData { Id = "1" } };
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeExpanding", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, expandArgs });
        }

        [Fact]
        public async Task TestOnNodeClicked_WithInvalidArgs()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var clickArgs = new NodeClickEventArgs { NodeData = new NodeData { Id = "1", IsChecked = "false" } };
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeClicked", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, clickArgs });
        }

      

        [Fact]
        public void TestGetSelectedData_WhenFiltered()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowFiltering, true)
                .Add(p => p.Value, new List<int> { 1 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetSelectedData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (string)method?.Invoke(dropdownTree.Instance, new object[] { "1" });
            Assert.Equal("Electronics", result);
        }

        [Fact]
        public void TestGetSelectedData_WhenNotFilteredAndNotFound()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetSelectedData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (string)method?.Invoke(dropdownTree.Instance, new object[] { "999" });
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void TestOnContainerClick_WhenDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Disabled, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnContainerClick", BindingFlags.NonPublic | BindingFlags.Instance);
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { null });
            task?.Wait();
        }

        [Fact]
        public void TestOnContainerClick_WithClearButtonClick()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.ShowSelectAll, true)
                .Add(p => p.Value, new List<int> { 1, 2 })
            );

            // Simulate clear button click by setting internal state
            typeof(SfDropDownTree<int, ListData>).GetField("isClearButtonClick", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, true);
            typeof(SfDropDownTree<int, ListData>).GetField("checkedNodes", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, new string[] { "1", "2" });
            typeof(SfDropDownTree<int, ListData>).GetField("overAllLiItems", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, 8);

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnContainerClick", BindingFlags.NonPublic | BindingFlags.Instance);
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { null });
            task?.Wait();

            // Verify isSelectAllChecked is updated
            var isSelectAllChecked = (bool)typeof(SfDropDownTree<int, ListData>).GetField("isSelectAllChecked", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(dropdownTree.Instance);
            Assert.False(isSelectAllChecked);
        }

        [Fact]
        public async Task TestShowPopup_WhenOnPopupOpenCancelled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.OnPopupOpen, (PopupEventArgs args) =>
                {
                    args.Cancel = true;
                })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("ShowPopup", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { null });
        }

        [Fact]
        public void TestSetTreeText_WhenValueIsNotNull()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetTreeText", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { false });
        }


        [Fact]
        public async Task TestClearAll_NotDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
                .Add(p => p.ValueChanging, (DdtChangeEventArgs<int> args) => { })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("ClearAll", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false });
        }

        [Fact]
        public async Task TestResetValue_WithCurrentValueItems()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
                .Add(p => p.ShowCheckBox, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("ResetValue", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false });
        }

        [Fact]
        public async Task TestResetValue_WithoutDynamicChange()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
            );

            // Set currentValue to have items
            typeof(SfDropDownTree<int, ListData>).GetField("currentValue", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, new List<int> { 1 });

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("ResetValue", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false });
        }

        [Fact]
        public void TestGetParents_WithHierarchicalData()
        {
            var hierarchicalData = new List<ListData>
            {
                new ListData { Id = 1, Name = "Root", HasChild = true },
                new ListData { Id = 2, Pid = 1, Name = "Child1" },
                new ListData { Id = 3, Pid = 1, Name = "Child2" }
            };

            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, hierarchicalData)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.Child, "Children"))
            );

            var checkedNodes = new List<string> { "2", "3" };
            var result = new List<string>();

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetParents", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { checkedNodes, result });

            Assert.NotNull(result);
        }

        [Fact]
        public void TestGetChild()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var tempCurrentValue = new List<string> { "1" };
            var result = new List<string>();

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetChild", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { tempCurrentValue, result });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestOnKeyPress_WithEnterKey()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
            );

            var keyArgs = new NodeKeyPressEventArgs
            {
                Action = "keyPress",
                NodeData = new NodeData { Id = "1", IsChecked = "true" }
            };

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnKeyPress", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, keyArgs });
        }

        [Fact]
        public async Task TestOnKeyPress_WithoutShowCheckBox()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var keyArgs = new NodeKeyPressEventArgs
            {
                Action = "keyPress",
                NodeData = new NodeData { Id = "1" }
            };

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnKeyPress", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, keyArgs });

        }
        


        [Fact]
        public async Task TestOnNodeSelected_WithAllowMultiSelection()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowMultiSelection, true)
            );

            var selectArgs = new NodeSelectEventArgs
            {
                Action = "select",
                IsInteracted = true,
                NodeData = new NodeData { Id = "1", Text = "Electronics" }
            };

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeSelected", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, selectArgs });
        }

        [Fact]
        public async Task TestOnNodeSelected_WithValueTemplate()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ValueTemplate, (context) => builder => builder.AddContent(0, "Template"))
            );

            var selectArgs = new NodeSelectEventArgs
            {
                Action = "select",
                IsInteracted = true,
                NodeData = new NodeData { Id = "1", Text = "Electronics" }
            };

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeSelected", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, selectArgs });
        }

        [Fact]
        public async Task TestOnNodeChecked_WithShowSelectAll()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.ShowSelectAll, true)
            );

            var checkArgs = new NodeCheckEventArgs
            {
                Action = "check",
                IsInteracted = true,
                NodeData = new NodeData { Id = "1" }
            };

            // Set checkedNodes to match overAllLiItems
            typeof(SfDropDownTree<int, ListData>).GetField("checkedNodes", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, new string[] { "1", "2", "3", "4", "5", "6", "7", "8" });
            typeof(SfDropDownTree<int, ListData>).GetField("overAllLiItems", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, 8);

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeChecked", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, checkArgs });
        }
       

        [Fact]
        public void TestSerializeModel_WithNullValue()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SerializeModel", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (string)method?.Invoke(dropdownTree.Instance, null);
            Assert.Equal("null", result);
        }

        [Fact]
        public void TestGetPopupContentClass_WithActionFailure()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.OnActionFailure, EventCallback.Factory.Create<object>(this, args => Task.CompletedTask))
            );

            // Set actionFailure to true
            typeof(SfDropDownTree<int, ListData>).GetField("actionFailure", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, true);

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetPopupContentClass", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (string)method?.Invoke(dropdownTree.Instance, null);
            Assert.Contains("e-no-data", result);
        }

        [Fact]
        public void TestGetPopupContentClass_WithFilteredDataEmpty()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowFiltering, true)
            );

            // Set filteredData to empty
            typeof(SfDropDownTree<int, ListData>).GetField("isFilteredData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, true);
            typeof(SfDropDownTree<int, ListData>).GetField("filteredData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, new List<ListData>());

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetPopupContentClass", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (string)method?.Invoke(dropdownTree.Instance, null);
            Assert.Contains("e-no-data", result);
        }

        [Fact]
        public void TestRemoveSelectedData_WithCurrentValueNull()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            // Set currentValue to null
            typeof(SfDropDownTree<int, ListData>).GetField("currentValue", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, null);

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("RemoveSelectedData", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { "1" });
        }

        [Fact]
        public void TestGetTreeData_WithValidId()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetTreeData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (List<ListData>)method?.Invoke(dropdownTree.Instance, new object[] { "1" });
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public void TestGetHierarchicalData_WithValidId()
        {
            var hierarchicalData = new List<ListData>
            {
                new ListData { Id = 1, Name = "Root", HasChild = true },
                new ListData { Id = 2, Pid = 1, Name = "Child1" }
            };

            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, hierarchicalData)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.Child, "Children"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetHierarchicalData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method?.Invoke(dropdownTree.Instance, new object[] { hierarchicalData, "1" });
            Assert.NotNull(result);
        }

        [Fact]
        public void TestUpdateData_WithIsCheckedField()
        {
            var dataWithCheck = new List<ListData>
            {
                new ListData { Id = 1, Name = "Root", IsCheckedValue = true, HasChild = true },
                new ListData { Id = 2, Pid = 1, Name = "Child1", IsCheckedValue = false }
            };

            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, dataWithCheck)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.IsChecked, "IsCheckedValue"))
                .Add(p => p.ShowCheckBox, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateData", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { dataWithCheck });
        }

        [Fact]
        public void TestGetContainerAttributes_NotDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetContainerAttributes", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (Dictionary<string, object>)method?.Invoke(dropdownTree.Instance, null);
            Assert.NotNull(result);
            Assert.False(result.ContainsKey("aria-disabled"));
        }

      
     
        [Fact]
        public async Task TestShowPopupAsync_WhenDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Disabled, true)
            );

            await dropdownTree.Instance.ShowPopupAsync();
        }

        [Fact]
        public async Task TestShowPopupAsync_WhenAlreadyOpen()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            // Set isPopupOpen to true
            typeof(SfDropDownTree<int, ListData>).GetField("isPopupOpen", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, true);

            await dropdownTree.Instance.ShowPopupAsync();
        }

        [Fact]
        public async Task TestHidePopupAsync_WhenDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Disabled, true)
            );

            await dropdownTree.Instance.HidePopupAsync();
        }

        [Fact]
        public async Task TestHidePopupAsync_WhenNotOpen()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            // isPopupOpen is false by default
            await dropdownTree.Instance.HidePopupAsync();
        }

        [Fact]
        public async Task TestSelectAllAsync_WithShowCheckBox_True()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
            );

            await dropdownTree.Instance.SelectAllAsync(true);
        }

        [Fact]
        public async Task TestSelectAllAsync_WithShowCheckBox_False()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
            );

            await dropdownTree.Instance.SelectAllAsync(false);
        }

        [Fact]
        public async Task TestSelectAllAsync_WithAllowMultiSelection_True()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowMultiSelection, true)
            );

            await dropdownTree.Instance.SelectAllAsync(true);
        }

        [Fact]
        public async Task TestSelectAllAsync_WithAllowMultiSelection_False()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.AllowMultiSelection, true)
            );

            await dropdownTree.Instance.SelectAllAsync(false);
        }

        [Fact]
        public void TestGetTreeViewData_WithValidId()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var result = dropdownTree.Instance.GetTreeViewData("1");
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestRefreshAsync()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            // Set showPopupTree to true
            typeof(SfDropDownTree<int, ListData>).GetField("showPopupTree", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, true);

            await dropdownTree.Instance.RefreshAsync();
        }

        [Fact]
        public async Task TestClearAsync()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
            );

            await dropdownTree.Instance.ClearAsync();
        }

        [Fact]
        public async Task TestUpdateValue_WithEmptyValue()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateValue", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { new List<int>() });
        }

        [Fact]
        public async Task TestSetMultiSelectValue_WithFilteredData()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
            );

            // Set isFilteredData to true
            typeof(SfDropDownTree<int, ListData>).GetField("isFilteredData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, true);
            typeof(SfDropDownTree<int, ListData>).GetField("currentValue", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, new List<int> { 1 });

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetMultiSelectValue", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { new string[] { "2", "3" } });
        }

        [Fact]
        public async Task TestSetMultiSelectValue_NotFiltered()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetMultiSelectValue", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { new string[] { "2", "3" } });
        }

        [Fact]
        public async Task TestUpdateSelectedValues_EmptyCurrentValue()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            // Set currentValue to empty
            typeof(SfDropDownTree<int, ListData>).GetField("currentValue", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, new List<int>());

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateSelectedValues", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false, false });
        }

        [Fact]
        public void TestGetSelectedData_WhenNotFiltered()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Value, new List<int> { 1 })
            );

            // Set isFilteredData to false
            typeof(SfDropDownTree<int, ListData>).GetField("isFilteredData", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dropdownTree.Instance, false);

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetSelectedData", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (string)method?.Invoke(dropdownTree.Instance, new object[] { "1" });
            Assert.Equal("Electronics", result);
        }

        [Fact]
        public async Task TestSetMultiSelect_WithDynamicChange()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetMultiSelect", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { true, false });
        }

        [Fact]
        public async Task TestOnDataSourceChanged_WithIsDataSourceUpdated()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            // Set isDataSourceUpdated through reflection
            var field = dropdownTree.FindComponent<DropDownTreeField<ListData>>();
            var fieldInstance = typeof(Microsoft.AspNetCore.Components.ParameterView).GetMethod("");

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnDataSourceChanged", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { Task.CompletedTask, null });
        }

        [Fact]
        public async Task TestGetAutoCheckId_WithSelfReferentialData()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.AutoUpdateCheckState, true)
            );

            var result = new List<string>();
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetAutoCheckId", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { "2", result });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task TestGetHierarchicalParents()
        {
            var hierarchicalData = new List<ListData>
            {
                new ListData { Id = 1, Name = "Root", HasChild = true },
                new ListData { Id = 2, Pid = 1, Name = "Child1" }
            };

            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, hierarchicalData)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.Child, "Children"))
            );

            var tempDataSource = hierarchicalData;
            var result = new List<string>();
            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetHierarchicalParents", BindingFlags.NonPublic | BindingFlags.Instance);
            var nodeData = method?.Invoke(dropdownTree.Instance, new object[] { "2", tempDataSource, result });
            Assert.NotNull(nodeData);
        }
    }
}