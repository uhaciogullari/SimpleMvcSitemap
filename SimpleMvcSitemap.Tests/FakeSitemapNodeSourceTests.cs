using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace SimpleMvcSitemap.Tests
{
    public class FakeSitemapNodeSourceTests : TestBase
    {
        [Test]
        public void Count_WhenCountIsNotSet_ThrowsException()
        {
            FakeSitemapNodeSource fakeSitemapNodeSource = new FakeSitemapNodeSource();

            Action act = () => { int count = fakeSitemapNodeSource.Count(); };

            act.ShouldThrow<NotImplementedException>();
        }


        [Test]
        public void Count_WhenCountIsSet_ReturnsCount()
        {
            FakeSitemapNodeSource fakeSitemapNodeSource = new FakeSitemapNodeSource().SetCount(7);

            fakeSitemapNodeSource.Count().Should().Be(7);
        }


        [Test]
        public void Skip_SetsItemCountToSkip()
        {
            FakeSitemapNodeSource fakeSitemapNodeSource = new FakeSitemapNodeSource();

            fakeSitemapNodeSource.Skip(10);

            fakeSitemapNodeSource.SkippedItemCount.Should().Be(10);
        }


        [Test]
        public void Take_TakesItemCountToTake()
        {
            FakeSitemapNodeSource fakeSitemapNodeSource = new FakeSitemapNodeSource();

            fakeSitemapNodeSource.Take(12);

            fakeSitemapNodeSource.TakenItemCount.Should().Be(12);
        }


    }
}