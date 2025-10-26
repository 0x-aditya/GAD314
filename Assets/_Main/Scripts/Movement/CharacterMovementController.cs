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


    private void Start() => SetMovementFunction();
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
        
        transform.Translate(move * (speed * Time.deltaTime));
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
