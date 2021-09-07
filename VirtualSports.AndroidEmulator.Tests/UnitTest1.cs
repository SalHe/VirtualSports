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
            Assert.IsTrue(await controller.Connect(), "�˴�û���������ӵ�AVD");
            Assert.IsTrue(await controller.Login(), "û�����������Ȩ��");
            Assert.IsTrue(await controller.UpdateLocation(114.301475, 30.604657));
            Assert.IsTrue(await controller.UpdateAcceleration(1, 2, 3));
        }
    }
}