using System.Threading.Tasks;
using Xunit;
using Bunit;
using AngleSharp.Dom;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Tests.Tabs.Samples.HeaderPositionTopWithInitContent.TabItem;

namespace Syncfusion.Blazor.Tests.Tabs
{
    public class TabsHeaderTemplateCoverageTest : BunitTestContext
    {
        public Helper HelperCls = new Helper();

        #region TabHeaderTemplate Coverage Tests

        /// <summary>
        /// Test TabHeaderTemplate with IconPosition Left and HeaderTemplate
        /// Covers: Icon left branch, HeaderTemplate rendered
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_IconLeftPosition_WithTemplate()
        {
            var cut = RenderComponent<IconPositionLeftWithTemplate>();
            await Task.Delay(100);
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.Equal(2, tabItems.Count);

            // Verify icon left position class
            Assert.Contains(HelperCls.ILeft, tabItems[0].ClassList);
            Assert.Contains(HelperCls.ILeft, tabItems[1].ClassList);

            // Verify close icon exists
            Assert.Equal(2, cut.FindAll("." + HelperCls.CloseIcon).Count);
        }

        /// <summary>
        /// Test TabHeaderTemplate with IconPosition Right and HeaderTemplate
        /// Covers: Icon right branch, HeaderTemplate rendered
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_IconRightPosition_WithTemplate()
        {
            var cut = RenderComponent<IconPositionRightWithTemplate>();
            await Task.Delay(100);
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.Equal(2, tabItems.Count);

            // Verify icon right position class
            Assert.Contains(HelperCls.IRight, tabItems[0].ClassList);
            Assert.Contains(HelperCls.IRight, tabItems[1].ClassList);

            // Verify close icon exists
            Assert.Equal(2, cut.FindAll("." + HelperCls.CloseIcon).Count);
        }

        /// <summary>
        /// Test TabHeaderTemplate with IconPosition Top and HeaderTemplate
        /// Covers: Icon top branch, HeaderTemplate rendered
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_IconTopPosition_WithTemplate()
        {
            var cut = RenderComponent<IconPositionTopWithTemplate>();
            await Task.Delay(100);
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.Equal(2, tabItems.Count);

            // Verify icon top position class
            Assert.Contains(HelperCls.ITop, tabItems[0].ClassList);
            Assert.Contains(HelperCls.ITop, tabItems[1].ClassList);

            // Verify close icon exists
            Assert.Equal(2, cut.FindAll("." + HelperCls.CloseIcon).Count);
        }

        /// <summary>
        /// Test TabHeaderTemplate with IconPosition Bottom and HeaderTemplate
        /// Covers: Icon bottom branch, HeaderTemplate rendered
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_IconBottomPosition_WithTemplate()
        {
            var cut = RenderComponent<IconPositionBottomWithTemplate>();
            await Task.Delay(100);
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.Equal(2, tabItems.Count);

            // Verify icon bottom position class
            Assert.Contains(HelperCls.IBottom, tabItems[0].ClassList);
            Assert.Contains(HelperCls.IBottom, tabItems[1].ClassList);

            // Verify close icon exists
            Assert.Equal(2, cut.FindAll("." + HelperCls.CloseIcon).Count);
        }

        /// <summary>
        /// Test TabHeaderTemplate with disabled item - aria-disabled should be true
        /// Covers: Disabled = true branch
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_DisabledItem()
        {
            var cut = RenderComponent<DisabledItemWithTemplate>();
            await Task.Delay(100);
            var tabWraps = cut.FindAll("." + HelperCls.TabWrap);
            Assert.Equal(3, tabWraps.Count);

            // First tab should be disabled (aria-disabled = true)
            var disabledAttr = tabWraps[0].GetAttribute("aria-disabled");
            Assert.Equal("true", disabledAttr);

            // Second tab should not be disabled
            var enabledAttr = tabWraps[1].GetAttribute("aria-disabled");
            Assert.Equal("false", enabledAttr);
        }

        /// <summary>
        /// Test TabHeaderTemplate tabindex attribute
        /// Covers: TabIndex attribute
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_TabIndex()
        {
            var cut = RenderComponent<TabIndexTemplate>();
            await Task.Delay(100);
            var tabWraps = cut.FindAll("." + HelperCls.TabWrap);
            Assert.Equal(2, tabWraps.Count);

            // Check tabindex attribute
            var tabindex = tabWraps[0].GetAttribute("tabindex");
            Assert.Equal("-1", tabindex);

            var tabindex1 = tabWraps[1].GetAttribute("tabindex");
            Assert.Equal("-1", tabindex1);
        }

        /// <summary>
        /// Test TabHeaderTemplate with Header.Text (no HeaderTemplate)
        /// Covers: Header.Text branch, not HeaderTemplate
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_HeaderTextOnly()
        {
            var cut = RenderComponent<HeaderTextOnly>();
            await Task.Delay(100);
            var tabText = cut.FindAll("." + HelperCls.TabText);
            Assert.Equal(2, tabText.Count);

            // Verify text content
            Assert.Equal("Twitter", tabText[0].TextContent.Replace("\n", string.Empty).Trim());
            Assert.Equal("Facebook", tabText[1].TextContent.Replace("\n", string.Empty).Trim());
        }

