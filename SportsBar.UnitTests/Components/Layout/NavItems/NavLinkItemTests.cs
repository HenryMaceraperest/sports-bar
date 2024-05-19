using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bunit.TestDoubles;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using SportsBar.Components.Layout.NavItems;

namespace SportsBar.UnitTests.Components.Layout.NavItems
{
    public class NavLinkItemTests
    {
        [Fact]
        public void NavLinkItemShouldNotBeActiveWhenInitializing()
        {
            using var testContext = new TestContext();

            var cut = testContext.RenderComponent<NavLinkItem>(parameters => parameters
                .Add(p => p.Href, "/test")
                .Add<RenderFragment>(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, "Test");
                }));
            var div = cut.Find("div");

            div.ClassList.Should().NotBeNull();
            div.ClassList.Should().Contain("nav-item");
            div.ClassList.Should().NotContain("active");
        }

        [Fact]
        public void NavLinkItemShouldNavigateToHrefIfHrefIsNotNull()
        {
            using var testContext = new TestContext();
            var testNavManager = testContext.Services.GetRequiredService<NavigationManager>();

            var cut = testContext.RenderComponent<NavLinkItem>(parameters => parameters
                .Add(p => p.Href, "/test")
                .Add<RenderFragment>(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, "Test");
                }));
            cut.Find("div").Click();

            testNavManager.Uri.Should().EndWith("/test");
        }


        [Fact]
        public void NavLinkItemShouldNotNavigatefIfHrefIsNull()
        {
            using var testContext = new TestContext();
            var testNavManager = testContext.Services.GetRequiredService<NavigationManager>();
            testNavManager.NavigateTo("/test");
            var initialUri = testContext.Services.GetRequiredService<NavigationManager>().Uri;

            var cut = testContext.RenderComponent<NavLinkItem>(parameters => parameters
                .Add(p => p.Href, "")
                .Add<RenderFragment>(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, "Test");
                }));
            
            cut.Find("div").Click();

            testNavManager.Uri.Should().Be(initialUri);
        }

        [Fact]
        public void NavLinkItemShouldNotBeNullAndShouldHaveTheActiveClassWhenClickedOnAndUriMatchesTheHref()
        {
            using var testContext = new TestContext();
            var testNavManager = testContext.Services.GetRequiredService<NavigationManager>();
            var cut = testContext.RenderComponent<NavLinkItem>(parameters => parameters
                .Add(p => p.Href, "/test")
                .Add<RenderFragment>(p => p.ChildContent, builder =>
                {
                    builder.AddContent(0, "Test");
                }));
            var div = cut.Find("div");
            cut.Find("div").Click();

            div.ClassList.Should().NotBeNull();
            div.ClassList.Should().Contain("active");
            testNavManager.Uri.Should().EndWith("/test");
        }
    }
}
