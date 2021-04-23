using UnityEditor;
using UnityEngine;

namespace Template.Scripts.EditorUtils.Editor
{
    public static class EditorUtils 
    {
        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:"+ typeof(T).Name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for(int i =0;i<guids.Length;i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
 
            return a;
 
        }
        
        public static T[] GetAllInstancesMonoByName<T>(string name) where T : MonoBehaviour
        {
            string[] guids = AssetDatabase.FindAssets(name);  //FindAssets uses tags check documentation for more info
            T[] a = new T[guids.Length];
            for(int i =0;i<guids.Length;i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }
 
            return a;
 
        }
    }
    
    
}
