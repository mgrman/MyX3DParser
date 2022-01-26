using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using MyX3DParser.Generated.Model;
using MyX3DParser.Generated.Model.AbstractNodes;
using MyX3DParser.Generated.Model.Fields;
using MyX3DParser.Generated.Model.Parsing;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using MyX3DParser.Generated;

namespace MyX3DParser.Tests
{
    public class RegressionTests
    {
        private readonly ITestOutputHelper output;

        private static readonly string rootFolder = Path.GetFullPath(Environment.CurrentDirectory);

        private static readonly string regressionDataFolder = Path.Combine(rootFolder, "RegressionData");

        private static readonly string[] ResultEndings = new[] { "result", "scene" }
            .Select(m => $".{m}.x3d")
            .ToArray();


        private static readonly string wrongFolder = Path.Combine(regressionDataFolder, "Wrong");

        public RegressionTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        static RegressionTests()
        {
            if (Directory.Exists(wrongFolder))
            {
                Directory.Delete(wrongFolder, true);
            }
            Directory.CreateDirectory(wrongFolder);
        }

        [Theory]
        [MemberData(nameof(RegressionData))]
        public void Reserialization_Regression(string relativePath)
        {
            DoRegressionTest(relativePath, x3d => x3d.ToX3DString(new HashSet<X3DNode>()), "result");
        }

        [Theory]
        [MemberData(nameof(RegressionData))]
        public void SceneTree_Regression(string relativePath)
        {
            DoRegressionTest(relativePath, x3d =>
            {
                x3d.ParentContext.ClearProtoTypes();
                ConvertToSceneTree(x3d.Scene);
                return x3d.ToX3DString(new HashSet<X3DNode>());
            }, "scene");
        }


        private void DoRegressionTest(string relativePath, Func<Generated.Model.Statements.X3D, string> adjustResult, string resultSuffix)
        {
            var absPath = Path.Combine(regressionDataFolder, relativePath);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(File.OpenRead(absPath));

            var resultX3D = Parser.Parse_X3D(xmlDoc.DocumentElement, new X3DContext());

            var resultXml = adjustResult(resultX3D)
                .BeautifyXml();

            var resultLength = resultXml.Length;
            var origLength = File.ReadAllText(absPath)
                .Length;
            output.WriteLine($"origSize:{origLength:N0}  resSize:{resultLength:N0}  diffSize:{(resultLength < origLength ? "decrease by " : "increase by ")}{Math.Abs(origLength - resultLength)}");
            output.WriteLine($"resultXml:{Environment.NewLine}{resultXml.BeautifyXml()}");


            var expectedFileName = Path.GetFileNameWithoutExtension(absPath) + $".{resultSuffix}.x3d";
            var expectedFilePath= Path.Combine(Path.GetDirectoryName(absPath) ?? "", expectedFileName);
            if (!File.Exists(expectedFilePath))
            {
                File.WriteAllText(GetWrongPath(expectedFilePath), resultXml);
                throw new Exception($"Missing results file '{expectedFilePath}'!");
            }

            var expectedXml = LoadBeautifiedXml(expectedFilePath);
            try
            {
                Assert.Equal(expectedXml, resultXml);
            }
            catch (EqualException) {

                File.WriteAllText(GetWrongPath(expectedFilePath), resultXml);
                throw;
            }
        }

        private string GetWrongPath(string absPath)
        {
            return Path.Combine(wrongFolder, Path.GetFileName(absPath));
        }

        private void ConvertToSceneTree(object x3d)
        {
            if (x3d == null)
            {
                return;
            }
            var props = x3d.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (typeof(SFNode).IsAssignableFrom(prop.PropertyType))
                {
                    var sfNode = prop.GetValue(x3d) as SFNode;
                    if (sfNode == null)
                    {
                        continue;
                    }
                    var sceneValue = sfNode.SceneValue;
                    ConvertToSceneTree(sceneValue);
                    sfNode.Value = sceneValue;
                }
                else if (typeof(MFNode).IsAssignableFrom(prop.PropertyType))
                {
                    var mfNode = prop.GetValue(x3d) as MFNode;
                    if (mfNode == null)
                    {
                        continue;
                    }
                    var sceneValue = mfNode.SceneValue;
                    foreach (var node in sceneValue)
                    {
                        ConvertToSceneTree(node);
                    }

                    mfNode.Value = sceneValue.Where(o=>o!=null).ToList();
                }
                else
                {
                    
                }
            }
        }


        [DebuggerStepThrough]
        private static string LoadBeautifiedXml(string path)
        {
            var expectedResult = new XmlDocument();
            expectedResult.Load(path);
            return expectedResult.Beautify();
        }

        public static IEnumerable<object[]> RegressionData
        {
            get
            {
                foreach (var file in Directory.GetFiles(regressionDataFolder, "*.x3d", SearchOption.AllDirectories))
                {
                    var name = Path.GetFullPath(file)[(regressionDataFolder.Length + 1)..];

                    if (name.StartsWith("Unused"))
                    {
                        continue;
                    }

                    if(ResultEndings.Any(m => file.EndsWith(m, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    if(File.ReadAllText(file).Contains("<Proto", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return new object[] {name};
                    }
                    else
                    {
                        yield return new object[] { name };
                    }
                }
            }
        }
          
    }
}