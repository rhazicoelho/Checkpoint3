using UnityEngine;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.PlayerLoop;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private bool playerAttacking;
    private Vector2 direction;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriterenderer;
    private Collider2D collider2D;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

   
    void Update()
    {
        Move();
        
    }

    void FixedUpdate()
    {
        OnMove();
    }

    void Move()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rigidbody2D.linearVelocity = direction.normalized * moveSpeed;
    }

   
}
