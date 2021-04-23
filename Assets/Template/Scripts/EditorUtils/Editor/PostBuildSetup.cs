using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

namespace Template.Scripts.EditorUtils.Editor
{
    public class PostBuildSetup : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;
        public void OnPostprocessBuild(BuildReport report)
        {
            UnityEngine.Debug.Log("BundleID:" + UnityEngine.Application.identifier);
            if (report.summary.platform == BuildTarget.iOS) // Check if the build is for iOS 
            {
#if UNITY_IOS
                string plistPath = report.summary.outputPath + "/Info.plist"; 

                PlistDocument plist = new PlistDocument(); // Read Info.plist file into memory
                plist.ReadFromString(File.ReadAllText(plistPath));

                PlistElementDict rootDict = plist.root;
                rootDict.SetBoolean("ITSAppUsesNonExemptEncryption", false); 

                File.WriteAllText(plistPath, plist.WriteToString()); // Override Info.plist
#endif

            }
        }
    }
}
