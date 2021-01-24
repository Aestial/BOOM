using UnityEngine;

namespace Liquid.Actions.Movement
{
    public class ObjectRotate : MonoBehaviour 
    {
        [SerializeField]
        private float speed = 50.0f;

        private void Update() {
            transform.eulerAngles += new Vector3(
                0, speed * Time.deltaTime);
        }
    }
}
