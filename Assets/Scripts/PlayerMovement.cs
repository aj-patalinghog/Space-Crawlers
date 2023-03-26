using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float walkingSpeed = 5f;
    private Vector2 _walkingInput;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxisRaw("Horizontal"));
        _walkingInput.x = Input.GetAxisRaw("Horizontal") * walkingSpeed;
        _walkingInput.y = Input.GetAxisRaw("Vertical") * walkingSpeed;
    }

    void FixedUpdate() {
        rb.velocity = transform.TransformDirection(_walkingInput);
    }
}
