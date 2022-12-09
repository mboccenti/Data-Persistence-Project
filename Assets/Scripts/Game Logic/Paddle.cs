using UnityEngine;
using UnityEngine.Serialization;

namespace Game_Logic
{
    public class Paddle : MonoBehaviour
    {
        [FormerlySerializedAs("Speed")] public float speed = 2.0f;
        [FormerlySerializedAs("MaxMovement")] public float maxMovement = 2.0f;
    
        // Update is called once per frame
        private void Update()
        {
            float input = Input.GetAxis("Horizontal");

            Vector3 pos = transform.position;
            pos.x += input * speed * Time.deltaTime;

            if (pos.x > maxMovement)
                pos.x = maxMovement;
            else if (pos.x < -maxMovement)
                pos.x = -maxMovement;

            transform.position = pos;
        }
    }
}
