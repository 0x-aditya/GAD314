using UnityEngine;

public class Follower : MonoBehaviour
{
    public PlayerTracker tracker;
    public float speed = 3f;
    public float minDistanceFromPlayer = 1f;
    
    void Update()
    {
        if (tracker.positions.Count == 0)
            return;

        float distToPlayer = Vector3.Distance(transform.parent.position, tracker.transform.position);
        if (distToPlayer < minDistanceFromPlayer)
            return;

        Vector3 target = tracker.positions.Peek();
        transform.parent.position = Vector3.MoveTowards(transform.parent.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.parent.position, target) < 0.05f)
            tracker.positions.Dequeue();
    }
}