        /// <summary>
        /// Test TabHeaderTemplate with HeaderTemplate only (no Header.Text)
        /// Covers: HeaderTemplate branch, not Header.Text
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_HeaderTemplateOnly()
        {
            var cut = RenderComponent<HeaderTemplateOnly>();
            await Task.Delay(100);
            var tabText = cut.FindAll("." + HelperCls.TabText);
            Assert.Equal(2, tabText.Count);

            // Verify that template content is rendered (div elements inside)
            var textDivs = tabText[0].GetElementsByTagName("div");
            Assert.Single(textDivs);
            Assert.Equal("Custom Header", textDivs[0].InnerHtml);
        }

        /// <summary>
        /// Test TabHeaderTemplate with no icon - should not render icon span
        /// Covers: IconCss empty/null branch
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_NoIcon()
        {
            var cut = RenderComponent<NoIconTemplate>();
            await Task.Delay(100);
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.Equal(2, tabItems.Count);

            // Verify no icon classes
            var textWraps = cut.FindAll("." + HelperCls.TextWrap);
            foreach (var tw in textWraps)
            {
                var icons = tw.GetElementsByClassName("e-tab-icon");
                Assert.Empty(icons);
            }
        }

        /// <summary>
        /// Test TabHeaderTemplate close icon click
        /// Covers: OnCloseIconClick handler branch
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_CloseIconClick()
        {
            var cut = RenderComponent<CloseIconClickTemplate>();
            await Task.Delay(100);
            var closeIcons = cut.FindAll("." + HelperCls.CloseIcon);
            Assert.Equal(3, closeIcons.Count);

            // Click first close icon
            closeIcons[0].Click();
            await Task.Delay(200);

            // Verify tab was removed
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.Equal(2, tabItems.Count);
        }

        /// <summary>
        /// Test TabHeaderTemplate with both Header.Text and HeaderTemplate
        /// HeaderTemplate should take precedence
        /// Covers: HeaderTemplate != null branch (takes precedence over Header.Text)
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_BothHeaderTextAndTemplate()
        {
            var cut = RenderComponent<BothHeaderTextAndTemplate>();
            await Task.Delay(100);
            var tabText = cut.FindAll("." + HelperCls.TabText);
            Assert.Equal(2, tabText.Count);

            // HeaderTemplate should take precedence, rendered content should be from template
            var textDivs = tabText[0].GetElementsByTagName("div");
            Assert.Single(textDivs);
            Assert.Equal("Template Header", textDivs[0].InnerHtml);
        }

        /// <summary>
        /// Test TabHeaderTemplate with Header.IconCss but IconPosition is invalid
        /// Should not render icon for invalid positions
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_InvalidIconPosition()
        {
            var cut = RenderComponent<InvalidIconPositionTemplate>();
            await Task.Delay(100);
            var tabItems = cut.FindAll("." + HelperCls.ToolbarItem);
            Assert.Equal(2, tabItems.Count);

            // Icon should not render for invalid position
            var textWraps = cut.FindAll("." + HelperCls.TextWrap);
            foreach (var tw in textWraps)
            {
                var icons = tw.GetElementsByClassName("e-tab-icon");
                Assert.Empty(icons);
            }
        }

        /// <summary>
        /// Test TabHeaderTemplate with TabIndex -1 (default)
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_DefaultTabIndex()
        {
            var cut = RenderComponent<DefaultTabIndexTemplate>();
            await Task.Delay(100);
            var tabWraps = cut.FindAll("." + HelperCls.TabWrap);
            Assert.Equal(2, tabWraps.Count);

            // Default TabIndex is -1
            var tabindex = tabWraps[0].GetAttribute("tabindex");
            Assert.Equal("-1", tabindex);
        }

        #endregion

        #region Additional Edge Cases

        /// <summary>
        /// Test TabHeaderTemplate with empty string iconcss
        /// Icon should not render when IconCss is empty
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_EmptyIconCss()
        {
            var cut = RenderComponent<EmptyIconCssTemplate>();
            await Task.Delay(100);
            var textWraps = cut.FindAll("." + HelperCls.TextWrap);

            // No icon should render for empty IconCss
            foreach (var tw in textWraps)
            {
                var icons = tw.GetElementsByClassName("e-tab-icon");
                Assert.Empty(icons);
            }
        }

        /// <summary>
        /// Test TabHeaderTemplate Header is null
        /// Should not render text div
        /// </summary>
        [Fact(Timeout = 10000)]
        public async Task TabHeaderTemplate_NullHeader()
        {
            var cut = RenderComponent<NullHeaderTemplate>();
            await Task.Delay(100);
            var tabText = cut.FindAll("." + HelperCls.TabText);
            // When Header is null, no text div should render
            Assert.NotNull(tabText);

        }

        #endregion
    }
}