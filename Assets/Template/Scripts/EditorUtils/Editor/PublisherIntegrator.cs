using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if GEEKON_LIONSTUDIO
using LionStudios;
#endif
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEditorInternal;
using UnityEngine;

namespace Template.Scripts.EditorUtils.Editor
{
    public static class PublisherIntegrator
    {
        
#if GEEKON_LIONSTUDIO
        public static void SetIds()
        {
            var geekonConfig = EditorUtils.GetAllInstances<GeekonPublishingConfig>()[0];
            var lionConfig = LionSettings.Get();
            var sObj = new SerializedObject(lionConfig);
            
            /*
             _Facebook
            [SerializeField] bool _Enabled = true;
            [SerializeField] string _AppId = "";
            */
            sObj.Update();

            sObj.FindProperty("_Facebook._AppId").stringValue = geekonConfig.FacebookId;
            sObj.FindProperty("_Facebook._Enabled").boolValue = true;
            
            /*
            bool _Enabled = true;
            string _iOSToken = null;
            string _AndroidToken = null;
             */

            sObj.FindProperty("_Adjust._iOSToken").stringValue = geekonConfig.AdjustIdiOS;
            sObj.FindProperty("_Adjust._AndroidToken").stringValue = geekonConfig.AdjustIdAndroid;
            sObj.FindProperty("_Adjust._Enabled").boolValue = true;
            sObj.ApplyModifiedProperties();
            
            EditorUtility.SetDirty(lionConfig);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            //call init for Lion to do it's magic because for some magic reasons there buisness logic is partly on UI part
            // it means you need to call there window in order to update apply (from ScriptableObject).
            var dynMethod = typeof(LionStudios.Editor.LionIntegrationManagerWindow).GetMethod("Init",
                BindingFlags.Static | BindingFlags.NonPublic);
            dynMethod.Invoke(null, null);
        }
#endif
        

        [MenuItem("Window/Geekongames/AddLionSDK")]
        public static void AddAllLionRoutine()
        {
            var reg = AddScopeRegistry("Game Package Registry by Google",
                "https://unityregistry-pa.googleapis.com",
                "com.google");

            EditorApplication.update += Wait1;
            void Wait1()
            {
                if (reg.IsCompleted)
                {
                    if (reg.Status == StatusCode.Success)
                    {
                        //move next
                        var reg2 = AddScopeRegistry("Lion Studios Packages",
                            "http://packages.lionstudios.cc:4873/",
                            "com.lionstudios.release", "com.lionstudios.templates");
                        
                        EditorApplication.update += Wait2;

                        void Wait2()
                        {
                            if (reg2.IsCompleted)
                            {
                                if (reg2.Status == StatusCode.Success)
                                {
                                    
                                    var request = Client.Add("com.lionstudios.release.lionkit");                        
                                    EditorApplication.update += Wait3;

                                    void Wait3()
                                    {
                                        if (request.IsCompleted)
                                        {

                                            if (request.Status == StatusCode.Success)
                                            {
                                                Debug.Log("Integration succeed!");
                                                Debug.Log("Adding GEEKON_LIONSTUDIO");
                                                AddDefineSymbols(new[] {"GEEKON_LIONSTUDIO"});
                                            }
                                            else
                                            {
                                                Debug.LogError("Call Andrei!, \n" + reg2.Error.message);
                                            }
                                            EditorApplication.update -= Wait3;
                                        }
                                    }

                                }
                                else
                                {
                                    Debug.LogError("Call Andrei!, \n" + reg2.Error.message);
                                }
                                
                                EditorApplication.update -= Wait2;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Call Andrei!, \n " + reg.Error.message);
                    }
                    
                    EditorApplication.update -= Wait1;

                }
            }
        }

        //https://stackoverflow.com/questions/49972866/how-to-refresh-recompile-custom-preprocessor
        private static void AddDefineSymbols(string[] symbol)
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> allDefines = definesString.Split(';').ToList();
            allDefines.AddRange(symbol.Except(allDefines));
            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", allDefines.ToArray()));
        }
        
        private static Request AddScopeRegistry(string registryName, string url, params string[] scopes)
        {
            var dynMethod = typeof(Client).GetMethod("AddScopedRegistry",
                BindingFlags.Static | BindingFlags.NonPublic);
            return (Request) dynMethod.Invoke(null, new object[]
            {
                registryName, url, scopes
            });
        }
        
        [MenuItem("Window/Geekongames/OpenConfig")]
        public static void OpenPublisherConfig()
        {
            var geekonConfig = EditorUtils.GetAllInstances<GeekonPublishingConfig>()[0];
            Selection.activeObject = geekonConfig;
        }
    }
}
