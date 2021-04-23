using UnityEngine;

namespace Template.Effects.Shaders
{
    [ExecuteInEditMode]
    public class RoundMesh : MonoBehaviour
    {
        public Transform Center;
        public Material Material;
        public float Power;
        public Vector3 Direction;
        void Update()
        {
            Material.SetVector("_Center", Center.localPosition);
            Material.SetVector("_Direction", Direction);
            Material.SetFloat("_Power", Power);
        }
    }
}
