using UnityEngine;
public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset;
    
    private void LateUpdate()
    {
        if (playerTransform == null) return;
        transform.position = playerTransform.position + offset;
    }
}
