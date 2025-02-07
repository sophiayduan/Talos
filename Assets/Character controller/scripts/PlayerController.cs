using UnityEngine;

public class PlayerController : MonoBehaviour, InputActions.IPlayerLocomotionMapActions
{
    public InputActions PlayerControls {get; private set;}
    public Vector2 MovementInput {get; private set;}
}
