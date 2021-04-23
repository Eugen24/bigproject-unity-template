using UnityEngine;

namespace Template.Scripts.DI.Utils
{
    [RequireComponent(typeof(Camera))]
    public class CameraHandler : SingleMono
    {
        [Get] public Camera Camera;
    }
}
