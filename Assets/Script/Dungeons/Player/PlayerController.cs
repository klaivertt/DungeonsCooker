using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.U2D.Physics.PhysicsBody;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    InputAction moveAction;

    Rigidbody rb;
    Vector2 moveDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        moveDir = moveAction.ReadValue<Vector2>();
    }
    void FixedUpdate()
    {
        Vector3 dir = new Vector3(moveDir.x, 0f, moveDir.y);
        rb.linearVelocity = new Vector3(dir.x * speed, rb.linearVelocity.y, dir.z * speed);
    }
}
