using System;
using ScriptLibrary.Inputs;
using UnityEngine;

public class CharacterRBMover : Vector2Input
{
    [Header("Player Interaction Range")]
public float interactionRange = 100f;

[Header("Movement Settings")]
[SerializeField] private float speed = 5f;

    [SerializeField] private Rigidbody2D _rb2D;

[Header("Movement Type")]
[SerializeField] private MovementType2 movementType = MovementType2.SixDirectional;
//[SerializeField] private bool snapToGrid = false;

private Action _movementMethod;


private void Start() => SetMovementFunction();
private void OnValidate() => SetMovementFunction();

private void SetMovementFunction()
{
        _rb2D.GetComponent<Rigidbody2D>();
_movementMethod = movementType switch
{
MovementType2.SixDirectional => SixDirectionalMovement,
MovementType2.FourDirectional => FourDirectionalMovement,
_ => throw new Exception("Invalid movement type")
};
}

private void Update() => _movementMethod();

private void SixDirectionalMovement()
{
var normalizedInput = VectorInput.normalized;
var move = new Vector3(normalizedInput.x, normalizedInput.y, 0f);

//transform.Translate(move * (speed * Time.deltaTime));
        _rb2D.linearVelocity = new Vector2(move.x * speed, move.y * speed);
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

        //transform.Translate(move * (speed * Time.deltaTime));
        _rb2D.linearVelocity = new Vector2(move.x * speed, move.y * speed);
    }

public static bool WithingRange(Vector3 t)
{
var player = FindAnyObjectByType<CharacterMovementController>();
return player.interactionRange >= Vector2.Distance(player.transform.position, t);
}
}

public enum MovementType2
{
    SixDirectional,
    FourDirectional
}
