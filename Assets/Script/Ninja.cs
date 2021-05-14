using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    private Transform trans;

    private int vidas = 3;

    public GameObject ArmaR;
    public GameObject ArmaL;

    public Vida Vida;
    public Puntaje Puntaje;


    private bool trepar = false;
    private bool muerte = false;
    private bool vuela = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        if (rb.velocity.y < -60)
            muerte = true;

        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetInteger("Estado", 0);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(10f, rb.velocity.y);
            animator.SetInteger("Estado", 1);
            sr.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-10f, rb.velocity.y);
            animator.SetInteger("Estado", 1);
            sr.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(rb.velocity.x, 30f), ForceMode2D.Impulse);
            animator.SetInteger("Estado", 2);
        }
        if (Input.GetKey(KeyCode.DownArrow))
            animator.SetInteger("Estado", 4);
        if (trepar)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            animator.SetInteger("Estado", 5);
            if (Input.GetKey(KeyCode.UpArrow))
                rb.velocity = new Vector2(rb.velocity.x, 10f);
            if (Input.GetKey(KeyCode.DownArrow))
                rb.velocity = new Vector2(rb.velocity.x, -10f);
        }
        if (!trepar)
            rb.gravityScale = 10;

        if (Input.GetKeyUp("x"))
        {
            animator.SetInteger("Estado", 3);
            if (!sr.flipX)
            {
                var KunaiPosition = new Vector3(trans.position.x + 3f, trans.position.y, trans.position.z);
                Instantiate(ArmaR, KunaiPosition, Quaternion.identity);
            }
            if (sr.flipX)
            {
                var KunaiPosition = new Vector3(trans.position.x - 3f, trans.position.y, trans.position.z);
                Instantiate(ArmaL, KunaiPosition, Quaternion.identity);
            }
        }

        if (muerte)
        {
            animator.SetInteger("Estado", 7);
            rb.velocity = new Vector2(0, 0);
        }

        if (Input.GetKey(KeyCode.Space) && vuela)
        {
            rb.gravityScale = 1;
            animator.SetInteger("Estado", 6);
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.velocity = new Vector2(10f, -10f);
                sr.flipX = false;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.velocity = new Vector2(-10f, -10f);
                sr.flipX = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            vidas--;
            if (vidas == 0)
            {
                muerte = true;
            }
            else
            {
                Vida.QuitarVida(1);
                Debug.Log(Vida.GetVida());
            }

        }
        if (collision.gameObject.layer == 8)
            vuela = false;
        else
            vuela = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            trepar = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
            trepar = false;
    }
}
