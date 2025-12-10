using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private float recordDistance = 0.2f;
    
    public Queue<Vector3> positions = new();
    public Vector3 lastPos;

    void Start()
    {
        lastPos = transform.parent.position;
    }

    void Update()
    {
        if (Vector3.Distance(transform.parent.position, lastPos) > recordDistance)
        {
            positions.Enqueue(transform.parent.position);
            lastPos = transform.parent.position;
        }
    }
}