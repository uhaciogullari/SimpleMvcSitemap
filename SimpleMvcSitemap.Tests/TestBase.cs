using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace SimpleMvcSitemap.Tests
{
    [TestFixture]
    public class TestBase
    {
        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            FakeDataRepository = new Fixture();
            VerifyAll = true;
            FinalizeSetUp();
        }

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

        private MockRepository _mockRepository;

        protected Mock<T> MockFor<T>() where T : class
        {
            return _mockRepository.Create<T>();
        }

        protected Mock<T> MockFor<T>(params object[] @params) where T : class
        {
            return _mockRepository.Create<T>(@params);
        }

        protected void EnableCustomization(ICustomization customization)
        {
            customization.Customize(FakeDataRepository);
        }

        protected void EnableCustomization<T>() where T : ICustomization, new()
        {
            new T().Customize(FakeDataRepository);
        }

        protected IFixture FakeDataRepository { get; set; }
        protected bool VerifyAll { get; set; }

        protected virtual void FinalizeTearDown() { }

        protected virtual void FinalizeSetUp() { }
    }
}