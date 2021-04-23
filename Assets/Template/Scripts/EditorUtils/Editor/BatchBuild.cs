using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Template.Scripts.EditorUtils.Editor
{
    public static class BatchBuild 
    {
        public static void Build_iOS()
        {
            var path = Environment.GetEnvironmentVariable("BUILD_PATH");
            if (string.IsNullOrEmpty(path))
                return;
            
            PreBuild();
#if UNITY_IOS
            PlayerSettings.iOS.appleEnableAutomaticSigning = true;
            PlayerSettings.iOS.appleDeveloperTeamID = "HJ453WUND2";
#endif
            var b = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes
                , path, BuildTarget.iOS, BuildOptions.None);
            
            PostBuildReport(b);
        }
        public static void Build_Android()
        {
            var path = Environment.GetEnvironmentVariable("BUILD_PATH") + "/AndroidBuild.apk";
            if (string.IsNullOrEmpty(path))
                return;

            PreBuild();
#if UNITY_ANDROID

            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel23;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel30;
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.All;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName = "Assets/Template/GPlay/user.keystore";
            PlayerSettings.Android.keystorePass = "marsdigger";
            PlayerSettings.Android.keyaliasName = "mars";
            PlayerSettings.Android.keyaliasPass = "digger";
#endif
            var b = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes
                , path, BuildTarget.Android, BuildOptions.None);
            PostBuildReport(b);
        }

        private static void PreBuild()
        {
            Assert.IsTrue(EditorBuildSettings.scenes[0].path.Contains("FirstScene"), 
             "First Scene should be FirsScene.unity");
            var buildNumber = Environment.GetEnvironmentVariable("BUILD_NUMBER");

#if GEEKON_LIONSTUDIO
            PublisherIntegrator.SetIds();
            Assert.IsNotEmpty(LionStudios.LionSettings.Facebook.AppId, "Facebook is not set");
            Assert.IsNotEmpty(LionStudios.LionSettings.Adjust.Token, "Adjust is not set");
#endif

            var number = int.Parse(buildNumber ?? "0");
            PlayerSettings.bundleVersion = $"1.{number}";
            
            Assert.IsTrue(PlayerSettings.applicationIdentifier.Contains("com.geekongames."), "Bundle ID should be set!");
            
#if UNITY_ANDROID
            PlayerSettings.Android.bundleVersionCode = number;
#endif

#if UNITY_IOS
            PlayerSettings.iOS.buildNumber = number.ToString();
#endif
        }


        private static void PostBuildReport(BuildReport result)
        {
            var fileOnFinish = Environment.GetEnvironmentVariable("UNITY_STATUS");
            Debug.Log(fileOnFinish);
            
#if UNITY_ANDROID
            Debug.Log("BuildNumber: " + PlayerSettings.Android.bundleVersionCode);
#endif

#if UNITY_IOS
            Debug.Log("BuildNumber: " + PlayerSettings.iOS.buildNumber);
#endif

            if (result.summary.result == BuildResult.Succeeded)
            {
                File.WriteAllText(fileOnFinish, "Success");
            }
            else
            {
                File.WriteAllText(fileOnFinish, "Fail");
            }
        }
    }
}
