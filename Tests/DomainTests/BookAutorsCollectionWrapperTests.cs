using BookStore.Domain.Model;
using FluentAssertions;

namespace DomainTests
{
    public class BookAutorsCollectionWrapperTests
    {
        private Author[] _authors = {
            new Author {Id=1,FullName="Test 1" },
            new Author {Id=2,FullName="Test 2" },
            new Author {Id=3,FullName="Test 3" }
        };
        private Author[] _authors2 = {
            new Author {Id=4,FullName="Test 4" },
            new Author {Id=5,FullName="Test 5" }
        };

        [Fact]
        public void AppendMany_Empty()
        {
            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);

            sut.Count.Should().Be(3);
            sut.First().Id.Should().Be(1);
            sut.Last().Id.Should().Be(3);
            sut.Last().Order.Should().Be(2);
        }

        [Fact]
        public void AppendMany_Populated()
        {
            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            sut.AppendMany(_authors2);

            sut.Count.Should().Be(5);
            sut.First().Id.Should().Be(1);
            sut.Last().Id.Should().Be(5);
        }

        [Fact]
        public void Add_Empty()
        {
            var sut = new BookAuthorsCollectionWrapper();

            sut.Add(_authors[0]);

            sut.Count.Should().Be(1);
            sut.First().Id.Should().Be(1);
        }

        [Fact]
        public void Add_Populated()
        {
            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            sut.Add(_authors2[0]);

            sut.Count.Should().Be(4);
            sut.First().Id.Should().Be(1);
            sut.Last().Id.Should().Be(4);
        }

        [Fact]
        public void InsertAfter_Empty()
        {
            var item = _authors[0];

            var sut = new BookAuthorsCollectionWrapper();

            var idx = sut.InsertAfter(item);

            idx.Should().Be(0);
        }

        [Fact]
        public void InsertAfter_AtEnd()
        {
            var item = _authors2[0];

            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            var idx = sut.InsertAfter(item);

            idx.Should().Be(3);
            sut.Count.Should().Be(4);
            sut.Last().Id.Should().Be(4);
            sut.Last().Order.Should().Be(3);
        }

        [Fact]
        public void InsertAfter_MoveFirstToMiddle()
        {
            var item = _authors[0];
            var targ = _authors[1];

            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            var idx = sut.InsertAfter(item, targ);

            idx.Should().Be(1);
            sut.Count.Should().Be(3);
            var auths = sut.ToArray();
            auths[0].Order.Should().Be(0);
            auths[0].Id.Should().Be(2);
            auths[1].Order.Should().Be(1);
            auths[1].Id.Should().Be(1);
            auths[2].Order.Should().Be(2);
            auths[2].Id.Should().Be(3);

        }

        [Fact]
        public void InsertAfter_MoveMiddleToLast()
        {
            var item = _authors[1];
            var targ = _authors[2];

            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            var idx = sut.InsertAfter(item, targ);

            idx.Should().Be(2);
            sut.Count.Should().Be(3);
            var auths = sut.ToArray();
            auths[0].Order.Should().Be(0);
            auths[0].Id.Should().Be(1);
            auths[1].Order.Should().Be(1);
            auths[1].Id.Should().Be(3);
            auths[2].Order.Should().Be(2);
            auths[2].Id.Should().Be(2);

        }
        
        [Fact]
        public void InsertAfter_MoveMiddleToEnd()
        {
            var item = _authors[1];

            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            var idx = sut.InsertAfter(item);

            idx.Should().Be(2);
            sut.Count.Should().Be(3);
            var auths = sut.ToArray();
            auths[0].Order.Should().Be(0);
            auths[0].Id.Should().Be(1);
            auths[1].Order.Should().Be(1);
            auths[1].Id.Should().Be(3);
            auths[2].Order.Should().Be(2);
            auths[2].Id.Should().Be(2);

        }
        
        [Fact]
        public void InsertBefore_Empty()
        {
            var item = _authors[0];

            var sut = new BookAuthorsCollectionWrapper();

            var idx = sut.InsertBefore(item);

            idx.Should().Be(0);
        }

        [Fact]
        public void InsertBefore_AtStart()
        {
            var item = _authors2[0];

            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            var idx = sut.InsertBefore(item);

            idx.Should().Be(0);
            sut.Count.Should().Be(4);
            sut.Last().Id.Should().Be(3);
            sut.Last().Order.Should().Be(3);
        }

        [Fact]
        public void InsertBefore_MoveMiddleToFirst()
        {
            var item = _authors[1];
            var targ = _authors[0];

            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            var idx = sut.InsertBefore(item, targ);

            idx.Should().Be(0);
            sut.Count.Should().Be(3);
            var auths = sut.ToArray();
            auths[0].Order.Should().Be(0);
            auths[0].Id.Should().Be(2);
            auths[1].Order.Should().Be(1);
            auths[1].Id.Should().Be(1);
            auths[2].Order.Should().Be(2);
            auths[2].Id.Should().Be(3);

        }

        [Fact]
        public void InsertBefore_MoveEndToMiddle()
        {
            var item = _authors[2];
            var targ = _authors[1];

            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            var idx = sut.InsertBefore(item, targ);

            idx.Should().Be(1);
            sut.Count.Should().Be(3);
            var auths = sut.ToArray();
            auths[0].Order.Should().Be(0);
            auths[0].Id.Should().Be(1);
            auths[1].Order.Should().Be(1);
            auths[1].Id.Should().Be(3);
            auths[2].Order.Should().Be(2);
            auths[2].Id.Should().Be(2);

        }
        [Fact]
        public void InsertBefore_MoveMiddleToStart()
        {
            var item = _authors[1];

            var sut = new BookAuthorsCollectionWrapper();

            sut.AppendMany(_authors);
            var idx = sut.InsertBefore(item);

            idx.Should().Be(0);
            sut.Count.Should().Be(3);
            var auths = sut.ToArray();
            auths[0].Order.Should().Be(0);
            auths[0].Id.Should().Be(2);
            auths[1].Order.Should().Be(1);
            auths[1].Id.Should().Be(1);
            auths[2].Order.Should().Be(2);
            auths[2].Id.Should().Be(3);

        }
    }
}