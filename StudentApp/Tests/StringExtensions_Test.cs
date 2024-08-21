using NUnit.Framework;
using System;
using System.IO;
using static StudentApp_FunctionalProgramming_.Prelude;


namespace StudentApp_FunctionalProgramming_.Tests
{
    [TestFixture()]
    public class StringExtensions_Test
    {
        [Test()]
        public void EncryptAES_Test()
        {
            "admin".EncryptAES().Match(
              Right: (v) => Assert.IsTrue(v == "eOr3sFal9lg/wCrUsnP6bQ=="),
              Left: (e) => { });
        }

        [Test()]
        public void DecryptAES_Test()
        {
            "eOr3sFal9lg/wCrUsnP6bQ==".DecryptAES().Match(
              Right: (v) => Assert.IsTrue(v == "admin"),
              Left: (e) => { });
        }

        [Test()]
        public void DeleteFileIfExists_Test()
        {
            var file = System.IO.Path.GetTempPath() + "deletefile.txt";
            File.CreateText(file);
            file.DeleteFileIfExists().Match(
              Right: (v) => Assert.IsTrue(v),
              Left: (e) => { });
        }

        [Test()]
        public void CreateDirectory_Test()
        {
            var folder = System.IO.Path.GetTempPath() + "createDir";
            folder.CreateDirectory().Match(
              Right: (v) => {
                  Directory.Delete(folder);
                  Assert.IsTrue(v);
              },
              Left: (e) => { });
        }
    }
}
