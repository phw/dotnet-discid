namespace DiscIdTests
{
    using DiscId;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DiscIdTest
    {
        [TestMethod]
        public void GetDefaultDeviceTest()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(Disc.DefaultDevice));
        }
    }
}
