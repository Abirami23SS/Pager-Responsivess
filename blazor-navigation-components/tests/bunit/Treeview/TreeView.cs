using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Bunit;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Base;
using static Bunit.ComponentParameterFactory;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using AngleSharp.Css.Dom;
using Microsoft.AspNetCore.Components.Web;
using AngleSharp.Html.Dom.Events;
using Syncfusion.Blazor.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Navigations.Internal;
using System.Dynamic;
using Newtonsoft.Json.Linq;
using Microsoft.JSInterop;
using System.Reflection;
using Syncfusion.Blazor.Tests.TreeView.Samples;

namespace Syncfusion.Blazor.Tests.Treeview
{
    public class TreeView : BunitTestContext
    {
        List<Listdata> ListDataSource { get; set; } = new List<Listdata>();
        List<TreeData> TreeDataSource { get; set; } = new List<TreeData>();

        List<PermissionGroup> PermissionGroups { get; set; } = new List<PermissionGroup>();
        public List<PermissionGroup> GeneratePermissionGroupData()
        {
            PermissionGroups.Add(
                new PermissionGroup
                {
                    Id = "1",
                    Name = "P1",
                    IsAllowed = true,
                    PermissionGroups = new List<PermissionGroup>() { new PermissionGroup() { Id = "1-1", Name = "SUB P1" } }
                });
            return PermissionGroups;
        }
        public List<PermissionGroup> GeneratePermissionGroupData1()
        {
            List<PermissionGroup> PermissionGroups1 = new List<PermissionGroup>();
            PermissionGroups1.Add(new PermissionGroup
            {
                Id = "2",
                Name = "P2",
                PermissionGroups = new List<PermissionGroup>() { new PermissionGroup() { Id = "2-1", Name = "SUB P2", IsAllowed = true } }
            });
            return PermissionGroups1;
        }
        public List<PermissionGroup> GeneratePermissionGroupData2()
        {
            List<PermissionGroup> PermissionGroups2 = new List<PermissionGroup>();
            PermissionGroups2.Add(new PermissionGroup
            {
                Id = "3",
                Name = "P3",
                PermissionGroups = new List<PermissionGroup>() { new PermissionGroup() { Id = "3-1", Name = "SUB P3" } }
            });
            return PermissionGroups2;
        }
        public List<TreeData> GenerateTreeData()
        {
            TreeDataSource.Add(new TreeData
            {
                Code = "NA",
                Name = "North America",
                Expanded = true,
                Child = new List<TreeData>()
            {
                new TreeData { Code = "USA", Name = "United States of America", Selected = true },
                new TreeData { Code = "CUB", Name = "Cuba" },
                new TreeData { Code = "MEX", Name = "Mexico" },
            },
                Link = "https://blazor.syncfusion.com/demos/"
            });
            TreeDataSource.Add(new TreeData
            {
                Code = "AF",
                Name = "Africa",
                Child = new List<TreeData>()
            {
                new TreeData { Code = "NGA", Name = "Nygeria" },
                new TreeData { Code = "EGY", Name = "Egypt" },
                new TreeData { Code = "ZAF", Name = "South Africa" },
            },
            });
            TreeDataSource.Add(new TreeData
            {
                Code = "AS",
                Name = "Asia",
                Child = new List<TreeData>()
            {
                new TreeData { Code = "CHN", Name = "China" },
                new TreeData { Code = "IND", Name = "India" },
                new TreeData { Code = "JPN", Name = "Japan" },
            },
            });
            TreeDataSource.Add(new TreeData
            {
                Code = "EU",
                Name = "Europe",
                Child = new List<TreeData>()
            {
                new TreeData { Code = "DNK", Name = "Denmark" },
                new TreeData { Code = "AUT", Name = "Austria" },
                new TreeData { Code = "FIN", Name = "Finland" },
            },
            });
            TreeDataSource.Add(new TreeData
            {
                Code = "SA",
                Name = "South America",
                Child = new List<TreeData>()
            {
                new TreeData { Code = "BRA", Name = "Brazil" },
                new TreeData { Code = "COL", Name = "Colombia" },
                new TreeData { Code = "ARG", Name = "Argentina" },
            },
            });
            TreeDataSource.Add(new TreeData
            {
                Code = "OC",
                Name = "Oceania",
                Child = new List<TreeData>()
            {
                new TreeData { Code = "AUS", Name = "Australia" },
                new TreeData { Code = "NZL", Name = "Newzealand" },
                new TreeData { Code = "WSM", Name = "Samoa" },
            },
            });
            TreeDataSource.Add(new TreeData
            {
                Code = "AN",
                Name = "Antartica",
                Child = new List<TreeData>()
            {
                new TreeData { Code = "BVT", Name = "Bouvet Island" },
                new TreeData { Code = "ATF", Name = "French Southern Lands" },
            },
            });
            return TreeDataSource;
        }

        public List<TreeData> NewTreeData()
        {
            TreeDataSource.Add(new TreeData
            {
                Code = "1",
                Name = "America",
                Expanded = true,
                Child = new List<TreeData>()
                {
                    new TreeData { Code = "CUB", Name = "Cuba" , Selected = true },
                    new TreeData { Code = "MEX", Name = "Mexico", Selected = true  },
                }
            });
            return TreeDataSource;
        }
        public class TestVm
        {
            public List<MailItem> MyFolder = new List<MailItem>();

