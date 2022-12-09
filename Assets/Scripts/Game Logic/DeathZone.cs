using UnityEngine;
using UnityEngine.Serialization;

namespace Game_Logic
{
    public class DeathZone : MonoBehaviour
    {
        [FormerlySerializedAs("GameManager")] public GameManager gameManager;

        private void OnCollisionEnter(Collision other)
        {
            Destroy(other.gameObject);
            gameManager.GameOver();
        }
    }
}
