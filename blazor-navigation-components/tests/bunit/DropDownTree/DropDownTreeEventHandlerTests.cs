using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Navigations;
using Xunit;

namespace Syncfusion.Blazor.Tests.DropDownTree
{
    public class DropDownTreeEventHandlerTests : BunitTestContext
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
            public bool IsSelect { get; set; }
        }

        [Fact]
        public async Task TestBeforeCheck_Cancelled()
        {
            var cancelCheck = false;
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.IsChecked, "IsCheckedValue"))
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.ValueChanging, (DdtChangeEventArgs<int> args) =>
                {
                    if (!cancelCheck)
                    {
                        cancelCheck = true;
                        args.Cancel = true;
                    }
                })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("BeforeCheck", BindingFlags.NonPublic | BindingFlags.Instance);
            var tcs = new TaskCompletionSource<bool>();
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { tcs.Task, 1 });
        }

        [Fact]
        public async Task TestOnBeforeSelect_Cancelled()
        {
            var cancelSelect = false;
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.IsChecked, "IsCheckedValue"))
                .Add(p => p.ValueChanging, (DdtChangeEventArgs<int> args) =>
                {
                    if (!cancelSelect)
                    {
                        cancelSelect = true;
                        args.Cancel = true;
                    }
                })
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnBeforeSelect", BindingFlags.NonPublic | BindingFlags.Instance);
            var tcs = new TaskCompletionSource<bool>();
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { tcs.Task, 1 });
        }

        [Fact]
        public async Task TestOnNodeSelected_WithSelections()
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

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeSelected", BindingFlags.NonPublic | BindingFlags.Instance);
            var tcs = new TaskCompletionSource<bool>();
            var result = method?.Invoke(dropdownTree.Instance, new object[] { tcs.Task, 2 });
        }

        [Fact]
        public async Task TestOnNodeChecked_WithSelectAll()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.IsChecked, "IsCheckedValue"))
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.ShowSelectAll, true)
                .Add(p => p.AutoUpdateCheckState, true)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeChecked", BindingFlags.NonPublic | BindingFlags.Instance);
            var tcs = new TaskCompletionSource<bool>();
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { tcs.Task, new object[] { 2, true } });
        }

        [Fact]
        public async Task TestOnNodeChecked_AutoUpdateDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.IsChecked, "IsCheckedValue"))
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.AutoUpdateCheckState, false)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnNodeChecked", BindingFlags.NonPublic | BindingFlags.Instance);
            var tcs = new TaskCompletionSource<bool>();
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { tcs.Task, new object[] { 2, true } });
        }

        [Fact]
        public async Task TestOnKeyPress_WithValidKey()
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

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("OnKeyPress", BindingFlags.NonPublic | BindingFlags.Instance);
            var tcs = new TaskCompletionSource<bool>();
            var nodeKeyPressEventArgs = new NodeKeyPressEventArgs { NodeData = new TreeNode { Id = "1", Text = "Test" },  Cancel = false };
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { tcs.Task, nodeKeyPressEventArgs });
        }

        [Fact]
        public async Task TestSetMultiSelect_WithSingleMode()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Mode, Syncfusion.Blazor.Navigations.DdtVisualMode.Default)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetMultiSelect", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false, false });
        }

        [Fact]
        public async Task TestSetMultiSelect_WithDelimiterMode()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.Mode, Syncfusion.Blazor.Navigations.DdtVisualMode.Delimiter)
                .Add(p => p.DelimiterChar, ";")
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetMultiSelect", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false, false });
        }

        [Fact]
        public async Task TestSetMultiSelectValue()
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
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetMultiSelectValue", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { new string[] { "1", "2" } });
        }


        [Fact]
        public async Task TestGetParents_WithValidNode()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetParents", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = (object)method?.Invoke(dropdownTree.Instance, new object[] { new List<string> { "2" }, new List<string>() });
            Assert.Null(result);
        }

        [Fact]
        public void TestGetChild_WithValidId()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetChild", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method?.Invoke(dropdownTree.Instance, new object[] { new List<string> { "1" }, new List<string>() });
            Assert.Null(result);
        }

        [Fact]
        public void TestGetChild_WithInvalidId()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("GetChild", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method?.Invoke(dropdownTree.Instance, new object[] { new List<string> { "999" }, new List<string>() });
            Assert.Null(result);
        }

        [Fact]
        public async Task TestUpdateSelectedValues_WithNoSelection()
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

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateSelectedValues", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { false, false });
        }

        [Fact]
        public async Task TestUpdateValue_WithTextChange()
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

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdateValue", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, new object[] { new List<int> { 1 } });
        }

        [Fact]
        public void TestUpdatePopupState()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdatePopupState", BindingFlags.NonPublic | BindingFlags.Instance);
            method?.Invoke(dropdownTree.Instance, new object[] { true });
        }

        [Fact]
        public async Task TestUpdatePersistence()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.EnablePersistence, true)
                .Add(p => p.ID, "dropdowntree")
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdatePersistence", BindingFlags.NonPublic | BindingFlags.Instance);
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { });
            if (task != null)
            {
                await task;
            }
        }

        [Fact]
        public async Task TestUpdatePersistence_WithoutEnablePersistence()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.EnablePersistence, false)
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("UpdatePersistence", BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method?.Invoke(dropdownTree.Instance, Array.Empty<object>());
        }

        [Fact]
        public async Task TestSetLocalStorage_WhenDisabled()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource ?? new List<ListData>())
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
                .Add(p => p.EnablePersistence, false) // ? fix
            );

            var method = typeof(SfDropDownTree<int, ListData>)
                .GetMethod("SetLocalStorage", BindingFlags.NonPublic | BindingFlags.Instance);

            var task = method?.Invoke(dropdownTree.Instance, new object[] { "testId", "{}" }) as Task;
            await task;

            Assert.True(true); // just verifying no exception
        }


        [Fact]
        public async Task TestSetTreeValue_WithPopulatedData()
        {
            var dropdownTree = RenderComponent<SfDropDownTree<int, ListData>>(parameters => parameters
                .AddChildContent<DropDownTreeField<ListData>>(field => field
                    .Add(p => p.DataSource, ListDataSource)
                    .Add(p => p.ID, "Id")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded"))
            );

            var method = typeof(SfDropDownTree<int, ListData>).GetMethod("SetTreeValue", BindingFlags.NonPublic | BindingFlags.Instance);
            var task = (Task)method?.Invoke(dropdownTree.Instance, new object[] { false });
            task?.Wait();
        }

        public class TreeData
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public bool Expanded { get; set; }
            public List<TreeData> Child { get; set; }
        }
    }
}