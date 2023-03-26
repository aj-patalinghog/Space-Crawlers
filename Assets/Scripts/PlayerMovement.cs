using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 walkingInput;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        walkingInput.x = Input.GetAxisRaw("Horizontal") * walkingSpeed;
        walkingInput.y = Input.GetAxisRaw("Vertical") * walkingSpeed;
    }

    void FixedUpdate() {
        rb.velocity = transform.TransformDirection(walkingInput);
    }
}
