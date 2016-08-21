using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace SimpleMvcSitemap.Tests
{
    public class FakeSitemapNodeSourceTests : TestBase
    {
        [Fact]
        public void Count_WhenCountIsNotSet_ThrowsException()
        {
            FakeDataSource fakeDataSource = new FakeDataSource();

            Action act = () => { fakeDataSource.Count(); };

            act.ShouldThrow<NotImplementedException>();
        }


        [Fact]
        public void Count_WhenCountIsSet_ReturnsCount()
        {
            FakeDataSource fakeDataSource = new FakeDataSource().WithCount(7);

            fakeDataSource.Count().Should().Be(7);
        }


        [Fact]
        public void Skip_SetsItemCountToSkip()
        {
            FakeDataSource fakeDataSource = new FakeDataSource();

            fakeDataSource.Skip(10);

            fakeDataSource.SkippedItemCount.Should().Be(10);
        }


        [Fact]
        public void Take_TakesItemCountToTake()
        {
            FakeDataSource fakeDataSource = new FakeDataSource();

            fakeDataSource.Take(12);

            fakeDataSource.TakenItemCount.Should().Be(12);
        }


    }
}