using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerClimbing : MonoBehaviour
{
    private CharacterController controller;
    public float climbSpeed = 5f;
    public bool IsClimbing { get; set; } = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (IsClimbing)
        {
            float vertical = Input.GetAxis("Vertical"); 
            Vector3 move = new Vector3(0, vertical * climbSpeed, 0);
            controller.Move(move * Time.deltaTime);
        }
    }
}
