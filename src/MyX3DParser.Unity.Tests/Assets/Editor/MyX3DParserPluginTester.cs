using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

[InitializeOnLoad]
public class MyX3DParserPluginTester : MonoBehaviour
{
    static MyX3DParserPluginTester()
    {
        if (Exists())
        {
            Debug.Log("MyX3DParser.Unity.dll is found and available.");
            return;
        }

        if (!EditorUtility.DisplayDialog("Missing MyX3DParser .dll",
                "The MyX3DParser.Unity project needs to be built first. Do you want to trigger it now (.NET needs to be installed, ie 'dotnet' command needs to be available)?",
                "Yes",
                "No"))
        {
            Debug.LogError(
                "Please build the main MyX3DParser.Unity.csproj first! The build artefacts from that solution are missing!");
            return;
        }

        var process = Process.Start("CMD.exe", "/C dotnet build ../MyX3DParser.Unity/MyX3DParser.Unity.csproj");
        process.WaitForExit();
        if (process.ExitCode == 0 && Exists())
        {
            Debug.Log("MyX3DParser.Unity.dll was rebuilt successfully.");
            return;
        }

        Debug.LogError(
            "MyX3DParser.Unity.dll was not rebuilt successfully, please try rebuilding the main solution manually!");
    }

    private static bool Exists()
    {
        return File.Exists(Path.Combine("Assets", "Plugins", "MyX3DParser.Unity.dll"));
    }
}