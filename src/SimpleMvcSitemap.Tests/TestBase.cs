using System;
using Moq;

namespace SimpleMvcSitemap.Tests
{
    public class TestBase : IDisposable
    {
        private readonly MockRepository _mockRepository;

        protected TestBase()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            VerifyAll = true;
        }

        protected Mock<T> MockFor<T>() where T : class
        {
            return _mockRepository.Create<T>();
        }


        protected bool VerifyAll { get; set; }


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