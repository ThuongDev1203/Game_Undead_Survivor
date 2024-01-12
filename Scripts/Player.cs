using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    public Scanner scanner;
    public Vector2 inputMove;
    public float speedMove = 4f;
    public RuntimeAnimatorController[] animCon;

    public Hand[] hands;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        speedMove *= Character.Speed;
        animator.runtimeAnimatorController = animCon[GameManager.instance.playerID];    
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        //inputMove.x = Input.GetAxis("Horizontal");
        //inputMove.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        Vector2 nextMove = inputMove.normalized * speedMove * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextMove);
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        UpdateAnimation();
        if(inputMove.x != 0)
        {
            spriteRenderer.flipX = inputMove.x < 0;
        }
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("isSpeed", inputMove.magnitude);
        //animator.SetTrigger("isDead");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
        {
            return;
        }

        GameManager.instance.health -= Time.deltaTime * 10;

        if(GameManager.instance.health < 0)
        {
            for(int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }

            animator.SetTrigger("isDead");
            GameManager.instance.GameOver();
        }
    }

    private void OnMove(InputValue value)
    {
        inputMove = value.Get<Vector2>(); 
    }
}
