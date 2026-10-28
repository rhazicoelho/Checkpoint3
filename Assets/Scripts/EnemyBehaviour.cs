using NUnit.Compatibility;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject center;
    [SerializeField] float distanceWakeUp;
    [SerializeField] float distanceAttack;
    [SerializeField] float speed;
    [SerializeField] int life;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2d;


    bool wakeUp;
    bool attacking;

    void animationManager()
    {
        animator.SetBool("pWakeUp", wakeUp);
        animator.SetBool("pAttack", attacking);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        center = GameObject.FindGameObjectWithTag("Anchor");
        wakeUp = false;
    }

    void Update()
    {
           
            if (Vector2.Distance(transform.position, player.transform.position) < distanceWakeUp)
            {
                wakeUp = true;                    
            }
            if (Vector2.Distance(transform.position, player.transform.position) > distanceWakeUp)
            {
            wakeUp = false;
            }
        if (wakeUp == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, center.transform.position, speed * Time.deltaTime);
            spriteRenderer.flipX = false;
        }


        else
        {
            if (Vector2.Distance(transform.position, player.transform.position) < distanceAttack)
            {
                Attack();
            }
            else
            {
                Move();
            }
        }

        animationManager();
    }


    IEnumerator Attacking()
    {
        attacking = true;
        wakeUp = true;
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }

    void Attack()
    {
        StartCoroutine(Attacking());
    }



    void Move()
    {
        if (attacking == true) return;

        if (wakeUp == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            if (player.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else if (player.transform.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }

        }

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        life--;

        if (life > 0)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
        else
        {      
            Destroy(gameObject);
        }
    }
}
