using Bunit;
using Syncfusion.Blazor.Navigations;
using Xunit;

namespace Syncfusion.Blazor.Tests.Accordion
{
    public class ContentRenderMultipleExpandModeEvent : BunitTestContext
    {
        public Helper HelperCls = new();

        [Fact(Timeout = 10000, DisplayName = "Accordion Events testing")]
        public void EventHandling()
        {
            var createdEventcount = 0;
            var clickedEventcount = 0;
            var expandingEventcount = 0;
            var expandedEventcount = 0;
            var collapsingEventcount = 0;
            var collapsedEventcount = 0;
            var destroyEventcount = 0;
            var accordion = RenderComponent<SfAccordion>(options => options.Add(content => content.LoadOnDemand, false).AddChildContent<AccordionEvents>(events => events.Add(e => e.Created, (object args) => {
                    createdEventcount++;
                    Assert.Equal(1, createdEventcount);
                }).Add(e => e.Clicked, (AccordionClickArgs args) => {
                    clickedEventcount++;
                    Assert.Equal(1, clickedEventcount);
                    Assert.NotNull(args.Item);
                }).Add(e => e.Expanding, (ExpandEventArgs args) => {
                    expandingEventcount++;
                    Assert.Equal(1, expandingEventcount);
                    Assert.NotNull(args.Item);
                }).Add(e => e.Expanded, (ExpandedEventArgs args) => {
                    expandedEventcount++;
                    Assert.Equal(1, expandedEventcount);
                    Assert.NotNull(args.Item);
                }).Add(e => e.Collapsing, (CollapseEventArgs args) => {
                    collapsingEventcount++;
                    Assert.Equal(1, collapsingEventcount);
                    Assert.NotNull(args.Item);
                }).Add(e => e.Collapsed, (CollapsedEventArgs args) => {
                    collapsedEventcount++;
                    Assert.Equal(1, collapsedEventcount);
                    Assert.NotNull(args.Item);
                }).Add(e => e.Destroyed, (object args) => {
                    destroyEventcount++;
                    Assert.Equal(1, destroyEventcount);
                })
           ));
        }

        [Fact(Timeout = 10000, DisplayName = "Clicked event arguments - Default value testing")]
        public void ClickedEventArgs()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(content => content.LoadOnDemand, false).AddChildContent<AccordionEvents>(events => events.Add(e => e.Clicked, (AccordionClickArgs args) => {
                    Assert.Equal(HelperCls.Clicked, args.Name);
                    Assert.Null(args.Item.HeaderTemplate);
                    Assert.Null(args.Item.ContentTemplate);
                    Assert.Null(args.Item.Content);
                    Assert.Null(args.Item.CssClass);
                    Assert.False(args.Item.Disabled);
                    Assert.False(args.Item.Expanded);
                    Assert.Null(args.Item.Header);
                    Assert.Null(args.Item.IconCss);
                    Assert.True(args.Item.Visible);
                })
            ));
        }

        [Fact(Timeout = 10000, DisplayName = "Expanding event arguments - Default value testing")]
        public void ExpandingEventArgs()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(content => content.LoadOnDemand, false).AddChildContent<AccordionEvents>(events => events.Add(e => e.Expanding, (ExpandEventArgs args) => {
                    Assert.Equal(HelperCls.Expanding, args.Name);
                    Assert.False(args.Cancel);
                    Assert.Equal(0, args.Index);
                    Assert.False(args.IsExpanded);
                    Assert.Null(args.Item.HeaderTemplate);
                    Assert.Null(args.Item.ContentTemplate);
                    Assert.Null(args.Item.Content);
                    Assert.Null(args.Item.CssClass);
                    Assert.False(args.Item.Disabled);
                    Assert.False(args.Item.Expanded);
                    Assert.Null(args.Item.Header);
                    Assert.Null(args.Item.IconCss);
                    Assert.True(args.Item.Visible);
                })
            ));
        }

        [Fact(Timeout = 10000, DisplayName = "Expanded event arguments - Default value testing")]
        public void ExpandedEventArgs()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(content => content.LoadOnDemand, false).AddChildContent<AccordionEvents>(events => events.Add(e => e.Expanded, (ExpandedEventArgs args) => {
                    Assert.Equal(HelperCls.Expanded, args.Name);
                    Assert.Equal(0, args.Index);
                    Assert.False(args.IsExpanded);
                    Assert.Null(args.Item.HeaderTemplate);
                    Assert.Null(args.Item.ContentTemplate);
                    Assert.Null(args.Item.Content);
                    Assert.Null(args.Item.CssClass);
                    Assert.False(args.Item.Disabled);
                    Assert.False(args.Item.Expanded);
                    Assert.Null(args.Item.Header);
                    Assert.Null(args.Item.IconCss);
                    Assert.True(args.Item.Visible);
                })
            ));
        }

        [Fact(Timeout = 10000, DisplayName = "Collapsing event arguments - Default value testing")]
        public void CollapsingEventArgs()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(content => content.LoadOnDemand, false).AddChildContent<AccordionEvents>(events => events.Add(e => e.Collapsing, (CollapseEventArgs args) => {
                    Assert.Equal(HelperCls.Collapsing, args.Name);
                    Assert.False(args.Cancel);
                    Assert.Equal(0, args.Index);
                    Assert.False(args.IsExpanded);
                    Assert.Null(args.Item.HeaderTemplate);
                    Assert.Null(args.Item.ContentTemplate);
                    Assert.Null(args.Item.Content);
                    Assert.Null(args.Item.CssClass);
                    Assert.False(args.Item.Disabled);
                    Assert.False(args.Item.Expanded);
                    Assert.Null(args.Item.Header);
                    Assert.Null(args.Item.IconCss);
                    Assert.True(args.Item.Visible);
                })
            ));
        }

        [Fact(Timeout = 10000, DisplayName = "Collapsed event arguments - Default value testing")]
        public void CollapsedEventArgs()
        {
            var accordion = RenderComponent<SfAccordion>(options => options.Add(content => content.LoadOnDemand, false).AddChildContent<AccordionEvents>(events => events.Add(e => e.Collapsed, (CollapsedEventArgs args) => {
                    Assert.Equal(HelperCls.Collapsed, args.Name);
                    Assert.Equal(0, args.Index);
                    Assert.False(args.IsExpanded);
                    Assert.Null(args.Item.HeaderTemplate);
                    Assert.Null(args.Item.ContentTemplate);
                    Assert.Null(args.Item.Content);
                    Assert.Null(args.Item.CssClass);
                    Assert.False(args.Item.Disabled);
                    Assert.False(args.Item.Expanded);
                    Assert.Null(args.Item.Header);
                    Assert.Null(args.Item.IconCss);
                    Assert.True(args.Item.Visible);
                })
            ));
        }
    }
}