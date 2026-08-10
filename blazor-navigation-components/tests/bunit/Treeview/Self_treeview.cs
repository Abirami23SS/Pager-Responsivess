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

namespace Syncfusion.Blazor.Tests.Treeview
{
    public class Self_treeview : BunitTestContext
    {
        List<Listdata> ListDataSource { get; set; } = new List<Listdata>();

        List<MailItem> MyFolder = new List<MailItem>();

        List<EnabledCR> MyFolders = new List<EnabledCR>();
        public List<EnabledCR> GenerateListDatas()
        {
            List<EnabledCR> Folder1 = new List<EnabledCR>();
            MyFolders.Add(new EnabledCR
            {
                Id = "01",
                FolderName = "Inbox",
                SubFolders = Folder1
            });

            List<EnabledCR> Folder2 = new List<EnabledCR>();

            Folder1.Add(new EnabledCR
            {
                Id = "01-01",
                FolderName = "Categories",
                SubFolders = Folder2
            });
            Folder2.Add(new EnabledCR
            {
                Id = "01-02",
                FolderName = "Primary"
            });
            Folder2.Add(new EnabledCR
            {
                Id = "01-03",
                FolderName = "Social"
            });
            Folder2.Add(new EnabledCR
            {
                Id = "01-04",
                FolderName = "Promotions"
            });

            List<EnabledCR> Folder3 = new List<EnabledCR>();

            MyFolders.Add(new EnabledCR
            {
                Id = "02",
                FolderName = "Others",
                Expanded = true,
                SubFolders = Folder3
            });
            Folder3.Add(new EnabledCR
            {
                Id = "02-01",
                FolderName = "Sent Items"
            });
            Folder3.Add(new EnabledCR
            {
                Id = "02-02",
                FolderName = "Delete Items"
            });
            Folder3.Add(new EnabledCR
            {
                Id = "02-03",
                FolderName = "Drafts"
            });
            Folder3.Add(new EnabledCR
            {
                Id = "02-04",
                FolderName = "Archive"
            });
            return MyFolders;
        }
        public List<Listdata> GenerateListData()
        {
            ListDataSource.Add(new Listdata
            {
                Id = "NA",
                Name = "North America",
                HasChild = true,
                Expanded = true,
                Link = "https://blazor.syncfusion.com/demos/"

            });
            ListDataSource.Add(new Listdata
            {
                Id = "USA",
                Pid = "NA",
                Name = "United States of America",
                Selected = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "CUB",
                Pid = "NA",
                Name = "Cuba"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "MEX",
                Pid = "NA",
                Name = "Mexico"
            });

            ListDataSource.Add(new Listdata
            {
                Id = "AF",
                Name = "Africa",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "NGA",
                Pid = "AF",
                Name = "Nygeria"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "EGY",
                Pid = "AF",
                Name = "Egypt"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "ZAF",
                Pid = "AF",
                Name = "South Africa"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "AS",
                Name = "Asia",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "CHN",
                Pid = "AS",
                Name = "China"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "IND",
                Pid = "AS",
                Name = "India"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "JPN",
                Pid = "AS",
                Name = "Japan"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "EU",
                Name = "Europe",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "DNK",
                Pid = "EU",
                Name = "Denmark"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "AUT",
                Pid = "EU",
                Name = "Austria"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "FIN",
                Pid = "EU",
                Name = "Finland"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "SA",
                Name = "South America",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "BRA",
                Pid = "SA",
                Name = "Brazil"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "COL",
                Pid = "SA",
                Name = "Colombia"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "ARG",
                Pid = "SA",
                Name = "Argentina"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "OC",
                Name = "Oceania",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "AUS",
                Pid = "OC",
                Name = "Australia"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "NZL",
                Pid = "OC",
                Name = "Newzealand"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "WSM",
                Pid = "OC",
                Name = "Samoa"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "AN",
                Name = "Antartica",
                HasChild = true
            });
            ListDataSource.Add(new Listdata
            {
                Id = "BVT",
                Pid = "AN",
                Name = "Bouvet Island"
            });
            ListDataSource.Add(new Listdata
            {
                Id = "ATF",
                Pid = "AN",
                Name = "French Southern Lands"
            });
            return ListDataSource;
        }

