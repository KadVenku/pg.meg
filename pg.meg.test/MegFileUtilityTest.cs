using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace pg.meg.test
{
    [TestClass]
    public class MegFileUtilityTest
    {
        private static readonly string TEST_DATA_PATH_IN = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..", "test_data","eaw_patch_2.meg"));
        private static readonly string TEST_DATA_PATH_BUILD = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..", "test_data","eaw_patch_build.meg"));

        private static readonly string TEST_DATA_PATH_OUT =
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..", "test_data","unpacked"));

        private static readonly string[] EXPECTED_FILES =
        {
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","SHADERS","GRASS.FXO")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","SHADERS","MESHSHADOWVOLUME.FXO")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","SHADERS","RSKINHEAT.FXO")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","TEXTURES","I_BUTTON_PETRO.DDS")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","TEXTURES","I_BUTTON_PETRO_MOUSE_OVER.DDS")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","TEXTURES","I_BUTTON_PETRO_SLIVER.DDS")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","TEXTURES","MENUBACK_OVERLAY.DDS")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","TEXTURES","MENUBACK_OVERLAY_GERMAN_FRENCH.DDS")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","TEXTURES","MENUBACK_OVERLAY_ITALIAN.DDS")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","ART","TEXTURES","MENUBACK_OVERLAY_SPANISH.DDS")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","RESOURCES","GUIDIALOG","GUIDIALOGS.RC")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","RESOURCES","GUIDIALOG","RESOURCE.H")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","XML","COMMANDBARCOMPONENTS.XML")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","XML","GUIDIALOGS.XML")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","XML","SPACEUNITSFRIGATES.XML")),
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..",
                "test_data","unpacked","DATA","XML","SQUADRONS.XML"))
        };

        [TestCleanup]
        public void CleanUp()
        {
            if (Directory.Exists(TEST_DATA_PATH_OUT))
            {
                Directory.Delete(TEST_DATA_PATH_OUT, true);
            }

            if (File.Exists(TEST_DATA_PATH_BUILD))
            {
                File.Delete(TEST_DATA_PATH_BUILD);
            }
        }

        [TestMethod]
        public void GetMegFileHeaderSize_TestSuccess()
        {
            uint headerSize = MegFileUtility.GetMegFileHeaderSize(TEST_DATA_PATH_IN);
            Assert.AreEqual(headerSize, Convert.ToUInt32(943));
        }

        // FIXME [KV]: Succeeds locally, but fails when using Travis.
        [TestMethod]
        [Ignore]
        public void UnpackMegFile_TestSuccess()
        {
            MegFileUtility.UnpackMegFile(TEST_DATA_PATH_IN, TEST_DATA_PATH_OUT);
            Assert.IsTrue(Directory.Exists(TEST_DATA_PATH_OUT));
            Assert.IsNotNull(Directory.GetFiles(TEST_DATA_PATH_OUT));
            foreach (string file in EXPECTED_FILES)
            {
                Assert.IsTrue(File.Exists(file));
            }
        }

        // FIXME [KV]: Succeeds locally, but fails when using Travis.
        [TestMethod]
        [Ignore]
        public void PackMegFile_TestSuccess()
        {
            MegFileUtility.UnpackMegFile(TEST_DATA_PATH_IN, TEST_DATA_PATH_OUT);
            MegFileUtility.PackMegFile(TEST_DATA_PATH_BUILD, EXPECTED_FILES);
            Assert.IsTrue(File.Exists(TEST_DATA_PATH_BUILD));
            if (Directory.Exists(TEST_DATA_PATH_OUT))
            {
                Directory.Delete(TEST_DATA_PATH_OUT, true);
            }
            MegFileUtility.UnpackMegFile(TEST_DATA_PATH_BUILD, TEST_DATA_PATH_OUT);
            Assert.IsTrue(Directory.Exists(TEST_DATA_PATH_OUT));
            Assert.IsNotNull(Directory.GetFiles(TEST_DATA_PATH_OUT));
            foreach (string file in EXPECTED_FILES)
            {
                Assert.IsTrue(File.Exists(file));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UnpackMegFile_TestDirectoryOutInvalid()
        {
            MegFileUtility.UnpackMegFile(TEST_DATA_PATH_IN, "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UnpackMegFile_TestDirectoryOutNull()
        {
            MegFileUtility.UnpackMegFile(TEST_DATA_PATH_IN, null);
        }

        [TestMethod]
        public void GetContainedFilesCount_Test()
        {
            Assert.AreEqual(16u, MegFileUtility.GetContainedFileCount(TEST_DATA_PATH_IN));
        }
    }
}
