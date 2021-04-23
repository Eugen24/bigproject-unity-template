using UnityEngine;

namespace Template.Effects.Shaders
{
    [ExecuteInEditMode]
    public class RoundMeshV2 : MonoBehaviour
    {
        public Transform Center;
        public Material Material;
        public float Power;
        public float Offset;
        void Update()
        {
            Material.SetVector("_Center", Center.localPosition);
            Material.SetFloat("_Power", Power);
            Material.SetFloat("_Offset", Offset);
        }
    }
}
