using Template.Scripts.DI;
using UnityEngine;

namespace Template.Samples.DI
{
    public class PlayerSingle : SingleMono
    {
        public void MoveRight()
        {
            transform.position += Vector3.right * Time.deltaTime;
        }

        public void MoveLeft()
        {
            transform.position += Vector3.left * Time.deltaTime;
        }

        public void MoveUp()
        {
            transform.position += Vector3.up * Time.deltaTime;
        }

        public void MoveDown()
        {
            transform.position += Vector3.down * Time.deltaTime;
        }
        
    }
}
