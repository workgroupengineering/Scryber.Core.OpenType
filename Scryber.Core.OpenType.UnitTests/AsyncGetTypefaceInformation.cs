﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Scryber.OpenType.UnitTests
{
    [TestClass()]
    public class AsyncGetTypefaceInformation
    {

        public const string RootUrl = "https://raw.githubusercontent.com/richard-scryber/scryber.core/master/Scryber.Drawing/Text";

        public const string UrlPath = "/_FontResources/Helvetica/Helvetica.ttf";
        public const string FailingUrlPath = "/NOT_HERE/Helvetica/Helvetica.ttf";

        public static readonly string PartialFilePath = "fonts/Helvetica.ttf";
        public static readonly string FailingPartialFilePath = "NOT_FOUND/fonts/Helvetica.ttf";

        /// <summary>
        /// A download of a text (csproj) file that can make sure an http client is still alive
        /// </summary>
        public const string CheckAliveUrl = "https://raw.githubusercontent.com/richard-scryber/scryber.core.opentype/master/Scryber.Core.OpenType/Scryber.Core.OpenType.csproj";


        [TestMethod("1. Async load from valid absolute url")]
        public void AsyncLoadFromAbsoluteUrl()
        {

            ITypefaceInfo info;

            using (var reader = new TypefaceReader())
            {
                var path = RootUrl;

                //valid path
                path = path + UrlPath;
                var uri = new Uri(path);

                info = reader.GetTypefaceInformationAsync(uri).Result;

                Assert.IsNotNull(info, "Info was not returned");
                Assert.IsTrue(string.IsNullOrEmpty(info.ErrorMessage), "An Error message was returned");
                //check the info
                ValidateHelvetica.AssertInfo(info, path, 5);

            }

        }

        [TestMethod("2. Async to load from valid relative url")]
        public void AsyncLoadFromRelativeUrl()
        {

            ITypefaceInfo info;

            var path = RootUrl;

            using (var reader = new TypefaceReader(new Uri(path)))
            {
                //valid path
                path = UrlPath;
                var uri = new Uri(path, UriKind.Relative);

                info = reader.GetTypefaceInformationAsync(uri).Result;

                Assert.IsNotNull(info, "Info was not returned");
                Assert.IsTrue(string.IsNullOrEmpty(info.ErrorMessage), "An Error message was returned");
                //check the info - but not the path
                ValidateHelvetica.AssertInfo(info, null, 6);

            }

        }

        
        [TestMethod("3. Async load from valid absolute file path")]
        public void AsyncLoadFromAbsoluteFile()
        {

            ITypefaceInfo info;

            var path = System.Environment.CurrentDirectory;

            using (var reader = new TypefaceReader())
            {
                //valid path
                path = Path.Combine(path, PartialFilePath);
                var file = new FileInfo(path);

                info = reader.GetTypefaceInformationAsync(file).Result;

                Assert.IsNotNull(info, "Info was not returned");
                Assert.IsTrue(string.IsNullOrEmpty(info.ErrorMessage), "An Error message was returned");
                //check the info - but not the path, as this will be changed
                ValidateHelvetica.AssertInfo(info, null, 7);

            }

        }

        [TestMethod("4. Async load from valid relative file path")]
        public void AsyncLoadFromRelativeFile()
        {

            ITypefaceInfo info;

            var path = System.Environment.CurrentDirectory;

            using (var reader = new TypefaceReader(new DirectoryInfo(path)))
            {
                //valid path
                path = PartialFilePath;
                var file = new FileInfo(path);

                info = reader.GetTypefaceInformationAsync(file).Result;

                Assert.IsNotNull(info, "Info was not returned");
                Assert.IsTrue(string.IsNullOrEmpty(info.ErrorMessage), "An Error message was returned");
                //check the info - but not the path, as this will be changed
                ValidateHelvetica.AssertInfo(info, null, 7);

            }

        }

        [TestMethod("5. Async load from valid relative file string")]
        public void AsyncLoadFromRelativeFileString()
        {

            ITypefaceInfo info;

            var path = System.Environment.CurrentDirectory;

            using (var reader = new TypefaceReader(new DirectoryInfo(path)))
            {
                //valid path
                path = PartialFilePath;
                

                info = reader.GetTypefaceInformationAsync(path).Result;

                Assert.IsNotNull(info, "Info was not returned");
                Assert.IsTrue(string.IsNullOrEmpty(info.ErrorMessage), "An Error message was returned");
                //check the info - but not the path, as this will be changed
                ValidateHelvetica.AssertInfo(info, null, 7);

            }

        }

        [TestMethod("6. Async load from valid relative url string")]
        public void AsyncLoadFromRelativeUrlString()
        {

            ITypefaceInfo info;

            var path = RootUrl;

            using (var reader = new TypefaceReader(new Uri(path)))
            {
                //valid path
                path = UrlPath;

                info = reader.GetTypefaceInformationAsync(path).Result;

                Assert.IsNotNull(info, "Info was not returned");
                Assert.IsTrue(string.IsNullOrEmpty(info.ErrorMessage), "An Error message was returned");
                //check the info - but not the path
                ValidateHelvetica.AssertInfo(info, null, 6);

            }

        }
    }
}
