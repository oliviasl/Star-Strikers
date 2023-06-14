using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float paddingLeft = 0.5f;
    [SerializeField] float paddingRight = 0.5f;
    [SerializeField] float paddingTop = 5.0f;
    [SerializeField] float paddingBottom = 2.0f;
    
    private Vector2 rawInput;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    private Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        // multiplying by deltaTime makes it framerate independent
        // deltaTime is the time the last frame took to render
        // so if frames take longer the player will move farther per frame
        Vector3 delta = (Time.deltaTime * moveSpeed * rawInput);
        Vector2 newPos = new Vector2();
        
        // clamping movement with viewport boundaries and padding
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
    
}
