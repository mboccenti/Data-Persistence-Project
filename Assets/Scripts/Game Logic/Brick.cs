using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Game_Logic
{
    public class Brick : MonoBehaviour
    {
        public UnityEvent<int> onDestroyed;
    
        [FormerlySerializedAs("PointValue")] public int pointValue;
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        private void Start()
        {
            Renderer r = GetComponentInChildren<Renderer>();

            MaterialPropertyBlock block = new MaterialPropertyBlock();
            switch (pointValue)
            {
                case 1 :
                    block.SetColor(BaseColor, Color.green);
                    break;
                case 2:
                    block.SetColor(BaseColor, Color.yellow);
                    break;
                case 5:
                    block.SetColor(BaseColor, Color.blue);
                    break;
                default:
                    block.SetColor(BaseColor, Color.red);
                    break;
            }
            r.SetPropertyBlock(block);
        }

        private void OnCollisionEnter(Collision other)
        {
            onDestroyed.Invoke(pointValue);
        
            //slight delay to be sure the ball have time to bounce
            Destroy(gameObject, 0.2f);
        }
    }
}
