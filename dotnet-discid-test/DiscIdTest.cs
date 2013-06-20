namespace DiscIdTests
{
	using System;
	using NUnit.Framework;
	using DiscId;

	[TestFixture]
    public class DiscIdTest
    {
		[Test]
        public void GetDefaultDeviceTest()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(Disc.DefaultDevice));
        }

		[Test]
        public void PutTest()
        {
            string discId = "Wn8eRBtfLDfM0qjYPdxrz.Zjs_U-";
            int firstTrack = 1;
            int lastTrack = 10;
            int sectors = 206535;
            int[] offsets = new int[] { 150, 18901, 39738, 59557, 79152, 100126,
                                        124833, 147278, 166336, 182560 };
            
            var disc = DiscId.Disc.Put(firstTrack, sectors, offsets);

            Assert.AreEqual(discId, disc.Id);
            Assert.AreEqual(firstTrack, disc.FirstTrackNumber);
            Assert.AreEqual(lastTrack, disc.LastTrackNumber);
            Assert.AreEqual(sectors, disc.Sectors);
        }
    }
}