        public class Listdata
        {
            public string Id { get; set; }
            public string Pid { get; set; }
            public string Name { get; set; }
            public bool HasChild { get; set; }
            public bool Expanded { get; set; }
            public bool Selected { get; set; }
            public string Link { get; set; }

        }

        public List<MailItem> GenerateMailItems()
        {
            MyFolder.Add(new MailItem

            {
                Id = "1",
                FolderName = "Inbox",
                HasSubFolders = true,
            });
            MyFolder.Add(new MailItem
            {
                Id = "2",
                ParentId = "1",
                HasSubFolders = true,
                FolderName = "Categories"
            });
            MyFolder.Add(new MailItem
            {
                Id = "3",
                ParentId = "2",
                FolderName = "Primary"
            });
            MyFolder.Add(new MailItem
            {
                Id = "4",
                ParentId = "2",
                FolderName = "Social"
            });
            MyFolder.Add(new MailItem
            {
                Id = "5",
                ParentId = "2",
                FolderName = "Promotions"
            });
            MyFolder.Add(new MailItem
            {
                Id = "6",
                FolderName = "Others",
                HasSubFolders = true,
                Expanded = false
            });
            MyFolder.Add(new MailItem
            {
                Id = "7",
                ParentId = "6",
                FolderName = "Sent Items"
            });
            MyFolder.Add(new MailItem
            {
                Id = "8",
                ParentId = "6",
                FolderName = "Delete Items"
            });
            MyFolder.Add(new MailItem
            {
                Id = "9",
                ParentId = "6",
                FolderName = "Drafts"
            });
            MyFolder.Add(new MailItem
            {
                Id = "10",
                ParentId = "6",
                FolderName = "Archive"
            });
            return MyFolder;
        }

        public class MailItem

        {
            public string Id { get; set; }

            public string ParentId { get; set; }

            public string FolderName { get; set; }

            public bool Expanded { get; set; }

            public bool HasSubFolders { get; set; }
        }

        public class EnabledCR
        {
            public string Id { get; set; }
            public string FolderName { get; set; }
            public bool Expanded { get; set; }
            public bool Enabled { get; set; }
            public List<EnabledCR> SubFolders { get; set; }
        }


        [Fact(Timeout = 10000, DisplayName = "Empty Initialization")]
        public void DefaultInitialize()
        {
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Default rendering with Self-Referential data source")]
        public void DefaultCase()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length;
            Assert.True(3 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(2 == ulLength, "Number of ul elements are generated properly");
            Assert.True(10 == liLength, "Number of li elements are generated properly");
        }

        [Fact(Timeout = 10000, DisplayName = "Default rendering with Hierarchical data source with unwanted binding enabled")]
        public void DefaultRenderingEnabledCase()
        {
            var data = GenerateListDatas();
            var treeview = RenderComponent<SfTreeView<EnabledCR>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<EnabledCR>>(field => field.Add(p => p.DataSource, data).Add(p => p.Id, "Id").Add(p => p.Text, "FolderName").Add(p => p.Child, "SubFolders").Add(p => p.Expanded, "Expanded")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var liLength = treeview.FindAll("li").Count;
            Assert.True(2 == ulElements.Count, "Number of ul elements are generated properly");
            Assert.True(6 == liLength, "Number of li elements are generated properly");
        }
        [Fact(Timeout = 10000, DisplayName = "Default rendering with Self-Referential data source With default properties")]
        public void DefaultCase_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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
            Assert.Null(treeview.Instance.DropArea);
            Assert.NotNull(treeview.Instance.ChildContent);
        }

        [Fact(Timeout = 10000, DisplayName = "Disabled with Self-Referential data source")]
        public void Disabled()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.Disabled, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(!treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly dyanamic update case");
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass with Self-Referential data source")]
        public void CssClass()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.CssClass, string.Empty).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.SetParametersAndRender(("CssClass", string.Empty));
            Assert.True(!treeEle.ClassList.Contains("custom"), "CssClass property working properly dynamic update case");
        }

