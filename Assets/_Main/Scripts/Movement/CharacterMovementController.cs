using System;
using ScriptLibrary.Inputs;
using UnityEngine;

public class CharacterMovementController : Vector2Input
{
    [Header("Player Interaction Range")]
    public float interactionRange = 100f;
    
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    
    [Header("Movement Type")]
    [SerializeField] private MovementType movementType = MovementType.SixDirectional;
    //[SerializeField] private bool snapToGrid = false;

    private Action _movementMethod;
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        SetMovementFunction();
        _animator = GetComponent<Animator>();
    }

    private void OnValidate() => SetMovementFunction();

    private void SetMovementFunction()
    {
        _movementMethod = movementType switch
        {
            MovementType.SixDirectional => SixDirectionalMovement,
            MovementType.FourDirectional => FourDirectionalMovement,
            _ => throw new Exception("Invalid movement type")
        };
    }
    
    private void Update() => _movementMethod();

    private void SixDirectionalMovement()
    {
        var normalizedInput = VectorInput.normalized;
        var move = new Vector3(normalizedInput.x, normalizedInput.y, 0f);
        
        _rigidbody2D.linearVelocity = move * speed;
        AnimateMovement();
    }
    
    private void FourDirectionalMovement()
    {
        var normalizedInput = VectorInput.normalized;
        
        if (Mathf.Abs(normalizedInput.x) > Mathf.Abs(normalizedInput.y))
        {
            normalizedInput.y = 0;
        }
        else
        {
            normalizedInput.x = 0;
        }
        var move = new Vector3(normalizedInput.x, normalizedInput.y, 0f);
        
        transform.Translate(move * (speed * Time.deltaTime));
        AnimateMovement();
    }

    private void AnimateMovement()
    {
        if (!_animator) return;

        const float deadzone = 0.1f;

        if (Mathf.Abs(VectorInput.x) <= deadzone && Mathf.Abs(VectorInput.y) <= deadzone)
        {
            _animator.SetFloat("MoveX", 0f);
            _animator.SetFloat("MoveY", 0f);
            _animator.SetBool("IsMoving", false);
            return;
        }
        // if |x or y| movement greater than zero then movex/movey = sign of x/y else 0
        var moveX = Mathf.Abs(VectorInput.x) > deadzone ? Mathf.Sign(VectorInput.x) : 0f;
        var moveY = Mathf.Abs(VectorInput.y) > deadzone ? Mathf.Sign(VectorInput.y) : 0f;

        _animator.SetFloat("MoveX", moveX);
        _animator.SetFloat("MoveY", moveY);
        _animator.SetBool("IsMoving", true);
    }


    public static bool WithingRange(Vector3 t)
    {
        var player = FindAnyObjectByType<CharacterMovementController>();
        return player.interactionRange >= Vector2.Distance(player.transform.position, t);
    }
}

public enum MovementType
{
    SixDirectional,
    FourDirectional
}