            public TestVm()
            {
            }
            public async Task Load()
            {
                await Task.Delay(100);
                List<MailItem> Folder1 = new List<MailItem>();
                MyFolder.Add(new MailItem
                {
                    Id = "01",
                    FolderName = "Inbox",
                    SubFolders = Folder1,
                    Expanded = true,
                    IsChecked = null
                });

                List<MailItem> Folder2 = new List<MailItem>();

                Folder1.Add(new MailItem
                {
                    Id = "01-01",
                    FolderName = "Categories",
                    SubFolders = Folder2,
                    IsChecked = true

                });
                Folder2.Add(new MailItem
                {
                    Id = "01-02",
                    FolderName = "Primary"
                });
                Folder2.Add(new MailItem
                {
                    Id = "01-03",
                    FolderName = "Social",
                    IsChecked = null
                });
                Folder2.Add(new MailItem
                {
                    Id = "01-04",
                    FolderName = "Promotions"
                });

                List<MailItem> Folder3 = new List<MailItem>();

                MyFolder.Add(new MailItem
                {
                    Id = "02",
                    FolderName = "Others",
                    Expanded = true,
                    SubFolders = Folder3,
                    IsChecked = null
                });
                Folder3.Add(new MailItem
                {
                    Id = "02-01",
                    FolderName = "Sent Items",
                    IsChecked = true
                });
                Folder3.Add(new MailItem
                {
                    Id = "02-02",
                    FolderName = "Delete Items"
                });
                Folder3.Add(new MailItem
                {
                    Id = "02-03",
                    FolderName = "Drafts"
                });
                Folder3.Add(new MailItem
                {
                    Id = "02-04",
                    FolderName = "Archive"
                });
            }
        }
        public List<Listdata> GenerateListData()
        {
            ListDataSource.Add(new Listdata
            {
                Id = "1",
                Name = "Australia",
                HasChild = true,
                Expanded = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "2",
                Pid = "1",
                IsCheckedValue = true,
                Name = "New South Wales",
            });
            ListDataSource.Add(new Listdata
            {
                Id = "3",
                Pid = "1",
                Name = "Victoria"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "4",
                Pid = "1",
                Name = "South Australia"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "6",
                Pid = "1",
                Name = "Western Australia",
            });
            ListDataSource.Add(new Listdata
            {
                Id = "7",
                Name = "Brazil",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "8",
                Pid = "7",
                Name = "Paraná"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "9",
                Pid = "7",
                Name = "Ceará"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "10",
                Pid = "7",
                Name = "Acre"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "11",
                Name = "China",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "12",
                Pid = "11",
                Name = "Guangzhou"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "13",
                Pid = "11",
                Name = "Shanghai"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "14",
                Pid = "11",
                Name = "Beijing"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "15",
                Pid = "11",
                Name = "Shantou"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "16",
                Name = "France",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "17",
                Pid = "16",
                Name = "Pays de la Loire"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "18",
                Pid = "16",
                Name = "Aquitaine"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "19",
                Pid = "16",
                Name = "Brittany"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "20",
                Pid = "16",
                Name = "Lorraine"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "21",
                Name = "India",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "22",
                Pid = "21",
                Name = "Assam"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "23",
                Pid = "21",
                Name = "Bihar"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "24",
                Pid = "21",
                Name = "Tamil Nadu"
            });
            return ListDataSource;
        }
        public List<Listdata> NewListData()
        {
            List<Listdata> TreeDataSource = new List<Listdata>();
            TreeDataSource.Add(new Listdata
            {
                Id = "1",
                Name = "Parent",
                HasChild = true,
                Expanded = true
            });
            TreeDataSource.Add(new Listdata
            {
                Id = "2",
                Pid = "1",
                IsCheckedValue = true,
                Name = "Child",
            });
            return TreeDataSource;
        }
        public static List<ExpandoObject> Data = new List<ExpandoObject>();
        public static int ParentRecordID { get; set; }
        public static int ChildRecordID { get; set; }
        public static List<ExpandoObject> GetData()
        {
            Data.Clear();
            ParentRecordID = 0;
            ChildRecordID = 0;
            for (var i = 1; i <= 3; i++)
            {
                dynamic ParentRecord = new ExpandoObject();
                ParentRecord.ID = ++ParentRecordID;
                ParentRecord.Name = "Parent " + i;
                ParentRecord.ParentID = null;
                ParentRecord.Expanded = true;
                Data.Add(ParentRecord);
                AddChildRecords(ParentRecordID);
            }
            return Data;
        }
        public static void AddChildRecords(int ParentId)
        {
            for (var i = 1; i < 3; i++)
            {
                dynamic ChildRecord = new ExpandoObject();
                ChildRecord.ID = ++ParentRecordID;
                ChildRecord.Name = "Child item" + ++ChildRecordID;
                ChildRecord.ParentID = ParentId;
                Data.Add(ChildRecord);
            }
        }
        public class PermissionGroup
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public bool IsAllowed { get; set; }
            public List<PermissionGroup> PermissionGroups { get; set; }
        }
        public class TreeData
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public bool Expanded { get; set; }
            public bool Expand { get; set; }
            public bool Select { get; set; }
            public bool Selected { get; set; }
            public string Link { get; set; }
            public List<TreeData> Child;
            public bool IsChecked { get; set; }
        }
        public class MailItem
        {
            public string Id { get; set; }
            public string FolderName { get; set; }
            public bool Expanded { get; set; }
            public bool? IsChecked { get; set; }
            public List<MailItem> SubFolders { get; set; }
        }
        public class Listdata
        {
            public string Id { get; set; }
            public string Pid { get; set; }
            public string Name { get; set; }
            public bool HasChild { get; set; }
            public bool IsCheckedValue { get; set; }
            public bool Expanded { get; set; }
        }
        public class RemoteTreeData
        {
            public int? EmployeeID { get; set; }
            public int OrderID { get; set; }
            public string ShipName { get; set; }
            public string FirstName { get; set; }
        }

        [Fact(Timeout = 10000, DisplayName = "Empty Initialization")]
        public void DefaultInitialize()
        {
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var rootEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            Assert.Contains("e-control", rootEle.ClassName);
            Assert.Contains("e-fullrow-wrap", rootEle.ClassName);
            Assert.True(ulElements.Count == 1);
            Assert.True(rootEle.ChildElementCount == 1);
            Assert.True(rootEle.HasChildNodes);
            Assert.True(rootEle.NodeName == "DIV");
            Assert.Equal("tree", rootEle.GetAttribute("role"));
        }

        [Fact(Timeout = 10000, DisplayName = "Empty Initialization With Properties")]
        public void DefaultInitialize_with_properties()
        {
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Child, "Child")));
            treeview.Find(".e-treeview");
            Assert.False(treeview.Instance.AllowDragAndDrop);
            Assert.False(treeview.Instance.AllowEditing);
            Assert.False(treeview.Instance.AllowMultiSelection);
            Assert.True(treeview.Instance.AutoCheck);
            Assert.False(treeview.Instance.Disabled);
            Assert.False(treeview.Instance.EnableRtl);
            Assert.False(treeview.Instance.FullRowNavigable);
            Assert.True(treeview.Instance.FullRowSelect);
            Assert.True(treeview.Instance.LoadOnDemand);
            Assert.False(treeview.Instance.ShowCheckBox);
            Assert.NotNull(treeview.Instance.SortOrder);
            Assert.Null(treeview.Instance.ID);
            Assert.Null(treeview.Instance.DropArea);

        }

        [Fact(Timeout = 10000, DisplayName = "Default rendering with Hierachial data source")]
        public void DefaultCase()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length;
            Assert.True(3 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(2 == ulLength, "Number of ul elements are generated properly");
            Assert.True(10 == liLength, "Number of li elements are generated properly");
        }

        [Fact(Timeout = 10000, DisplayName = "Default rendering with Hierachial data source With default properties")]
        public void DefaultCase_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length;
            Assert.True(3 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(2 == ulLength, "Number of ul elements are generated properly");
            Assert.True(10 == liLength, "Number of li elements are generated properly");

            Assert.False(treeview.Instance.AllowDragAndDrop);
            Assert.False(treeview.Instance.AllowEditing);
            Assert.False(treeview.Instance.AllowMultiSelection);
            Assert.True(treeview.Instance.AutoCheck);
            Assert.False(treeview.Instance.Disabled);
            Assert.False(treeview.Instance.EnableRtl);
            Assert.False(treeview.Instance.FullRowNavigable);
            Assert.True(treeview.Instance.FullRowSelect);
            Assert.True(treeview.Instance.LoadOnDemand);
            Assert.False(treeview.Instance.ShowCheckBox);
            Assert.NotNull(treeview.Instance.SortOrder);
            Assert.Null(treeview.Instance.ID);
            //Assert.Null(treeview.Instance.ExpandedNodes);
            Assert.Null(treeview.Instance.DropArea);
            Assert.NotNull(treeview.Instance.ChildContent);
        }

        [Fact(Timeout = 10000, DisplayName = "Disabled with Hierachial data source")]
        public void Disabled()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.SetParametersAndRender(("Disabled", false));
            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass with Hierachial data source")]
        public void CssClass()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, string.Empty).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.SetParametersAndRender(("CssClass", string.Empty));
            Assert.True(!treeEle.ClassList.Contains("custom"), "CssClass property working properly dynamic update case");
        }

        [Fact(Timeout = 10000, DisplayName = "RTL with Hierachial data source")]
        public void RTL()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.SetParametersAndRender(("EnableRtl", false));
            Assert.True(!treeEle.ClassList.Contains("e-rtl"), "RTL property working properly dynamic update case");
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded with Hierachial data source")]
        public void ExpandedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        [Fact(Timeout = 10000, DisplayName = "Selected with Hierachial data source (without Multiselection)")]
        public void SelectedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");
        }

        [Fact(Timeout = 10000, DisplayName = "Show Checkbox with Hierachial data source")]
        public void ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
            treeview.SetParametersAndRender(("ShowCheckBox", false));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Checked Nodes with Hierachial data source")]
        public void CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Checked Nodes with auto check Hierachial data source")]
        public void AutoCheck()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i == 0 || i == 4)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
        }

        [Fact(Timeout = 10000, DisplayName = "FullRowSelect with Hierachial data source")]
        public void FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowSelect, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            Assert.DoesNotContain("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle == null, "FullRowSelect property is working properly");
            }
            treeview.SetParametersAndRender(("FullRowSelect", true));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            Assert.Contains("e-control e-lib e-treeview", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "SortData with Hierachial data source")]
        public void SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).Add(p=> p.SortComparer, null).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Navigation URL checking with Hierachial data source")]
        public void NavigationUrl()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowNavigable, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow-wrap");
                Assert.True(fullRowEle == null, "FullRowSelect property is working properly");
            }
            treeview.SetParametersAndRender(("FullRowSelect", true));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Navigation URL checking with Hierachial data source")]
        public void NavigationUrl_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
            treeview.SetParametersAndRender(("FullRowSelect", false));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            Assert.DoesNotContain(".e-navigable", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle == null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "ID with Hierachial data source")]
        public void ID_mapping()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "LoadOnDemand with Hierachial data source")]
        public void LoadOnDemand()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            var treeload = treeview.Instance.LoadOnDemand;
            Assert.True(treeload);

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var treeload1 = treeview.Instance.LoadOnDemand;
            Assert.True(!treeload1);

        }
        [Fact(Timeout = 10000, DisplayName = "LoadOnDemand and id1 with Hierachial data source")]
        public void LoadOnDemand_id()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            var treeload = treeview.Instance.LoadOnDemand;
            Assert.True(treeload);

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var treeload1 = treeview.Instance.LoadOnDemand;
            Assert.True(!treeload1);
          
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "Persistence with Hierachial data source (without Multiselection)")]
        public void EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "Persistence with Hierachial data source (with Multiselection)")]
        public void EnablePersistence_With_multiselection()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI not generated properly with multiselection with persistence");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI not generated properly with multiselection with persistence");
        }

        [Fact(Timeout = 10000, DisplayName = "LoadOndemand")]
        public void LoadOnDemand_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(7 == liLength, "Number of li elements are not generated properly");

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var liElements1 = treeview.FindAll("li");
            var liLength1 = treeview.FindAll("li").Count;

            Assert.True(27 == liLength1, "Number of li elements are not generated properly");
        }

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck with Hierachial data source")]
        public void RTL_with_autocheck()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");

            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "RTL_CssClass with Hierachial data source")]
        public void RTL_CssClass()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            treeview.SetParametersAndRender(("CssClass", "custom"));
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

        }

        [Fact(Timeout = 10000, DisplayName = "RTL with_Disabled Hierachial data source")]
        public void RTL_Disabled()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property not working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

        }

        [Fact(Timeout = 10000, DisplayName = "RTL With FullRowSelect with Hierachial data source")]
        public void RTL_FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowSelect, false).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");
            var liElements = treeview.FindAll("li");
            Assert.DoesNotContain("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle == null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "RTL_ID with Hierachial data source")]
        public void RTL_with_ID_mapping()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.Find("#tree");
           
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "RTL_LoadOndemand")]
        public void RTL_LoadOnDemand_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            Assert.True(7 == liLength, "Number of li elements are not generated properly");

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var liElements1 = treeview.FindAll("li");
            var liLength1 = treeview.FindAll("li").Count;

            Assert.True(27 == liLength1, "Number of li elements are not generated properly");
        }

        [Fact(Timeout = 10000, DisplayName = "RTL Navigation URL checking with Hierachial data source")]
        public void RTL_NavigationUrl_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

         
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var liElements = treeview.FindAll("li");
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
            treeview.SetParametersAndRender(("FullRowSelect", false));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            Assert.DoesNotContain(".e-navigable", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle == null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "RTL_Persistence with Hierachial data source (without Multiselection)")]
        public void RTL_EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "RTL Enabled Show Checkbox with Hierachial data source")]
        public void RTL_ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
            treeview.SetParametersAndRender(("ShowCheckBox", false));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "RTL and SortData with Hierachial data source")]
        public void RTL_SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Disabled and Checked Nodes with auto check Hierachial data source")]
        public void Disabled_AutoCheck()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.Disabled, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

        }

        [Fact(Timeout = 10000, DisplayName = "Disabled and Checked Nodes with Hierachial data source")]
        public void Disabled_CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.Disabled, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Disabled and CssClass with Hierachial data source")]
        public void Disabled_CssClass()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property not working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property working properly initial case");

        }

        [Fact(Timeout = 10000, DisplayName = "Disabled and FullRowSelect with Hierachial data source")]
        public void Disabled_FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowSelect, true).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Disabledtrue and ID with Hierachial data source")]
        public void Disabled_ID_mapping()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.ID, "tree").AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

       
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }
        [Fact(Timeout = 10000, DisplayName = "Disabledfalse and ID with Hierachial data source")]
        public void DisabledFalse_ID_mapping()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.ID, "tree").AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "Disabledtrue and LoadOndemand")]
        public void Disabled_LoadOnDemand_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            Assert.True(7 == liLength, "Number of li elements are not generated properly");

        }
        [Fact(Timeout = 10000, DisplayName = "Disabledfalse and LoadOndemand")]
        public void DisabledFalse_LoadOnDemand_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");

            Assert.True(7 == liLength, "Number of li elements are not generated properly");

        }


        [Fact(Timeout = 10000, DisplayName = "Disabledtrue and Navigation URL checking with Hierachial data source")]
        public void DisabledTrue_NavigationUrl_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");

            
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

        }
        [Fact(Timeout = 10000, DisplayName = "Disabledfalse and Navigation URL checking with Hierachial data source")]
        public void DisabledFalse_NavigationUrl_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");


            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");

            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "Disabledtrue and Persistence with Hierachial data source (without Multiselection)")]
        public void DisabledTrue_EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

        }
        [Fact(Timeout = 10000, DisplayName = "Disabledfalse and Persistence with Hierachial data source (without Multiselection)")]
        public void DisabledFalse_EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }
        [Fact(Timeout = 10000, DisplayName = "Disabledtrue and Show Checkbox with Hierachial data source")]
        public void Disabledtrue_ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
            treeview.SetParametersAndRender(("ShowCheckBox", false));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Disabledfalse and Show Checkbox with Hierachial data source")]
        public void Disabledfalse_ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
            treeview.SetParametersAndRender(("ShowCheckBox", false));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }
        
        [Fact(Timeout = 10000, DisplayName = "Disabledtrue and SortData with Hierachial data source")]
        public void DisabledTrue_SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Disabledfalse and SortData with Hierachial data source")]
        public void DisabledFalse_SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");

            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
        }
       

        [Fact(Timeout = 10000, DisplayName = "CssClass and Autocheck Hierachial data source")]
        public void CssClass_Autocheck()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

           
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i == 0 || i == 4)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and Checked Nodes with Hierachial data source")]
        public void CssClass_CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and FullRowSelect with Hierachial data source")]
        public void CssClass_FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.FullRowSelect, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var liElements = treeview.FindAll("li");
            Assert.DoesNotContain("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle == null, "FullRowSelect property is working properly");
            }
            treeview.SetParametersAndRender(("FullRowSelect", true));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            Assert.Contains("e-control e-lib e-treeview custom", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and ID with Hierachial data source")]
        public void CssClass_ID_mapping()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.ID, "tree").AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

           
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and LoadOndemand")]
        public void CssClass_LoadOnDemand_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(7 == liLength, "Number of li elements are not generated properly");

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var liElements1 = treeview.FindAll("li");
            var liLength1 = treeview.FindAll("li").Count;

            Assert.True(27 == liLength1, "Number of li elements are not generated properly");
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and Navigation URL checking with Hierachial data source")]
        public void CssClass_NavigationUrl_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

           
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var liElements = treeview.FindAll("li");
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and Persistence with Hierachial data source (without Multiselection)")]
        public void CssClass_EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and Show Checkbox with Hierachial data source")]
        public void CssClass_ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and Selected with Hierachial data source (without Multiselection)")]
        public void CssClass_SelectedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

       
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and SortData with Hierachial data source")]
        public void CssClass_SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and Expanded with Hierachial data source")]
        public void CssClass_EpandedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        [Fact(Timeout = 10000, DisplayName = "ID and  auto check Hierachial data source")]
        public void ID_AutoCheck()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i == 0 || i == 4)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
        }

        [Fact(Timeout = 10000, DisplayName = "ID and FullRowSelect with Hierachial data source")]
        public void ID_FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.FullRowSelect, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            var liElements = treeview.FindAll("li");
            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

        }
        [Fact(Timeout = 10000, DisplayName = "ID and Navigation URL checking with Hierachial data source")]
        public void ID_NavigationUrl_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            var liElements = treeview.FindAll("li");
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "ID and Persistence with Hierachial data source (without Multiselection)")]
        public void ID_EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

            
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "ID and Show Checkbox with Hierachial data source")]
        public void ID_ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }

        }
        [Fact(Timeout = 10000, DisplayName = "ID and SortData with Hierachial data source")]
        public void ID_SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "AutoCheck and FullRowSelect with Hierachial data source")]
        public void AutoCheck_FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.FullRowSelect, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.False(fullRowEle == null, "FullRowSelect property is working properly");
            }
            
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i == 0 || i == 4)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
        }

       [Fact(Timeout = 10000, DisplayName = "Autocheck and LoadOndemand")]
        public void AutoCheck_LoadOnDemand_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.LoadOnDemand, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i == 0 || i == 4)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

            Assert.False(7 == liLength, "Number of li elements are not generated properly");

            
        }
        [Fact(Timeout = 10000, DisplayName = "Autocheck and Navigation URL checking with Hierachial data source")]
        public void Autocheck_NavigationUrl_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");

            var liLength = treeview.FindAll("li").Count;
            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Autocheck and Persistence with Hierachial data source (without Multiselection)")]
        public void Autocheck_EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Autocheck and Show Checkbox with Hierachial data source")]
        public void Autocheck_ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Autocheck and Selected with Hierachial data source (without Multiselection)")]
        public void Autocheck_SelectedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Autocheck and SortData with Hierachial data source")]
        public void Autocheck_SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Autocheck and Expanded with Hierachial data source")]
        public void Autocheck_EpandedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var liElements = treeview.FindAll("li");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Autocheck and Checked Nodes with Hierachial data source")]
        public void Autocheck_CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }

            }
        }


        [Fact(Timeout = 10000, DisplayName = "Loadondemand and Checked Nodes with Hierachial data source")]
        public void LoadOnDemand_CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "LoadonDemand and FullRowSelect with Hierachial data source")]
        public void Loadondemand_FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowSelect, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "LoadonDemand and Navigation URL checking with Hierachial data source")]
        public void LoadonDemand_NavigationUrl_1()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "LoadonDemand and Persistence with Hierachial data source (without Multiselection)")]
        public void Loadondemand_EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(7 == liLength, "Number of li elements are not generated properly");

            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "Loadondemand and Show Checkbox with Hierachial data source")]
        public void Loadondemand_ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(7 == liLength, "Number of li elements are not generated properly");

        }

        [Fact(Timeout = 10000, DisplayName = "Loadondemand and Selected with Hierachial data source (without Multiselection)")]
        public void Loadondemand_SelectedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(7 == liLength, "Number of li elements are not generated properly");

            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

        }

        [Fact(Timeout = 10000, DisplayName = "Loadondemand and SortData with Hierachial data source")]
        public void Loadondemand_SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
        }


        [Fact(Timeout = 10000, DisplayName = "Loadondemand and Expanded with Hierachial data source")]
        public void Loadondemand_EpandedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(13 == liLength, "Number of li elements are not generated properly");

            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        [Fact(Timeout = 10000, DisplayName = "FullrowNavigable and Checked Nodes with Hierachial data source")]
        public void Fullrownavigable_CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("FullRowNavigable", true));
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Fullrownavigable and FullRowSelect with Hierachial data source")]
        public void Fullrownavigable_FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.FullRowSelect, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("FullRowNavigable", true));
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

        }


        [Fact(Timeout = 10000, DisplayName = "Fullrownavigable and Persistence with Hierachial data source (without Multiselection)")]
        public void Fullrownavigable_EnablePersistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("FullRowNavigable", true));
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "Fullrownavigable and Show Checkbox with Hierachial data source")]
        public void Fullrownavigable_ShowCheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("FullRowNavigable", true));
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "Fullrownavigable and Selected with Hierachial data source (without Multiselection)")]
        public void Fullrownavigable_SelectedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("FullRowNavigable", true));
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

        }

        [Fact(Timeout = 10000, DisplayName = "Fullrownavigable and SortData with Hierachial data source")]
        public void fullrownavigable_SortData()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("FullRowNavigable", true));
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Fullrownavigable and Expanded with Hierachial data source")]
        public void Fullrownavigable_EpandedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("FullRowNavigable", true));
            treeview.Find(".e-navigable");
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        [Fact(Timeout = 10000, DisplayName = "EnablePersistence and showcheckbox true with Hierachial data source")]
        public void EnablePersistence_ShowCheckBox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "EnablePersistence and showcheckbox false with Hierachial data source")]
        public void EnablePersistence_ShowCheckBoxFalse()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

            treeview.SetParametersAndRender(("ShowCheckBox", false));
            var liElements = treeview.FindAll("li");

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }
 
        [Fact(Timeout = 10000, DisplayName = "Show Checkbox true and sortData Ascending with Hierachial data source")]
        public void ShowCheckbox_SortDataAscending()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            
            
            var liLength = treeview.FindAll("li").Count;
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }

        }
        [Fact(Timeout = 10000, DisplayName = "Show Checkbox false and sortData Ascending with Hierachial data source")]
        public void ShowCheckboxFalse_SortDataAscending()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }

            

        }
        [Fact(Timeout = 10000, DisplayName = "Show Checkbox true and sortData Descending with Hierachial data source")]
        public void ShowCheckbox_SortDataDescending()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
        
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }


            var liLength = treeview.FindAll("li").Count;
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Show Checkbox false and sortData Descending with Hierachial data source")]
        public void ShowCheckboxFalse_SortDataDescending()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }

            treeview.SetParametersAndRender(("ShowCheckBox", false));
            liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Show Checkbox true and sortData None with Hierachial data source")]
        public void ShowCheckbox_SortDataNone()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }


            var liLength = treeview.FindAll("li").Count;
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Show Checkbox false and sortData None with Hierachial data source")]
        public void ShowCheckboxFalse_SortDataNone()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }

            treeview.SetParametersAndRender(("ShowCheckBox", false));
            liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Expanded and showcheckbox true with Hierachial data source")]
        public void ExpandedNodes_showcheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");

            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Expanded and showcheckbox false with Hierachial data source")]
        public void ExpandedNodes_showcheckboxFalse()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");

            treeview.SetParametersAndRender(("ShowCheckBox", false));
            var liElements = treeview.FindAll("li");

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Selected(without Multiselection) and Showcheckbox true with Hierachial data source")]
        public void SelectedNodes_Showcheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");

            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Selected(without Multiselection) and Showcheckbox false with Hierachial data source")]
        public void SelectedNodes_ShowcheckboxFalse()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");

            treeview.SetParametersAndRender(("ShowCheckBox", false));
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }

        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and showcheckbox true with Hierachial data source")]
        public void CheckedNodes_Showcheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }

             liElements = treeview.FindAll("li");
             liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and showcheckbox false with Hierachial data source")]
        public void CheckedNodes_ShowcheckboxFalse()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }

            treeview.SetParametersAndRender(("ShowCheckBox", false));
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle == null, "Show checkbox property is working properly after dyanmic update");
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and Enable Persistance with Hierachial data source")]
        public void CheckedNodes_EnablePersistance()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }

            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");
        }
        [Fact(Timeout = 10000, DisplayName = "Selected nodes and EnablePersistance with Hierachial data source (without Multiselection)")]
        public void SelectedNodes_EnablePersistance()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


             selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");
        }
        [Fact(Timeout = 10000, DisplayName = "Persistence and Expanded Nodes with Hierachial data source (without Multiselection)")]
        public void EnablePersistence_ExpandedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

            
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(3 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(13 == liLength, "Number of li elements are generated properly after dynamic update");

        }
        [Fact(Timeout = 10000, DisplayName = "SortData Ascending and Persistance with Hierachial data source")]
        public void SortData_Persistance()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }

           
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

        }
        [Fact(Timeout = 10000, DisplayName = "SortData Descending and Persistance with Hierachial data source")]
        public void SortDataDescen_Persistance()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }


            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

        }
        [Fact(Timeout = 10000, DisplayName = "SortData None and Persistance with Hierachial data source")]
        public void SortDataNone_Persistance()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");

            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var persistence = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");
        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and selected nodes with Hierachial data source")]
        public void CheckedNodes_SelectedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }

     
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");
        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and expanded nodes nodes with Hierachial data source")]
        public void CheckedNodes_ExpandedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }
            
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(3 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(13 == liLength, "Number of li elements are generated properly after dynamic update");
        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and sortOrder Descending nodes with Hierachial data source")]
        public void CheckedNodes_SortDescending()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }

           
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }

        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and sortOrder None nodes with Hierachial data source")]
        public void CheckedNodes_SortNone()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }


            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }

        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and sortOrder Ascending nodes with Hierachial data source")]
        public void CheckedNodes_SortAscending()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }


            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Ascending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }

        }
        [Fact(Timeout = 10000, DisplayName = "Selected and expanded nodes with Hierachial data source (without Multiselection)")]
        public void Selected_ExpandedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");

           
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(3 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(13 == liLength, "Number of li elements are generated properly after dynamic update");
        }
        [Fact(Timeout = 10000, DisplayName = "Selected and sortAscending with Hierachial data source (without Multiselection)")]
        public void Selected_SortAscen()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");
        }
        [Fact(DisplayName = "Selected and sortDescending with Hierachial data source (without Multiselection)")]
        public void Selected_SortDescen()
        {

            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.Descending));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }


            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("USA" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
        }
        [Fact(Timeout = 10000, DisplayName = "Selected and sortNone with Hierachial data source (without Multiselection)")]
        public void Selected_SortNone()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }
            treeview.SetParametersAndRender(("SortOrder", Blazor.Navigations.SortOrder.None));
            treeEle = treeview.Find(".e-treeview");
            liElements = treeview.FindAll("li");
            for (var i = 0; i < liElements.Count; i++)
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None as sorting property is working properly");
            }
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");
        }
        [Fact(Timeout = 10000, DisplayName = "Expanded and Rtl with Hierachial data source")]
        public void ExpandedNodes_Rtl()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");
        }
        [Fact(Timeout = 10000, DisplayName = "RTL and Selected with Hierachial data source (without Multiselection)")]
        public void RTL_SelectedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

           
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");
        }
        [Fact(Timeout = 10000, DisplayName = "RTL with Checked Nodes with auto check Hierachial data source")]
        public void RTl_CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");

            
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i == 0 || i == 4)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
            treeview.SetParametersAndRender(("AutoCheck", true));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }
        }
        [Fact(Timeout = 10000, DisplayName = "Expanded and disabled true with Hierachial data source")]
        public void ExpandedNodes_DisabledTrue()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");

           
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);
           
        }
        [Fact(Timeout = 10000, DisplayName = "Expanded and disabled False with Hierachial data source")]
        public void ExpandedNodes_DisabledFalse()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");


            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");

        }
        [Fact(Timeout = 10000, DisplayName = "Selected and disabled true with Hierachial data source (without Multiselection)")]
        public void SelectedNodes_Disabled()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");

            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);
        }
        [Fact(Timeout = 10000, DisplayName = "Selected and disabled false with Hierachial data source (without Multiselection)")]
        public void SelectedNodes_DisabledFalse()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");


            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");
        }

        [Fact(Timeout = 10000, DisplayName = "Disabled true and Checked Nodes with Hierachial data source")]
        public void CheckedNodes_DisabledTrue()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);
        }
        [Fact(Timeout = 10000, DisplayName = "Disabled true and Checked Nodes with Hierachial data source")]
        public void CheckedNodes_DisabledFalse()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.Disabled, false).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }
            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");
        }
        
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes and id with Hierachial data source")]
        public void CheckedNodes_id()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { }));
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "Checked nodes property is working properly after dyanmic update");
            }

            
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);
        }
        [Fact(Timeout = 10000, DisplayName = "Selected and id with Hierachial data source (without Multiselection)")]
        public void SelectedNodes_id()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("AllowMultiSelection", true));
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "EU", "SA" }));
            selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI generated properly with multiselection");
            dataUid = treeEle.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI generated properly with multiselection");

            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded and id with Hierachial data source")]
        public void ExpandedNodes_id()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");

            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);
        }

    // // RTL , AutoCheck and CheckedNodes combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and CheckedNodes with Hierachial data source")]
        public void RTL_autocheck_CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p =>p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }

            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

        }

        // // RTL , AutoCheck and CssClass combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and CssClass with Hierachial data source")]
        public void RTL_autocheck_CssClass()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ShowCheckBox, true).Add(p => p.CssClass, "custom").Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property working properly initial case");  // // CssClass validation

        }

        // // RTL , AutoCheck and Disabled combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and Disabled with Hierachial data source")]
        public void RTL_autocheck_Disabled()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ShowCheckBox, true).Add(p => p.Disabled, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");  // // Disabled validation

        }

        // // RTL , AutoCheck and FullRowSelect combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and FullRowSelect with Hierachial data source")]
        public void RTL_autocheck_FullRowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ShowCheckBox, true).Add(p => p.FullRowSelect, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);                                               // // FullRowSelect Validation
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

        }

        // // RTL , AutoCheck and ID combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and ID with Hierachial data source")]
        public void RTL_autocheck_ID()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ShowCheckBox, true).Add(p => p.ID, "tree").Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.Find("#tree");

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

            var treeload = treeview.Instance.ID;                                              // // ID Validation
            Assert.Equal("tree",treeload);

        }

        // // RTL , AutoCheck and LoadOnDemand combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and LoadOnDemand with Hierachial data source")]
        public void RTL_autocheck_LoadOnDemand()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ShowCheckBox, true).Add(p => p.LoadOnDemand, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

            Assert.True(10 == liLength, "Number of li elements are not generated properly");                    // // LoadOndemand Validation  

        }

        // // RTL , AutoCheck and Navigationurl combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and Navigationurl with Hierachial data source")]
        public void RTL_autocheck_Navigationurl()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ShowCheckBox, true).Add(p => p.FullRowNavigable, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

            treeview.Find(".e-navigable");                                                                      // // Fullrow Navigable validation
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }                     

        }

        // // RTL , AutoCheck and Persistence combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and Persistence with Hierachial data source")]
        public void RTL_autocheck_Persistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }

            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");
        }

        // // RTL , AutoCheck and Showcheckbox combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and Showcheckbox with Hierachial data source")]
        public void RTL_autocheck_Showcheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)                                                                  // // Autocheck Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "AutoCheck property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "AutoCheck property is working properly");
                }
            }


            for (var i = 0; i < liLength; i++)                                                                  // // Showcheckbox Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }
            
        }

        // // RTL , AutoCheck and Selected combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and Selected with Hierachial data source")]
        public void RTL_autocheck_Selected()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

        }

        // // RTL , AutoCheck and SortOrder combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and SortOrder with Hierachial data source")]
        public void RTL_autocheck_SortOrder_Ascending()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            for (var i = 0; i < liElements.Count; i++)                                                           // // Sort Order Ascending   
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }

        }


        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and SortOrder-Descending with Hierachial data source")]
        public void RTL_autocheck_SortOrder_Descending()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Descending).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            for (var i = 0; i < liElements.Count; i++)                                                           // // SortOrder Descending
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and SortOrder-None with Hierachial data source")]
        public void RTL_autocheck_SortOrder_None()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.None).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            for (var i = 0; i < liElements.Count; i++)                                                           // // SortOrder None
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None sorting property is working properly");
            }

        }

        // // RTL and Autocheck, Expanded Combinations

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and Expanded with Hierachial data source")]
        public void RTL_autocheck_Expanded()
        {
            var data = GenerateTreeData();
            
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.AutoCheck, true).Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;    // // Expanded nodes
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");

        }

        // // RTL , CheckedNodes and CssClass combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck and CheckedNodes with Hierachial data source")]
        public void RTL_CheckedNodes_CssClass()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.CssClass, "custom").Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property working properly initial case");  // // CssClass validation

            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

        }

        // // RTL , CheckedNodes and Disabled combination

        [Fact(Timeout = 10000, DisplayName = "RTL and CheckedNodes and Disabled with Hierachial data source")]
        public void RTL_CheckedNodes_Disabled()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.Disabled, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");  // // Diasbled validation

            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

        }

        // // RTL , CheckedNodes and FullrowSelect combination

        [Fact(Timeout = 10000, DisplayName = "RTL and CheckedNodes and FullrowSelect with Hierachial data source")]
        public void RTL_CheckedNodes_FullrowSelect()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.FullRowSelect, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);                                               // // FullrowSelect validation
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }

            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

        }

        // // RTL , CheckedNodes and ID combination

        [Fact(Timeout = 10000, DisplayName = "RTL and CheckedNodes and ID with Hierachial data source")]
        public void RTL_CheckedNodes_ID()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.ID, "tree").Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.Find("#tree");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            var treeload = treeview.Instance.ID;                                               // // ID validation
            Assert.Equal("tree",treeload);

            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

        }

        // // RTL , CheckedNodes and LoadOnDemand combination

        [Fact(Timeout = 10000, DisplayName = "RTL and CheckedNodes and LoadOnDemand with Hierachial data source")]
        public void RTL_CheckedNodes_LoadOnDemand()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.LoadOnDemand, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            Assert.True(10 == liLength, "Number of li elements are not generated properly");                     // // Loadondemand Validation

            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

        }

        // // RTL , CheckedNodes and Navigationurl combination

        [Fact(Timeout = 10000, DisplayName = "RTL and CheckedNodes and Navigationurl with Hierachial data source")]
        public void RTL_CheckedNodes_Navigationurl()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.FullRowNavigable, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);                                               // // Fullrownavigable validation 
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }                  

            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

        }

        // // RTL , CheckedNodes and Persistence combination

        [Fact(Timeout = 10000, DisplayName = "RTL and CheckedNodes and Persistence with Hierachial data source")]
        public void RTL_CheckedNodes_Persistence()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation
          

            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");    // // Persistence
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<TreeData>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

        }

        // // RTL , CheckedNodes and Showcheckbox combination

        [Fact(Timeout = 10000, DisplayName = "RTL and CheckedNodes and Showcheckbox with Hierachial data source")]
        public void RTL_CheckedNodes_Showcheckbox()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;

            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation


            for (var i = 0; i < liLength; i++)                                                                  // // CheckedNodes Validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper .e-check");
                if (i < 5)
                {
                    Assert.True(checkboEle != null, "Checked nodes property is working properly");
                }
                else
                {
                    Assert.True(checkboEle == null, "Checked nodes property is working properly");
                }
            }

            for (var i = 0; i < liLength; i++)                                                                    // // Showcheckbox validation
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-checkbox-wrapper");
                Assert.True(checkboEle != null, "Show checkbox property is working properly");
            }

        }

        // // RTL , Checkednodes and Selected combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Checkednodes and Selected with Hierachial data source")]
        public void RTL_Checkednodes_Selected()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");    // // Selected Validation
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

        }

        // // RTL , Checkednodes and SortOrder combination

        [Fact(Timeout = 10000, DisplayName = "RTL and Checkednodes and SortOrder with Hierachial data source")]
        public void RTL_Checkednodes_SortOrder_Ascending()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            for (var i = 0; i < liElements.Count; i++)                                                           // // Sort Order Ascending   
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(ascendingData[i] == textValue, "Ascending sorting property is working properly");
            }

        }


        [Fact(Timeout = 10000, DisplayName = "RTL and Checkednodes and SortOrder-Descending with Hierachial data source")]
        public void RTL_Checkednodes_SortOrder_Descending()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Descending).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            for (var i = 0; i < liElements.Count; i++)                                                           // // SortOrder Descending
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(descendingData[i] == textValue, "Descending sorting property is working properly");
            }

        }

        [Fact(Timeout = 10000, DisplayName = "RTL and Checkednodes and SortOrder-None with Hierachial data source")]
        public void RTL_Checkednodes_SortOrder_None()
        {
            var data = GenerateTreeData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.SortOrder, Blazor.Navigations.SortOrder.None).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            for (var i = 0; i < liElements.Count; i++)                                                           // // SortOrder None
            {
                var textValue = liElements[i].QuerySelector(".e-list-text").TextContent.Trim();
                Assert.True(NoneData[i] == textValue, "None sorting property is working properly");
            }

        }

        // // RTL and Checkednodes, Expanded Combinations

        [Fact(Timeout = 10000, DisplayName = "RTL and Checkednodes and Expanded with Hierachial data source")]
        public void RTL_Checkednodes_Expanded()
        {
            var data = GenerateTreeData();

            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;


            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property working properly initial case");     // // RTL Validation

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;    // // Expanded nodes
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(3 == ulLength, "Number of ul elements are generated properly");
            Assert.True(13 == liLength, "Number of li elements are generated properly");

        }


        [Fact(Timeout = 10000, DisplayName = "Created event test case")]
        public void CreatedEventTest()
        {
            var createdEventCount = 0;
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<TreeData>>(events => events.Add(e => e.Created, (ActionEventArgs args) =>
            {
                createdEventCount++;
                Assert.NotNull("Create event is triggered, when render the component");
                Assert.Equal(1, createdEventCount);
                Assert.True(args.Name == "Created");
            })));
        }

        [Fact(Timeout = 10000, DisplayName = "Destroyed event test case")]
        public void DestroyedEventTest()
        {
            var destroyedEventCount = 0;
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Child, "Child")));
            {
                destroyedEventCount++;
                Assert.NotNull("Destroy event is triggered, when render the component");
                Assert.Equal(1, destroyedEventCount);
            };
        }
		 [Fact(Timeout = 10000, DisplayName = "TreeView EnsureVisible method")]
        public async Task TreeView_EnsureVisible()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(s => s.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");
            await treeview.Instance.EnsureVisibleAsync("2");
           
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView GetNode method")]
        public void  TreeView_GetNode()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(s => s.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");
            treeview.Instance.GetNode("2");
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView GetTreeData method")]
        public void TreeView_GetTreeData()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(s => s.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");
            Listdata treeData  = treeview.Instance.GetTreeData("2")[0];
            Assert.True(treeData.IsCheckedValue == true, "Checked nodes updated properly in GetTreeData method");
            treeview.SetParametersAndRender(("CheckedNodes", null));
            Assert.True(treeData.IsCheckedValue == true, "Checked nodes updated properly in GetTreeData method");
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView RemoveNodes method")]
        public void TreeView_RemoveNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(s => s.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");
            treeview.Instance.GetNode("2");
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView UpdateData method")]
        public async void TreeView_UpdateData()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");
            await treeview.InvokeAsync(() =>
            {
               treeview.Instance.UpdateData(ListDataSource);
            });
  
        }
		 [Fact(Timeout = 10000, DisplayName = "TreeView OnAfterScriptRendered method")]
        public async void TreeView_OnAfterScriptRendered()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            //await treeview.InvokeAsync(() =>
            //{
            //    treeview.Instance.OnAfterScriptRendered();
            //});
        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes not updated properly while using await method in Bind the datasource")]
        public async void CheckedNodesUpdateAwaitDataBinding()
        {
            TestVm vm = new TestVm();
            await vm.Load();
            var treeview = RenderComponent<SfTreeView<MailItem>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.AutoCheck, true).AddChildContent<TreeViewFieldsSettings<MailItem>>(field => field.Add(p => p.DataSource, vm.MyFolder).Add(p => p.Id, "Id").Add(p => p.Text, "FolderName").Add(p => p.Expanded, "Expanded").Add(p => p.IsChecked, "IsChecked").Add(p => p.Child, "SubFolders")));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.InvokeAsync(() =>
            {
                treeview.Instance.UpdateData(vm.MyFolder, true);
            }).ContinueWith(async (t) =>
            {
                var treeEle = treeview.Find(".e-treeview");
                var checkedLiElements = treeEle.QuerySelectorAll("li .e-checkbox-wrapper span.e-check");
                var CheckedNodes = checkedLiElements.Length;
                Assert.True(3 == CheckedNodes, "Checked nodes updated properly in UI");
                Assert.True(treeview.Instance.CheckedNodes.Length == CheckedNodes, "Checked nodes updated properly in CheckedNodes Property of TreeView");
            });
        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes with Dynamic Binding")]
        public void CheckedNodesDynamic()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.IsChecked, "Checked").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            Assert.True(2 == ulLength, "Number of ul elements are generated properly");
            Assert.True(10 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(2 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(10 == liLength, "Number of li elements are generated properly after dynamic update");
        }
        [Fact(Timeout = 10000, DisplayName = "Checked Nodes with Dynamic Datasource update Binding")]
        public async void CheckedNodesDynamicDataBinding()
        {
            var data = GeneratePermissionGroupData();
            var treeview = RenderComponent<SfTreeView<PermissionGroup>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.AutoCheck, true).Add(p => p.AllowEditing, false).AddChildContent<TreeViewFieldsSettings<PermissionGroup>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "IsAllowed").Add(p => p.IsChecked, "IsAllowed").Add(p => p.Child, "PermissionGroups")));
            var treeEle = treeview.Find(".e-treeview");
            var checkedLiElements = treeEle.QuerySelectorAll("li .e-checkbox-wrapper span.e-check");
            var CheckedNodes = checkedLiElements.Length;
            Assert.True(2 == CheckedNodes, "Checked nodes updated properly in UI");
            Assert.True(treeview.Instance.CheckedNodes.Length == CheckedNodes, "Checked nodes updated properly in CheckedNodes Property of TreeView");
            await treeview.InvokeAsync(() =>
            {
                treeview.Instance.ClearStateAsync();
                var ListDataSource = GeneratePermissionGroupData1();
                treeview.Instance.UpdateData(ListDataSource, true);
            }).ContinueWith(async (t) =>
            {
                var treeEle2 = treeview.Find(".e-treeview");
                var checkedLiElements2 = treeEle2.QuerySelectorAll("li .e-checkbox-wrapper span.e-check");
                var CheckedNodes2 = checkedLiElements2.Length;
                Assert.True(1 == CheckedNodes, "Checked nodes updated properly in UI");
                Assert.True(treeview.Instance.CheckedNodes.Length == 2, "Checked nodes updated properly in CheckedNodes Property of TreeView");
            });
            await treeview.InvokeAsync(() =>
            {
                treeview.Instance.ClearStateAsync();
                var ListDataSource1 = GeneratePermissionGroupData2();
                treeview.Instance.UpdateData(ListDataSource1, true);
            }).ContinueWith(async (t) =>
            {
                var treeEle1 = treeview.Find(".e-treeview");
                var checkedLiElements1 = treeEle1.QuerySelectorAll("li .e-checkbox-wrapper span.e-check");
                var CheckedNodes1 = checkedLiElements1.Length;
                Assert.True(0 == CheckedNodes, "Checked nodes updated properly in UI");
                Assert.True(treeview.Instance.CheckedNodes.Length == 0, "Checked nodes updated properly in CheckedNodes Property of TreeView");
            });
        }
		
		[Fact(Timeout = 10000, DisplayName = "AllowWrapText with Hierachial data source")]
        public void AllowWrapTextFeature()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.AllowTextWrap, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-text-wrap");
                Assert.False(checkboEle != null, "AllowTextwrap property is working properly");
            }
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-text-wrap");
                Assert.True(checkboEle == null, "AllowTextWrap property is working properly after dyanmic update");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView OnAfterScriptRendered with CheckedNodes method")]
        public async void TreeView_OnAfterScriptRenderedWithCheckedNoded()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var liElements = treeview.FindAll("li");
            //treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.SetParametersAndRender(("CheckedNodes", new string[] { "NA" }));
            
            //await treeview.InvokeAsync(() =>
            //{
            //    treeview.Instance.OnAfterScriptRendered();
            //});
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView Animation testing")]
        public async void Tree_Animation()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewNodeAnimationSettings>(t => t.AddChildContent<TreeViewAnimationExpand>(a => a.Add(ex => ex.Duration, 1000).Add(ex => ex.Effect, AnimationEffect.FadeOut).Add(ex => ex.Easing, "linear"))
            .AddChildContent<TreeViewAnimationCollapse>(a => a.Add(c => c.Duration, 1000).Add(ex => ex.Effect, AnimationEffect.FadeIn).Add(ex => ex.Easing, "linear"))));
            var treeEle = treeview.Find(".e-treeview");
            //Assert.NotNull(treeview.Instance.AnimationSettings);
            //Assert.NotNull(treeview.Instance.AnimationSettings.NodeAnimationExpand);
            //Assert.NotNull(treeview.Instance.AnimationSettings.NodeAnimationCollapse);
            //Assert.Equal(1000, treeview.Instance.AnimationSettings.NodeAnimationExpand.Duration);
            //Assert.Equal(1000, treeview.Instance.AnimationSettings.NodeAnimationCollapse.Duration);
            //Assert.Equal(AnimationEffect.FadeOut, treeview.Instance.AnimationSettings.NodeAnimationExpand.Effect);
            //Assert.Equal(AnimationEffect.FadeIn, treeview.Instance.AnimationSettings.NodeAnimationCollapse.Effect);
            //Assert.Equal("linear", treeview.Instance.AnimationSettings.NodeAnimationExpand.Easing);
            //Assert.Equal("linear", treeview.Instance.AnimationSettings.NodeAnimationCollapse.Easing);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView Destroyed event testing")]
        public async void Tree_Destroyed()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.Destroyed, (ActionEventArgs args) =>
            {
                Assert.Null(args);
            })));
            var treeEle = treeview.Find(".e-treeview");
            treeview.Instance.Dispose();
        }
        //[Fact(Timeout = 10000, DisplayName = "TreeView keypress event testing")]
        //public async void Tree_keypress()
        //{
        //    var data = GenerateListData();
        //    NodeKeyPressEventArgs eventArgs = new NodeKeyPressEventArgs() { 
        //        Name = "OnKeyPress",
        //        NodeData = new NodeData() { Id = "1", Text = "Australia" },
        //        Event = null,
        //        Action = "Enter",
        //        Cancel = false,
        //        Key = "Enter"
        //    };
        //    var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
        //    .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.OnKeyPress, (NodeKeyPressEventArgs args) =>
        //    {
        //        Assert.NotNull(args);
        //        Assert.True(args.Name == "OnKeyPress");
        //        Assert.True(args.NodeData.Id == "1");
        //        Assert.True(args.NodeData.Text == "Australia");
        //        Assert.Null(args.Event);
        //        Assert.True(args.Action == "Enter");
        //        Assert.False(args.Cancel);
        //        Assert.Equal("Enter", args.Key);
        //    })));
        //    var treeEle = treeview.Find(".e-treeview");
        //    await treeview.Instance.TriggerKeyboardEvent(eventArgs, "1", "Enter", "Enter");
        //}
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeChecked event testing")]
        public async void Tree_NodeChecked()
        {
            var data = GenerateListData();
            NodeCheckEventArgs eventArgs = new NodeCheckEventArgs()
            {
                Action = "check",
                Cancel = false,
                NodeData = new NodeData() { Id = "1", Text = "Australia" },
                IsInteracted = true,
                Name = "NodeChecked",
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p=> p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeChecked, (NodeCheckEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeChecked");
                Assert.True(args.NodeData.Id == "1");
                Assert.True(args.NodeData.Text == "Australia");
                Assert.True(args.NodeData.HasChildren);
                Assert.True(args.Action == "check");
                Assert.False(args.Cancel);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeCheckingEvent(eventArgs);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeChecking event testing")]
        public async void Tree_NodeChecking()
        {
            var data = GenerateListData();
            NodeCheckEventArgs eventArgs = new NodeCheckEventArgs()
            {
                Action = "check",
                Cancel = false,
                NodeData = new NodeData() { Id = "1", Text = "Australia" },
                IsInteracted = true,
                Name = "NodeChecking",
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeChecking, (NodeCheckEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeChecking");
                Assert.True(args.NodeData.Id == "1");
                Assert.True(args.NodeData.Text == "Australia");
                Assert.True(args.Action == "check");
                Assert.False(args.Cancel);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeCheckingEvent(eventArgs);
        }
        
        [Fact(Timeout = 10000, DisplayName = "TreeView ExpandoObject event testing")]
        public async void Tree_ExpandoObject()
        {
            List<ExpandoObject> data = GetData().ToList();
            NodeCheckEventArgs eventArgs = new NodeCheckEventArgs()
            {
                Action = "check",
                Cancel = false,
                NodeData = new NodeData() { Id = "1", Text = "Parent 1" },
                IsInteracted = true,
                Name = "NodeChecking",
            };
            var treeview = RenderComponent<SfTreeView<ExpandoObject>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<ExpandoObject>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "ParentID").Add(p => p.Id, "ID").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "ChildRecordID"))
            );
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeCheckingEvent(eventArgs);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeCollapsed event testing")]
        public async void Tree_NodeCollapsed()
        {
            var data = GenerateListData();
            NodeExpandEventArgs eventArgs = new NodeExpandEventArgs()
            {
                Cancel = false,
                NodeData = new NodeData() { Id = "1", Text = "Australia" },
                IsInteracted = true,
                Event = new ClickEventArgs(),

            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeCollapsed, (NodeExpandEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeCollapsed");
                Assert.True(args.NodeData.Id == "1");
                Assert.True(args.NodeData.Text == "Australia");
                Assert.False(args.Cancel);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeCollapsedEvent(eventArgs);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView NodeCollapsing event testing")]
        public async void Tree_NodeCollapsing()
        {
            var data = GenerateListData();
            NodeExpandEventArgs eventArgs = new NodeExpandEventArgs()
            {
                Cancel = false,
                NodeData = new NodeData() { Id = "1", Text = "Australia" },
                IsInteracted = true,
                Event = new ClickEventArgs(),
                Name = "NodeCollapsing"
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeCollapsing, (NodeExpandEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeCollapsing");
                Assert.True(args.NodeData.Id == "1");
                Assert.True(args.NodeData.Text == "Australia");
                Assert.False(args.Cancel);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.NodeCollapsingEventCallback(eventArgs);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeExpanded event testing")]
        public async void Tree_NodeExpanded()
        {
            var data = GenerateListData();
            NodeExpandEventArgs eventArgs = new NodeExpandEventArgs()
            {
                Cancel = false,
                NodeData = new NodeData() { Id = "1", Text = "Australia" },
                IsInteracted = true,
                Event = new ClickEventArgs(),
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeExpanded, (NodeExpandEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeExpanded");
                Assert.True(args.NodeData.Id == "1");
                Assert.True(args.NodeData.Text == "Australia");
                Assert.False(args.Cancel);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeExpandedEvent(eventArgs);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeExpanding event testing")]
        public async void Tree_NodeExpanding()
        {
            var data = GenerateListData();
            Blazor.Navigations.Internal.ExpandEventArgs eventArgs = new Blazor.Navigations.Internal.ExpandEventArgs()
            {
                Cancel = false,
                NodeData = new NodeData() { Id = "1", Text = "Australia" },
                IsInteracted = true,
                Event = new ClickEventArgs(),
                IsLoaded = true,
                NodeLevel = 1
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeExpanding, (NodeExpandEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeExpanding");
                Assert.True(args.NodeData.Id == "1");
                Assert.True(args.NodeData.Text == "Australia");
                Assert.False(args.Cancel);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeExpandingEvent(eventArgs);
        }
        //[Fact(Timeout = 10000, DisplayName = "TreeView NodeClicked event testing")]
        //public async void Tree_NodeClicked()
        //{
        //    var data = GenerateListData();
        //    ClickEventArgs eventArgs = new ClickEventArgs()
        //    {
        //        Cancel = false,
        //        Name = "NodeClicked",
        //        OriginalEvent=null,
        //        Item=null
        //    };
        //    MouseEventArgs mouseEventArgs = new MouseEventArgs();
        //    var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ExpandOn, ExpandAction.Click).Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
        //    .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeClicked, (NodeClickEventArgs args) =>
        //    {
        //        Assert.NotNull(args);
        //        Assert.True(args.NodeData.Id == "1");
        //        Assert.True(args.Name == "NodeClicked");
        //        Assert.Equal(25, args.Left);
        //        Assert.Equal(20, args.Top);
        //        Assert.NotNull(args.Event);
        //    })));
        //    var treeEle = treeview.Find(".e-treeview");
        //    await treeview.Instance.TriggerNodeClickingEvent(eventArgs, mouseEventArgs, "1", 25, 20);
        //}

        [Fact(Timeout = 10000, DisplayName = "TreeView RefreshNode testing")]
        public async void RefreshNode()
        {
            var data = GenerateListData();
            var newData = new List<Listdata>() {
                new Listdata{ Id= "1", Name = "new Australia", Expanded = true, HasChild = true }
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.Instance.RefreshNodeAsync("1", newData);
            Assert.NotNull(newData);
            //Assert.True(treeview.Instance.TreeViewFields.DataSource.Count() == 23);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView OnNodeDragStart event testing")]
        public async void Tree_OnNodeDragStart()
        {
            var data = GenerateListData();
            DragAndDropEventArgs eventArgs = new DragAndDropEventArgs()
            {
                Cancel = false,
                DraggedNodeData = new NodeData() { Id = "1", Text = "Australia" },
                DropIndex = 1,
                Name = "OnNodeDragStart"
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.AllowDragAndDrop, true).Add(p=> p.DropArea, ".container").Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.OnNodeDragStart, (DragAndDropEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "OnNodeDragStart");
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerDragStartEvent(eventArgs, 25, 20);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView OnNodeDragStop event testing")]
        public async void Tree_OnNodeDragStop()
        {
            var data = GenerateListData();
            DragAndDropEventArgs eventArgs = new DragAndDropEventArgs()
            {
                Cancel = false,
                DraggedNodeData = new NodeData() { Id = "1", Text = "Australia" },
                DropIndex = 1,
                Name = "OnNodeDragStop"
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.AllowDragAndDrop, true).Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.OnNodeDragStop, (DragAndDropEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "OnNodeDragStop");
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerDragStopEvent(eventArgs, 25, 20, null);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView OnNodeDragged event testing")]
        public async void Tree_OnNodeDragged()
        {
            var data = GenerateListData();
            DragAndDropEventArgs eventArgs = new DragAndDropEventArgs()
            {
                Cancel = false,
                DraggedNodeData = new NodeData() { Id = "1", Text = "Australia" },
                DropIndex = 1,
                Name = "OnNodeDragged"
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.AllowDragAndDrop, true).Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.OnNodeDragged, (DragAndDropEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "OnNodeDragged");
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeDraggingEvent(eventArgs, 25, 20);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeDropped event testing")]
        public async void Tree_NodeDropped()
        {
            var data = GenerateListData();
            DragAndDropEventArgs eventArgs = new DragAndDropEventArgs()
            {
                Cancel = false,
                DraggedNodeData = new NodeData() { Id = "1", Text = "Australia" },
                DroppedNodeData = new NodeData() { Id = "2", Text = "South Wales" },
                DropIndex = 1,
                DropIndicator = "In",
                DropLevel = 1,
                //Event = null,
                PreventTargetExpand = false,
                Name = "NodeDropped"
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.AllowDragAndDrop, true).Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeDropped, (DragAndDropEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeDropped");
                Assert.Equal(25, args.Left);
                Assert.Equal(20, args.Top);
                Assert.False(args.PreventTargetExpand);
                Assert.Null(args.Event);
                Assert.True(args.DropIndicator == "In");
                Assert.True(args.DropLevel == 1);
                Assert.Equal(1, args.DropIndex);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeDropped(eventArgs, 25, 20);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView DropNodeAsSibling testing")]
        public async void Tree_DropNodeAsSibling()
        {
            var data = GenerateListData();
            DropTreeArgs eventArgs = new DropTreeArgs()
            {
                DragLi = "2",
                DropLi = "7",
                DropParentLi = "7",
                DragParentLi = "1",
                IsExternalDrag = false
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.AllowDragAndDrop, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeDropped, (DragAndDropEventArgs args) =>
            {
                Assert.NotNull(args);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.DropNodeAsSibling(eventArgs);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView DropNodeAsChild testing")]
        public async void Tree_DropNodeAsChild()
        {
            var data = GenerateListData();
            DropTreeArgs eventArgs = new DropTreeArgs()
            {
                DragLi = "2",
                DropLi = "7",
                DropParentLi = "7",
                DragParentLi = "1",
                IsExternalDrag = false
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p=> p.AllowDragAndDrop, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeDropped, (DragAndDropEventArgs args) =>
            {
                Assert.NotNull(args);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.DropNodeAsChild(eventArgs);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView NodeEditing event testing")]
        public async void Tree_NodeEditing()
        {
            var data = GenerateListData();
            NodeEditEventArgs eventArgs = new NodeEditEventArgs()
            {
                Cancel = false,
                InnerHtml = "Australia",
                NewText = "NewAutsralia",
                NodeData = new NodeData() { Id = "1", Text = "Australia" },
                //OldText = "Australia",
                Name = "NodeEditing"
            };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeEditing, (NodeEditEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeEditing");
                //Assert.Equal("Australia", args.InnerHtml);
            })));
            var treeEle = treeview.Find(".e-treeview");
            await treeview.Instance.TriggerNodeEditingEvent(eventArgs);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeEdited event testing")]
        public async void Tree_NodeEdited()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.AllowEditing, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeEdited, (NodeEditEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeEdited");
            })));
            var treeEle = treeview.Find(".e-treeview");
            //await treeview.Instance.TriggerNodeEditedEvent("NewAutsralia");
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView DataBound event testing")]
        public async void Tree_DataBound()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.DataBound, (DataBoundEventArgs<Listdata> args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "DataBound");
            })));
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView DataSourceChanged event testing")]
        public async void Tree_DataSourceChanged()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child").Add(p => p.DataSourceChanged, (IEnumerable<Listdata> data) =>
            {
                Assert.NotNull(data);
            }))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.DataSourceChanged, (DataSourceChangedEventArgs<Listdata> args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "DataSourceChanged");
            })));
            var treeEle = treeview.Find(".e-treeview");
            treeview.SetParametersAndRender(parameters =>
              parameters.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, NewListData())));
            //Assert.True(treeview.Instance.TreeViewFields.DataSource.Count() == 2);
            //treeview.Instance.TriggerDataSourceChangedEvent();
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView OnActionFailure event testing")]
        public async void Tree_OnActionFailure()
        {
            try
            {
                var data = GenerateListData();
                var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.ParentID, "Pid").Add(p => p.Id, "EmployeeID").Add(p => p.Text, "FirstName").Add(p => p.HasChildren, "EmployeeID")
                .AddChildContent<SfDataManager>(a => a.Add(p => p.Adaptor, Adaptors.ODataAdaptor).Add(p => p.Url, "https://services.odata.org/V3/Northwind/Northwind.svc")))
                .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.OnActionFailure, (FailureEventArgs args) =>
                {
                    Assert.NotNull(args);
                    Assert.NotNull(args.Error);
                    Assert.Contains("HttpClient to be supplied", args.Error.Message);
                    Assert.True(args.Name == "OnActionFailure");
                })));
            }
            catch (Exception ex)
            {
                return;
                throw;
            }
            
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView OnNodeRender event testing")]
        public async void Tree_OnNodeRender()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.OnNodeRender, (NodeRenderEventArgs<Listdata> args) =>
            {
                Assert.NotNull(args);
                Assert.NotNull(args.Node);
                Assert.NotNull(args.NodeData);
                Assert.NotEmpty(args.Text);
                Assert.True(args.Name == "OnNodeRender");
            })));
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeSelected event testing")]
        public async void Tree_NodeSelected()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeSelected, (NodeSelectEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeUnSelected");
                Assert.Null(args.Action);
                Assert.False(args.Cancel);
                Assert.True(args.IsInteracted);
                Assert.NotNull(args.NodeData);
            })));
            var treeEle = treeview.Find(".e-treeview");
            SelectionEventArgs eventArgs = new SelectionEventArgs()
            { 
                IsMultiSelect = true, IsCtrKey = false, IsShiftKey = false, Nodes = null, NodeData = new NodeData() { Id = "2" }, IsInteracted = true
            };
            NodeSelectEventArgs args = new NodeSelectEventArgs() { Cancel = false };
            await treeview.Instance.TriggerNodeSelectingEvent(eventArgs);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView NodeSelecting event testing")]
        public async void Tree_NodeSelecting()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child"))
            .AddChildContent<TreeViewEvents<Listdata>>(events => events.Add(e => e.NodeSelecting, (NodeSelectEventArgs args) =>
            {
                Assert.NotNull(args);
                Assert.True(args.Name == "NodeUnSelecting");
                Assert.Null(args.Action);
                Assert.False(args.Cancel);
                Assert.False(args.IsInteracted);
                Assert.NotNull(args.NodeData);
            })));
            var treeEle = treeview.Find(".e-treeview");
            SelectionEventArgs eventArgs = new SelectionEventArgs()
            {
                IsMultiSelect = true,
                IsCtrKey = false,
                IsShiftKey = false,
                Nodes = null,
                NodeData = new NodeData() { Id = "1" }
            };
            await treeview.Instance.TriggerNodeSelectingEvent(eventArgs);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView TreeViewFieldChild testing")]
        public async void Tree_TreeViewFieldChild()
        {
            try
            {
                Query Query = new Query().From("Employees").Select(new List<string> { "EmployeeID", "FirstName" }).Take(3).RequiresCount();
                Query SubQuery = new Query().From("Orders").Select(new List<string> { "OrderID", "EmployeeID", "ShipName" }).Take(2).RequiresCount();
                string Url = "https://services.odata.org/V3/Northwind/Northwind.svc";
                var treeview = RenderComponent<SfTreeView<RemoteTreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<RemoteTreeData>>(
                    field => field.Add(p => p.Query, Query).Add(p => p.Id, "EmployeeID").Add(p => p.Text, "FirstName").Add(p => p.HasChildren, "EmployeeID")
                        .AddChildContent<DataManager>(a => a.Add(p => p.Adaptor, Adaptors.ODataAdaptor).Add(p => p.Url, Url).Add(p => p.CrossDomain, true))
                        .AddChildContent<TreeViewFieldChild<RemoteTreeData>>(
                            c => c.Add(p => p.Query, SubQuery).Add(p => p.Id, "OrderID").Add(p => p.Text, "ShipName").Add(p => p.ParentID, "EmployeeID")
                            .AddChildContent<DataManager>(b => b.Add(d => d.Adaptor, Adaptors.ODataAdaptor).Add(d => d.Url, Url).Add(d => d.CrossDomain, true))
                         ))
                    .AddChildContent<TreeViewEvents<RemoteTreeData>>(events => events.Add(e => e.OnActionFailure, (FailureEventArgs args) =>
                    {
                        Assert.NotNull(args);
                    })));
            }
            catch (Exception ex)
            {
                Assert.Contains("Data operation failed", ex.Message);
                return;
                throw;
            }
        }
        [Fact(Timeout = 10000, DisplayName = "TreeView PreventRender testing")]
        public void PreventRender()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.Instance.PreventRender();
        }

        [Fact(Timeout = 10000, DisplayName = "Created event method testing")]
        public async void CreatedEvent()
        {
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Child, "Child")));
            await treeview.Instance.CreatedEvent();
        }

        [Fact(Timeout = 10000, DisplayName = "Addnodes method testing")]
        public void AddNodes()
        {
            List<Listdata> newNodes = new List<Listdata>
            {
                new Listdata { Id = "111", Name = "new Australia" }
            };
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            treeview.Instance.AddNodes(newNodes, null);
        }

        [Fact(Timeout = 10000, DisplayName = "BeginEdit method testing")]
        public void BeginEdit()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.AllowEditing, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            treeview.Instance.BeginEditAsync("1");
            Assert.True(treeview.Instance.AllowEditing);
        }
        [Fact(Timeout = 10000, DisplayName = "CheckAll method testing")]
        public void CheckAll()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            treeview.Instance.CheckAllAsync();
            Assert.True(treeview.Instance.ShowCheckBox);
        }

        [Fact(Timeout = 10000, DisplayName = "ClearState method testing")]
        public void ClearState()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            treeview.Instance.ClearStateAsync();
            Assert.Null(treeview.Instance.CheckedNodes);
        }
        [Fact(Timeout = 10000, DisplayName = "CollapseAll method testing")]
        public async void CollapseAll()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            await treeview.Instance.CollapseAllAsync();
            Assert.Equal(0, treeview.Instance.ExpandedNodes.Length);
        }
        [Fact(Timeout = 10000, DisplayName = "ExpandAll method testing")]
        public async void ExpandAll()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            await treeview.Instance.ExpandAllAsync();
            Assert.Equal(5, treeview.Instance.ExpandedNodes.Length);
        }
        [Fact(Timeout = 10000, DisplayName = "DisableNodes method testing")]
        public async void DisableNodes()
        {
            var data = GenerateListData();
            string[] nodesToDisable = { "1", "2" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            await treeview.Instance.DisableNodesAsync(nodesToDisable);
            //Assert.Equal(2, treeview.Instance.AllDisabledNodes.Count());
            //var disabled = await treeview.Instance.GetDisabledNodesAsync();
            //Assert.Equal(2, disabled.Count);
        }
        [Fact(Timeout = 10000, DisplayName = "EnableNodes method testing")]
        public async void EnableNodes()
        {
            var data = GenerateListData();
            string[] nodesToEnable = { "1", "2" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            await treeview.Instance.EnableNodesAsync(nodesToEnable);
            //Assert.Equal(0, treeview.Instance.AllDisabledNodes.Count());
        }
        [Fact(Timeout = 10000, DisplayName = "RemoveNodes method testing")]
        public void RemoveNodes()
        {
            var data = GenerateListData();
            string[] nodesToRemove = { "2" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child")));
            treeview.Instance.RemoveNodes(nodesToRemove);
            //Assert.Equal(22, treeview.Instance.InternalData.Count());
        }
        [Fact(Timeout = 10000, DisplayName = "UncheckAll method ")]
        public async void UncheckAll()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var checkedItems = treeview.Instance.GetAllCheckedNodes(false);
            Assert.Equal(2, checkedItems.Count());
            await treeview.Instance.UncheckAllAsync();
            Assert.Null(treeview.Instance.CheckedNodes);
        }
        [Fact(Timeout = 10000, DisplayName = "TreeFields members")]
        public async void TreeFields()
        {
            var data = GenerateTreeData();
            TreeViewFieldOptions<TreeData> fields = new TreeViewFieldOptions<TreeData>()
            {
                HtmlAttributes = "HtmlAttr",
                IconCss = "IconCss",
                ImageUrl = "ImageUrl",
                TableName = "Table"
            };
            try
            {
                var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "").Add(p => p.Selected, "Selected").Add(p => p.Expanded, "Expanded").Add(p => p.Child, "Child")));
                var treeEle = treeview.Find(".e-treeview");
            }
            catch (Exception ex)
            {
                Assert.Contains("Text of TreeView cannot be empty.", ex.Message);
                return;
                throw;
            }
            Assert.Equal("HtmlAttr", fields.HtmlAttributes);
            Assert.Equal("IconCss", fields.IconCss);
            Assert.Equal("ImageUrl", fields.ImageUrl);
            Assert.Equal("Table", fields.TableName);
        }
        [Fact(Timeout = 10000, DisplayName = "CheckNodes update Binding")]
        public async void CheckNodes()
        {
            var data = GeneratePermissionGroupData();
            NodeCheckEventArgs eventArgs = new NodeCheckEventArgs()
            {
                Action = "check",
                Cancel = false,
                NodeData = new NodeData() { Id = "1", Text = "P1" },
                IsInteracted = true,
                Name = "NodeChecking",
            };
            var treeview = RenderComponent<SfTreeView<PermissionGroup>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.AutoCheck, true).Add(p => p.AllowEditing, false).AddChildContent<TreeViewFieldsSettings<PermissionGroup>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "IsAllowed").Add(p => p.IsChecked, "IsAllowed").Add(p => p.Child, "PermissionGroups")));
            //treeview.Instance.ListReference.DataType = TreeViewDataType.RemoteData;
            //var treeEle = treeview.Find(".e-treeview");
            //var checkedLiElements = treeEle.QuerySelectorAll("li .e-checkbox-wrapper span.e-check");
            //var CheckedNodes = checkedLiElements.Length;
            //Assert.True(2 == CheckedNodes, "Checked nodes updated properly in UI");
            //Assert.True(treeview.Instance.CheckedNodes.Length == CheckedNodes, "Checked nodes updated properly in CheckedNodes Property of TreeView");
            //treeview.Instance.TriggerNodeCheckingEvent(eventArgs);
            //await treeview.InvokeAsync(() =>
            //{
            //    treeview.Instance.ClearStateAsync();
            //    var ListDataSource = GeneratePermissionGroupData1();
            //    treeview.Instance.UpdateData(ListDataSource, true);
            //});
            //await treeview.InvokeAsync(() =>
            //{
            //    treeview.Instance.ClearStateAsync();
            //    var ListDataSource1 = GeneratePermissionGroupData2();
            //    treeview.Instance.UpdateData(ListDataSource1, true);
            //});
        }
        
        [Fact(Timeout = 10000, DisplayName = "CheckedNodes SelfReferentialData testing")]
        public void SelfReferentialData_CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child").Add(p=> p.IsChecked, "IsCheckedValue"))
            .Add(p => p.CheckedNodesChanged, (string[] args) =>
            {
                Assert.NotNull(args);
            }));
            var treeEle = treeview.Find(".e-treeview");
            //Assert.Equal(23, treeview.Instance.TreeViewFields.DataSource.Count());
            treeview.Instance.UpdateData(NewListData(), true);
        }

        [Fact(Timeout = 10000, DisplayName = "CheckedNodes HierarchicalData testing")]
        public void HierarchicalData_CheckedNodes()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.IsChecked, "Selected").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child"))
            .Add(p => p.CheckedNodesChanged, (string[] args) =>
            {
                Assert.NotNull(args);
            }));
            var treeEle = treeview.Find(".e-treeview");
            //Assert.Equal(7, treeview.Instance.TreeViewFields.DataSource.Count());
            treeview.Instance.UpdateData(NewTreeData(), true);
        }

        [Fact(Timeout = 10000, DisplayName = "SelectedNodes SelfReferential testing")]
        public void SelfReferential_SelectedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "1", "2" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child").Add(p => p.IsChecked, "IsCheckedValue"))
            .Add(p => p.SelectedNodesChanged, (string[] args) =>
            {
                Assert.NotNull(args);
            }));
            var treeEle = treeview.Find(".e-treeview");
            treeview.SetParametersAndRender(("SelectedNodes", new string[] { "3" }));
            Assert.Equal(1, treeview.Instance.SelectedNodes.Length);
        }

        [Fact(Timeout = 10000, DisplayName = "virtualization testing")]
        public void Virtualization()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.EnableVirtualization, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child").Add(p => p.IsChecked, "IsCheckedValue")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeview.Instance.EnableVirtualization);
            treeview.SetParametersAndRender(("EnableVirtualization", false));
        }
        [Fact(Timeout = 10000, DisplayName = "Height testing")]
        public void Height()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.Height, "300px").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.Selected, "Selected").Add(p => p.HasChildren, "HasChild").Add(p => p.Child, "Child").Add(p => p.IsChecked, "IsCheckedValue")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.Equal("300px", treeview.Instance.Height);
            treeview.SetParametersAndRender(("Height", "400px"));
        }
        [Fact(Timeout = 10000, DisplayName = "Multiselection false")]
        public void Multiselection_false()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p=> p.AllowMultiSelection, true).Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(2 == selectedLi);
            treeview.SetParametersAndRender(("AllowMultiSelection", false));
        }
        [Fact(Timeout = 10000, DisplayName = "EnablePersistence true")]
        public void EnablePersistence_true()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.Add(p => p.EnablePersistence, true).Add(p => p.ID, "tree-1").Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeview.Instance.EnablePersistence);
        }
        [Fact(Timeout = 10000, DisplayName = "UpdateExpandedNode method testing")]
        public void UpdateExpandedNode_method()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Code").Add(p => p.Text, "Name").Add(p => p.Selected, "Select").Add(p => p.Expanded, "Expand").Add(p => p.Child, "Child")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.Instance.UpdateExpandedNode(new string[] { "AS", "AF" });
            Assert.Equal(2, treeview.Instance.ExpandedNodes.Length);
        }
        [Fact(Timeout = 10000, DisplayName = "DataSourceChanged Event Test")]
        public void DataSourceChanged_ShouldTriggerEvent_WhenDataSourceChanges()
        {
            var component = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(fieldSettings => fieldSettings
                    .Add(p => p.DataSource, new List<TreeData> { new TreeData { Code = "NA", Name = "North America" } })
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.DataSourceChanged, args =>
                    {
                        Assert.NotNull(args);
                        Assert.Contains(args, item => item.Code == "NA" && item.Name == "North America");
                    })
                )
            );
            component.SetParametersAndRender(parameters => parameters
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(fieldSettings => fieldSettings
                    .Add(p => p.DataSource, new List<TreeData> { new TreeData { Code = "EU", Name = "Europe" } })
                )
            );
        }
        [Fact(Timeout = 10000, DisplayName = "Node Expanding Event for Already Loaded Nodes")]
        public async Task NodeExpanding_AlreadyLoadedNodes()
        {
            try
            {
                Query Query = new Query().From("Employees").Select(new List<string> { "EmployeeID", "FirstName" }).Take(3).RequiresCount();
                Query SubQuery = new Query().From("Orders").Select(new List<string> { "OrderID", "EmployeeID", "ShipName" }).Take(2).RequiresCount();
                string Url = "https://services.odata.org/V3/Northwind/Northwind.svc";
                var treeview = RenderComponent<SfTreeView<RemoteTreeData>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<RemoteTreeData>>(
                    field => field.Add(p => p.Query, Query).Add(p => p.Id, "EmployeeID").Add(p => p.Text, "FirstName").Add(p => p.HasChildren, "EmployeeID")
                        .AddChildContent<DataManager>(a => a.Add(p => p.Adaptor, Adaptors.ODataAdaptor).Add(p => p.Url, Url).Add(p => p.CrossDomain, true))
                        .AddChildContent<TreeViewFieldChild<RemoteTreeData>>(
                            c => c.Add(p => p.Query, SubQuery).Add(p => p.Id, "OrderID").Add(p => p.Text, "ShipName").Add(p => p.ParentID, "EmployeeID")
                            .AddChildContent<DataManager>(b => b.Add(d => d.Adaptor, Adaptors.ODataAdaptor).Add(d => d.Url, Url).Add(d => d.CrossDomain, true))
                         ))
                    .AddChildContent<TreeViewEvents<RemoteTreeData>>(events => events.Add(e => e.OnActionFailure, (FailureEventArgs args) =>
                    {
                        Assert.NotNull(args);
                    })));
                var nodeData = new NodeData { Id = "1", Text = "Nancy" };
                Blazor.Navigations.Internal.ExpandEventArgs eventArgs = new Blazor.Navigations.Internal.ExpandEventArgs()
                {
                    NodeData = nodeData,
                    IsLoaded = true
                };
                await treeview.Instance.TriggerNodeExpandingEvent(eventArgs);
                Assert.True(eventArgs.IsLoaded);
                Assert.Equal("NodeExpanding", eventArgs.Name);
            }
            catch (Exception ex)
            {
                Assert.Contains("Data operation failed", ex.Message);
                return;
                throw;
            }
        }
        [Fact(DisplayName = "Drag and Drop as Sibling for Hierarchical Data")]
        public async Task DropNodeAsSibling_HierarchicalData()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                .Add(p => p.AllowDragAndDrop, true)
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field
                    .Add(p => p.DataSource, data)
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Child, "Child")
                )
            );
            DropTreeArgs args = new DropTreeArgs
            {
                DragLi = "NA", 
                DropLi = "AS", 
                IsExternalDrag = false
            };
            await treeview.Instance.DropNodeAsSibling(args);
            //Assert.Contains("NA", treeview.Instance.ListReference.ItemsData.ToList().Select(n => n.Code));
            //Assert.True(treeview.Instance.InternalData.Any(item => item.Code == "NA"), "Dragged node should be part of the internal data.");
        }
        [Fact(DisplayName = "Drag and Drop as Child for Hierarchical Data")]
        public async Task DropNodeAsChild_HierarchicalData()
        {
            var data = GenerateTreeData();
            var treeview = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                .Add(p => p.AllowDragAndDrop, true)
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field
                    .Add(p => p.DataSource, data)
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Child, "Child")
                )
            );
            DropTreeArgs args = new DropTreeArgs
            {
                DragLi = "MEX", 
                DropLi = "NA",  
                IsExternalDrag = false
            };
            await treeview.Instance.DropNodeAsChild(args);
            //Assert.Contains("MEX", treeview.Instance.InternalData.FirstOrDefault(d => d.Code == "NA").Child.Select(c => c.Code));
        }
        [Fact(DisplayName = "Test Expand Event Args Properties")]
        public void TestExpandEventArgsProperties()
        {
            Blazor.Navigations.Internal.ExpandEventArgs eventArgs = new Blazor.Navigations.Internal.ExpandEventArgs();
            eventArgs.IsLoaded = true;
            eventArgs.NodeLevel = 3;
            Assert.True(eventArgs.IsLoaded, "IsLoaded property should be set to true.");
            Assert.Equal(3, eventArgs.NodeLevel);
        }
        [Fact(DisplayName = "Test DropTreeArgs Properties")]
        public void TestDropTreeArgsProperties()
        {
            var args = new DropTreeArgs();
            args.DragLi = "DraggedItem";
            args.DropLi = "DroppedItem";
            args.DropParentLi = "DroppedParentItem";
            args.DragParentLi = "DraggedParentItem";
            args.Pre = true;
            args.IsExternalDrag = false;
            var dotNetObject = DotNetObjectReference.Create(new object());
            args.SrcTree = dotNetObject;
            Assert.Equal("DraggedItem", args.DragLi);
            Assert.Equal("DroppedItem", args.DropLi);
            Assert.Equal("DroppedParentItem", args.DropParentLi);
            Assert.Equal("DraggedParentItem", args.DragParentLi);
            Assert.True(args.Pre, "Pre property should be true.");
            Assert.False(args.IsExternalDrag, "IsExternalDrag property should be false.");
            Assert.Equal(dotNetObject, args.SrcTree);
        }
        //[Fact(DisplayName = "NodeId Property Get and Set")]
        //public void NodeId_GetAndSet()
        //{
        //    var listGeneration = new ListGeneration<object>();
        //    //var remoteFieldsData = new ListGeneration<object>.RemoteFieldsData();
        //    string expectedNodeId = "Node123";
        //    remoteFieldsData.NodeId = expectedNodeId;
        //    string retrievedNodeId = remoteFieldsData.NodeId;
        //    Assert.Equal(expectedNodeId, retrievedNodeId);
        //}

        //[Fact(DisplayName = "FieldSettings Property Get and Set")]
        //public void FieldSettings_GetAndSet()
        //{
            //var listGeneration = new ListGeneration<object>();
            //var remoteFieldsData = new ListGeneration<object>.RemoteFieldsData();
            //var expectedFieldSettings = new TreeFieldsMapping { Text = "NameField", HtmlAttributes = "Attributes" };
            //remoteFieldsData.FieldSettings = expectedFieldSettings;
            //var retrievedFieldSettings = remoteFieldsData.FieldSettings;
            //Assert.Equal(expectedFieldSettings, retrievedFieldSettings);
        //}

        //[Fact(DisplayName = "RemoteData Property Get and Set")]
        //public void RemoteData_GetAndSet()
        //{
        //    var listGeneration = new ListGeneration<object>();
        //    var remoteFieldsData = new ListGeneration<object>.RemoteFieldsData();
        //    var expectedRemoteData = new List<object> { new { Id = 1, Name = "RemoteItem1" }, new { Id = 2, Name = "RemoteItem2" } };
        //    remoteFieldsData.RemoteData = expectedRemoteData;
        //    var retrievedRemoteData = remoteFieldsData.RemoteData;
        //    Assert.Equal(expectedRemoteData, retrievedRemoteData);
        //    Assert.Equal(2, retrievedRemoteData.Count);
        //    Assert.Equal("RemoteItem1", ((dynamic)retrievedRemoteData[0]).Name);
        //}
        [Fact(DisplayName = "Test NodeLevel Property Set and Get")]
        public void TestNodeLevelProperty()
        {
            var args = new TreeItemCreatedArgs<object>();
            int expectedNodeLevel = 3;
            args.NodeLevel = expectedNodeLevel;
            Assert.Equal(expectedNodeLevel, args.NodeLevel);
        }

        [Fact(DisplayName = "Test Options Property Set and Get")]
        public void TestOptionsProperty()
        {
            var args = new TreeItemCreatedArgs<object>();
            var expectedOptions = new ListModel{Fields = new FieldsMapping{}};
            args.Options = expectedOptions;
            Assert.Equal(expectedOptions.Fields, args.Options.Fields);
        }

        [Fact(Timeout = 10000, DisplayName = "GetPropertyChanges includes updated flags")]
        public void GetPropertyChanges_Covers_Targeted_Properties()
        {
            var data = GenerateTreeData();
            var comp = RenderComponent<SfTreeView<TreeData>>(p => p
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(fields => fields
                    .Add(f => f.DataSource, data)
                    .Add(f => f.Id, "Code")
                    .Add(f => f.Text, "Name")
                    .Add(f => f.Child, "Child")
                )
            );

            var instance = comp.Instance;
            var type = instance.GetType();

            type.GetProperty(nameof(SfTreeView<TreeData>.AllowDragAndDrop))?.SetValue(instance, true);
            type.GetProperty(nameof(SfTreeView<TreeData>.AllowEditing))?.SetValue(instance, true);
            type.GetProperty(nameof(SfTreeView<TreeData>.AllowTextWrap))?.SetValue(instance, true);
            type.GetProperty(nameof(SfTreeView<TreeData>.Disabled))?.SetValue(instance, true);
            type.GetProperty(nameof(SfTreeView<TreeData>.CssClass))?.SetValue(instance, "custom-css");
            type.GetProperty(nameof(SfTreeView<TreeData>.DropArea))?.SetValue(instance, "#drop-here");
            type.GetProperty(nameof(SfTreeView<TreeData>.ExpandOn))?.SetValue(instance, Syncfusion.Blazor.Navigations.ExpandAction.Click);

            var dynamicChanges = new Dictionary<string, object>
            {
                { nameof(SfTreeView<TreeData>.AllowDragAndDrop), true },
                { nameof(SfTreeView<TreeData>.AllowEditing), true },
                { nameof(SfTreeView<TreeData>.AllowTextWrap), true },
                { nameof(SfTreeView<TreeData>.Disabled), true },
                { nameof(SfTreeView<TreeData>.CssClass), "custom-css" },
                { nameof(SfTreeView<TreeData>.DropArea), "#drop-here" },
                { nameof(SfTreeView<TreeData>.ExpandOn), Syncfusion.Blazor.Navigations.ExpandAction.Click },
            };

            var method = type.GetMethod("GetPropertyChanges", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            Assert.NotNull(method);

            var result = (Dictionary<string, object>)method.Invoke(instance, new object[] { dynamicChanges });
            Assert.NotNull(result);

            string keyAllowDnD = (string)type.GetField("TREEVIEWALLOWDRAGANDDROP", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
            string keyAllowEdit = (string)type.GetField("TREEVIEWALLOWEDITING", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
            string keyTextWrap = (string)type.GetField("TEXTWRAP", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
            string keyDisabled = (string)type.GetField("TREEVIEWDISABLED", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
            string keyDragArea = (string)type.GetField("DRAGAREA", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
            string keyCssClass = (string)type.GetField("TREEVIEWCSSCLASS", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);
            string keyExpandOn = (string)type.GetField("EXPANDONTYPE", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)?.GetValue(null);

            Assert.False(string.IsNullOrEmpty(keyAllowDnD));
            Assert.False(string.IsNullOrEmpty(keyAllowEdit));
            Assert.False(string.IsNullOrEmpty(keyTextWrap));
            Assert.False(string.IsNullOrEmpty(keyDisabled));
            Assert.False(string.IsNullOrEmpty(keyDragArea));
            Assert.False(string.IsNullOrEmpty(keyCssClass));
            Assert.False(string.IsNullOrEmpty(keyExpandOn));

            Assert.True(result.ContainsKey(keyAllowDnD));
            Assert.Equal(true, (bool)result[keyAllowDnD]);

            Assert.True(result.ContainsKey(keyAllowEdit));
            Assert.Equal(true, (bool)result[keyAllowEdit]);

            Assert.True(result.ContainsKey(keyTextWrap));
            Assert.Equal(true, (bool)result[keyTextWrap]);

            Assert.True(result.ContainsKey(keyDisabled));
            Assert.Equal(true, (bool)result[keyDisabled]);

            Assert.True(result.ContainsKey(keyDragArea));
            Assert.Equal("#drop-here", (string)result[keyDragArea]);

            Assert.True(result.ContainsKey(keyCssClass));
            Assert.Equal("custom-css", (string)result[keyCssClass]);

            Assert.True(result.ContainsKey(keyExpandOn));
            Assert.Equal(Syncfusion.Blazor.Navigations.ExpandAction.Click, (Syncfusion.Blazor.Navigations.ExpandAction)result[keyExpandOn]);
        }

        [Fact(DisplayName = "UpdateDraggedTree should move child nodes from one tree to another")]
        public async Task UpdateDraggedTree_MovesChildNodes_SelfReferential()
        {
            var sourceData = new List<Listdata>
            {
                new Listdata { Id = "1", Name = "Parent", HasChild = true },
                new Listdata { Id = "2", Pid = "1", Name = "Child 1" },
                new Listdata { Id = "3", Pid = "1", Name = "Child 2" }
            };

            var targetData = new List<Listdata>
            {
                new Listdata { Id = "10", Name = "Target Root", HasChild = true }
            };

            var sourceTree = RenderComponent<SfTreeView<Listdata>>(parameters => parameters
                .Add(p => p.ID, "SourceTree")
                .AddChildContent<TreeViewFieldsSettings<Listdata>>(fields => fields
                    .Add(p => p.DataSource, sourceData)
                    .Add(p => p.Id, "Id")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.HasChildren, "HasChild")
                ));

            var targetTree = RenderComponent<SfTreeView<Listdata>>(parameters => parameters
                .Add(p => p.ID, "TargetTree")
                .Add(p => p.AllowDragAndDrop, true)
                .AddChildContent<TreeViewFieldsSettings<Listdata>>(fields => fields
                    .Add(p => p.DataSource, targetData)
                    .Add(p => p.Id, "Id")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.HasChildren, "HasChild")
                ));

            var dropArgs = new DropTreeArgs
            {
                DragLi = "1", 
                DropLi = "10", 
                Pre = false,
                DragParentLi = null,
                DropParentLi = null,
                IsExternalDrag = true,
                SrcTree = DotNetObjectReference.Create<object>(sourceTree.Instance)
            };

            await targetTree.Instance.DropNodeAsSibling(dropArgs);

            //var sourceDs = sourceTree.Instance.ListReference.DataSource;
            //Assert.DoesNotContain(sourceDs, d => d.Id == "1");
            //Assert.DoesNotContain(sourceDs, d => d.Id == "2");
            //Assert.DoesNotContain(sourceDs, d => d.Id == "3");

            //var targetDs = targetTree.Instance.ListReference.DataSource;

            //Assert.Contains(targetDs, d => d.Id == "1");
            //Assert.Contains(targetDs, d => d.Id == "2");
            //Assert.Contains(targetDs, d => d.Id == "3");
        }

        [Fact(DisplayName = "DropNodeAsSibling with external drag should remove from source tree")]
        public async Task DropNodeAsSibling_ExternalDrag_RemovesFromSource()
        {
            var sourceData = new List<Listdata>
            {
                new Listdata { Id = "100", Name = "External Node" }
            };
            var sourceTree = RenderComponent<SfTreeView<Listdata>>(parameters => parameters
                .Add(p => p.ID, "sourceTree")
                .AddChildContent<TreeViewFieldsSettings<Listdata>>(fields => fields
                    .Add(p => p.DataSource, sourceData)
                    .Add(p => p.Id, "Id")
                    .Add(p => p.Text, "Name")
                ));
            var targetData = new List<Listdata>
            {
                new Listdata { Id = "1", Name = "Root Node", HasChild = true }
            };
            var targetTree = RenderComponent<SfTreeView<Listdata>>(parameters => parameters
                .Add(p => p.ID, "targetTree")
                .Add(p => p.AllowDragAndDrop, true)
                .AddChildContent<TreeViewFieldsSettings<Listdata>>(fields => fields
                    .Add(p => p.DataSource, targetData)
                    .Add(p => p.Id, "Id")
                    .Add(p => p.Text, "Name")
                ));

            await targetTree.Instance.DropNodeAsSibling(new DropTreeArgs
            {
                DragLi = "100",
                DropLi = "1",
                Pre = false,
                DragParentLi = null,
                DropParentLi = null,
                IsExternalDrag = true,
                SrcTree = DotNetObjectReference.Create<object>(sourceTree.Instance)
            });

            //var sourceRemaining = sourceTree.Instance.ListReference.DataSource;
            //var targetUpdated = targetTree.Instance.ListReference.DataSource;

            //Assert.DoesNotContain(sourceRemaining, x => x.Id == "100");
            //Assert.Contains(targetUpdated, x => x.Id == "100");
        }

        public class SelfNode
        {
            public string Id { get; set; }
            public string Pid { get; set; }
            public string Name { get; set; }
            public bool HasChild { get; set; }
            public bool IsCheckedVal { get; set; }
        }

        public class HierNode
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public bool? IsChecked { get; set; }
            public List<HierNode> Child { get; set; }
        }

        private List<SelfNode> GetSelfData()
        {
            return new List<SelfNode>
            {
                new SelfNode{ Id = "1", Name = "Root", HasChild = true },
                new SelfNode{ Id = "2", Pid = "1", Name = "Child A" },
                new SelfNode{ Id = "3", Pid = "1", Name = "Child B" }
            };
        }

        private List<HierNode> GetHierData()
        {
            return new List<HierNode>
            {
                new HierNode
                {
                    Code = "NA",
                    Name = "North America",
                    Child = new List<HierNode>
                    {
                        new HierNode{ Code = "USA", Name = "United States" },
                        new HierNode{ Code = "MEX", Name = "Mexico" }
                    }
                },
                new HierNode
                {
                    Code = "EU",
                    Name = "Europe",
                    Child = new List<HierNode>
                    {
                        new HierNode{ Code = "AUT", Name = "Austria" }
                    }
                }
            };
        }

        [Fact(DisplayName = "SelfReferential: UpdateCheckedValueToDatasource should update underlying IsChecked only when CheckedNodesChanged is provided and CssClass is set")]
        public async Task SelfReferential_CheckedNodes_UpdatesData_When_Callback_And_CssClass()
        {
            var data = GetSelfData();
            string[] lastChecked = null;

            var treeview = RenderComponent<SfTreeView<SelfNode>>(parameters => parameters
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.AutoCheck, true)
                .Add(p => p.CssClass, "custom")
                .Add(p => p.CheckedNodesChanged, (string[] args) => lastChecked = args)
                .AddChildContent<TreeViewFieldsSettings<SelfNode>>(fields => fields
                    .Add(f => f.DataSource, data)
                    .Add(f => f.ParentID, "Pid")
                    .Add(f => f.Id, "Id")
                    .Add(f => f.Text, "Name")
                    .Add(f => f.HasChildren, "HasChild")
                    .Add(f => f.IsChecked, "IsCheckedVal")
                )
            );

            var args = new NodeCheckEventArgs
            {
                Action = "check",
                Cancel = false,
                IsInteracted = true,
                NodeData = new NodeData { Id = "2" }
            };
            await treeview.Instance.TriggerNodeCheckingEvent(args);

            var updated = data.ToDictionary(d => d.Id, d => d.IsCheckedVal);
            Assert.True(updated["2"], "Node '2' should be checked true in the underlying data");
            Assert.False(updated["1"], "Root should be false as UpdateSelfReferentialData sets exact checked id(s)");
            Assert.False(updated["3"], "Sibling should be false");
            Assert.NotNull(lastChecked);
            Assert.Contains("2", lastChecked);
        }

        [Fact(DisplayName = "SelfReferential: UpdateCheckedValueToDatasource should NOT update underlying IsChecked without CheckedNodesChanged delegate")]
        public async Task SelfReferential_CheckedNodes_DoesNotUpdateData_Without_Callback()
        {
            var data = GetSelfData();

            var treeview = RenderComponent<SfTreeView<SelfNode>>(parameters => parameters
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.AutoCheck, true)
                .Add(p => p.CssClass, "custom")
                .AddChildContent<TreeViewFieldsSettings<SelfNode>>(fields => fields
                    .Add(f => f.DataSource, data)
                    .Add(f => f.ParentID, "Pid")
                    .Add(f => f.Id, "Id")
                    .Add(f => f.Text, "Name")
                    .Add(f => f.HasChildren, "HasChild")
                    .Add(f => f.IsChecked, "IsCheckedVal")
                )
            );
            var args = new NodeCheckEventArgs
            {
                Action = "check",
                Cancel = false,
                IsInteracted = true,
                NodeData = new NodeData { Id = "2" }
            };
            await treeview.Instance.TriggerNodeCheckingEvent(args);
            Assert.True(data.All(n => n.IsCheckedVal == false), "Without CheckedNodesChanged delegate, IsChecked should not be mutated in data source");
        }

        [Fact(DisplayName = "SelfReferential: UpdateCheckedValueToDatasource should NOT update underlying IsChecked when CssClass is empty")]
        public async Task SelfReferential_CheckedNodes_DoesNotUpdateData_When_CssClassEmpty()
        {
            var data = GetSelfData();
            string[] lastChecked = null;

            var treeview = RenderComponent<SfTreeView<SelfNode>>(parameters => parameters
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.AutoCheck, true)
                .Add(p => p.CssClass, "")
                .Add(p => p.CheckedNodesChanged, (string[] args) => lastChecked = args)
                .AddChildContent<TreeViewFieldsSettings<SelfNode>>(fields => fields
                    .Add(f => f.DataSource, data)
                    .Add(f => f.ParentID, "Pid")
                    .Add(f => f.Id, "Id")
                    .Add(f => f.Text, "Name")
                    .Add(f => f.HasChildren, "HasChild")
                    .Add(f => f.IsChecked, "IsCheckedVal")
                )
            );
            var args = new NodeCheckEventArgs
            {
                Action = "check",
                Cancel = false,
                IsInteracted = true,
                NodeData = new NodeData { Id = "3" }
            };
            await treeview.Instance.TriggerNodeCheckingEvent(args);
            Assert.True(data.All(n => n.IsCheckedVal == false), "No item should be updated when CssClass is empty");
            Assert.NotNull(lastChecked);
            Assert.Contains("3", lastChecked);
        }

        [Fact(DisplayName = "Hierarchical: UpdateCheckedValueToDatasource should update IsChecked recursively when CheckedNodesChanged is provided and CssClass is set")]
        public async Task Hierarchical_CheckedNodes_UpdatesDataRecursively_When_Callback_And_CssClass()
        {
            var data = GetHierData();
            string[] lastChecked = null;

            var treeview = RenderComponent<SfTreeView<HierNode>>(parameters => parameters
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.AutoCheck, true)
                .Add(p => p.CssClass, "custom")
                .Add(p => p.CheckedNodesChanged, (string[] args) => lastChecked = args)
                .AddChildContent<TreeViewFieldsSettings<HierNode>>(fields => fields
                    .Add(f => f.DataSource, data)
                    .Add(f => f.Id, "Code")
                    .Add(f => f.Text, "Name")
                    .Add(f => f.IsChecked, "IsChecked")
                    .Add(f => f.Child, "Child")
                )
            );
            var args = new NodeCheckEventArgs
            {
                Action = "check",
                Cancel = false,
                IsInteracted = true,
                NodeData = new NodeData { Id = "USA" }
            };
            await treeview.Instance.TriggerNodeCheckingEvent(args);
            var flat = Flatten(data).ToDictionary(x => x.Code, x => x.IsChecked);
            Assert.True(flat["USA"] == true, "'USA' should be true");
            Assert.All(flat.Where(kv => kv.Key != "USA"), kv => Assert.False(kv.Value ?? false, $"'{kv.Key}' should be false"));
            Assert.NotNull(lastChecked);
            Assert.Contains("USA", lastChecked);
        }

        [Fact(DisplayName = "Hierarchical: UpdateCheckedValueToDatasource should NOT update IsChecked if CheckedNodesChanged is not provided")]
        public async Task Hierarchical_CheckedNodes_DoesNotUpdateData_Without_Callback()
        {
            var data = GetHierData();

            var treeview = RenderComponent<SfTreeView<HierNode>>(parameters => parameters
                .Add(p => p.ShowCheckBox, true)
                .Add(p => p.AutoCheck, true)
                .Add(p => p.CssClass, "custom")
                .AddChildContent<TreeViewFieldsSettings<HierNode>>(fields => fields
                    .Add(f => f.DataSource, data)
                    .Add(f => f.Id, "Code")
                    .Add(f => f.Text, "Name")
                    .Add(f => f.IsChecked, "IsChecked")
                    .Add(f => f.Child, "Child")
                )
            );

            var args = new NodeCheckEventArgs
            {
                Action = "check",
                Cancel = false,
                IsInteracted = true,
                NodeData = new NodeData { Id = "MEX" }
            };
            await treeview.Instance.TriggerNodeCheckingEvent(args);
            Assert.All(Flatten(data), n => Assert.Null(n.IsChecked));
        }

        private static IEnumerable<HierNode> Flatten(IEnumerable<HierNode> list)
        {
            foreach (var n in list)
            {
                yield return n;
                if (n.Child != null)
                {
                    foreach (var c in Flatten(n.Child))
                        yield return c;
                }
            }
        }

        [Fact(Timeout = 10000, DisplayName = "ExpandedNodes property change collapses removed nodes and expands new nodes")]
        public void ExpandedNodes_PropertyChange_ShouldCollapseRemoved_AndExpandAdded()
        {
            var data = GenerateTreeData();
            var component = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field
                    .Add(p => p.DataSource, data)
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Child, "Child"))
            );

            component.SetParametersAndRender(("ExpandedNodes", new[] { "AS", "AF" }));

            var ulElements = component.FindAll("ul");
            var initialExpandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == initialExpandedLiCount, "Initial expanded nodes should show 6 li under two expanded nodes.");

            component.SetParametersAndRender(("ExpandedNodes", new[] { "NA", "EU" }));

            ulElements = component.FindAll("ul");
            var updatedExpandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == updatedExpandedLiCount, "Updated expanded nodes should show new expansion reflecting NA and EU.");

            var expanded = component.Instance.ExpandedNodes;
            Assert.NotNull(expanded);
            Assert.True(expanded.Length == 2);
            Assert.Contains("NA", expanded);
            Assert.Contains("EU", expanded);
        }

        [Fact(Timeout = 10000, DisplayName = "ExpandedNodes two-way binding callback triggers through property change handler path")]
        public void ExpandedNodes_PropertyChange_ShouldInvokeExpandedNodesChanged()
        {
            var data = GenerateTreeData();
            string[] callbackValue = null;
            var component = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                .Add(p => p.ExpandedNodesChanged, (string[] values) =>
                {
                    callbackValue = values;
                })
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field
                    .Add(p => p.DataSource, data)
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Child, "Child"))
            );

            var newSet = new[] { "AS", "AF" };
            component.SetParametersAndRender(("ExpandedNodes", newSet));

            Assert.NotNull(callbackValue);
            Assert.Equal(2, callbackValue.Length);
            Assert.Contains("AS", callbackValue);
            Assert.Contains("AF", callbackValue);
        }

        [Fact(Timeout = 10000, DisplayName = "ExpandedNodes property set with same values should keep visual tree stable (no extra expansions)")]
        public void ExpandedNodes_SetSameValues_ShouldNotChangeRenderedStructure()
        {
            var data = GenerateTreeData();
            var component = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field
                    .Add(p => p.DataSource, data)
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Child, "Child"))
            );
            var initialSet = new[] { "AS", "AF" };
            component.SetParametersAndRender(("ExpandedNodes", initialSet));

            var ulBefore = component.FindAll("ul").Count;
            var liBefore = component.FindAll("li").Count;

            component.SetParametersAndRender(("ExpandedNodes", initialSet));

            var ulAfter = component.FindAll("ul").Count;
            var liAfter = component.FindAll("li").Count;
            Assert.Equal(ulBefore, ulAfter);
            Assert.Equal(liBefore, liAfter);
        }

        [Fact(Timeout = 10000, DisplayName = "ExpandedNodes property change performing both collapse and expand in same pass")]
        public void ExpandedNodes_PropertyChange_ShouldHandleMixedCollapseAndExpand()
        {
            var data = GenerateTreeData();
            var component = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field
                    .Add(p => p.DataSource, data)
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Child, "Child"))
            );

            component.SetParametersAndRender(("ExpandedNodes", new[] { "NA", "AS" }));
            var ulBefore = component.FindAll("ul").Count;

            component.SetParametersAndRender(("ExpandedNodes", new[] { "AF" }));

            var ulAfter = component.FindAll("ul").Count;
            Assert.True(ulAfter >= 2, "There should be nested ul elements after updating to a different expanded node.");
            Assert.NotEqual(ulBefore, ulAfter);

            var expanded = component.Instance.ExpandedNodes;
            Assert.Single(expanded);
            Assert.Equal("AF", expanded[0]);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeViewFieldsSettings.DataSourceChanged getter and direct invoke")]
        public async Task Fields_DataSourceChanged_GetterAndInvoke_Works()
        {
            var data = GenerateTreeData();
            IEnumerable<TreeView.TreeData> handlerReceived = null;

            var component = RenderComponent<SfTreeView<TreeView.TreeData>>(parameters => parameters
                .AddChildContent<TreeViewFieldsSettings<TreeView.TreeData>>(fields => fields
                    .Add(p => p.DataSource, data)
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Child, "Child")
                    .Add(p => p.DataSourceChanged, (IEnumerable<TreeView.TreeData> changed) => handlerReceived = changed)
                )
            );

            var fieldsComp = component.FindComponent<TreeViewFieldsSettings<TreeView.TreeData>>();

            var callback = fieldsComp.Instance.DataSourceChanged;

            Assert.True(callback.HasDelegate);

            var newData = new List<TreeView.TreeData>
            {
                new TreeView.TreeData { Code = "X1", Name = "Region X1" },
                new TreeView.TreeData { Code = "X2", Name = "Region X2" },
            };
            await fieldsComp.Instance.DataSourceChanged.InvokeAsync(newData);

            Assert.NotNull(handlerReceived);
            Assert.Equal(2, handlerReceived.Count());
            Assert.Contains(handlerReceived, x => x.Code == "X1" && x.Name == "Region X1");
            Assert.Contains(handlerReceived, x => x.Code == "X2" && x.Name == "Region X2");
        }



        [Fact(Timeout = 10000, DisplayName = "Query change without DataManager - branch executes safely")]
        public void QueryChange_WithoutDataManager_ShouldNotThrow()
        {
            var data = GenerateTreeData();
            var initialQuery = new Query().Take(1);
            var updatedQuery = new Query().Take(2);

            var ex = Record.Exception(() =>
            {
                var treeview = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                    .AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field
                        .Add(p => p.DataSource, data)
                        .Add(p => p.Query, initialQuery)
                        .Add(p => p.Id, "Code")
                        .Add(p => p.Text, "Name")
                        .Add(p => p.Child, "Child")
                    )
                );
                treeview.SetParametersAndRender(p => p
                    .AddChildContent<TreeViewFieldsSettings<TreeData>>(field => field
                        .Add(p => p.DataSource, data)
                        .Add(p => p.Query, updatedQuery)
                        .Add(p => p.Id, "Code")
                        .Add(p => p.Text, "Name")
                        .Add(p => p.Child, "Child")
                    )
                );
            });
            Assert.Null(ex);
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerNodeEditingEvent cancel clears EditedNodeId and does not commit changes")]
        public async Task TriggerNodeEditingEvent_Cancel_DoesNotCommit()
        {
            var data = GenerateListData();
            int nodeEditedCount = 0;
            bool nodeEditingCalled = false;

            var treeview = RenderComponent<SfTreeView<Listdata>>(parameters => parameters
                .Add(p => p.AllowEditing, true)
                .AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field
                    .Add(p => p.DataSource, data)
                    .Add(p => p.ParentID, "Pid")
                    .Add(p => p.Id, "Id")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.HasChildren, "HasChild")
                )
                .AddChildContent<TreeViewEvents<Listdata>>(events => events
                    .Add(e => e.NodeEditing, (NodeEditEventArgs args) =>
                    {
                        nodeEditingCalled = true;
                        args.Cancel = true;
                    })
                    .Add(e => e.NodeEdited, (NodeEditEventArgs args) =>
                    {
                        nodeEditedCount++;
                    })
                )
            );

            treeview.Find(".e-treeview");

            var editArgs = new NodeEditEventArgs
            {
                Cancel = false,
                NodeData = new NodeData { Id = "1" }
            };
            await treeview.Instance.TriggerNodeEditingEvent(editArgs);

            Assert.True(nodeEditingCalled);
            Assert.Equal(0, nodeEditedCount);

            var original = treeview.Instance.GetTreeData("1").FirstOrDefault();
            Assert.NotNull(original);
            Assert.Equal("Australia", original.Name);
        }

        [Fact(Timeout = 10000, DisplayName = "SetMultiSelection trims selections when disabled")]
        public void SetMultiSelection_TrimsSelection_WhenDisabled()
        {
            var data = GenerateTreeData();
            string[] lastSelected = null;

            var treeview = RenderComponent<SfTreeView<TreeData>>(parameters => parameters
                .Add(p => p.AllowMultiSelection, true)
                .Add(p => p.SelectedNodes, new[] { "EU", "SA" })
                .Add(p => p.SelectedNodesChanged, (string[] v) => lastSelected = v)
                .AddChildContent<TreeViewFieldsSettings<TreeData>>(fields => fields
                    .Add(p => p.DataSource, data)
                    .Add(p => p.Id, "Code")
                    .Add(p => p.Text, "Name")
                    .Add(p => p.Selected, "Selected")
                    .Add(p => p.Expanded, "Expanded")
                    .Add(p => p.Child, "Child"))
            );

            var root = treeview.Find(".e-treeview");
            var selectedBefore = root.QuerySelectorAll("li.e-active").Length;
            Assert.Equal(2, selectedBefore);

            treeview.SetParametersAndRender(("AllowMultiSelection", false));

            var selectedAfter = root.QuerySelectorAll("li.e-active");
            Assert.Equal(1, selectedAfter.Length);

            Assert.NotNull(lastSelected);
            Assert.Single(lastSelected);
            Assert.Contains(lastSelected[0], new[] { "EU", "SA" });
        }

        [Fact(Timeout = 10000, DisplayName = "NodeClicked event testing with NodeClickEventArgs properties")]
        public async Task NodeClickedEvent_Test()
        {
            // Arrange
            var cut = RenderComponent<NodeClickedEvent>();
            await Task.Delay(200);
            var outputSpan = cut.Find("span#event");
            Assert.NotNull(outputSpan);

            // Click the button to trigger initialization
            cut.Find("button").Click();
            await Task.Delay(200);

            // Get the TreeView component instance
            var treeview = cut.FindComponent<SfTreeView<NodeClickedEvent.TreeData>>();
            Assert.NotNull(treeview);

            // Create click event args to test the event handler
            var clickEventArgs = new ClickEventArgs();
            var mouseEventArgs = new MouseEventArgs { ClientX = 10, ClientY = 20 };

            // Invoke the treeview's node click event handler directly
            // This simulates what happens when user clicks on a tree node
            // Must use InvokeAsync to run on the dispatcher thread
            await treeview.InvokeAsync(async () =>
            {
                await treeview.Instance.TriggerNodeClickingEvent(clickEventArgs, mouseEventArgs, "NA", 10, 20);
            });

            await Task.Delay(200);

            // Assert
            var output = outputSpan.TextContent;
            Assert.Contains("NodeClicked event", output);
            Assert.Contains("Name=", output);
            Assert.Contains("NodeText=", output);
            Assert.Contains("Event=", output);
            Assert.Contains("NodeData=", output);
            Assert.Contains("Left=", output);
            Assert.Contains("Top=", output);
        }

        [Fact(Timeout = 10000, DisplayName = "NodeKeyPress event testing with NodeKeyPressEventArgs properties")]
        public async Task NodeKeyPressEvent()
        {
            var cut = RenderComponent<NodeKeyPressEvent>();
            await Task.Delay(200);
            var outputSpan = cut.Find("span#keypress-event");
            Assert.NotNull(outputSpan);
            
            // Click the button to trigger initialization
            cut.Find("button").Click();
            await Task.Delay(200);
            
            // Get the TreeView component instance
            var treeview = cut.FindComponent<SfTreeView<NodeKeyPressEvent.TreeData>>();
            Assert.NotNull(treeview);
            
            // Create NodeKeyPressEventArgs to test the event handler
            var keyPressArgs = new NodeKeyPressEventArgs
            {
                Cancel = false,
                NodeData = new NodeData { Id = "NA", Text = "North America" },
                Name = "OnKeyPress",
                Action = "select"
                // Event and Key properties are internal set, so they're set by the framework
            };
            
            // Invoke the treeview's keyboard event handler directly
            // This simulates what happens when user presses a key on the treeview
            // Must use InvokeAsync to run on the dispatcher thread
            await treeview.InvokeAsync(async () =>
            {
                await treeview.Instance.TriggerKeyboardEvent(keyPressArgs, "NA", "select", "Enter");
            });
            
            await Task.Delay(200);
            
            var keyPressOutput = outputSpan.TextContent;
            Assert.Contains("OnKeyPress event", keyPressOutput);
            Assert.Contains("Cancel=", keyPressOutput);
            Assert.Contains("Event=", keyPressOutput);
            Assert.Contains("NodeData=", keyPressOutput);
            Assert.Contains("Name=", keyPressOutput);
            Assert.Contains("Action=", keyPressOutput);
            Assert.Contains("Key=", keyPressOutput);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView Remote Data loading and GetDataSource testing")]
        public async Task TreeViewRemoteDataTest()
        {
            var cut = RenderComponent<RemoteDataTest>();
            await Task.Delay(500);

            // Verify remote data was loaded
            var remoteDataLoaded = cut.Find("#remoteDataLoaded");
            Assert.NotNull(remoteDataLoaded);
            Assert.Contains("Loaded", remoteDataLoaded.TextContent);

            // Verify GetDataSource was called
            var getDataSourceResult = cut.Find("#getDataSourceResult");
            Assert.NotNull(getDataSourceResult);
            Assert.Contains("Yes", getDataSourceResult.TextContent);

            // Verify IdentifyDataSource completed
            var identifyDataSourceResult = cut.Find("#identifyDataSourceResult");
            Assert.NotNull(identifyDataSourceResult);
            Assert.Contains("Completed", identifyDataSourceResult.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView OnInitializedAsync and UpdateExpandedNodesState testing")]
        public async Task TreeViewDataOperationsTest()
        {
            var cut = RenderComponent<TreeViewDataOperationsTest>();
            await Task.Delay(500);

            // Verify OnInitializedAsync was called
            var onInitializedResult = cut.Find("#onInitializedResult");
            Assert.NotNull(onInitializedResult);
            Assert.Contains("Initialized", onInitializedResult.TextContent);

            // Verify UpdateExpandedNodes state
            var updateExpandedNodesResult = cut.Find("#updateExpandedNodesResult");
            Assert.NotNull(updateExpandedNodesResult);
            Assert.Contains("Ready", updateExpandedNodesResult.TextContent);

            // Verify IdentifyDataSource was called
            var identifyDataSourceResult = cut.Find("#identifyDataSourceResult");
            Assert.NotNull(identifyDataSourceResult);
            Assert.Contains("Ready", identifyDataSourceResult.TextContent);

            // Verify RefreshTreeNodes is ready
            var refreshNodeResult = cut.Find("#refresNodeResult");
            Assert.NotNull(refreshNodeResult);
            Assert.Contains("Ready", refreshNodeResult.TextContent);

            // Verify OnAfterRenderAsync is ready
            var onAfterRenderAsyncResult = cut.Find("#onAfterRenderAsyncResult");
            Assert.NotNull(onAfterRenderAsyncResult);
            Assert.Contains("Ready", onAfterRenderAsyncResult.TextContent);

            // Test UpdateFields
            var updateFieldsButton = cut.Find("button");
            updateFieldsButton.Click();
            await Task.Delay(200);

            var updateFieldsResult = cut.Find("#updateFieldsResult");
            Assert.NotNull(updateFieldsResult);
            Assert.Contains("successfully", updateFieldsResult.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView Node Update and Delete operations testing")]
        public async Task TreeViewNodeOperationsTest()
        {
            var cut = RenderComponent<TreeViewNodeOperationsTest>();
            await Task.Delay(500);

            // Verify initial setup
            var updateSelfNodeTextResult = cut.Find("#updateSelfNodeTextResult");
            Assert.NotNull(updateSelfNodeTextResult);
            Assert.Contains("Ready", updateSelfNodeTextResult.TextContent);

            // Verify UpdateRemoteNodeText readiness
            var updateRemoteNodeTextResult = cut.Find("#updateRemoteNodeTextResult");
            Assert.NotNull(updateRemoteNodeTextResult);
            Assert.Contains("Ready", updateRemoteNodeTextResult.TextContent);

            // Verify UpdateNodeText readiness
            var updateNodeTextResult = cut.Find("#updateNodeTextResult");
            Assert.NotNull(updateNodeTextResult);
            Assert.Contains("Ready", updateNodeTextResult.TextContent);

            // Verify RemoveNodes readiness
            var removeNodesResult = cut.Find("#removeNodesResult");
            Assert.NotNull(removeNodesResult);
            Assert.Contains("Ready", removeNodesResult.TextContent);

            // Verify AddChildListData readiness
            var addChildListDataResult = cut.Find("#addChildListDataResult");
            Assert.NotNull(addChildListDataResult);
            Assert.Contains("Ready", addChildListDataResult.TextContent);

            // Test node operations
            var testButton = cut.Find("button");
            testButton.Click();
            await Task.Delay(300);

            // Verify operations completed
            var updateNodeTextResultAfter = cut.Find("#updateNodeTextResult");
            Assert.NotNull(updateNodeTextResultAfter);
            Assert.Contains("completed", updateNodeTextResultAfter.TextContent);

            var removedNodesResultAfter = cut.Find("#removeNodesResult");
            Assert.NotNull(removedNodesResultAfter);
            Assert.Contains("Removed", removedNodesResultAfter.TextContent);

            var addedChildResultAfter = cut.Find("#addChildListDataResult");
            Assert.NotNull(addedChildResultAfter);
            Assert.Contains("Added", addedChildResultAfter.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView Rendering operations: EnsureExpandNodes, UpdateExpandState, RenderRemoteLi testing")]
        public async Task TreeViewRenderingOperationsTest()
        {
            var cut = RenderComponent<TreeViewRenderingTest>();
            await Task.Delay(500);

            // Verify EnsureExpandNodes
            var ensureExpandNodesResult = cut.Find("#ensureExpandNodesResult");
            Assert.NotNull(ensureExpandNodesResult);
            Assert.Contains("ready", ensureExpandNodesResult.TextContent);

            // Verify UpdateExpandState
            var updateExpandStateResult = cut.Find("#updateExpandStateResult");
            Assert.NotNull(updateExpandStateResult);
            Assert.Contains("initialized", updateExpandStateResult.TextContent);

            // Verify RenderRemoteLi
            var renderRemoteLiResult = cut.Find("#renderRemoteLiResult");
            Assert.NotNull(renderRemoteLiResult);
            Assert.Contains("enabled", renderRemoteLiResult.TextContent);

            // Verify GetRemovedHierData
            var getRemovedHierDataResult = cut.Find("#getRemovedHierDataResult");
            Assert.NotNull(getRemovedHierDataResult);
            Assert.Contains("accessible", getRemovedHierDataResult.TextContent);

            // Verify GetHierarchicalAndRemoteParent
            var getHierarchicalAndRemoteParentResult = cut.Find("#getHierarchicalAndRemoteParentResult");
            Assert.NotNull(getHierarchicalAndRemoteParentResult);
            Assert.Contains("resolved", getHierarchicalAndRemoteParentResult.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView ListGeneration - Load Hierarchical Data")]
        public async Task TreeViewListGeneration_HierarchicalData()
        {
            var cut = RenderComponent<TreeViewListGenerationTest>();
            
            // Verify initial state
            var dataCountSpan = cut.Find("#dataCount");
            Assert.Contains("0", dataCountSpan.TextContent);

            // Click Load Hierarchical Data button
            var loadHierBtn = cut.Find("#loadHierarchicalBtn");
            loadHierBtn.Click();

            // Wait for async operations
            await Task.Delay(200);
            cut.Render();

            // Verify hierarchical data loaded
            var hierarchicalSpan = cut.Find("#hierarchicalLoaded");
            Assert.Contains("Yes", hierarchicalSpan.TextContent);

            // Verify data count updated
            dataCountSpan = cut.Find("#dataCount");
            Assert.Contains("Data Count: 2", dataCountSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView ListGeneration - Load Self-Referential Data")]
        public async Task TreeViewListGeneration_SelfReferentialData()
        {
            var cut = RenderComponent<TreeViewListGenerationTest>();
            
            // Click Load Self-Referential Data button
            var loadSelfRefBtn = cut.Find("#loadSelfRefBtn");
            loadSelfRefBtn.Click();

            // Wait for async operations
            await Task.Delay(200);
            cut.Render();

            // Verify self-referential data loaded
            var selfRefSpan = cut.Find("#selfRefLoaded");
            Assert.Contains("Yes", selfRefSpan.TextContent);

            // Verify data count (5 items total)
            var dataCountSpan = cut.Find("#dataCount");
            Assert.Contains("Data Count: 5", dataCountSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView ListGeneration - Expand Node Operation")]
        public async Task TreeViewListGeneration_ExpandNode()
        {
            var cut = RenderComponent<TreeViewListGenerationTest>();
            
            // First load hierarchical data
            var loadHierBtn = cut.Find("#loadHierarchicalBtn");
            loadHierBtn.Click();
            await Task.Delay(150);
            cut.Render();

            // Expand node
            var expandBtn = cut.Find("#expandNodeBtn");
            expandBtn.Click();
            await Task.Delay(150);
            cut.Render();

            // Verify expanded state
            var expandedSpan = cut.Find("#expandedState");
            Assert.Contains("Expanded", expandedSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView ListGeneration - Sort Data Operation")]
        public async Task TreeViewListGeneration_SortData()
        {
            var cut = RenderComponent<TreeViewListGenerationTest>();
            
            // Load hierarchical data
            var loadHierBtn = cut.Find("#loadHierarchicalBtn");
            loadHierBtn.Click();
            await Task.Delay(150);
            cut.Render();

            // Sort data
            var sortBtn = cut.Find("#sortDataBtn");
            sortBtn.Click();
            await Task.Delay(150);
            cut.Render();

            // Verify sorted state
            var sortedSpan = cut.Find("#sortedState");
            Assert.Contains("Sorted", sortedSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView ListGeneration - Multiple Data Operations")]
        public async Task TreeViewListGeneration_MultipleOperations()
        {
            var cut = RenderComponent<TreeViewListGenerationTest>();
            
            // Load hierarchical data
            var loadHierBtn = cut.Find("#loadHierarchicalBtn");
            loadHierBtn.Click();
            await Task.Delay(150);
            cut.Render();

            // Expand node
            var expandBtn = cut.Find("#expandNodeBtn");
            expandBtn.Click();
            await Task.Delay(100);
            cut.Render();

            // Sort data
            var sortBtn = cut.Find("#sortDataBtn");
            sortBtn.Click();
            await Task.Delay(150);
            cut.Render();

            // Verify all operations completed
            var expandedSpan = cut.Find("#expandedState");
            var sortedSpan = cut.Find("#sortedState");
            var hierarchicalSpan = cut.Find("#hierarchicalLoaded");

            Assert.Contains("Expanded", expandedSpan.TextContent);
            Assert.Contains("Sorted", sortedSpan.TextContent);
            Assert.Contains("Yes", hierarchicalSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView ListGeneration - Child Data Structure")]
        public async Task TreeViewListGeneration_ChildDataStructure()
        {
            var cut = RenderComponent<TreeViewListGenerationTest>();
            
            // Load hierarchical data with child elements
            var loadHierBtn = cut.Find("#loadHierarchicalBtn");
            loadHierBtn.Click();
            await Task.Delay(200);
            cut.Render();

            // Verify hierarchical structure was created
            var hierarchicalSpan = cut.Find("#hierarchicalLoaded");
            Assert.Contains("Yes", hierarchicalSpan.TextContent);

            // Verify data count includes parent nodes
            var dataCountSpan = cut.Find("#dataCount");
            Assert.Contains("Data Count: 2", dataCountSpan.TextContent);
        }

        [Fact(Timeout = 10000, DisplayName = "TreeView ListGeneration - Field Mapping")]
        public async Task TreeViewListGeneration_FieldMapping()
        {
            var cut = RenderComponent<TreeViewListGenerationTest>();
            
            // Load self-referential data (tests field mapping)
            var loadSelfRefBtn = cut.Find("#loadSelfRefBtn");
            loadSelfRefBtn.Click();
            await Task.Delay(200);
            cut.Render();

            // Verify self-referential data was loaded
            var selfRefSpan = cut.Find("#selfRefLoaded");
            Assert.Contains("Yes", selfRefSpan.TextContent);

            // Verify parent-child relationships maintained
            var dataCountSpan = cut.Find("#dataCount");
            Assert.Contains("Data Count: 5", dataCountSpan.TextContent);
        }

    }
}


