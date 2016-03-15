using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    /*
    Version: 0.1
    Changlog: added Movement, Dash, Jump
    Ernesto
    */
    public float maxSpeed;
    public float speed;
    public float jumpPower;
    public float dashPower;
    public float dashReload;

    public bool isDashing;
    public bool dashReloadB;
    public bool grounded;

    private Animator anim;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
       
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        //walking animation
        anim.SetFloat("isWalking", Mathf.Abs(Input.GetAxis("Horizontal")));
        //dash
        if (Input.GetAxis("Horizontal") < -0.1f && Input.GetKey(KeyCode.LeftShift) && isDashing == false && dashReloadB == false)
        {
            //indien je dasht addforce naar links met dashPower, isDashing wordt true (puur voor maxSpeed tijdelijk uit te zetten) dashReload is de cooldown
            rb2d.AddForce(Vector2.left * dashPower);
            isDashing = true;
            dashReloadB = true;
        }
        //dash
        if (Input.GetAxis("Horizontal") > 0.1f && Input.GetKey(KeyCode.LeftShift) && isDashing == false && dashReloadB == false)
        {
            //indien je dasht addforce naar links met dashPower, isDashing wordt true (puur voor maxSpeed tijdelijk uit te zetten) dashReload is de cooldown
            rb2d.AddForce(Vector2.right * dashPower);
            isDashing = true;
            dashReloadB = true;
        }
        //cooldown voor de dash
        if (dashReloadB)
        {
            dashReload -= Time.deltaTime;
            if(dashReload <= 4.8f)
            {
                isDashing = false;
            }
            if(dashReload <= 0)
            {
                //reset cooldown dash
                dashReload = 5f;
                dashReloadB = false;
            }
        }

        //roteren van de sprite aan de hand van links/rechts
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
            grounded = false;
        }
    }

    void FixedUpdate()
    {
        //Movement
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f;

        float h = Input.GetAxisRaw("Horizontal");
        //als je grounded ben minder velocity flexibiliteit, in de lucht verplaats je smoother
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        rb2d.AddForce((Vector2.right * speed) * h);

        if (rb2d.velocity.x > maxSpeed && isDashing == false)
        {

            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }
        if (rb2d.velocity.x < -maxSpeed && isDashing == false)
        {
            rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

    }

    void OnTriggerStay2D()
    {
        grounded = true;
    }

}
