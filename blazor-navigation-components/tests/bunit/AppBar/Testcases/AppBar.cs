using Xunit;
using Bunit;
using AngleSharp.Dom;
using AngleSharp.Css.Dom;
using System.Threading.Tasks;
using Syncfusion.Blazor.Tests.AppBar.Samples;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Components.Web;

namespace Syncfusion.Blazor.Tests.AppBar
{
    public class AppBar : BunitTestContext
    {
        public Helper HelperCls = new Helper();

        [Fact(Timeout = 10000, DisplayName = "Initial loading testing")]
        public void Default()
        {
            var cut = RenderComponent<Default>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
            Assert.Contains(HelperCls.Inherit, cut.Find("." + HelperCls.Button).ClassName);
            Assert.Contains("e-menu", cut.Find("." + HelperCls.ButtonIcon).ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Properties default value testing- AppBar")]
        public void DefaultValueAppBar()
        {
            var cut = RenderComponent<SfAppBar>();
            Assert.Equal(AppBarMode.Regular, cut.Instance.Mode);
            Assert.Equal(AppBarColor.Light, cut.Instance.ColorMode);
            Assert.Equal(AppBarPosition.Top, cut.Instance.Position);
            Assert.False(cut.Instance.IsSticky);
            Assert.Null(cut.Instance.CssClass);
        }

        [Fact(Timeout = 10000, DisplayName = "Prominent AppBar testing")]
        public void Prominent()
        {
            var cut = RenderComponent<Prominent>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Prominent, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
        }

        [Fact(Timeout = 10000, DisplayName = "Dense AppBar testing")]
        public void Dense()
        {
            var cut = RenderComponent<Dense>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Dense, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
        }

        [Fact(Timeout = 10000, DisplayName = "Light AppBar testing")]
        public void Light()
        {
            var cut = RenderComponent<Light>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
        }

        [Fact(Timeout = 10000, DisplayName = "Dark AppBar testing")]
        public void Dark()
        {
            var cut = RenderComponent<Dark>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Dark, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
        }

        [Fact(Timeout = 10000, DisplayName = "Primary AppBar testing")]
        public void Primary()
        {
            var cut = RenderComponent<Primary>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Primary, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
        }

        [Fact(Timeout = 10000, DisplayName = "Inherit AppBar testing")]
        public void Inherit()
        {
            var cut = RenderComponent<Inherit>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Inherit, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
        }

        [Fact(Timeout = 10000, DisplayName = "AppBarSpacer testing")]
        public void AppBarSpacer()
        {
            var cut = RenderComponent<Spacer>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Equal(HelperCls.AppBarSpacer, cut.Find("." + HelperCls.AppBarSpacer).ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
        }

        [Fact(Timeout = 10000, DisplayName = "AppBarSeparator testing")]
        public void AppBarSeparator()
        {
            var cut = RenderComponent<Separator>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Equal(HelperCls.AppBarSeparator, cut.Find("." + HelperCls.AppBarSeparator).ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
        }

        [Fact(Timeout = 10000, DisplayName = "AppBar Buttons testing")]
        public void AppBarButtons()
        {
            var cut = RenderComponent<AppBarButtons>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
            Assert.Contains(HelperCls.Inherit, cut.Find("." + HelperCls.Button).ClassName);
            Assert.Contains(HelperCls.Inherit, cut.Find("." + HelperCls.DropDownButton).ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "AppBar Menu testing")]
        public void Menu()
        {
            var cut = RenderComponent<AppBarMenu>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
            Assert.Contains(HelperCls.Inherit, cut.Find("." + HelperCls.Button).ClassName);
            Assert.Contains(HelperCls.Inherit, cut.Find("." + HelperCls.MenuContainer).ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Bottom AppBar testing")]
        public void Bottom()
        {
            var cut = RenderComponent<Bottom>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
            Assert.Contains(HelperCls.HorizontalBottom, appbarHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "Sticky AppBar testing")]
        public void Sticky()
        {
            var cut = RenderComponent<Sticky>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
            Assert.Contains(HelperCls.Sticky, appbarHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "CssClass testing")]
        public void CssClass()
        {
            var cut = RenderComponent<CssClass>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
            Assert.Contains(HelperCls.CustomAppBar, appbarHtml.ClassName);
        }

        [Fact(Timeout = 10000, DisplayName = "HtmlAttributes testing")]
        public void HtmlAttributes()
        {
            var cut = RenderComponent<HtmlAttributes>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
            Assert.Equal("appbar", cut.Find("." + HelperCls.AppBar).GetAttribute("aria-label"));
        }

        [Fact(Timeout = 10000, DisplayName = "Property Changes testing")]
        public void AppBarPropertyChanges()
        {
            var cut = RenderComponent<AppBarPropertyChanges>();
            var appbarHtml = cut.Find("." + HelperCls.AppBar);
            Assert.NotNull(appbarHtml);
            Assert.Contains(HelperCls.Control, appbarHtml.ClassName);
            Assert.Contains(HelperCls.AppBar, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Light, appbarHtml.ClassName);
            Assert.Contains(HelperCls.Header, appbarHtml.NodeName);
            cut.Instance.HeightMode = AppBarMode.Prominent;
            cut.Render();
            Assert.Contains(HelperCls.Prominent, cut.Find("." + HelperCls.AppBar).ClassName);
            cut.Instance.HeightMode = AppBarMode.Dense;
            cut.Render();
            Assert.Contains(HelperCls.Dense, cut.Find("." + HelperCls.AppBar).ClassName);
            cut.Instance.ColorMode = AppBarColor.Dark;
            cut.Render();
            Assert.Contains(HelperCls.Dark, cut.Find("." + HelperCls.AppBar).ClassName);
            cut.Instance.ColorMode = AppBarColor.Primary;
            cut.Render();
            Assert.Contains(HelperCls.Primary, cut.Find("." + HelperCls.AppBar).ClassName);
            cut.Instance.ColorMode = AppBarColor.Inherit;
            cut.Render();
            Assert.Contains(HelperCls.Inherit, cut.Find("." + HelperCls.AppBar).ClassName);
            cut.Instance.ColorMode = AppBarColor.Light;
            cut.Render();
            Assert.Contains(HelperCls.Light, cut.Find("." + HelperCls.AppBar).ClassName);
            cut.Instance.PositionMode = AppBarPosition.Bottom;
            cut.Render();
            Assert.Contains(HelperCls.HorizontalBottom, cut.Find("." + HelperCls.AppBar).ClassName);
            cut.Instance.Sticky = true;
            cut.Render();
            Assert.Contains(HelperCls.Sticky, cut.Find("." + HelperCls.AppBar).ClassName);
            Assert.Contains("light-bg", cut.Find("." + HelperCls.AppBar).ClassName);
            cut.Instance.cssClass = "dark-bg";
            cut.Render();
            Assert.Contains("dark-bg", cut.Find("." + HelperCls.AppBar).ClassName);
        }
    }

}