        [Fact(Timeout = 10000, DisplayName = "RTL with Self-Referential data source")]
        public void RTL()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.EnableRtl, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(!treeEle.ClassList.Contains("e-rtl"), "RTL property working properly dynamic update case");
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded with Self-Referential data source")]
        public void EpandedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            treeview.Render();
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(4 == ulLength, "Number of ul elements are generated properly");
            Assert.True(16 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        [Fact(Timeout = 10000, DisplayName = "Selected with Self-Referential data source (without Multiselection)")]
        public void SelectedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Show Checkbox with Self-Referential data source")]
        public void ShowCheckbox()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Checked Nodes with Self-Referential data source")]
        public void CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Checked Nodes with auto check Self-Referential data source")]
        public void AutoCheck()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "FullRowSelect with Self-Referential data source")]
        public void FullRowSelect()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowSelect, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "SortData with Self-Referential data source")]
        public void SortData()
        {
            var data = GenerateListData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Navigation URL checking with Self-Referential data source")]
        public void NavigationUrl()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowNavigable, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Navigation URL checking with Self-Referential data source")]
        public void NavigationUrl_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "ID with Self-Referential data source")]
        public void ID_mapping()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ID, "tree").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "LoadOnDemand with Self-Referential data source")]
        public void LoadOnDemand()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");

            var treeload = treeview.Instance.LoadOnDemand;
            Assert.True(treeload);

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var treeload1 = treeview.Instance.LoadOnDemand;
            Assert.True(!treeload1);

        }

        [Fact(Timeout = 10000, DisplayName = "Persistence with Self-Referential data source (without Multiselection)")]
        public void EnablePersistence()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "Persistence with Self-Referential data source (with Multiselection)")]
        public void EnablePersistence_With_multiselection()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            Assert.True(2 == selectedLi, "Number of selected LI generated properly with multiselection");
            dataUid = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("EU" == dataUid, "Data Uid of selected LI not generated properly with multiselection with persistence");
            dataUid = treeEle1.QuerySelectorAll("li.e-active")[1].GetAttribute("data-uid");
            Assert.True("SA" == dataUid, "Data Uid of selected LI not generated properly with multiselection with persistence");
        }

        [Fact(Timeout = 10000, DisplayName = "Persistence testing for ExpandedNodes - BLAZ-16114")]
        public void ExpandEnablePersistence()
        {
            var data = GenerateMailItems();
            var treeview = RenderComponent<SfTreeView<MailItem>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { }).Add(p => p.ID, "tree-1").Add(p => p.EnablePersistence, true).AddChildContent<TreeViewFieldsSettings<MailItem>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "ParentId").Add(p => p.Id, "Id").Add(p => p.Text, "FolderName").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasSubFolders")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            Assert.True(1 == ulLength, "Number of ul elements are generated properly");
            Assert.True(2 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "1" }));
            liLength = treeview.FindAll("li").Count;
            Assert.True(3 == liLength, "Number of li elements are generated properly after dynamic update");
            treeview.Render<SfTreeView<MailItem>>();
            liLength = treeview.FindAll("li").Count;
            Assert.True(3 == liLength, "Number of li elements are generated properly after page refresh");
        }

        [Fact(Timeout = 10000, DisplayName = "LoadOndemand")]
        public void LoadOnDemand_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var liElements1 = treeview.FindAll("li");
            var liLength1 = treeview.FindAll("li").Count;

            Assert.True(27 == liLength1, "Number of li elements are not generated properly");
        }

        // // RTL Combination cases

        [Fact(Timeout = 10000, DisplayName = "RTL and Autocheck with Self-Referential data source")]
        public void RTL_with_autocheck()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "RTL with Checked Nodes with auto check Self-Referential data source")]
        public void RTl_CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.EnableRtl, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "RTL_CssClass with Self-Referential data source")]
        public void RTL_CssClass()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.EnableRtl, true).Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            treeview.SetParametersAndRender(("CssClass", "custom"));
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

        }

        [Fact(Timeout = 10000, DisplayName = "RTL with_Disabled Self-Referential data source")]
        public void RTL_Disabled()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.Disabled, true).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property not working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

        }

        [Fact(Timeout = 10000, DisplayName = "RTL With FullRowSelect with Self-Referential data source")]
        public void RTL_FullRowSelect()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowSelect, false).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");
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
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "RTL_ID with Self-Referential data source")]
        public void RTL_with_ID_mapping()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            treeview.Find("#tree");
            treeview.SetParametersAndRender(("EnableRTL", true));
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "RTL_LoadOndemand")]
        public void RTL_LoadOnDemand_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var liElements1 = treeview.FindAll("li");
            var liLength1 = treeview.FindAll("li").Count;

            Assert.True(27 == liLength1, "Number of li elements are not generated properly");
        }

        [Fact(Timeout = 10000, DisplayName = "RTL Navigation URL checking with Self-Referential data source")]
        public void RTL_NavigationUrl_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.NavigateUrl, "Link").Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "RTL_Persistence with Self-Referential data source (without Multiselection)")]
        public void RTL_EnablePersistence()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "RTL Enabled Show Checkbox with Self-Referential data source")]
        public void RTL_ShowCheckbox()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "RTL and Selected with Self-Referential data source (without Multiselection)")]
        public void RTL_SelectedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "RTL and SortData with Self-Referential data source")]
        public void RTL_SortData()
        {
            var data = GenerateListData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "RTL and Expanded with Self-Referential data source")]
        public void RTL_EpandedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).Add(p => p.EnableRtl, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            treeview.Render();
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-rtl"), "RTL property not working properly");

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(4 == ulLength, "Number of ul elements are generated properly");
            Assert.True(16 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        // // Disabled property combinations

        [Fact(Timeout = 10000, DisplayName = "Disabled and Checked Nodes with auto check Self-Referential data source")]
        public void Disabled_AutoCheck()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.Disabled, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Disabled and Checked Nodes with Self-Referential data source")]
        public void Disabled_CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.Disabled, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Disabled and CssClass with Self-Referential data source")]
        public void Disabled_CssClass()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.CssClass, "custom").Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property not working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property working properly initial case");

        }

        [Fact(Timeout = 10000, DisplayName = "Disabled and FullRowSelect with Self-Referential data source")]
        public void Disabled_FullRowSelect()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowSelect, true).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Disabled and ID with Self-Referential data source")]
        public void Disabled_ID_mapping()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "Disabled and LoadOndemand")]
        public void Disabled_LoadOnDemand_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            Assert.True(10 == liLength, "Number of li elements are not generated properly");

        }

        [Fact(Timeout = 10000, DisplayName = "Disabled and Navigation URL checking with Self-Referential data source")]
        public void Disabled_NavigationUrl_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "Disabled and Persistence with Self-Referential data source (without Multiselection)")]
        public void Disabled_EnablePersistence()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "Disabled and Show Checkbox with Self-Referential data source")]
        public void Disabled_ShowCheckbox()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Disabled and Selected with Self-Referential data source (without Multiselection)")]
        public void Disabled_SelectedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

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


        [Fact(Timeout = 10000, DisplayName = "Disabled and SortData with Self-Referential data source")]
        public void Disabled_SortData()
        {
            var data = GenerateListData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };

            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "Disabled and Expanded with Self-Referential data source")]
        public void Disabled_EpandedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).Add(p => p.Disabled, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            treeview.Render();
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("e-disabled"), "Disabled property working properly initial case");
            Assert.Contains("e-disabled", treeEle.ClassName);

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(4 == ulLength, "Number of ul elements are generated properly");
            Assert.True(16 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        // // CssClass Property Combinations

        [Fact(Timeout = 10000, DisplayName = "CssClass and Autocheck Self-Referential data source")]
        public void CssClass_Autocheck()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CssClass, "custom").Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "CssClass and Checked Nodes with Self-Referential data source")]
        public void CssClass_CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CssClass, "custom").Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(Timeout = 10000, DisplayName = "CssClass and FullRowSelect with Self-Referential data source")]
        public void CssClass_FullRowSelect()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowSelect, false).Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            for (var i = 0; i < liElements.Count; i++)
            {
                var fullRowEle = liElements[i].QuerySelector(".e-fullrow");
                Assert.True(fullRowEle != null, "FullRowSelect property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and ID with Self-Referential data source")]
        public void CssClass_ID_mapping()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ID, "tree").Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            treeview.Find("#tree");
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and LoadOndemand")]
        public void CssClass_LoadOnDemand_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            Assert.True(treeEle.ClassList.Contains("custom"), "CssClass property not working properly initial case");

            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            treeview.SetParametersAndRender(("LoadOnDemand", false));
            var liElements1 = treeview.FindAll("li");
            var liLength1 = treeview.FindAll("li").Count;

            Assert.True(27 == liLength1, "Number of li elements are not generated properly");
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and Navigation URL checking with Self-Referential data source")]
        public void CssClass_NavigationUrl_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "CssClass and Persistence with Self-Referential data source (without Multiselection)")]
        public void CssClass_EnablePersistence()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.CssClass, "custom").Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");

        }

        [Fact(Timeout = 10000, DisplayName = "CssClass and Show Checkbox with Self-Referential data source")]
        public void CssClass_ShowCheckbox()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "CssClass and Selected with Self-Referential data source (without Multiselection)")]
        public void CssClass_SelectedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "CssClass and SortData with Self-Referential data source")]
        public void CssClass_SortData()
        {
            var data = GenerateListData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };

            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).Add(p => p.CssClass, "custom").AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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

        [Fact(Timeout = 10000, DisplayName = "CssClass and Expanded with Self-Referential data source")]
        public void CssClass_EpandedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            treeview.Render();
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(4 == ulLength, "Number of ul elements are generated properly");
            Assert.True(16 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        // // ID Combination test cases

        [Fact(Timeout = 10000, DisplayName = "ID and  auto check Hierachial data source")]
        public void ID_AutoCheck()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.AutoCheck, false).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
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

        [Fact(Timeout = 10000, DisplayName = "ID and Checked Nodes with Hierachial data source")]
        public void ID_CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

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

        [Fact(Timeout = 10000, DisplayName = "ID and FullRowSelect with Hierachial data source")]
        public void ID_FullRowSelect()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowSelect, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
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

        [Fact(Timeout = 10000, DisplayName = "ID and LoadOndemand")]
        public void ID_LoadOnDemand_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            Assert.True(10 == liLength, "Number of li elements are not generated properly");
        }

        [Fact(Timeout = 10000, DisplayName = "ID and Navigation URL checking with Hierachial data source")]
        public void ID_NavigationUrl_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

            treeview.SetParametersAndRender(("ID", "tree"));
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "ID and Show Checkbox with Hierachial data source")]
        public void ID_ShowCheckbox()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
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

        [Fact(Timeout = 10000, DisplayName = "ID and Selected with Hierachial data source (without Multiselection)")]
        public void ID_SelectedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

        }

        [Fact(Timeout = 10000, DisplayName = "ID and SortData with Hierachial data source")]
        public void ID_SortData()
        {
            var data = GenerateListData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
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

        [Fact(Timeout = 10000, DisplayName = "ID and Expanded with Hierachial data source")]
        public void ID_EpandedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");

            treeview.SetParametersAndRender(("ID", "tree"));
            var treeid = treeview.Instance.ID;
            Assert.Equal("tree", treeid);

            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(4 == ulLength, "Number of ul elements are generated properly");
            Assert.True(16 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        // // Autocheck Property combination cases

        [Fact(Timeout = 10000, DisplayName = "AutoCheck and FullRowSelect with Hierachial data source")]
        public void AutoCheck_FullRowSelect()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");

            var liLength = treeview.FindAll("li").Count;
            liElements = treeview.FindAll("li");
            liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            Assert.Contains("e-fullrow-wrap", treeEle.ClassName);
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

        [Fact(Timeout = 10000, DisplayName = "Autocheck and LoadOndemand")]
        public void AutoCheck_LoadOnDemand_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.LoadOnDemand, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            var liElements = treeview.FindAll("li");
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Autocheck and Navigation URL checking with Hierachial data source")]
        public void Autocheck_NavigationUrl_1()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();

            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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

        [Fact(DisplayName = "Autocheck and Expanded with Hierachial data source")]
        public void Autocheck_EpandedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.AutoCheck, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            treeview.Render();
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var liElements = treeview.FindAll("li");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("ShowCheckBox", true));
            treeview.SetParametersAndRender(("AutoCheck", true));

            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(4 == ulLength, "Number of ul elements are generated properly");
            Assert.True(16 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
            for (var i = 0; i < liLength; i++)
            {
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        [Fact(Timeout = 10000, DisplayName = "Autocheck and Checked Nodes with Hierachial data source")]
        public void Autocheck_CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
                var checkboEle = liElements[i].QuerySelector(".e-text-content .e-icon-wrapper .e-check");
                Assert.True(checkboEle == null, "AutoCheck property is working properly");
            }
        }

        // // LoadOn Demand property combination

        [Fact(Timeout = 10000, DisplayName = "Loadondemand and Checked Nodes with Hierachial data source")]
        public void LoadOnDemand_CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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
        }

        [Fact(Timeout = 10000, DisplayName = "LoadonDemand and FullRowSelect with Hierachial data source")]
        public void Loadondemand_FullRowSelect()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowSelect, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowNavigable, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;
            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");
            treeview.SetParametersAndRender(("EnablePersistence", true));
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "Loadondemand and Show Checkbox with Hierachial data source")]
        public void Loadondemand_ShowCheckbox()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            Assert.True(10 == liLength, "Number of li elements are not generated properly");

        }

        [Fact(Timeout = 10000, DisplayName = "Loadondemand and Selected with Hierachial data source (without Multiselection)")]
        public void Loadondemand_SelectedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;
            var selectedLi = treeEle.QuerySelectorAll("li.e-active").Length;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(10 == liLength, "Number of li elements are not generated properly");

            Assert.True(1 == selectedLi, "Number of selected LI generated properly without multiselection");
            var dataUid = treeEle.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == dataUid, "Data Uid of selected LI generated properly without multiselection");

        }

        [Fact(Timeout = 10000, DisplayName = "Loadondemand and SortData with Hierachial data source")]
        public void Loadondemand_SortData()
        {
            var data = GenerateListData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            treeview.Render();
            var treeEle = treeview.Find(".e-treeview");
            var ulElements = treeview.FindAll("ul");
            var ulLength = treeview.FindAll("ul").Count;
            var liLength = treeview.FindAll("li").Count;

            treeview.SetParametersAndRender(("LoadOnDemand", true));
            Assert.True(16 == liLength, "Number of li elements are not generated properly");

            var expandedLiCount = ulElements[1].QuerySelectorAll("li").Length + ulElements[2].QuerySelectorAll("li").Length;
            Assert.True(6 == expandedLiCount, "Number of li in expanded is generated properly");
            Assert.True(4 == ulLength, "Number of ul elements are generated properly");
            Assert.True(16 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

        // // Fullrownavigable property combination cases

        [Fact(Timeout = 10000, DisplayName = "FullrowNavigable and Checked Nodes with Hierachial data source")]
        public void Fullrownavigable_CheckedNodes()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).Add(p => p.CheckedNodes, new string[] { "NA", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.FullRowSelect, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            treeview.Render<SfTreeView<Listdata>>();
            var treeEle1 = treeview.Find(".e-treeview");
            var persistence = treeEle1.QuerySelectorAll("li.e-active")[0].GetAttribute("data-uid");
            Assert.True("AS" == persistence, "Data Uid of not selected LI generated for persistence enabled");


        }

        [Fact(Timeout = 10000, DisplayName = "Fullrownavigable and Show Checkbox with Hierachial data source")]
        public void Fullrownavigable_ShowCheckbox()
        {
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ShowCheckBox, true).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SelectedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var ascendingData = new string[] { "Africa", "Antartica", "Asia", "Europe", "North America", "Cuba", "Mexico", "United States of America", "Oceania", "South America" };
            var descendingData = new string[] { "South America", "Oceania", "North America", "United States of America", "Mexico", "Cuba", "Europe", "Asia", "Antartica", "Africa" };
            var NoneData = new string[] { "North America", "United States of America", "Cuba", "Mexico", "Africa", "Asia", "Europe", "South America", "Oceania", "Antartica" };
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.SortOrder, Blazor.Navigations.SortOrder.Ascending).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));

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
            var data = GenerateListData();
            var treeview = RenderComponent<SfTreeView<Listdata>>(Parameter => Parameter.Add(p => p.ExpandedNodes, new string[] { "AS", "AF" }).AddChildContent<TreeViewFieldsSettings<Listdata>>(field => field.Add(p => p.DataSource, data).Add(p => p.ParentID, "Pid").Add(p => p.Id, "Id").Add(p => p.Text, "Name").Add(p => p.Expanded, "Expanded").Add(p => p.HasChildren, "HasChild")));
            treeview.Render();
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
            Assert.True(4 == ulLength, "Number of ul elements are generated properly");
            Assert.True(16 == liLength, "Number of li elements are generated properly");
            treeview.SetParametersAndRender(("ExpandedNodes", new string[] { "NA" }));
            ulLength = treeview.FindAll("ul").Count;
            liLength = treeview.FindAll("li").Count;
            Assert.True(4 == ulLength, "Number of ul elements are generated properly after dynamic update");
            Assert.True(16 == liLength, "Number of li elements are generated properly after dynamic update");
        }

    }
}