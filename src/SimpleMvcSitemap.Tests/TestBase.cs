using System;
using System.Collections.Generic;
using Moq;
using Ploeh.AutoFixture;

namespace SimpleMvcSitemap.Tests
{
    public class TestBase : IDisposable
    {
        private readonly MockRepository _mockRepository;

        protected TestBase()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            FakeDataRepository = new Fixture();
            VerifyAll = true;
        }

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


        public virtual void Dispose()
        {
            if (VerifyAll)
            {
                _mockRepository.VerifyAll();
            }
            else
            {
                _mockRepository.Verify();
            }
        }
    }
}