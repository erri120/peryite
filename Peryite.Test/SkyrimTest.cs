using Microsoft.VisualStudio.TestTools.UnitTesting;
using Peryite.Common.Skyrim;

namespace Peryite.Test
{
    [TestClass]
    public class SkyrimTest
    {
        [TestMethod]
        public void TestSkyrimSaveFile()
        {
            const string file = "skyrim-se-3.ess";

            var skyrimFile = new SkyrimSaveFile();
            skyrimFile.LoadFile(file);

            Assert.IsTrue(skyrimFile.Header.Version == 12);
            Assert.IsTrue(skyrimFile.Header.PlayerName == "Prisoner");
            Assert.IsTrue(skyrimFile.IsSpecialEdition);
            Assert.IsTrue(skyrimFile.Header.PlayerSex == 0);
            Assert.IsTrue(skyrimFile.PluginInfo.Plugins[0] == "Skyrim.esm");
            Assert.IsTrue(skyrimFile.PluginInfo.Plugins[1] == "Update.esm");
            Assert.IsTrue(skyrimFile.PluginInfo.Plugins[2] == "Dawnguard.esm");
            Assert.IsTrue(skyrimFile.PluginInfo.Plugins[3] == "HearthFires.esm");
            Assert.IsTrue(skyrimFile.PluginInfo.Plugins[4] == "Dragonborn.esm");
        }
    }
}
