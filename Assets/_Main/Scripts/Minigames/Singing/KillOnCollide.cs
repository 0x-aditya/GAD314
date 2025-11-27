using UnityEngine;

namespace Minigames.Singing
{
    [RequireComponent(typeof(Collider2D))]
    public class KillOnCollide : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Note"))
            {
                Destroy(other.gameObject);
                MusicGameManager.Instance.AddStrike();
            }
        }
    }
}
