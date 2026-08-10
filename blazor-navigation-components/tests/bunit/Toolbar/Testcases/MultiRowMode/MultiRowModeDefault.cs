using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Toolbar.Samples.MultiRowMode.Default;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Toolbar
{
    public class MultiRowModeDefault : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Properties default value testing- Toolbar")]
        public void DefaultValueToolbar()
        {
            var toolbar = RenderComponent<SfToolbar>(options => options.Add(mode => mode.OverflowMode, Syncfusion.Blazor.Navigations.OverflowMode.MultiRow));
            Assert.True(toolbar.Find("." + HelperCls.Toolbar).FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbar.Find("." + HelperCls.Toolbar).FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            Assert.True(toolbar.Instance.AllowKeyboard);
            Assert.Equal(string.Empty, toolbar.Instance.CssClass);
            Assert.True(toolbar.Instance.EnableCollision);
            Assert.False(toolbar.Instance.EnableRtl);
            Assert.Equal("auto", toolbar.Instance.Height);
            Assert.Equal(Syncfusion.Blazor.Navigations.OverflowMode.MultiRow, toolbar.Instance.OverflowMode);
            Assert.Equal(0, toolbar.Instance.ScrollStep);
            Assert.Equal("auto", toolbar.Instance.Width);
        }

        [Fact(Timeout = 10000, DisplayName = "Initial loading testing with text")]
        public void ToolbarWithText()
        {
            var toolbar = RenderComponent<ToolbarWithText>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            Assert.Contains(HelperCls.Control, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Toolbar, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Library, toolbarHtml.ClassName);
            var toolbarStyle = toolbarHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:auto", toolbarStyle);
            Assert.Contains("height:auto", toolbarStyle);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonText).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonSpanText).Count);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "Initial loading testing with icon")]
        public void ToolbarWithIcon()
        {
            var toolbar = RenderComponent<ToolbarWithIcon>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            Assert.Contains(HelperCls.Control, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Toolbar, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Library, toolbarHtml.ClassName);
            var toolbarStyle = toolbarHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:auto", toolbarStyle);
            Assert.Contains("height:auto", toolbarStyle);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonAlign).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarIconButton).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonIcon).Count);
            var cutIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[0];
            Assert.NotNull(cutIconHtml);
            //Assert.Contains(HelperCls.IconLeft, cutIconHtml.ClassName);
            Assert.Contains(HelperCls.CutIcon, cutIconHtml.ClassName);
            var copyIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[1];
            Assert.NotNull(copyIconHtml);
            //Assert.Contains(HelperCls.IconLeft, copyIconHtml.ClassName);
            Assert.Contains(HelperCls.CopyIcon, copyIconHtml.ClassName);
            var pasteIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[2];
            Assert.NotNull(pasteIconHtml);
            //Assert.Contains(HelperCls.IconLeft, pasteIconHtml.ClassName);
            Assert.Contains(HelperCls.PasteIcon, pasteIconHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Initial loading testing with text and icon")]
        public void ToolbarWithTextIcon()
        {
            var toolbar = RenderComponent<ToolbarWithTextIcon>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            Assert.Contains(HelperCls.Control, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Toolbar, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Library, toolbarHtml.ClassName);
            var toolbarStyle = toolbarHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:auto", toolbarStyle);
            Assert.Contains("height:auto", toolbarStyle);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonText).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonSpanText).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonIcon).Count);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
            var cutIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[0];
            Assert.NotNull(cutIconHtml);
            Assert.Contains(HelperCls.IconLeft, cutIconHtml.ClassName);
            Assert.Contains(HelperCls.CutIcon, cutIconHtml.ClassName);
            var copyIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[1];
            Assert.NotNull(copyIconHtml);
            Assert.Contains(HelperCls.IconLeft, copyIconHtml.ClassName);
            Assert.Contains(HelperCls.CopyIcon, copyIconHtml.ClassName);
            var pasteIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[2];
            Assert.NotNull(pasteIconHtml);
            Assert.Contains(HelperCls.IconLeft, pasteIconHtml.ClassName);
            Assert.Contains(HelperCls.PasteIcon, pasteIconHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass property testing")]
        public async Task CssClass()
        {
            var toolbar = RenderComponent<CssClass>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            Assert.Contains(HelperCls.CustomClass, toolbarHtml.ClassName);
            var toolbarComp = RenderComponent<SfToolbar>(options => options.Add(p => p.CssClass, "e-custom-class").Add(mode => mode.OverflowMode, Syncfusion.Blazor.Navigations.OverflowMode.MultiRow));
            var toolbarEle = toolbarComp.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarEle);
            Assert.Contains(HelperCls.CustomClass, toolbarEle.ClassName);
            toolbarComp.SetParametersAndRender(("CssClass", "e-custom"));
            await Task.Delay(100);
        }

        [Fact(Timeout = 10000, DisplayName = "Height property testing")]
        public void Height()
        {
            var toolbar = RenderComponent<SfToolbar>(options => options.Add(p => p.Height, "260px").Add(mode => mode.OverflowMode, Syncfusion.Blazor.Navigations.OverflowMode.MultiRow));
            var toolbarEle = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarEle);
            var tabHeight = toolbarEle.GetAttribute("data-sf-style");
            var expectedValue = "width:auto;height:260px;";
            Assert.True(toolbarEle.FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbarEle.FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            expectedValue.MarkupMatches(tabHeight);
            Assert.Contains("height:260px", tabHeight.Replace(" ", string.Empty));
            toolbar.SetParametersAndRender((HelperCls.Height, "300px"));
            toolbarEle = toolbar.Find("." + HelperCls.Toolbar);
            tabHeight = toolbarEle.GetAttribute("data-sf-style");
            Assert.Contains("height:300px", tabHeight.Replace(" ", string.Empty));
        }

        [Fact(Timeout = 10000, DisplayName = "Width property testing")]
        public void Width()
        {
            var toolbar = RenderComponent<SfToolbar>(options => options.Add(p => p.Width, "600px").Add(mode => mode.OverflowMode, Syncfusion.Blazor.Navigations.OverflowMode.MultiRow));
            var toolbarEle = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarEle);
            var tabWidth = toolbarEle.GetAttribute("data-sf-style");
            var expectedValue = "width:600px;height:auto;";
            Assert.True(toolbarEle.FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbarEle.FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            expectedValue.MarkupMatches(tabWidth);
            Assert.Contains("width:600px", tabWidth.Replace(" ", string.Empty));
            toolbar.SetParametersAndRender((HelperCls.Width, "700px"));
            toolbarEle = toolbar.Find("." + HelperCls.Toolbar);
            tabWidth = toolbarEle.GetAttribute("data-sf-style");
            Assert.Contains("width:700px", tabWidth.Replace(" ", string.Empty));
        }

        [Fact(Timeout = 10000, DisplayName = "Items property testing")]
        public void ItemsProperty()
        {
            var toolbar = RenderComponent<ItemsProperty>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.Contains(HelperCls.Control, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Toolbar, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Library, toolbarHtml.ClassName);
            var toolbarStyle = toolbarHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:auto", toolbarStyle);
            Assert.Contains("height:auto", toolbarStyle);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
            Assert.Equal("Bold", toolbar.FindAll("." + HelperCls.ToolbarItem)[3].GetInnerText().Trim());
            Assert.Equal("Underline", toolbar.FindAll("." + HelperCls.ToolbarItem)[4].GetInnerText().Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "EnableRTL property testing")]
        public async Task EnableRTL()
        {
            var toolbar = RenderComponent<EnableRTL>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbarHtml.FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            Assert.Contains(HelperCls.RTL, toolbarHtml.ClassName);
            var toolbarComp = RenderComponent<SfToolbar>(options => options.Add(p => p.EnableRtl, true).Add(mode => mode.OverflowMode, Syncfusion.Blazor.Navigations.OverflowMode.MultiRow));
            var toolbarEle = toolbarComp.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarEle);
            Assert.Contains(HelperCls.RTL, toolbarEle.ClassName);
            toolbarComp.SetParametersAndRender(("EnableRtl", false));
            await Task.Delay(100);
        }

        [Fact(Timeout = 10000, DisplayName = "ARIA attributes testing")]
        public void ARIAAttributes()
        {
            var toolbar = RenderComponent<ToolbarWithTextIcon>();
            Assert.True(toolbar.Find("." + HelperCls.Toolbar).FirstElementChild.ClassList.Contains(HelperCls.ToolbarItems));
            Assert.True(toolbar.Find("." + HelperCls.Toolbar).FirstElementChild.ClassList.Contains(HelperCls.ToolbarMultirow));
            Assert.Equal("toolbar", toolbar.Find("." + HelperCls.Toolbar).GetAttribute("role"));
            Assert.Equal("horizontal", toolbar.Find("." + HelperCls.Toolbar).GetAttribute("aria-orientation"));
            Assert.Equal("false", toolbar.FindAll("." + HelperCls.ToolbarItem)[0].QuerySelector("button").GetAttribute("aria-disabled"));
            Assert.Equal("false", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].QuerySelector("button").GetAttribute("aria-disabled"));
            Assert.Equal("false", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].QuerySelector("button").GetAttribute("aria-disabled"));
            Assert.Equal("0", toolbar.FindAll("." + HelperCls.ToolbarItem)[0].GetAttribute("data-index"));
            Assert.Equal("1", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetAttribute("data-index"));
            Assert.Equal("2", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetAttribute("data-index"));
        }
    }
}