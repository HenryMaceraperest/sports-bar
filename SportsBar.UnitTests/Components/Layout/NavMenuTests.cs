using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using SportsBar.Components.Layout;

namespace SportsBar.UnitTests.Components.Layout
{
    public class NavMenuTests
    {
        [Fact]
        public void NavMenuShouldHaveHamburgerIconClosedWhenInitializing()
        {
            using var testContext = new TestContext();

            var cut = testContext.RenderComponent<NavMenu>();

            cut.Find(".hamburger-closed").Should().NotBeNull();
            cut.FindAll(".hamburger-open").Should().BeEmpty();
        }

        [Fact]
        public void NavMenuShouldOpenHamburgerIconWhenClickedAndCloseWhenClickedAgain()
        {
            using var testContext = new TestContext();

            var cut = testContext.RenderComponent<NavMenu>();

            cut.Find(".hamburger-closed").Should().NotBeNull();
            cut.FindAll(".hamburger-open").Should().BeEmpty();

            cut.Find(".hamburger-closed").Click();

            cut.Find(".hamburger-open").Should().NotBeNull();
            cut.FindAll(".hamburger-closed").Should().BeEmpty();

            cut.Find(".hamburger-open").Click();

            cut.Find(".hamburger-closed").Should().NotBeNull();
            cut.FindAll(".hamburger-open").Should().BeEmpty();
        }

        [Fact]
        public void NavMenuShouldShowLivestreamOptionWhenThereIsAnActiveLivestreamAndHideTheLivestreamOptionWhenThereIsNoActiveLivestream()
        {
            using var testContext = new TestContext();

            var cut = testContext.RenderComponent<NavMenu>();

            var hasActiveLiveStreamBoolean = cut.Instance.hasActiveLiveStream;

            if (hasActiveLiveStreamBoolean)
            {
                cut.Find(".showLiveStreamNavLink").Should().NotBeNull();
                cut.FindAll(".hideLiveStreamNavLink").Should().BeEmpty();
            }
            else
            {
                cut.Find(".hideLiveStreamNavLink").Should().NotBeNull();
                cut.FindAll(".showLiveStreamNavLink").Should().BeEmpty();
            }

        }


    }
}
