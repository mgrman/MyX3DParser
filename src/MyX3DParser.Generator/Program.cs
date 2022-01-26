using MyX3DParser.Model;
using MyX3DParser.Model.Builders;
using MyX3DParser.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace MyX3DParser.Run
{
    static class Program
    {
        static void Main(string[] args)
        {
            string rootPath = Environment.CurrentDirectory;

            if (args.Length > 0)
            {
                rootPath = args[0];
            }
            rootPath = Path.GetFullPath(rootPath);


            var configFilePaths = Directory.GetFiles(rootPath, "MyX3DParser.Generator.json", SearchOption.AllDirectories);

            foreach(var configFilePath in configFilePaths)
            {
                var configText = File.ReadAllText(configFilePath);
              var  jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters =
    {
        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
    }
                };
                var config = JsonSerializer.Deserialize<GeneratorConfig>(configText, jsonOptions);

                var path = Path.Combine(Path.GetDirectoryName(configFilePath), config.targetFolder);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var uomPath = Path.Combine(Path.GetDirectoryName(configFilePath), config.unifiedObjectModel);
                using var stream = File.OpenRead(uomPath);

                var builders = TypeParser.Parse3_3(stream,config);
                UpdateFiles(path, builders);
            }
        }

        private static void UpdateFiles(string path,  IReadOnlyList<IFileBuilder> builders)
        {
            var oldFiles = new HashSet<string>(Directory.GetFiles(path, "*.*", SearchOption.AllDirectories));
            foreach (var pair in builders)
            {

                var fileDir = Path.Combine(path, pair.GetCategory());
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                var filePath = Path.Combine(fileDir, pair.CleanName + ".cs");
                try
                {
                    var contents = pair.ToString();

                    if (contents == null)
                    {
                        continue;
                    }


                    oldFiles.Remove(filePath);
                    if (File.Exists(filePath))
                    {
                        if (File.ReadAllText(filePath) == contents)
                        {
                            continue;
                        }
                    }

                    File.WriteAllText(filePath, contents);
                }
                catch (Exception ex)
                {
                    File.WriteAllText(filePath, $"{ex.GetType().FullName}{Environment.NewLine}{ex.Message}{Environment.NewLine}{ex.StackTrace}");
                }
            }

            foreach (var toRemove in oldFiles)
            {
                File.Delete(toRemove);
            }


            foreach (var dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                var dirInf = new DirectoryInfo(dir);
                if (dirInf.EnumerateFiles().Any() || dirInf.EnumerateDirectories().Any())
                {
                    continue;
                }
                dirInf.Delete();
            }
        }
    }
}
