using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SimpleMvcSitemap.Tests
{
    [TestFixture]
    public class TestBase
    {
        private MockRepository _mockRepository;

        protected Mock<T> MockFor<T>() where T : class
        {
            return _mockRepository.Create<T>();
        }

        protected IFixture FakeDataRepository { get; set; }

        protected bool VerifyAll { get; set; }


        protected T Create<T>()
        {
            return FakeDataRepository.Create<T>();
        }
        
        protected IEnumerable<T> CreateMany<T>()
        {
            return FakeDataRepository.CreateMany<T>();
        }

        protected IEnumerable<T> CreateMany<T>(int count)
        {
            return FakeDataRepository.CreateMany<T>(count);
        }


        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            FakeDataRepository = new Fixture();
            VerifyAll = true;
            FinalizeSetUp();
        }

        protected virtual void FinalizeSetUp() { }


        [TearDown]
        public void TearDown()
        {
            if (VerifyAll)
            {
                _mockRepository.VerifyAll();
            }
            else
            {
                _mockRepository.Verify();
            }
            FinalizeTearDown();
        }

        protected virtual void FinalizeTearDown() { }

    }
}