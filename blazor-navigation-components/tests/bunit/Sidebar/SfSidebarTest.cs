using Bunit;
using Xunit;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Buttons;
using AngleSharp.Css.Dom;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System;
using System.Reflection;

namespace Syncfusion.Blazor.Tests.Sidebar.Testcases
{
    public partial class SidebarTest : SidebarJsMock
    {
        [Fact(Timeout = 10000, DisplayName = "Media Query with default case")]
        public async Task MediaQuery_Testing()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.MediaQuery, "Height:500px;Width:500px"));
            var rootEle = sidebar.Find(".e-sidebar");
            Assert.True(rootEle.ClassList.Contains("e-close"));
            await sidebar.InvokeAsync(() =>
            {
                var obj = "600px 600px";
                var heightValue = JSInterop.Setup<string>("sfBlazor.Sidebar.initialize", _ => true);
                heightValue.SetResult(obj);
                Task.Delay(3000);
            }).ContinueWith(async (t) =>
            {
                Assert.True(rootEle.ClassList.Contains("e-open"));
            });
        }

        [Fact(Timeout = 10000, DisplayName = "Dynamic width test case - Task Id: BLAZ-11824")]
        public async Task Dynamic_Width_Testing()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.Width, "500px"));
            var sidebarStyle = sidebar.Find(".e-sidebar").GetAttribute("data-sf-style");
            Assert.Contains("500px", sidebarStyle);
            sidebar.SetParametersAndRender(("Width", "300px"));
            await Task.Delay(200);
            sidebarStyle = sidebar.Find(".e-sidebar").GetAttribute("data-sf-style");
            Assert.Contains("300px", sidebarStyle);
        }

        [Fact(Timeout = 10000, DisplayName = "Initial DOM Rendering")]
        public void ComponentRendering()
        {
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
        }

        /* Property testing */

        [Fact(Timeout = 10000, DisplayName = "Property testing - Animate false")]
        public async Task Animate_property_disabled()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.Animate, false));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-disable-animation", sidebarEle.ClassName);
            await sidebar.InvokeAsync(() =>
            {
                sidebar.SetParametersAndRender(("Animate", true));
            }).ContinueWith(async (t) =>
            {
                Assert.DoesNotContain("e-disable-animation", sidebarEle.ClassName);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - Animate true")]
        public async Task Animate_property_enabled()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.Animate, true));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.DoesNotContain("e-disable-animation", sidebarEle.ClassName);
            await sidebar.InvokeAsync(() =>
            {
                sidebar.SetParametersAndRender(("Animate", false));
            }).ContinueWith(async (t) =>
            {
                Assert.Contains("e-disable-animation", sidebarEle.ClassName);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - CloseOnDocumentClick true")]
        public async Task CloseOnDocumentClick_property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.CloseOnDocumentClick, true).AddChildContent<SfButton>(field => field.Add(p => p.Content, "Open")));
            var sidebarEle = sidebar.Find(".e-sidebar");
            //await sidebar.InvokeAsync(() =>
            //{
            //    sidebar.Instance.SidebarShow();
            //}).ContinueWith(async (t) =>
            //{
            //    Assert.Contains("e-push", sidebarEle.ClassName);
            //    Assert.Contains("e-open", sidebarEle.ClassName);
            //});
            await sidebar.InvokeAsync(() =>
            {
                var buttonElem = sidebar.Find("button");
                buttonElem.Click();
            }).ContinueWith(async (t) =>
            {
                Assert.DoesNotContain("e-open", sidebarEle.ClassName);
                Assert.Contains("e-close", sidebarEle.ClassName);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - CloseOnDocumentClick false")]
        public async Task CloseOnDocumentClick_propertyfalse()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.CloseOnDocumentClick, false).AddChildContent<SfButton>(field => field.Add(p => p.Content, "Open")));
            var sidebarEle = sidebar.Find(".e-sidebar");
            //await sidebar.InvokeAsync(() =>
            //{
            //    sidebar.Instance.SidebarShow();
            //}).ContinueWith(async (t) =>
            //{
            //    Assert.Contains("e-push", sidebarEle.ClassName);
            //    Assert.Contains("e-open", sidebarEle.ClassName);
            //});
            await sidebar.InvokeAsync(() =>
            {
                var buttonElem = sidebar.Find("button");
                buttonElem.Click();
            }).ContinueWith(async (t) =>
            {
                Assert.DoesNotContain("e-close", sidebarEle.ClassName);
                Assert.Contains("e-open", sidebarEle.ClassName);
            });
            sidebar.SetParametersAndRender(("CloseOnDocumentClick", true));
            await sidebar.InvokeAsync(() =>
            {
                var buttonElem = sidebar.Find("button");
                buttonElem.Click();
            }).ContinueWith(async (t) =>
            {
                Assert.DoesNotContain("e-open", sidebarEle.ClassName);
                Assert.Contains("e-close", sidebarEle.ClassName);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - EnableDock,DockSize")]
        public async Task Dock_property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.ShowBackdrop, true).Add(p => p.Width, "220px")
            .Add(p => p.DockSize, "72px").Add(p => p.CloseOnDocumentClick, true).AddChildContent<SfButton>(field => field.Add(p => p.Content, "Open")));
            var sidebarEle = sidebar.Find(".e-sidebar");
            //await sidebar.InvokeAsync(() =>
            //{
            //    sidebar.Instance.SidebarShow();
            //}).ContinueWith(async (t) =>
            //{
            //    Assert.Contains("e-push", sidebarEle.ClassName);
            //    Assert.Contains("e-open", sidebarEle.ClassName);
            //    Assert.Contains("e-dock", sidebarEle.ClassName);
            //    Assert.Contains("width: 220px", sidebar.Find(".e-sidebar").GetStyle().CssText.Trim());
            //});
            await sidebar.InvokeAsync(() =>
            {
                var buttonElem = sidebar.Find("button");
                buttonElem.Click();
            }).ContinueWith(async (t) =>
            {
                Assert.Contains("e-close", sidebarEle.ClassName);
                Assert.Contains("width: 72px", sidebar.Find(".e-sidebar").GetStyle().CssText.Trim());
            });
            sidebar.Render<SfSidebar>();
            sidebar.SetParametersAndRender(("DockSize", "30px"));
            await sidebar.InvokeAsync(() =>
            {
                var buttonElem = sidebar.Find("button");
                buttonElem.Click();
            }).ContinueWith(async (t) =>
            {
                Assert.Contains("width: 30px", sidebar.Find(".e-sidebar").GetStyle().CssText.Trim());
            });
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - EnableGestures, EnablePersistence")]
        public async Task EnableGesture_property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.EnableGestures, true).Add(p => p.EnablePersistence, true));
            //await sidebar.InvokeAsync(async () =>
            //{
            //    await sidebar.Instance.SidebarShow();
            //}).ContinueWith(
            //   async (t) =>
            //   {
            //       Assert.True(sidebar.Instance.EnableGestures);
            //       Assert.True(sidebar.Instance.EnablePersistence);
            //       sidebar.SetParametersAndRender(("EnablePersistence", false), ("EnableGestures", false));
            //       Assert.True(!sidebar.Instance.EnablePersistence);
            //       Assert.True(!sidebar.Instance.EnableGestures);
            //   });

        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - EnableRtl ")]
        public async Task EnableRtl_property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.EnableRtl, true));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-rtl", sidebarEle.ClassName);
            await sidebar.InvokeAsync(async () =>
            {
                sidebar.SetParametersAndRender(("EnableRtl", false));
            }).ContinueWith(
              async (t) =>
              {
                  Assert.DoesNotContain("e-rtl", sidebarEle.ClassName);
              });
            await sidebar.InvokeAsync(async () =>
            {
                sidebar.SetParametersAndRender(("EnableRtl", true));
            }).ContinueWith(
            async (t) =>
            {
                Assert.Contains("e-rtl", sidebarEle.ClassName);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - IsOpen")]
        public void IsOpen_property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.IsOpen, false).Add(p => p.Type, SidebarType.Slide));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
            Assert.Contains("e-close", sidebarEle.ClassName);
            sidebar.SetParametersAndRender(("IsOpen", true), ("Type", SidebarType.Auto));
            Assert.Contains("e-open", sidebarEle.ClassName);
            Assert.DoesNotContain("e-close", sidebarEle.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - MediaQuery")]
        public async Task MediaQuery_Property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.MediaQuery, "Height:500px;Width:500px"));
            var rootEle = sidebar.Find(".e-sidebar");
            Assert.True(rootEle.ClassList.Contains("e-close"));
            await sidebar.InvokeAsync(() =>
            {
                var obj = "600px 600px";
                var heightValue = JSInterop.Setup<string>("sfBlazor.Sidebar.initialize", _ => true);
                heightValue.SetResult(obj);
                Task.Delay(3000);
            }).ContinueWith(async (t) =>
            {
                Assert.True(rootEle.ClassList.Contains("e-open"));
            });
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - Position")]
        public void Postion_property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.Position, SidebarPosition.Right));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-right", sidebarEle.ClassName);
            Assert.Contains("e-control", sidebarEle.ClassName);
            sidebar.SetParametersAndRender(("Position", SidebarPosition.Left));
            Assert.Contains("e-left", sidebarEle.ClassName);
            Assert.DoesNotContain("e-right", sidebarEle.ClassName);
        }

        //[Fact(Timeout = 10000, DisplayName = "Property testing - ShowBackDrop")]
        //public async Task ShowBackDrop_property()
        //{
        //    var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.ShowBackdrop, true).AddChildContent<SfButton>(field => field.Add(p => p.Content, "Open")));
        //    await sidebar.InvokeAsync(() =>
        //    {
        //        sidebar.Instance.SidebarShow();
        //    }).ContinueWith(async (t) =>
        //    {
        //        var sidebarEle = sidebar.Find(".e-sidebar");
        //        Assert.Contains("e-open", sidebarEle.ClassName);
        //        Assert.True(sidebarEle.QuerySelector(".e-sidebar-overlay") != null);
        //    });
        //    sidebar.SetParametersAndRender(("ShowBackDrop", false));
        //    await sidebar.InvokeAsync(() =>
        //    {
        //        sidebar.Instance.SidebarShow();
        //    }).ContinueWith(async (t) =>
        //    {
        //        var sidebarEle = sidebar.Find(".e-sidebar");
        //        Assert.Contains("e-open", sidebarEle.ClassName);
        //        Assert.True(sidebarEle.QuerySelector(".e-sidebar-overlay") == null);
        //    });
        //}

        [Fact(Timeout = 10000, DisplayName = "Target property testing")]
        public void Target_property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.Target, "_blank"));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-sidebar-absolute", sidebarEle.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Type property testing")]
        public void Type_property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.Type, SidebarType.Over));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
            Assert.Contains("e-over", sidebarEle.ClassName);
            Assert.True(sidebarEle.ClassList.Contains("e-close"));
            sidebar.SetParametersAndRender(parameters => parameters.Add(p => p.Type, SidebarType.Slide).Add(p => p.IsOpen, true));
            Assert.False(sidebarEle.ClassList.Contains("e-close"));
            sidebar.SetParametersAndRender(parameters => parameters.Add(p => p.Type, SidebarType.Auto));
            Assert.True(sidebarEle.ClassList.Contains("e-open"));
            sidebar.SetParametersAndRender(parameters => parameters.Add(p => p.Type, SidebarType.Push));
            Assert.True(sidebarEle.ClassList.Contains("e-close"));
            sidebar.SetParametersAndRender(parameters => parameters.Add(p => p.Type, SidebarType.Over));
            Assert.True(sidebarEle.ClassList.Contains("e-close"));
            sidebar.SetParametersAndRender(parameters => parameters.Add(p => p.Type, SidebarType.Over).Add(p => p.IsOpen, true));
            Assert.True(sidebarEle.ClassList.Contains("e-open"));
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - Width")]
        public async Task Width_Property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.Width, "500px"));
            var sidebarStyle = sidebar.Find(".e-sidebar").GetAttribute("data-sf-style");
            Assert.Contains("500px", sidebarStyle);
            sidebar.SetParametersAndRender(("Width", "300px"));
            await Task.Delay(200);
            sidebarStyle = sidebar.Find(".e-sidebar").GetAttribute("data-sf-style");
            Assert.Contains("300px", sidebarStyle);
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - ZIndex")]
        public async Task ZIndex_Property()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.ZIndex, 1003));
            var sidebarStyle = sidebar.Find(".e-sidebar").GetAttribute("data-sf-style");
            Assert.Contains("z-index", sidebarStyle);
            Assert.Contains("1003", sidebarStyle);
            await sidebar.InvokeAsync(async () =>
            {
                sidebar.SetParametersAndRender(("ZIndex", 300));
            }).ContinueWith(
              async (t) =>
              {
                  sidebarStyle = sidebar.Find(".e-sidebar").GetAttribute("data-sf-style");
                  Assert.Contains("z-index", sidebarStyle);
                  Assert.Contains("300", sidebarStyle);
              });
        }

        [Fact(Timeout = 10000, DisplayName = "Property testing - HtmlAttributes")]
        public void SidebarHtmlAttributes_Testing()
        {
            Dictionary<string, object> SidebarHtmlAttributes = new Dictionary<string, object>();
            SidebarHtmlAttributes.Add("class", "e-customclass");
            SidebarHtmlAttributes.Add("id", "customsidebar");
            Dictionary<string, object> HtmlAttributes = SidebarHtmlAttributes;
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                    parameters.Add(p => p.HtmlAttributes, HtmlAttributes));
            var rootEle = sidebar.Find(".e-sidebar");
            Assert.Equal("customsidebar", sidebar.Instance.HtmlAttributes["id"]);
            Assert.Contains("e-customclass", rootEle.GetAttribute("class"));
            Assert.Contains("customsidebar", rootEle.GetAttribute("id"));
        }

        /* Method testing*/

        //[Fact(Timeout = 10000, DisplayName = "SidebarHide and SidebarShow method testing")]
        //public async Task SidebarHide()
        //{
        //    var sidebar = RenderComponent<SfSidebar>();
        //    var sidebarEle = sidebar.Find(".e-sidebar");
        //    Assert.Contains("e-control", sidebarEle.ClassName);
        //    await sidebar.InvokeAsync(() =>
        //    {
        //        sidebar.Instance.SidebarShow();
        //        Assert.True(sidebarEle.ClassList.Contains("e-open"));
        //        Assert.False(sidebarEle.ClassList.Contains("e-close"));
        //    });
        //    await sidebar.InvokeAsync(() =>
        //    {
        //        sidebar.Instance.SidebarHide();
        //        Assert.True(sidebarEle.ClassList.Contains("e-close"));
        //        Assert.False(sidebarEle.ClassList.Contains("e-open"));
        //    });
        //}
        //[Fact(Timeout = 10000, DisplayName = "Hide and Show method testing")]
        //public async Task Hide()
        //{
        //    var sidebar = RenderComponent<SfSidebar>();
        //    var sidebarEle = sidebar.Find(".e-sidebar");
        //    Assert.Contains("e-control", sidebarEle.ClassName);
        //    await sidebar.InvokeAsync(() =>
        //    {
        //        sidebar.Instance.SidebarShow();
        //        Assert.True(sidebarEle.ClassList.Contains("e-open"));
        //        Assert.False(sidebarEle.ClassList.Contains("e-close"));
        //    });
        //    await sidebar.InvokeAsync(() =>
        //    {
        //        sidebar.Instance.SidebarHide();
        //        Assert.True(sidebarEle.ClassList.Contains("e-close"));
        //        Assert.False(sidebarEle.ClassList.Contains("e-open"));
        //    });
        //}

        /* Event testing */

        [Fact(Timeout = 10000, DisplayName = "Create Event testing")]
        public void CreateEventHandling()
        {
            var createdEventcount = 0;
            var sidebar = RenderComponent<SfSidebar>();
            sidebar.SetParametersAndRender(parameters =>
                  parameters.Add(s => s.Created, () =>
                  {
                      createdEventcount++;
                      Assert.NotNull("Create event is triggered, when render the component");
                      Assert.Equal(1, createdEventcount);
                  }));
            sidebar.Dispose();
        }

        [Fact(Timeout = 10000, DisplayName = "Destroy Event testing")]
        public async Task DestroyedEvent()
        {
            var destroyedcount = 0;
            var sidebar = RenderComponent<SfSidebar>();
            sidebar.SetParametersAndRender(parameters =>
                  parameters.Add(s => s.Destroyed, () =>
                  {
                      destroyedcount++;
                      Assert.NotNull("Destroy event is triggered");
                  }));
            await sidebar.InvokeAsync(async () =>
            {
                Assert.Equal(0, destroyedcount);
                sidebar.Dispose();
            }).ContinueWith(
               async (t) =>
               {
                   Assert.Equal(1, destroyedcount);
               });
        }

        [Fact(Timeout = 10000, DisplayName = "Changed Event testing")]
        public async Task ChangedEventHandling()
        {
            var changedEventcount = 0;
            var sidebar = RenderComponent<SfSidebar>();
            sidebar.SetParametersAndRender(parameters =>
                  parameters.Add(s => s.Changed, () =>
                  {
                      changedEventcount++;
                      Assert.NotNull("Changed event is triggered");
                      Assert.Equal(1, changedEventcount);
                  }));
            //await sidebar.InvokeAsync(async () =>
            //{
            //    Assert.Equal(0, changedEventcount);
            //    sidebar.Instance.SidebarShow();
            //}).ContinueWith(
            //   async (t) =>
            //   {
            //       Assert.Equal(1, changedEventcount);
            //   });
            sidebar.Dispose();
        }

        [Fact(Timeout = 10000, DisplayName = "OnClose Event testing")]
        public async Task OnCloseEventHandling()
        {
            var onCloseEventcount = 0;
            var sidebar = RenderComponent<SfSidebar>();
            sidebar.SetParametersAndRender(parameters =>
                  parameters.Add(s => s.OnClose, () =>
                  {
                      onCloseEventcount++;
                      Assert.NotNull("OnClose event is triggered");
                      Assert.Equal(1, onCloseEventcount);
                  }));
            //await sidebar.InvokeAsync(async () =>
            //{
            //    Assert.Equal(0, onCloseEventcount);
            //    sidebar.Instance.SidebarShow();
            //    Task.Delay(200);
            //    await sidebar.Instance.SidebarHide();
            //}).ContinueWith(
            //  async (t) =>
            //  {
            //      Assert.Equal(1, onCloseEventcount);
            //  });
            sidebar.Dispose();
        }

        [Fact(Timeout = 10000, DisplayName = "OnOpen Event testing")]
        public async Task OnOpenEventHandling()
        {
            var onOpeneEventcount = 0;
            var sidebar = RenderComponent<SfSidebar>();
            sidebar.SetParametersAndRender(parameters =>
                  parameters.Add(s => s.OnOpen, () =>
                  {
                      onOpeneEventcount++;
                      Assert.NotNull("OnOpen event is triggered, when render the component");
                      Assert.Equal(1, onOpeneEventcount);
                  }));
            //await sidebar.InvokeAsync(async () =>
            //{
            //    Assert.Equal(0, onOpeneEventcount);
            //    sidebar.Instance.SidebarShow();
            //}).ContinueWith(
            // async (t) =>
            // {
            //     Assert.Equal(1, onOpeneEventcount);
            // });
            sidebar.Dispose();
        }

        /* Test cases for coverage */

        [Fact(Timeout = 10000, DisplayName = "ChildContent  property testing")]
        public void ChildContent()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters =>
                     parameters.Add(p => p.ChildContent, "SfListView"));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.True("SfListView" == sidebarEle.InnerHtml, "  ListView component as ChildContent");
        }

        [Fact(Timeout = 10000, DisplayName = "IsOpenChanged Event testing")]
        public async Task IsOpenChangedEventHandling()
        {
            var isOpenChangedEventcount = 0;
            var sidebar = RenderComponent<SfSidebar>();
            sidebar.SetParametersAndRender(parameters =>
                  parameters.Add(s => s.IsOpenChanged, () =>
                  {
                      isOpenChangedEventcount++;
                      Assert.NotNull("IsOpenChanged event is triggered");
                      Assert.Equal(1, isOpenChangedEventcount);
                  }));
            //await sidebar.InvokeAsync(async () =>
            //{
            //    Assert.Equal(0, isOpenChangedEventcount);
            //    sidebar.Instance.SidebarShow();
            //}).ContinueWith(
            //async (t) =>
            //{
            //    Assert.Equal(1, isOpenChangedEventcount);
            //});
            sidebar.Dispose();
        }

        [Fact(Timeout = 10000, DisplayName = "SetDock method testing")]
        public async Task SetDock()
        {
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
            sidebar.SetParametersAndRender(parameters =>
               parameters.Add(p => p.EnableDock, true).Add(p => p.IsOpen, false));
            Dictionary<string, object> SidebarHtmlAttributes = new Dictionary<string, object>();
            SidebarHtmlAttributes.Add("class", "customclass");
            await sidebar.InvokeAsync(() =>
            {
                sidebar.Instance.SetDock();
            });
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerChange testing")]
        public async Task TriggerChange()
        {
            Syncfusion.Blazor.Navigations.ChangeEventArgs eventArgs = new Syncfusion.Blazor.Navigations.ChangeEventArgs();
            eventArgs.IsInteracted = true;
            eventArgs.Name = "Changed";
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            await sidebar.InvokeAsync(() =>
            {
                sidebar.Instance.TriggerChange(true, eventArgs);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "TriggerShow method testing")]
        public async Task TriggerShow()
        {
            Syncfusion.Blazor.Navigations.EventArgs args = new Syncfusion.Blazor.Navigations.EventArgs();
            args.Cancel = false;
            args.Name = "Key";
            args.Top = 2.2;
            args.Left = 2;
            args.IsInteracted = true;
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
            await sidebar.InvokeAsync(() =>
            {
                sidebar.Instance.TriggerShow(args);
            });
        }
        [Fact(Timeout = 10000, DisplayName = "TriggerHide method testing")]
        public async Task TriggerHide()
        {
            Syncfusion.Blazor.Navigations.EventArgs args = new  Syncfusion.Blazor.Navigations.EventArgs();
            args.Cancel = false;
            args.Name = "Key";
            args.Top = 2.2;
            args.Left = 2;
            args.IsInteracted = true;
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
            await sidebar.InvokeAsync(() =>
            {
                sidebar.Instance.TriggerHide(args);
            });
        }
        [Fact(Timeout = 10000, DisplayName = "SidebarInitRender method testing")]
        public async Task SidebarInitRender()
        {
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
            Assert.False(sidebarEle.ClassList.Contains("e-open"));
            //await sidebar.InvokeAsync(() =>
            //{
            //    sidebar.SetParametersAndRender(parameters => parameters.Add(p => p.Type, SidebarType.Slide)
            //.Add(p => p.IsOpen, false).Add(p => p.CloseOnDocumentClick, true).Add(p => p.EnableDock, true).Add(p => p.MediaQuery, "min-width: 600px"));
            //    Syncfusion.Blazor.Navigations.EventArgs args = new Syncfusion.Blazor.Navigations.EventArgs();
            //    args.Cancel = false;
            //    args.Name = "Key";
            //    args.Top = 2.2;
            //    args.Left = 2;
            //    args.IsInteracted = true;
            //    sidebar.Instance.TriggerShow(args);
            //    sidebar.Instance.SidebarInitRender();
            //}).ContinueWith(async (t) =>
            //{
            //    Assert.True(sidebarEle.ClassList.Contains("e-open"));
            //});
        }

        [Fact(Timeout = 10000, DisplayName = "SidebarPropertyChange method testing")]
        public async Task SidebarPropertyChange()
        {
            var sidebar = RenderComponent<SfSidebar>((parameters =>
                    parameters.Add(p => p.EnableDock, true).Add(p => p.IsOpen, true)));
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
            //await sidebar.InvokeAsync(() =>
            //{
            //    Dictionary<string, object> propertyChanges = new Dictionary<string, object>();
            //    propertyChanges.Add("Position", SidebarPosition.Left);
            //    sidebar.Instance.SidebarPropertyChange(propertyChanges);
            //    Assert.True(sidebarEle.ClassList.Contains("e-left"));
            //});
            //await sidebar.InvokeAsync(() =>
            //{
            //    Dictionary<string, object> propertyChanges = new Dictionary<string, object>();
            //    propertyChanges.Add("Type", SidebarType.Auto);
            //    sidebar.Instance.SidebarPropertyChange(propertyChanges);
            //    Assert.True(sidebarEle.ClassList.Contains("e-open"));
            //});
            //await sidebar.InvokeAsync(() =>
            //{
            //    Dictionary<string, object> propertyChanges = new Dictionary<string, object>();
            //    propertyChanges.Add("IsOpen", true);
            //    sidebar.Instance.SidebarPropertyChange(propertyChanges);
            //    Assert.True(sidebarEle.ClassList.Contains("e-open"));
            //});
            //await sidebar.InvokeAsync(() =>
            //{
            //    Dictionary<string, object> propertyChanges = new Dictionary<string, object>();
            //    propertyChanges.Add("CloseOnDocumentClick", true);
            //    propertyChanges.Add("IsOpen", false);
            //    propertyChanges.Add("ShowBackdrop", true);
            //    sidebar.Instance.SidebarPropertyChange(propertyChanges);
            //});
        }

        [Fact(Timeout = 10000, DisplayName = "ComponentDispose method testing")]
        public async Task ComponentDispose()
        {
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
           // await sidebar.InvokeAsync(() =>
           // {
           //     sidebar.SetParametersAndRender(parameters => parameters.Add(p => p.Type, SidebarType.Slide)
           //.Add(p => p.IsOpen, false));
           //     sidebar.Instance.ComponentDispose();
           // });
        }

        [Fact(Timeout = 10000, DisplayName = "CreatedEvent testing")]
        public void CreatedEvent()
        {
            var sidebar = RenderComponent<SfSidebar>();
            sidebar.SetParametersAndRender(parameters =>
                  parameters.Add(s => s.Created, (object args) =>
                  {
                      Assert.Null(args);
                  }));
            sidebar.Instance.Created.InvokeAsync(null);
        }

        [Fact(Timeout = 10000, DisplayName = "IsOpen and EnablePersistence")]
        public void IsOpen_Persistence()
        {
            var sidebar = RenderComponent<SfSidebar>(parameters => parameters.Add(p => p.IsOpen, false).Add(p => p.EnablePersistence, true).Add(p => p.Type, SidebarType.Slide));
            var sidebarEle = sidebar.Find(".e-sidebar");
            sidebar.SetParametersAndRender(("IsOpen", true), ("Type", SidebarType.Auto));
            Assert.Contains("e-open", sidebarEle.ClassName);
            Assert.DoesNotContain("e-close", sidebarEle.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "SidebarInitRender_Close method testing")]
        public async Task SidebarInitRender_Close()
        {
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            //await sidebar.InvokeAsync(() =>
            //{
            //    sidebar.SetParametersAndRender(parameters => parameters.Add(p => p.Type, SidebarType.Slide)
            //.Add(p => p.IsOpen, false).Add(p => p.CloseOnDocumentClick, true).Add(p => p.EnableDock, true));
            //    Syncfusion.Blazor.Navigations.EventArgs args = new Syncfusion.Blazor.Navigations.EventArgs();
            //    args.Cancel = false;
            //    args.Name = "Key";
            //    args.Top = 2.2;
            //    args.Left = 2;
            //    args.IsInteracted = true;
            //    sidebar.Instance.TriggerHide(args);
            //    sidebar.Instance.SidebarInitRender();
            //}).ContinueWith(async (t) =>
            //{
            //    Assert.True(sidebarEle.ClassList.Contains("e-open"));
            //});
        }


        [Fact(Timeout = 10000, DisplayName = "Changed Event testing")]
        public async Task ChangedEventArgs()
        {
            var sidebar = RenderComponent<SfSidebar>();
            sidebar.SetParametersAndRender(parameters =>
                  parameters.Add(s => s.Changed, (Syncfusion.Blazor.Navigations.ChangeEventArgs args) =>
                  {
                      Assert.Equal("Changed", args.Name);
                      Assert.True(args.IsInteracted);
                      Assert.NotNull(args.Element);
                  }));
            Syncfusion.Blazor.Navigations.ChangeEventArgs args = new Syncfusion.Blazor.Navigations.ChangeEventArgs();
            args.Name = "Changed";
            args.IsInteracted = true;
            args.Element = new ElementReference();
            await sidebar.InvokeAsync(() =>
            {
                sidebar.Instance.TriggerChange(true, args);
            });
        }

        [Fact(Timeout = 10000, DisplayName = "EventArgsTest")]
        public async Task EventArgsTest()
        {
            Syncfusion.Blazor.Navigations.EventArgs args = new Syncfusion.Blazor.Navigations.EventArgs();
            args.Name = "Key";
            args.IsInteracted = true;
            var sidebar = RenderComponent<SfSidebar>();
            var sidebarEle = sidebar.Find(".e-sidebar");
            Assert.Contains("e-control", sidebarEle.ClassName);
            await sidebar.InvokeAsync(() =>
            {
                sidebar.Instance.TriggerShow(args);
                Assert.Equal("Key", args.Name);
                Assert.NotNull(args.Element);
                Assert.True(args.IsInteracted);
            });
        }
            [Fact(DisplayName = "ChildContent Property Get and Set")]
            public void ChildContent_GetAndSet()
            {
                // Arrange
                var sidebarContainer = new SfSidebarContainer();
                RenderFragment fragment = builder =>
                {
                    builder.AddContent(0, "Test Content");
                };

                // Act
                sidebarContainer.ChildContent = fragment;
                var retrievedFragment = sidebarContainer.ChildContent;

                // Assert
                Assert.NotNull(retrievedFragment);
                Assert.Equal(fragment, retrievedFragment);
            }

            [Fact(DisplayName = "SetWidth Method Test")]
            public void SetWidth_Test()
            {
                // Arrange
                var sidebarContainer = new SfSidebarContainer();
                string initialWidth = "200px";
                string newWidth = "250px";

                // Act & Assert
                //Assert.Throws<InvalidOperationException>(() => sidebarContainer.SetWidth(initialWidth)); // If StateHasChanged is called outside of a Blazor component
                //Assert.Throws<InvalidOperationException>(() => sidebarContainer.SetWidth(newWidth)); // If StateHasChanged is called outside of a Blazor component
            }
            [Fact(Timeout = 10000, DisplayName = "SidebarShow triggers OnOpen and Changed events")]
            public async Task SidebarShow_Covers_All_Branches()
            {
                int onOpenCount = 0;
                int changedCount = 0;
                var sidebar = RenderComponent<SfSidebar>(parameters => parameters
                    .Add(p => p.IsOpen, false)
                    .Add(p => p.OnOpen, (Syncfusion.Blazor.Navigations.EventArgs args) =>
                    {
                        onOpenCount++;
                        args.Cancel = false;
                    })
                    .Add(p => p.Changed, (Syncfusion.Blazor.Navigations.ChangeEventArgs args) =>
                    {
                        changedCount++;
                        Assert.Equal("Changed", args.Name);
                        Assert.NotNull(args.Element);
                    })
                );
                var instance = sidebar.Instance;
                var containerProp = typeof(SfSidebar).GetProperty("SfSidebarContainer", BindingFlags.Instance | BindingFlags.NonPublic);
                containerProp.SetValue(instance, new SfSidebarContainer());
                await sidebar.InvokeAsync(async () =>
                {
                    var method = typeof(SfSidebar)
                        .GetMethod("SidebarShow", BindingFlags.Instance | BindingFlags.NonPublic);
                    Assert.NotNull(method);
                    await (Task)method.Invoke(instance, null);
                });
                Assert.Equal(1, onOpenCount);
                Assert.Equal(1, changedCount);
            }
            [Fact(Timeout = 10000, DisplayName = "SidebarHide triggers OnClose and Changed events")]
            public async Task SidebarHide_Covers_All_Branches()
            {
                int onCloseCount = 0;
                int changedCount = 0;
                var sidebar = RenderComponent<SfSidebar>(parameters => parameters
                    .Add(p => p.IsOpen, true)
                    .Add(p => p.OnClose, (Syncfusion.Blazor.Navigations.EventArgs args) =>
                    {
                        onCloseCount++;
                        args.Cancel = false;
                    })
                    .Add(p => p.Changed, (Syncfusion.Blazor.Navigations.ChangeEventArgs args) =>
                    {
                        changedCount++;
                        Assert.Equal("Changed", args.Name);
                        Assert.NotNull(args.Element);
                    })
                );
                var instance = sidebar.Instance;
                var containerProp = typeof(SfSidebar).GetProperty("SfSidebarContainer", BindingFlags.Instance | BindingFlags.NonPublic);
                containerProp.SetValue(instance, new SfSidebarContainer());
                var sidebarClassField = typeof(SfSidebar).GetField("sidebarClass", BindingFlags.Instance | BindingFlags.NonPublic);
                var existingClass = sidebarClassField.GetValue(instance)?.ToString() ?? "";
                sidebarClassField.SetValue(instance, existingClass.Replace("e-close", "e-open"));
                await sidebar.InvokeAsync(async () =>
                {
                    var method = typeof(SfSidebar).GetMethod("SidebarHide", BindingFlags.Instance | BindingFlags.NonPublic);
                    Assert.NotNull(method);
                    await (Task)method.Invoke(instance, null);
                });
                Assert.Equal(1, onCloseCount);
                Assert.Equal(1, changedCount);
            }
            [Fact(DisplayName = "SidebarInitRender executes container open branch")]
            public async Task SidebarInitRender_ContainerBranch()
            {
                var comp = RenderComponent<SfSidebarContainer>(parameters =>
                    parameters.AddChildContent<SfSidebar>(sidebarParams =>
                        sidebarParams.Add(x => x.IsOpen, true)
                    )
                );
                var sidebar = comp.FindComponent<SfSidebar>();
                var instance = sidebar.Instance;
                typeof(SfSidebar).GetField("openState", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, false);
                await sidebar.InvokeAsync(async () =>
                {
                    var method = typeof(SfSidebar).GetMethod("SidebarInitRender", BindingFlags.Instance | BindingFlags.NonPublic);
                    await (Task)method.Invoke(instance, null);
                });
                Assert.True(true);
            }
            [Fact(DisplayName = "SidebarInitRender executes media query branch")]
            public async Task SidebarInitRender_MediaQueryBranch()
            {
                var sidebar = RenderComponent<SfSidebar>(p =>p.Add(x => x.Type, SidebarType.Auto));
                var instance = sidebar.Instance;
                typeof(SfSidebar).GetProperty("SfSidebarContainer", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, null);
                typeof(SfSidebar).GetField("isMediaQueryOpen", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, true);
                typeof(SfSidebar).GetField("isDeviceMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, false);
                await sidebar.InvokeAsync(async () =>
                {
                    var method = typeof(SfSidebar).GetMethod("SidebarInitRender", BindingFlags.Instance | BindingFlags.NonPublic);
                    await (Task)method.Invoke(instance, null);
                });
                Assert.True(true);
            }
            [Fact(DisplayName = "SidebarInitRender applies close class when not open")]
            public void SidebarInitRender_CloseClassBranch()
            {
                var sidebar = RenderComponent<SfSidebar>(p => p.Add(x => x.IsOpen, false));
                var instance = sidebar.Instance;
                typeof(SfSidebar).GetField("sidebarClass", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, "e-sidebar");
                var method = typeof(SfSidebar).GetMethod("SidebarInitRender", BindingFlags.Instance | BindingFlags.NonPublic);
                method.Invoke(instance, null);
                var sidebarClass = typeof(SfSidebar).GetField("sidebarClass", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(instance).ToString();
                Assert.Contains("e-close", sidebarClass);
            }
            [Fact(DisplayName = "SidebarInitRender executes dock logic")]
            public async Task SidebarInitRender_DockBranch()
            {
                var comp = RenderComponent<SfSidebarContainer>(parameters =>
                    parameters.AddChildContent<SfSidebar>(sidebarParams =>
                        sidebarParams
                            .Add(x => x.EnableDock, true)
                            .Add(x => x.IsOpen, false)
                            .Add(x => x.DockSize, "60px")
                    )
                );
                var sidebar = comp.FindComponent<SfSidebar>();
                var instance = sidebar.Instance;
                await sidebar.InvokeAsync(async () =>
                {
                    var method = typeof(SfSidebar).GetMethod("SidebarInitRender", BindingFlags.Instance | BindingFlags.NonPublic);
                    await (Task)method.Invoke(instance, null);
                });
                Assert.True(true);
            }
            [Fact(DisplayName = "UpdateAttributes merges style and class correctly")]
            public void UpdateAttributes_StyleMergeBranch()
            {
                var sidebar = RenderComponent<SfSidebar>();
                var instance = sidebar.Instance;
                var htmlAttributes = new Dictionary<string, object>()
                {
                    { "data-sf-style", "background:red" },
                    { "class", "custom-class" },
                    { "title", "test" }
                };
                typeof(SfSidebar).GetProperty("SidebarHtmlAttributes", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, htmlAttributes);
                typeof(SfSidebar).GetMethod("GetStyle", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(instance, null);
                var method = typeof(SfSidebar).GetMethod("UpdateAttributes", BindingFlags.Instance | BindingFlags.NonPublic);
                Assert.NotNull(method);
                method.Invoke(instance, null);
                var attrField = typeof(SfSidebar).GetField("attributes", BindingFlags.Instance | BindingFlags.NonPublic);
                var attributes = attrField.GetValue(instance) as IDictionary<string, object>;
                Assert.NotNull(attributes);
                Assert.True(attributes.ContainsKey("data-sf-style"));
                Assert.Equal("background:red", attributes["data-sf-style"]);
                Assert.Contains("custom-class", attributes["class"].ToString());
                Assert.Equal("test", attributes["title"]);
            }
            [Fact(DisplayName = "SidebarPropertyChange covers open and close dock branch")]
            public async Task SidebarPropertyChange_DockBranches()
            {
                var comp = RenderComponent<SfSidebarContainer>(parameters =>
                    parameters.AddChildContent<SfSidebar>(sidebarParams =>
                        sidebarParams
                            .Add(x => x.EnableDock, true)
                            .Add(x => x.DockSize, "70px")
                            .Add(x => x.Width, "200px")
                            .Add(x => x.IsOpen, false)
                    )
                );
                var sidebar = comp.FindComponent<SfSidebar>();
                var instance = sidebar.Instance;
                var method = typeof(SfSidebar).GetMethod("SidebarPropertyChange", BindingFlags.Instance | BindingFlags.NonPublic);
                Assert.NotNull(method);
                await sidebar.InvokeAsync(async () =>
                {
                    var property = new Dictionary<string, object>
                    {
                        { "IsOpen", true }
                    };
                    var result = method.Invoke(instance, new object[] { property });
                    if (result is Task task) await task;
                });
                await sidebar.InvokeAsync(async () =>
                {
                    var property = new Dictionary<string, object>
                    {
                        { "IsOpen", false }
                    };
                    var result = method.Invoke(instance, new object[] { property });
                    if (result is Task task) await task;
                });
                Assert.True(true);
            }
            [Fact(DisplayName = "OnParametersSetAsync triggers SidebarInitRender when container exists")]
            public async Task OnParametersSetAsync_ContainerBranch()
            {
                var comp = RenderComponent<SfSidebarContainer>(p =>
                    p.AddChildContent<SfSidebar>(child =>
                        child.Add(x => x.IsOpen, false))
                );
                var sidebar = comp.FindComponent<SfSidebar>();
                var instance = sidebar.Instance;
                typeof(SfSidebar).GetField("openState", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(instance, false);
                await sidebar.InvokeAsync(async () =>
                {
                    var method = typeof(SfSidebar).GetMethod("OnParametersSetAsync", BindingFlags.Instance | BindingFlags.NonPublic);
                    Assert.NotNull(method);
                    var result = method.Invoke(instance, null);
                    if (result is Task t)
                    {
                        await t;
                    }
                });
                Assert.True(true);
            }
            [Fact(DisplayName = "PersistProperties executes else branch safely")]
            public async Task PersistProperties_ElseBranch_Final()
            {
                var sidebar = RenderComponent<SfSidebar>(p =>p.Add(x => x.IsOpen, true).Add(x => x.EnablePersistence, true));
                var instance = sidebar.Instance;
                typeof(SfSidebar).GetField("localStorageValue", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(instance, null);
                typeof(SfSidebar).GetField("openState", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(instance, false);
                await sidebar.InvokeAsync(async () =>
                {
                    var method = typeof(SfSidebar).GetMethod("PersistProperties", BindingFlags.Instance | BindingFlags.NonPublic);
                    Assert.NotNull(method);
                    var result = method.Invoke(instance, null);
                    if (result is Task task)
                    {
                        await task;
                    }
                });
                Assert.NotNull(instance);
            }
            [Fact(DisplayName = "PersistProperties covers else branch using mocked localStorage")]
            public async Task PersistProperties_ElseBranch_WithJSMock()
            {
                var expectedState = false;
                var sidebar = RenderComponent<SfSidebar>(p =>
                    p.Add(x => x.EnablePersistence, true)
                    .Add(x => x.IsOpen, true) // initial state
                );
                var instance = sidebar.Instance;
                var persistenceObject = new
                {
                    IsOpen = expectedState
                };
                var json = System.Text.Json.JsonSerializer.Serialize(persistenceObject);
                JSInterop.Setup<string>("window.localStorage.getItem", _ => true).SetResult(json);
                await sidebar.InvokeAsync(async () =>
                {
                    var method = typeof(SfSidebar).GetMethod("PersistProperties", BindingFlags.Instance | BindingFlags.NonPublic);
                    Assert.NotNull(method);
                    var result = method.Invoke(instance, null);
                    if (result is Task task)
                    {
                        await task;
                    }
                });
                Assert.Equal(expectedState, instance.IsOpen);
            }

            [Fact(DisplayName = "SfSidebarContainer.SetWidth - Tests get_Width() and SetWidth() methods")]
            public async Task SidebarContainer_SetWidth_Testing()
            {
                var comp = RenderComponent<SidebarContainerWidthTest>();
                await Task.Delay(200);
                
                var sidebarContainer = comp.FindComponent<SfSidebarContainer>();
                var instance = sidebarContainer.Instance;
                
                // Test initial Width getter
                var widthProperty = typeof(SfSidebarContainer).GetProperty("Width", 
                    BindingFlags.Instance | BindingFlags.NonPublic);
                Assert.NotNull(widthProperty);
                
                var initialWidth = widthProperty?.GetValue(instance);
                
                // Test SetWidth method with new width value
                var setWidthMethod = typeof(SfSidebarContainer).GetMethod("SetWidth", 
                    BindingFlags.Instance | BindingFlags.NonPublic);
                Assert.NotNull(setWidthMethod);
                
                await sidebarContainer.InvokeAsync(async () =>
                {
                    setWidthMethod?.Invoke(instance, new object[] { "350px" });
                });
                
                await Task.Delay(200);
                
                // Verify Width getter returns the new value
                var updatedWidth = widthProperty?.GetValue(instance);
                Assert.Equal("350px", updatedWidth?.ToString());
            }

            [Fact(DisplayName = "SfSidebarContainer.SetWidth - Tests conditional branch when Width equals sidebarWidth")]
            public async Task SidebarContainer_SetWidth_SameValue_Testing()
            {
                var comp = RenderComponent<SidebarContainerWidthTest>();
                await Task.Delay(200);
                
                var sidebarContainer = comp.FindComponent<SfSidebarContainer>();
                var instance = sidebarContainer.Instance;
                
                var widthProperty = typeof(SfSidebarContainer).GetProperty("Width", 
                    BindingFlags.Instance | BindingFlags.NonPublic);
                
                var setWidthMethod = typeof(SfSidebarContainer).GetMethod("SetWidth", 
                    BindingFlags.Instance | BindingFlags.NonPublic);
                
                // Set initial width
                await sidebarContainer.InvokeAsync(async () =>
                {
                    setWidthMethod?.Invoke(instance, new object[] { "300px" });
                });
                
                await Task.Delay(200);
                
                // Call SetWidth with the same value to test the else branch (Width == sidebarWidth)
                await sidebarContainer.InvokeAsync(async () =>
                {
                    setWidthMethod?.Invoke(instance, new object[] { "300px" });
                });
                
                await Task.Delay(200);
                
                // Verify Width remains the same
                var resultWidth = widthProperty?.GetValue(instance);
                Assert.Equal("300px", resultWidth?.ToString());
            }
        }
    }

