using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using SimpleMvcSitemap.Routing;
using Xunit;
// ReSharper disable UnusedMember.Local

namespace SimpleMvcSitemap.Tests
{
    public class ReflectionHelperTests : TestBase
    {
        private readonly IReflectionHelper reflectionHelper;

        public ReflectionHelperTests()
        {
            reflectionHelper = new ReflectionHelper();

        }

        private class SampleType1 { }

        [Fact]
        public void GetUrlProperties_ClassHasNoProperties_DoesNotThrowException()
        {
            reflectionHelper.GetPropertyModel(typeof(SampleType1)).Should().NotBeNull();
        }


        private class SampleType2
        {
            [Url]
            public string Url { get; set; }

            public string Title { get; set; }

            [Url]
            public string Url4 => null;

            [Url]
            public string Url5 { set { } }

            [Url]
            public int Url2 { get; set; }

            [Url]
            public SampleType2 Url3 { get; set; }
        }

        [Fact]
        public void GetUrlProperties_ClassHasUrlProperties_ReturnUrlProperty()
        {
            UrlPropertyModel urlPropertyModel = reflectionHelper.GetPropertyModel(typeof(SampleType2));

            urlPropertyModel.UrlProperties.Should().HaveCount(1).And.ContainSingle(info => info.Name == "Url");
        }

        private class SampleType3
        {
            public List<SampleType1> List1 { get; set; }
            [Url]
            public SampleType1[] List2 { get; set; }
            public SampleType1 Item { get; set; }
            public IEnumerable List3 { get; set; }
            public IEnumerable<SampleType2> List4 { set { } }
        }

        [Fact]
        public void GetUrlProperties_ClassHasEnumerableProperties_FindsEnumerableProperties()
        {
            UrlPropertyModel urlPropertyModel = reflectionHelper.GetPropertyModel(typeof(SampleType3));

            urlPropertyModel.UrlProperties.Should().BeEmpty();
            urlPropertyModel.EnumerableProperties.Should().HaveCount(3);
        }

        private class SampleType4
        {
            public SampleType1 SampleType1 { get; set; }
            [Url]
            public SampleType2 SampleType2 { get; set; }

            public string S { get; set; }

            public SampleType3 SampleType3 { set { } }
        }

        [Fact]
        public void GetUrlProperties_ClassHasClassProperties_FindsClassProperties()
        {
            UrlPropertyModel urlPropertyModel = reflectionHelper.GetPropertyModel(typeof(SampleType4));

            urlPropertyModel.UrlProperties.Should().BeEmpty();
            urlPropertyModel.ClassProperties.Should().HaveCount(2);
        }

    }
}