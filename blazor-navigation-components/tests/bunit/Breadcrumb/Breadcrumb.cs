using Bunit;
using Xunit;
using System.Threading.Tasks;
using Syncfusion.Blazor.Navigations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Tests.Breadcrumb.Samples;

namespace Syncfusion.Blazor.Tests.Navigations.TestCase
{
   public partial class Breadcrumb : BunitTestContext
   {
      [Trait("Breadcrumb", "Basic")]
      [Fact(DisplayName = "Basic")]
      public void Basic()
        {
            var cut = RenderComponent<Default>();
            var breadcrumbElems = cut.FindAll("nav",true);
            Assert.Contains("e-breadcrumb",breadcrumbElems[0].ClassName);
            Assert.Contains("e-breadcrumb-text",breadcrumbElems[0].ClassName);
            Assert.Equal("e-breadcrumb-item",breadcrumbElems[0].FirstElementChild.Children[0].ClassName);
            Assert.Equal("e-breadcrumb-separator",breadcrumbElems[0].FirstElementChild.Children[1].ClassName);
            Assert.Equal("e-breadcrumb-text",breadcrumbElems[0].FirstElementChild.Children[0].Children[0].ClassName);
            Assert.Equal("Home",breadcrumbElems[0].FirstElementChild.Children[0].TextContent.Trim());
            Assert.Equal("Buttons",breadcrumbElems[0].FirstElementChild.Children[2].TextContent.Trim());
            Assert.Equal("Breadcrumb",breadcrumbElems[0].FirstElementChild.Children[4].TextContent.Trim());
         }
      [Trait("Breadcrumb", "Enable/Disable Navigation")]
      [Fact(DisplayName = "EnableNavigation")]
      public async Task EnableNavigation()
      {
         var cut = RenderComponent<Icons>();
         var breadcrumbElems = cut.FindAll("nav",true);
         Assert.Equal(2,breadcrumbElems.Count);
         var breadItem=cut.FindAll("a.e-breadcrumb-text",true);
         breadItem[3].Click();
         await Task.Delay(1000);
         var breadcrumbElems1 = cut.FindAll("nav",true);
         Assert.Equal(2,breadcrumbElems1.Count);
      }
      [Trait("Breadcrumb", "Enable/Disable Navigation")]
      [Fact(DisplayName = "DisableNavigation")]
      public async Task DisableNavigation()
        {
            var cut = RenderComponent<Default>();
            var breadcrumbElems = cut.FindAll("nav", true);
            Assert.Equal(4, breadcrumbElems.Count);
            var breadItem = cut.FindAll("a.e-breadcrumb-text", true);
            breadItem[1].Click();
            await Task.Delay(1000);
            var breadcrumbElems1 = cut.FindAll("nav", true);
            Assert.Equal(4, breadcrumbElems1.Count);
        }
     
