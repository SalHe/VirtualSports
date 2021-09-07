using System.Threading.Tasks;
using NUnit.Framework;

namespace VirtualSports.AndroidEmulator.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task TestConnect()
        {
            var controller = new EmulatorController("localhost", 5554);
            Assert.IsTrue(await controller.Connect(), "此处没有正常连接到AVD");
            Assert.IsTrue(await controller.Login(), "没有正常完成授权！");
            Assert.IsTrue(await controller.UpdateLocation(114.301475, 30.604657));
            Assert.IsTrue(await controller.UpdateAcceleration(1, 2, 3));
        }
    }
}