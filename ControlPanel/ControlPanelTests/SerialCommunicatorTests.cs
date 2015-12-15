using ControlPanel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControlPanelTests
{
    [TestClass()]
    public class SerialCommunicatorTests
    {
        private SerialCommunicator mSerialCommunicator;

        [TestInitialize()]
        public void Initialise()
        {
            mSerialCommunicator = new SerialCommunicator("COM123");
        }

        [TestMethod()]
        public void InvalidPortAddressFailsToConnect()
        {
            SerialCommunicator communicator = new SerialCommunicator("NOTAPORTNUMBER");

            Assert.IsFalse(communicator.Connect());
        }

        [TestMethod()]
        public void BadPortAddressFailsToConnect()
        {
            Assert.IsFalse(mSerialCommunicator.Connect());
        }

        [TestMethod()]
        public void UnconnectedCommunicatorWriteIsNoOp()
        {
            mSerialCommunicator.Write(null);
        }

        [TestMethod()]
        public void UnconnectedCommunicatorReadReturns0()
        {
            Assert.AreEqual(0, mSerialCommunicator.Read());
        }
    }
}
