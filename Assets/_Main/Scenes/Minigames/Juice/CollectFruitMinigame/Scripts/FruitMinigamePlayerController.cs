using UnityEngine;

public class FruitMinigamePlayerController : ScriptLibrary.Inputs.Vector2Input
{
    [SerializeField] private float speed = 5f;
    private float horizontalInput => VectorInput.x;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        _rigidbody2D.linearVelocity = new Vector2(horizontalInput * speed, 0);
    }
}