      [Trait("Breadcrumb", "Items")]
      [Fact(DisplayName = "AbsoluteURL")]
      public async Task AbsoluteURL()
      {
         var cut = RenderComponent<BindToLocation>();
         await Task.Delay(500);
         var breadItem=cut.FindAll(".e-icon-item",true);
         Assert.Equal("https://syncfusion.github.io/",breadItem[1].Children[0].GetAttribute("href"));
      }
      [Trait("Breadcrumb", "Icon")]
      [Fact(DisplayName = "LeftPosition")]
      public async Task LeftPosition()
      {
         var cut = RenderComponent<Icons>();
         await Task.Delay(500);
         var breadcrumbElems=cut.FindAll("nav",true);
         Assert.DoesNotContain("e-icon-right",breadcrumbElems[0].ClassName);
      }
      [Trait("Breadcrumb", "Icon")]
      [Fact(DisplayName = "RightPosition")]
      public async Task RightPosition()
      {
         var cut = RenderComponent<Icons>();
         await Task.Delay(500);
         var breadcrumbElems=cut.FindAll("nav",true);
         Assert.Contains("e-icon-right",breadcrumbElems[1].ClassName);
      }
      [Trait("Breadcrumb", "Icon")]
      [Fact(DisplayName = "WithFontIcon")]
      public async Task WithFontIcon()
      {
         var cut = RenderComponent<Icons>();
         await Task.Delay(500);
         var breadcrumbElems=cut.FindAll("a.e-breadcrumb-text",true);
         Assert.Equal("e-breadcrumb-icon e-bicons e-folder",breadcrumbElems[1].Children[0].ClassName);
      }
      [Trait("Breadcrumb", "Icon")]
      [Fact(DisplayName = "WithImageIcon")]
      public async Task WithImageIcon()
        {
            var cut = RenderComponent<IconTypes>();
            await Task.Delay(500);
            var breadcrumbElems = cut.FindAll("a.e-breadcrumb-text", true);
            Assert.Equal("e-breadcrumb-icon e-icons e-home", breadcrumbElems[3].Children[0].ClassName);
        }
      [Trait("Breadcrumb", "Icon")]
      [Fact(DisplayName = "WithSVGIcon")]
      public async Task WithSVGIcon()
        {
            var cut = RenderComponent<IconTypes>();
            await Task.Delay(500);
            var breadcrumbElems = cut.FindAll("a.e-breadcrumb-text", true);
            Assert.Equal("e-breadcrumb-icon e-icons e-home", breadcrumbElems[6].Children[0].ClassName);
        }
      [Trait("Breadcrumb", "Icon")]
      [Fact(DisplayName = "WithIconOnly")]
      public async Task WithIconOnly()
        {
            var cut = RenderComponent<IconTypes>();
            await Task.Delay(500);
            var breadcrumbElems=cut.FindAll("a.e-breadcrumb-text",true);
            var breadcrumbControl=cut.FindAll(".e-breadcrumb",true);
            Assert.Equal("",breadcrumbElems[9].TextContent.Trim());
            Assert.Equal("",breadcrumbElems[10].TextContent.Trim());
            Assert.Equal("",breadcrumbElems[11].TextContent.Trim());
            Assert.Equal("e-breadcrumb-icon e-icons e-folder-open",breadcrumbElems[10].Children[0].ClassName);
            Assert.Equal(3,breadcrumbControl[3].QuerySelectorAll(".e-breadcrumb-icon").Length);
         }
      [Trait("Breadcrumb", "Icon")]
      [Fact(DisplayName = "OnlyforFirstItem")]
      public async Task OnlyforFirstItem()
        {
            var cut = RenderComponent<IconTypes>();
            await Task.Delay(500);
            var breadcrumbElems=cut.FindAll(".e-breadcrumb-text",true);
            Assert.Equal(1,breadcrumbElems[12].QuerySelectorAll(".e-breadcrumb-icon").Length);
         }
      [Trait("Breadcrumb", "Navigation")]
      [Fact(DisplayName = "RelativeURL")]
      public async Task RelativeURL()
        {
            var cut = RenderComponent<Default>();
            await Task.Delay(500);
            var breadcrumbElems=cut.FindAll(".e-breadcrumb-text",true);
            Assert.Equal("javascript:void(0);", breadcrumbElems[1].GetAttribute("href"));
         }
      [Trait("Breadcrumb", "Navigation")]
      [Fact(DisplayName = "Navigation_AbsoluteURL")]
      public async Task Navigation_AbsoluteURL()
        {
            var cut = RenderComponent<BindToLocation>();
            await Task.Delay(500);
            var breadItem=cut.FindAll(".e-icon-item",true);
            Assert.Equal("https://syncfusion.github.io/",breadItem[1].Children[0].GetAttribute("href"));
         }
      [Trait("Breadcrumb", "Navigation")]
      [Fact(DisplayName = "ForLastBreadcrumbItem")]
      public async Task ForLastBreadcrumbItem()
        {
            var cut = RenderComponent<ActiveItem>();
            await Task.Delay(500);
            var breadItem=cut.FindAll(".e-breadcrumb-text",true);
            Assert.Equal("javascript:void(0);", breadItem[3].GetAttribute("href"));
            var breadBtn = cut.FindAll(".e-brd-btn", true);
            breadBtn[0].Click();
        }
      [Trait("Breadcrumb", "OverflowMode")]
      [Fact(DisplayName = "OverflowMode_Menu")]
      public async Task OverflowMode_Menu()
        {
            var cut = RenderComponent<Overflow>();
            await Task.Delay(100);
            var breadcrumbCtrl=cut.FindAll(".e-breadcrumb",true);
            Assert.NotNull(breadcrumbCtrl[0].QuerySelector(".e-breadcrumb-menu"));
            breadcrumbCtrl[0].QuerySelector(".e-breadcrumb-menu").Click();
            await Task.Delay(100);
            var popupItem=cut.FindAll(".e-breadcrumb-popup");
            Assert.Equal(1,popupItem.Count);
            var ulElems=cut.FindAll("ul");
            Assert.Equal(3,ulElems[0].ChildElementCount);
            ulElems[0].Children[1].Click();
            breadcrumbCtrl=cut.FindAll(".e-breadcrumb",true);
            Assert.Null(breadcrumbCtrl[0].QuerySelector(".e-breadcrumb-menu"));
         }
      [Trait("Breadcrumb", "OverflowMode")]
      [Fact(DisplayName = "OverflowMode_Hidden")]
      public async Task OverflowMode_Hidden()
        {
            var cut = RenderComponent<Overflow>();
            await Task.Delay(100);
            var breadcrumbCtrl=cut.FindAll(".e-breadcrumb",true);
            Assert.Equal(3,breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-separator").Length);
            Assert.Equal(3,breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-item").Length);
            Assert.Equal("Home",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[0].TextContent.Trim());
            Assert.Equal("Navigation",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[1].TextContent.Trim());
            Assert.Equal("Overflow",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[2].TextContent.Trim());
            breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[1].Click();
            Assert.Equal("Icons",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[1].TextContent.Trim());
            Assert.Equal(3,breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-separator").Length);
            breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[1].Click();
            Assert.Equal("Default",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[1].TextContent.Trim());
            Assert.Equal(3,breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-separator").Length);
            breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[1].Click();
            Assert.Equal("Breadcrumb",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[1].TextContent.Trim());
            Assert.Equal(2,breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-separator").Length);
         }
      [Trait("Breadcrumb", "OverflowMode")]
      [Fact(DisplayName = "OverflowMode_Collapsed")]
      public async Task OverflowMode_Collapsed()
        {
            var cut = RenderComponent<Overflow>();
            await Task.Delay(100);
            var breadcrumbCtrl=cut.FindAll(".e-breadcrumb",true);
            Assert.NotNull(breadcrumbCtrl[2].QuerySelectorAll(".e-breadcrumb-collapsed"));
            Assert.Equal(2,breadcrumbCtrl[2].QuerySelectorAll(".e-breadcrumb-item").Length);
            breadcrumbCtrl[2].QuerySelectorAll(".e-breadcrumb-collapsed")[0].Click();
            Assert.Equal(6,breadcrumbCtrl[2].QuerySelectorAll(".e-breadcrumb-item").Length);
         }
      [Trait("Breadcrumb", "OverflowMode")]
      [Fact(DisplayName = "OverflowMode_Wrap")]
      public async Task OverflowMode_Wrap()
        {
            var cut = RenderComponent<Overflow>();
            await Task.Delay(100);
            var breadcrumbCtrl=cut.FindAll(".e-breadcrumb",true);
            Assert.Contains("e-breadcrumb-wrap-mode",breadcrumbCtrl[3].ClassName);
            Assert.Contains("e-breadcrumb-first-ol",breadcrumbCtrl[3].Children[0].ClassName);
            Assert.Contains("e-breadcrumb-wrapped-ol",breadcrumbCtrl[3].Children[1].ClassName);
            Assert.Equal(5,breadcrumbCtrl[3].QuerySelectorAll(".e-breadcrumb-item-wrapper").Length);
         }
      [Trait("Breadcrumb", "OverflowMode")]
      [Fact(DisplayName = "OverflowMode_Scroll")]
      public async Task OverflowMode_Scroll()
        {
            var cut = RenderComponent<Overflow>();
            await Task.Delay(100);
            var breadcrumbCtrl=cut.FindAll(".e-breadcrumb",true);
            Assert.Contains("e-breadcrumb-scroll-mode",breadcrumbCtrl[4].ClassName);
            var breadcrumbStyle = breadcrumbCtrl[4].GetAttribute("data-sf-style");
            Assert.Contains("300px", breadcrumbStyle);
        }
        [Trait("Breadcrumb", "Templates")]
      [Fact(DisplayName = "Templates_Itemtemplate")]
      public async Task Templates_Itemtemplate()
        {
            var cut = RenderComponent<Items>();
            await Task.Delay(100);
            var breadcrumbCtrl=cut.FindAll(".e-breadcrumb",true);
            Assert.Equal(1,breadcrumbCtrl[1].QuerySelectorAll(".e-chip-list").Length);
            Assert.Equal(1,breadcrumbCtrl[1].QuerySelectorAll(".e-chip-text").Length);
         }
      [Trait("Breadcrumb", "Templates")]
      [Fact(DisplayName = "Templates_Separatortemplate")]
      public async Task Templates_Separatortemplate()
      {
         var cut = RenderComponent<Default>();
         await Task.Delay(500);
         var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
         Assert.Equal(2,breadcrumbCtrl[3].QuerySelectorAll(".e-bullet-arrow").Length);
      }
      [Trait("Breadcrumb", "Others")]
      [Fact(DisplayName = "Others_Diable_AllItems")]
      public async Task Others_Diable_AllItems()
        {
            var cut = RenderComponent<Disabled>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Contains("e-disabled",breadcrumbCtrl[0].ClassName);
         }
      [Trait("Breadcrumb", "Others")]
      [Fact(DisplayName = "Others_Diable_SpecificItems")]
      public async Task Others_Diable_SpecificItems()
        {
            var cut = RenderComponent<Disabled>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Contains("e-disabled",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-item")[1].ClassName);
            Assert.DoesNotContain("e-disabled",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-item")[0].ClassName);
            Assert.DoesNotContain("e-disabled",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-item")[2].ClassName);
         }
      [Trait("Breadcrumb", "Others")]
      [Fact(DisplayName = "Others_ActiveItems")]
      public async Task Others_ActiveItems()
        {
            var cut = RenderComponent<ActiveItem>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal("Active Item",breadcrumbCtrl[0].QuerySelectorAll(".e-breadcrumb-item")[3].TextContent.Trim());
         }
      [Trait("Breadcrumb", "Others")]
      [Fact(DisplayName = "Others_MaxItems")]
      public async Task Others_MaxItems()
        {
            var cut = RenderComponent<OverflowModeBC>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal(4,breadcrumbCtrl[0].QuerySelectorAll(".e-breadcrumb-item").Length);
         }
      [Trait("Breadcrumb", "Others")]
      [Fact(DisplayName = "Others_Enable_RTL")]
      public async Task Others_Enable_RTL()
        {
            var cut = RenderComponent<Default>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Contains("e-rtl",breadcrumbCtrl[1].ClassName);
         }
      [Trait("Breadcrumb", "Others")]
      [Fact(DisplayName = "Others_Width")]
      public async Task Others_Width()
        {
            var cut = RenderComponent<Default>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal("200px",breadcrumbCtrl[0].GetAttribute("width"));
         }
      [Trait("Breadcrumb", "PropertyChanges")]
      [Fact(DisplayName = "PropertyChanges_ActiveItem")]
      public async Task PropertyChanges_ActiveItem()
        {
            var cut = RenderComponent<ActiveItem>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal("Active Item",breadcrumbCtrl[0].QuerySelectorAll(".e-breadcrumb-item")[3].TextContent.Trim());
            cut.SetParametersAndRender((nameof(ActiveItem.active_item),"./breadcrumb/overflowmode"));
            breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal("OverflowMode",breadcrumbCtrl[0].QuerySelectorAll(".e-breadcrumb-item")[4].TextContent.Trim());
         }
      [Trait("Breadcrumb", "PropertyChanges")]
      [Fact(DisplayName = "PropertyChanges_Disabled")]
      public async Task PropertyChanges_Disabled()
        {
            var cut = RenderComponent<Disabled>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Contains("e-disabled",breadcrumbCtrl[0].ClassName);
            cut.SetParametersAndRender((nameof(Disabled.disabled),false));
            breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.DoesNotContain("e-disabled",breadcrumbCtrl[0].ClassName);
         }
      [Trait("Breadcrumb", "PropertyChanges")]
      [Fact(DisplayName = "PropertyChanges_EnableActiveItemNavigation")]
      public async Task PropertyChanges_EnableActiveItemNavigation()
        {
            var cut = RenderComponent<ActiveItem>();
            await Task.Delay(500);
            var breadcrumbItem=cut.FindAll("li.e-breadcrumb-item",true);
            Assert.NotNull(breadcrumbItem[3].QuerySelector("a"));
            cut.SetParametersAndRender((nameof(ActiveItem.activenavigation),false));
            Assert.Null(breadcrumbItem[3].QuerySelector("a"));
         }
      [Trait("Breadcrumb", "PropertyChanges")]
      [Fact(DisplayName = "PropertyChanges_EnableNavigation")]
      public async Task PropertyChanges_EnableNavigation()
        {
            var cut = RenderComponent<Default>();
            await Task.Delay(500);
            var breadcrumbCtrl = cut.FindAll("nav.e-breadcrumb", true);
            breadcrumbCtrl[0].QuerySelectorAll(".e-breadcrumb-item")[1].Click();
            Assert.Equal(4, breadcrumbCtrl.Count);
            cut.SetParametersAndRender((nameof(Default.enable_navigation), true));
            await Task.Delay(1000);
            breadcrumbCtrl[0].QuerySelectorAll(".e-breadcrumb-item")[1].Click();
            Assert.Equal(4, breadcrumbCtrl.Count);
        }
      
      [Trait("Breadcrumb", "PropertyChanges")]
      [Fact(DisplayName = "PropertyChanges_MaxItems")]
      public async Task PropertyChanges_MaxItems()
        {
            var cut = RenderComponent<OverflowModeBC>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal(4,breadcrumbCtrl[0].QuerySelectorAll(".e-breadcrumb-item").Length);
            cut.SetParametersAndRender((nameof(OverflowModeBC.max_items),3));
            breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal(3,breadcrumbCtrl[0].QuerySelectorAll(".e-breadcrumb-item").Length);
         }

      [Trait("Breadcrumb", "PropertyChanges")]
      [Fact(DisplayName = "PropertyChanges_URL")]
      public async Task PropertyChanges_URL()
        {
            var cut = RenderComponent<BindToLocation>();
            await Task.Delay(500);
            var breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal("https://syncfusion.github.io/blazor-component/buttons",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[2].GetAttribute("href"));
            cut.SetParametersAndRender((nameof(BindToLocation.url),"https://syncfusion.github.io/blazor-component"));
            await Task.Delay(1000);
            breadcrumbCtrl=cut.FindAll("nav.e-breadcrumb",true);
            Assert.Equal("https://syncfusion.github.io/",breadcrumbCtrl[1].QuerySelectorAll(".e-breadcrumb-text")[0].GetAttribute("href"));
         }


        [Fact(DisplayName = "Breadcrumb Clicked Event - Item Property")]
        public void Breadcrumb_Item_From_Event()
        {
            BreadcrumbClickedEventArgs capturedArgs = null;

            var cut = RenderComponent<SfBreadcrumb>(parameters => parameters
                .Add(p => p.Items, new List<BreadcrumbItem>
                {
            new BreadcrumbItem { Text = "Home", Url = "/" }
                })
                .Add(p => p.ItemClicked, args => capturedArgs = args)
            );

            // Simulate click on breadcrumb item
            cut.Find("li").Click();

            Assert.NotNull(capturedArgs);
            Assert.NotNull(capturedArgs.Item);
            Assert.Equal("Home", capturedArgs.Item.Text);
            Assert.Equal("/", capturedArgs.Item.Url);
        }

        [Trait("Breadcrumb", "Events")]
        [Fact(DisplayName = "ItemClicked event testing with BreadcrumbClickedEventArgs properties")]
        public async Task ItemClickedEvent()
        {
            var cut = RenderComponent<ItemClickedEvent>();
            await Task.Delay(100);
            var outputSpan = cut.Find("span#eventOutput");
            Assert.NotNull(outputSpan);
            // Click the button to trigger ItemClicked event
            cut.Find("button").Click();
            await Task.Delay(100);
            var clickedOutput = outputSpan.TextContent;
            Assert.Contains("ItemClicked event", clickedOutput);
            Assert.Contains("Item:", clickedOutput);
        }
        [Fact(DisplayName = "Test Cancel Property Getter and Setter")]
        public void TestCancelProperty()
        {
            var args = new BreadcrumbItemRenderingEventArgs();
            args.Cancel = true;
            Assert.True(args.Cancel, "Cancel should be set to true");
            args.Cancel = false;
            Assert.False(args.Cancel, "Cancel should be set to false");
        }
        [Fact(DisplayName = "Test EnablePersistence Property")]
        public void TestEnablePersistenceProperty()
        {
            var cut = RenderComponent<SfBreadcrumb>(parameters => parameters
                .Add(p => p.EnablePersistence, true));
            var breadcrumbComponent = cut.Instance;
            Assert.True(breadcrumbComponent.EnablePersistence, "EnablePersistence property should be set to true.");
        }
        [Fact(DisplayName = "Test ActiveItemChanged EventCallback")]
        public async Task TestActiveItemChanged()
        {
            bool callbackTriggered = false;
            var cut = RenderComponent<SfBreadcrumb>(parameters => parameters
                .Add(p => p.ActiveItemChanged, EventCallback.Factory.Create<string>(this, activeItem => callbackTriggered = true))
            );
            await cut.Instance.ActiveItemChanged.InvokeAsync("NewActiveItem");
            Assert.True(callbackTriggered, "ActiveItemChanged callback should be triggered on active item change.");
        }
        [Fact(DisplayName = "Test HtmlAttributes Property")]
        public void TestHtmlAttributes()
        {
            var attributes = new Dictionary<string, object>
        {
            { "style", "width:200px" },
            { "class", "custom-breadcrumb-class" }
        };
            var cut = RenderComponent<SfBreadcrumb>(parameters => parameters
                .Add(p => p.HtmlAttributes, attributes)
            );
            var breadcrumbContainer = cut.Find(".custom-breadcrumb-class");
            var breadcrumbStyle = breadcrumbContainer.GetAttribute("data-sf-style");
            Assert.Contains("200px", breadcrumbStyle);
        }

    }
}
