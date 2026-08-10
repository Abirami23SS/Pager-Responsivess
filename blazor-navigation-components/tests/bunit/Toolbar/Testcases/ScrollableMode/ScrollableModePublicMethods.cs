using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Toolbar.Samples.ScrollableMode.PublicMethods;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Toolbar
{
    public class ScrollableModePublicMethods : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "AddItems public method testing")]
        public async Task AddItems()
        {
            var toolbar = RenderComponent<AddItems>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
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
            toolbar.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonText).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonSpanText).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonIcon).Count);
            Assert.Equal("Bold", toolbar.FindAll("." + HelperCls.ToolbarItem)[3].GetInnerText().Trim());
            Assert.Equal("Italic", toolbar.FindAll("." + HelperCls.ToolbarItem)[4].GetInnerText().Trim());
            var boldIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[3];
            Assert.NotNull(boldIconHtml);
            Assert.Contains(HelperCls.IconLeft, boldIconHtml.ClassName);
            Assert.Contains(HelperCls.BoldIcon, boldIconHtml.ClassName);
            var italicIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[4];
            Assert.NotNull(italicIconHtml);
            Assert.Contains(HelperCls.IconRight, italicIconHtml.ClassName);
            Assert.Contains(HelperCls.ItalicIcon, italicIconHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "RemoveItems public method testing")]
        public async Task RemoveItems()
        {
            var toolbar = RenderComponent<RemoveItems>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
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
            toolbar.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonText).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonSpanText).Count);
            Assert.Equal(3, toolbar.FindAll("." + HelperCls.ToolbarButtonIcon).Count);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
            var firstIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[0];
            Assert.NotNull(firstIconHtml);
            Assert.Contains(HelperCls.IconLeft, firstIconHtml.ClassName);
            Assert.Contains(HelperCls.CutIcon, firstIconHtml.ClassName);
            var secondIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[1];
            Assert.NotNull(secondIconHtml);
            Assert.Contains(HelperCls.IconLeft, secondIconHtml.ClassName);
            Assert.Contains(HelperCls.CopyIcon, secondIconHtml.ClassName);
            var pasteHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[2];
            Assert.NotNull(pasteHtml);
            Assert.Contains(HelperCls.Hidden, pasteHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "HideItem public method testing")]
        public async Task HideItem()
        {
            var toolbar = RenderComponent<HideItem>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
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
            toolbar.Find("button").Click();
            await Task.Delay(100);
            var cutHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[0];
            Assert.NotNull(cutHtml);
            Assert.Contains(HelperCls.Hidden, cutHtml.ClassName);
        }
    }
}