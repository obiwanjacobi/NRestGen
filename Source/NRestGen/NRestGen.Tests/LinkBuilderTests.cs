using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NRestGen.Tests
{
    [TestClass]
    public class LinkBuilderTests
    {
        [TestMethod]
        public void SelfLink()
        {
            var builder = new LinkBuilder("api/customer", "?$skip=10&$top=10");
            var link = builder.Self();

            link.Rel.Should().Be(Link.RelSelf);
            link.Action.Should().Be(Link.ActionGet);
            link.Href.Should().Be("api/customer?$skip=10&$top=10");
        }

        [TestMethod]
        public void NextPageLink()
        {
            var builder = new LinkBuilder("api/customer", "?");
            var link = builder.NextPage(0, 10);

            link.Rel.Should().Be(Link.RelNextPage);
            link.Action.Should().Be(Link.ActionGet);
            link.Href.Should().Be("api/customer?$skip=10&$top=10");
        }

        [TestMethod]
        public void PrevPageLink()
        {
            var builder = new LinkBuilder("api/customer", "?");
            var link = builder.PrevPage(20, 10);

            link.Rel.Should().Be(Link.RelPrevPage);
            link.Action.Should().Be(Link.ActionGet);
            link.Href.Should().Be("api/customer?$skip=10&$top=10");
        }
    }
}
