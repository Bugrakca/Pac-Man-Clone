using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    
    // Pac-man in hareketi icin bir tru false deger tutmak gerekiyor. Saga dogru git dediginde o degeri tutmasi gerekiyor.
    // Daha sonra saginda bir duvar varsa, saga dogru donmeye calismamasi gerekiyor. Duvar yoksa saga donmeli.
    // Bunun bir yolunu bulmam gerek.
    
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public LayerMask obstacleLayer;
    public float testValue;

    private Rigidbody2D _rigidbody2D;
    
    private Vector2 _direction = Vector2.right;
    private Vector2 _nextDirection;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_nextDirection != Vector2.zero)
        {
            SetDirection(_nextDirection);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetDirection(-Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetDirection(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetDirection(-Vector2.right);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = _rigidbody2D.position;
        Vector2 translation = _direction * speed * speedMultiplier * Time.fixedDeltaTime;
        
        _rigidbody2D.MovePosition(position + translation);
    }

    bool Valid(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * testValue, 0.0f, direction, 1.5f, obstacleLayer);
        return hit.collider == null;
    }

    public void SetDirection(Vector2 direction)
    {
        if (Valid(direction))
        {
            _direction = direction;
            _nextDirection = Vector2.zero;
        }
        else
        {
            _nextDirection = direction;
        }
        
    }
}
