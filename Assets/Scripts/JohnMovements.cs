using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovements : MonoBehaviour
{
    public GameObject BulletPrefad;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    public float JumpForce;
    public float Speed;
    private bool Grounded;
    private Animator Animator;
    private float LastShoot;
    private int Health = 10;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

        if (Horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (Horizontal > 0.0f ) 
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        Animator.SetBool("running", Horizontal != 0.0f);
        Animator.SetBool("jumping", Input.GetKeyDown(KeyCode.W) && Grounded);

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
        }

        if(Input.GetKeyDown(KeyCode.W) && Grounded) 
        {
            Jump();
        }
        
        if (Input.GetKey(KeyCode.Q) && Time.time > LastShoot + 0.15f)
        {
            Shoot();
            LastShoot = Time.time;
        }

    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;
        GameObject bullet = Instantiate(BulletPrefad, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce * 1.05f);
        
    }

    private void FixedUpdate() 
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    public void Hit()
    {
        Health = Health - 1;
        if (Health == 0) 
        {
            Destroy(gameObject);
        }
    }

}
