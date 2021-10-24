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
    }
}
