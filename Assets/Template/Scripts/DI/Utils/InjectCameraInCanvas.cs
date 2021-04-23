using UnityEngine;

namespace Template.Scripts.DI.Utils
{
    [RequireComponent(typeof(Canvas))]
    public class InjectCameraInCanvas : InjectedMono
    {
        [In] private CameraHandler _cameraHandler;
        [Get] private Canvas _canvas;

        public override void OnSyncStart()
        {
            _canvas.worldCamera = _cameraHandler.Camera;
        }
    }
}
