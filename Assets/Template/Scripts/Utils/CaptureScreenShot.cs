#if UNITY_EDITOR_WIN || UNITY_EDITOR_OSX
using System.Collections;
using System.Reflection;
using UnityEditor;
using System.IO;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Utils
{
    public class CaptureScreenShot : MonoBehaviour
    {
#if UNITY_EDITOR_WIN || UNITY_EDITOR_OSX
        public int resolution1 = 2;
        private MethodInfo getGroup;
        private object gameViewSizesInstance;
        //https://answers.unity.com/questions/956123/add-and-select-game-view-resolution.html?_ga=2.118943948.413073860.1597397616-1968540479.1546346021
        void Awake()
        {
            // gameViewSizesInstance  = ScriptableSingleton<GameViewSizes>.instance;
            var sizesType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizes");
            var singleType = typeof(ScriptableSingleton<>).MakeGenericType(sizesType);
            var instanceProp = singleType.GetProperty("instance");
            getGroup = sizesType.GetMethod("GetGroup");
            gameViewSizesInstance = instanceProp.GetValue(null, null);
        }
    
        void Update()
        {
#if UNITY_EDITOR_WIN
            if ((Input.GetKeyUp(KeyCode.F3)))
#endif
#if UNITY_EDITOR_OSX
            if ((Input.GetKeyUp(KeyCode.Alpha3)))
#endif
            {
                StartCoroutine(RecordFrame());
            }
        }
    
    
        IEnumerator RecordFrame()
        {
            var gameViewSizeType = GameViewSizeGroupType.Standalone;
#if UNITY_ANDROID
            gameViewSizeType = GameViewSizeGroupType.Android;
#endif
            
#if UNITY_IOS
            gameViewSizeType = GameViewSizeGroupType.iOS;
#endif

            
            AddCustomSize(GameViewSizeType.FixedResolution, gameViewSizeType, 2048/resolution1, 2732/resolution1, "screen_IphoneScreen");
            Canvas.ForceUpdateCanvases();
            yield return new WaitForEndOfFrame();
            StartCoroutine(RecordScreen("2048x2732","2048x2732___"));
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            var f = FindSize(gameViewSizeType, "screen_IphoneScreen");
            RemoveCustomSize(f, gameViewSizeType);
            yield return new WaitForEndOfFrame();


            AddCustomSize(GameViewSizeType.FixedResolution, gameViewSizeType, 1242/resolution1, 2208/resolution1, "screen_iphoneScreen");
            Canvas.ForceUpdateCanvases();
            yield return new WaitForEndOfFrame();
            StartCoroutine(RecordScreen("1242x2208", "1242x2208___"));
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            f = FindSize(gameViewSizeType, "screen_iphoneScreen");
            RemoveCustomSize(f, gameViewSizeType);
            yield return new WaitForEndOfFrame();
        
        
        
            AddCustomSize(GameViewSizeType.FixedResolution, gameViewSizeType, 1242/resolution1, 2688/resolution1, "screen_iphoneScreenX");
            Canvas.ForceUpdateCanvases();
            yield return new WaitForEndOfFrame();
            StartCoroutine(RecordScreen("1242x2688","1242x2688___"));
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
        
            f = FindSize(gameViewSizeType, "screen_iphoneScreenX");
            RemoveCustomSize(f, gameViewSizeType);
            yield return new WaitForEndOfFrame();
        }

        IEnumerator RecordScreen(string size, string prefix)
        {
            yield return new WaitForEndOfFrame();
            var texture = ScreenCapture.CaptureScreenshotAsTexture(resolution1);
            // do something with texture
            var sep = System.IO.Path.DirectorySeparatorChar;
            var root = $"C:{sep}ScreenShots{sep}";
#if UNITY_EDITOR_OSX
            root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + sep + "ScreenShots" + sep;
#endif
            var path = root + Application.productName + sep + size;

            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
        
            SaveTextureAsPNG(texture,  path + sep + prefix + 
                                       System.DateTime.Now.Ticks.ToString() + ".jpg");
            // cleanup
            Object.Destroy(texture);
        }

    
        public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
        {
            byte[] _bytes = _texture.EncodeToJPG();
            System.IO.File.WriteAllBytes(_fullPath, _bytes);
            Debug.Log(_bytes.Length/1024  + "Kb was saved as: " + _fullPath);
        }
    
        public enum GameViewSizeType
        {
            AspectRatio, FixedResolution
        }
    
        public void AddCustomSize(GameViewSizeType viewSizeType, GameViewSizeGroupType sizeGroupType, int width, int height, string text)
        {
            // GameViewSizes group = gameViewSizesInstance.GetGroup(sizeGroupTyge);
            // group.AddCustomSize(new GameViewSize(viewSizeType, width, height, text);
            var group = GetGroup(sizeGroupType);
            var addCustomSize = getGroup.ReturnType.GetMethod("AddCustomSize"); // or group.GetType().
            var gvsType = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSize");
            var c = gvsType.GetConstructors();
            var ctor = c[0];
            var newSize = ctor.Invoke(new object[] { (int)viewSizeType, width, height, text });
            addCustomSize.Invoke(group, new object[] { newSize });
            SetSize(FindSize(sizeGroupType, text));
        }

        public void RemoveCustomSize(int index, GameViewSizeGroupType sizeGroupType)
        {
            var group = GetGroup(sizeGroupType);
            var removeCustomSize = getGroup.ReturnType.GetMethod("RemoveCustomSize"); // or group.GetType().
            removeCustomSize.Invoke(group, new object[] { index });
        }
    
    
        public int FindSize(GameViewSizeGroupType sizeGroupType, string text)
        {
            // GameViewSizes group = gameViewSizesInstance.GetGroup(sizeGroupType);
            // string[] texts = group.GetDisplayTexts();
            // for loop...
 
            var group = GetGroup(sizeGroupType);
            var getDisplayTexts = group.GetType().GetMethod("GetDisplayTexts");
            var displayTexts = getDisplayTexts.Invoke(group, null) as string[];
            for(int i = 0; i < displayTexts.Length; i++)
            {
                string display = displayTexts[i];
                // the text we get is "Name (W:H)" if the size has a name, or just "W:H" e.g. 16:9
                // so if we're querying a custom size text we substring to only get the name
                // You could see the outputs by just logging
                // Debug.Log(display);
                int pren = display.IndexOf('(');
                if (pren != -1)
                    display = display.Substring(0, pren-1); // -1 to remove the space that's before the prens. This is very implementation-depdenent
                if (display == text)
                    return i;
            }
            return -1;
        }
    
        public static void SetSize(int index)
        {
            var gvWndType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
            var selectedSizeIndexProp = gvWndType.GetProperty("selectedSizeIndex",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var gvWnd = EditorWindow.GetWindow(gvWndType);
            selectedSizeIndexProp.SetValue(gvWnd, index, null);
        }
    
        object GetGroup(GameViewSizeGroupType type)
        {
            return getGroup.Invoke(gameViewSizesInstance, new object[] { (int)type });
        }
#endif
    }
}

