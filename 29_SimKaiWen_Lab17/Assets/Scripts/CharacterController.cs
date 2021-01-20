using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public int healthCount;
    public int coinCount;

    private float gravityMod = 3f;
    private int jumpCount;

    //Texts UI referrencing
    public GameObject healthText;
    public GameObject coinText;

    //Components
    private Rigidbody2D rb;
    private Animator animator;

    //Audio
    public AudioClip[] sounds;
    public AudioSource audioSource;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Physics.gravity *= gravityMod;
    }

    // Update is called once per frame
    void Update()
    {
        //Assigning coin and healthCount to text in UI
        coinText.GetComponent<Text>().text = "Coins: " + coinCount;
        healthText.GetComponent<Text>().text = "Health: " + healthCount;

        float hVelocity = 0;
        float vVelocity = 0;

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            hVelocity = -moveSpeed;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            hVelocity = moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount == 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(sounds[0]);

            animator.SetTrigger("JumpTrigger");
            animator.SetBool("onGround", false);
            jumpCount++;
        }

        hVelocity = Mathf.Clamp(rb.velocity.x + hVelocity, -5, 5);
        rb.velocity = new Vector2(hVelocity, rb.velocity.y + vVelocity);


        //Use the following code to set the animator parameter on User Input:
        /*
        animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
        animator.SetFloat("xVelocity", 0);
        */

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.CompareTag("Enemy"))
        {
            healthCount -= 1;
        }

        if(collision.collider.gameObject.CompareTag("Coin"))
        {
            coinCount++;
            Destroy(collision.collider.gameObject);
        }

        if(collision.collider.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("onGround", true);
            jumpCount = 0;
        }
    }
}
