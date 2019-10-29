using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pg.meg.builder;
using pg.meg.builder.attributes;
using pg.meg.service;
using pg.meg.typedef;

namespace pg.meg.test
{
    [TestClass]
    public class MegFileBinaryFileBuilderUnitTest
    {

        private static readonly string TEST_DATA_PATH_IN =
            Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..", "test_data","eaw_patch_2.meg"));

        private static readonly string TEST_DATA_PATH_UNPACK_OUT = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..", "test_data","unpacked"));
        private static readonly string TEST_DATA_PATH_PACK_OUT = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..","..","..", "test_data","pack.meg"));

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
            if (Directory.Exists(TEST_DATA_PATH_UNPACK_OUT))
            {
                Directory.Delete(TEST_DATA_PATH_UNPACK_OUT, true);
            }

            if (File.Exists(TEST_DATA_PATH_PACK_OUT))
            {
                File.Delete(TEST_DATA_PATH_PACK_OUT);
            }
        }

        [TestMethod]
        public void MegBuilderBuildHeaderTest()
        {
            MegFileBinaryFileBuilder megFileBinaryFileBuilder = new MegFileBinaryFileBuilder();
            byte[] b = File.ReadAllBytes(TEST_DATA_PATH_IN);
            MegFile megFile = megFileBinaryFileBuilder.Build(b);
            Assert.IsNotNull(megFile);
        }

        [TestMethod]
        public void MegBuilderBuildFromAttributeTest()
        {
            MegFileUtility.UnpackMegFile(TEST_DATA_PATH_IN, TEST_DATA_PATH_UNPACK_OUT);
            foreach (string file in Directory.GetFiles(TEST_DATA_PATH_UNPACK_OUT))
            {
                Console.Out.WriteLine(file);
            }

            /*
            MegFileBinaryFileBuilder builder = new MegFileBinaryFileBuilder();
            MegFileAttribute attribute = new MegFileAttribute
            {
                FilePath = TEST_DATA_PATH_PACK_OUT,
                FileName = Path.GetFileName(TEST_DATA_PATH_PACK_OUT),
                ContentFiles = EXPECTED_FILES
            };
            MegFile megFile = builder.Build(attribute);
            Assert.IsNotNull(megFile);
            */
        }
    }
}
