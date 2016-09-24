using System;
using Moq;

namespace SimpleMvcSitemap.Tests
{
    public class TestBase : IDisposable
    {
        private readonly MockRepository mockRepository;

        protected TestBase()
        {
            mockRepository = new MockRepository(MockBehavior.Strict);
            VerifyAll = true;
        }

        protected Mock<T> MockFor<T>() where T : class
        {
            return mockRepository.Create<T>();
        }


        protected bool VerifyAll { get; set; }


        public virtual void Dispose()
        {
            if (VerifyAll)
            {
                mockRepository.VerifyAll();
            }
            else
            {
                mockRepository.Verify();
            }
        }
    }
}