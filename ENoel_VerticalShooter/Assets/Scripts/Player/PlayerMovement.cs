using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Vector2 _input;
    [SerializeField] private Vector2 moveSpeed;
    [SerializeField] private float boundsLeft = -5;
    [SerializeField] private float boundsRight = 5;
    [SerializeField] private float boundsUp = -2f;
    [SerializeField] private float boundsDown = -15f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        _rb.velocity = _input * moveSpeed;
        // Get current position
        Vector2 position = transform.position;
        // Limit the position values to our bounds
        position.x = Mathf.Clamp(position.x, boundsLeft, boundsRight);
        position.y = Mathf.Clamp(position.y, boundsDown, boundsUp);
        // Set current position to that limited value
        transform.position = position;
    }

    public Vector2 GetDirection()
    {
        return _input;
    }

}
