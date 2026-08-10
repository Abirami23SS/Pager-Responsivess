using AngleSharp.Css.Dom;
using AngleSharp.Dom;
using Bunit;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Toolbar.Samples.PopupMode.ToolbarItem;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Syncfusion.Blazor.Tests.Toolbar
{
    public class PopupModeToolbarItem : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "CssClass property testing")]
        public void CssClass()
        {
            var toolbar = RenderComponent<CssClass>();
            var itemHtml = toolbar.Find("." + HelperCls.ToolbarItem);
            Assert.NotNull(itemHtml);
            Assert.Contains(HelperCls.CutClass, itemHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Disabled property testing")]
        public void DisabledProperty()
        {
            var toolbar = RenderComponent<Disabled>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.Contains("e-overlay", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].ClassName);
            Assert.Equal("false", toolbar.FindAll("." + HelperCls.ToolbarItem)[0].QuerySelector("button").GetAttribute("aria-disabled"));
            Assert.Equal("false", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].QuerySelector("button").GetAttribute("aria-disabled"));
            Assert.Equal("true", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].QuerySelector("button").GetAttribute("aria-disabled"));
        }

        [Fact(Timeout = 10000, DisplayName = "Id property testing")]
        public void Id()
        {
            var toolbar = RenderComponent<ItemId>();
            var cutHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[0];
            Assert.Equal(HelperCls.Cut, toolbar.FindAll("." + HelperCls.ToolbarItem)[0].GetAttribute("Id"));
            var copyHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[1];
            Assert.Equal(HelperCls.Copy, toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetAttribute("Id"));
            var pasteHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[2];
            Assert.Equal(HelperCls.Paste, toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetAttribute("Id"));
        }

        [Fact(Timeout = 10000, DisplayName = "PrefixIcon property testing")]
        public void PrefixIcon()
        {
            var toolbar = RenderComponent<PrefixIcon>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
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

        [Fact(Timeout = 10000, DisplayName = "SuffixIcon property testing")]
        public void SuffixIcon()
        {
            var toolbar = RenderComponent<SuffixIcon>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            var cutIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[0];
            Assert.NotNull(cutIconHtml);
            //Assert.Contains(HelperCls.IconRight, cutIconHtml.ClassName);
            Assert.Contains(HelperCls.CutIcon, cutIconHtml.ClassName);
            var copyIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[1];
            Assert.NotNull(copyIconHtml);
            //Assert.Contains(HelperCls.IconRight, copyIconHtml.ClassName);
            Assert.Contains(HelperCls.CopyIcon, copyIconHtml.ClassName);
            var pasteIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[2];
            Assert.NotNull(pasteIconHtml);
            //Assert.Contains(HelperCls.IconRight, pasteIconHtml.ClassName);
            Assert.Contains(HelperCls.PasteIcon, pasteIconHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Text property testing")]
        public void Text()
        {
            var toolbar = RenderComponent<Text>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "TooltipText property testing")]
        public void TooltipText()
        {
            var toolbar = RenderComponent<TooltipText>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.Equal(HelperCls.Cut, toolbar.FindAll("." + HelperCls.ToolbarItem)[0].GetAttribute("title"));
            Assert.Equal(HelperCls.Copy, toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetAttribute("title"));
            Assert.Equal(HelperCls.Paste, toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetAttribute("title"));
        }

        [Fact(Timeout = 10000, DisplayName = "Type property testing")]
        public void Type()
        {
            var toolbar = RenderComponent<ToolbarItemType>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            var separatorHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[1];
            Assert.NotNull(separatorHtml);
            Assert.Contains(HelperCls.Separator, separatorHtml.ClassName);
            var templateHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[2];
            Assert.NotNull(templateHtml);
            Assert.Contains(HelperCls.Template, templateHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Visible property testing")]
        public void Visible()
        {
            var toolbar = RenderComponent<Visible>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            var cutHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[0];
            Assert.NotNull(cutHtml);
            Assert.DoesNotContain(HelperCls.Hidden, cutHtml.ClassName);
            var copyHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[1];
            Assert.NotNull(copyHtml);
            Assert.DoesNotContain(HelperCls.Hidden, copyHtml.ClassName);
            var pasteHtml = toolbar.FindAll("." + HelperCls.ToolbarItem)[2];
            Assert.NotNull(pasteHtml);
            Assert.Contains(HelperCls.Hidden, pasteHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Width property testing")]
        public void Width()
        {
            var toolbar = RenderComponent<Width>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            var button0Style = toolbar.FindAll("." + HelperCls.ToolbarButton)[0].GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:20px", button0Style);
            var button1Style = toolbar.FindAll("." + HelperCls.ToolbarButton)[1].GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:40px", button1Style);
            var button2Style = toolbar.FindAll("." + HelperCls.ToolbarButton)[2].GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:60px", button2Style);
        }

        [Fact(Timeout = 10000, DisplayName = "Conditional rendering foreach loop testing")]
        public async Task ForeachLoop()
        {
            var toolbar = RenderComponent<ForLoop>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.Contains(HelperCls.Control, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Toolbar, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Library, toolbarHtml.ClassName);
            var toolbarStyle = toolbarHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:auto", toolbarStyle);
            Assert.Contains("height:auto", toolbarStyle);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonText).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonSpanText).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonIcon).Count);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
            Assert.Equal("Bold", toolbar.FindAll("." + HelperCls.ToolbarItem)[3].GetInnerText().Trim());
            Assert.Equal("Underline", toolbar.FindAll("." + HelperCls.ToolbarItem)[4].GetInnerText().Trim());
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
            var boldIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[3];
            Assert.NotNull(boldIconHtml);
            Assert.Contains(HelperCls.IconLeft, boldIconHtml.ClassName);
            Assert.Contains(HelperCls.BoldIcon, boldIconHtml.ClassName);
            var underlineIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[4];
            Assert.NotNull(underlineIconHtml);
            Assert.Contains(HelperCls.IconLeft, underlineIconHtml.ClassName);
            Assert.Contains(HelperCls.UnderlineIcon, underlineIconHtml.ClassName);
            toolbar.Find("button").Click();
            await Task.Delay(100);
            Assert.Equal(6, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(6, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(6, toolbar.FindAll("." + HelperCls.ToolbarButtonText).Count);
            Assert.Equal(6, toolbar.FindAll("." + HelperCls.ToolbarButtonSpanText).Count);
            Assert.Equal(6, toolbar.FindAll("." + HelperCls.ToolbarButtonIcon).Count);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Copy", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
            Assert.Equal("Bold", toolbar.FindAll("." + HelperCls.ToolbarItem)[3].GetInnerText().Trim());
            Assert.Equal("Underline", toolbar.FindAll("." + HelperCls.ToolbarItem)[4].GetInnerText().Trim());
            Assert.Equal("Italic", toolbar.FindAll("." + HelperCls.ToolbarItem)[5].GetInnerText().Trim());
            var italicIconHtml = toolbar.FindAll("." + HelperCls.ToolbarButtonIcon)[5];
            Assert.NotNull(italicIconHtml);
            Assert.Contains(HelperCls.IconLeft, italicIconHtml.ClassName);
            Assert.Contains(HelperCls.ItalicIcon, italicIconHtml.ClassName);
            toolbar.FindAll("button")[1].Click();
            await Task.Delay(100);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonText).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonSpanText).Count);
            Assert.Equal(5, toolbar.FindAll("." + HelperCls.ToolbarButtonIcon).Count);
            Assert.Equal("Copy", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal("Bold", toolbar.FindAll("." + HelperCls.ToolbarItem)[2].GetInnerText().Trim());
            Assert.Equal("Underline", toolbar.FindAll("." + HelperCls.ToolbarItem)[3].GetInnerText().Trim());
            Assert.Equal("Italic", toolbar.FindAll("." + HelperCls.ToolbarItem)[4].GetInnerText().Trim());
        }

        [Fact(Timeout = 10000, DisplayName = "Conditional rendering if statement testing")]
        public void IfStatement()
        {
            var toolbar = RenderComponent<IfStatement>();
            var toolbarHtml = toolbar.Find("." + HelperCls.Toolbar);
            Assert.NotNull(toolbarHtml);
            Assert.Contains(HelperCls.Control, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Toolbar, toolbarHtml.ClassName);
            Assert.Contains(HelperCls.Library, toolbarHtml.ClassName);
            var toolbarStyle = toolbarHtml.GetAttribute("data-sf-style").Replace(" ", string.Empty);
            Assert.Contains("width:auto", toolbarStyle);
            Assert.Contains("height:auto", toolbarStyle);
            Assert.Equal(2, toolbar.FindAll("." + HelperCls.ToolbarItem).Count);
            Assert.Equal(2, toolbar.FindAll("." + HelperCls.ToolbarButton).Count);
            Assert.Equal(2, toolbar.FindAll("." + HelperCls.ToolbarButtonText).Count);
            Assert.Equal(2, toolbar.FindAll("." + HelperCls.ToolbarButtonSpanText).Count);
            Assert.Equal("Cut", toolbar.Find("." + HelperCls.ToolbarItem).GetInnerText().Trim());
            Assert.Equal(HelperCls.Cut, toolbar.FindAll("." + HelperCls.ToolbarItem)[0].GetAttribute("title"));
            Assert.Equal("Paste", toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetInnerText().Trim());
            Assert.Equal(HelperCls.Paste, toolbar.FindAll("." + HelperCls.ToolbarItem)[1].GetAttribute("title"));
        }
    }
}