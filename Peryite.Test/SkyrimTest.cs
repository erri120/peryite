﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Peryite.Common.Skyrim;

namespace Peryite.Test
{
    [TestClass]
    public class SkyrimTest
    {
        [TestMethod]
        public void TestSkyrimSaveFile()
        {
            const string file = "skyrim-le-trawz.ess";

            var skyrimFile = new SkyrimSaveFile();
            skyrimFile.LoadFile(file);
        }
    }
}
