using JetBrains.Annotations;
using Microsoft.Cci;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool onGround, isDash, isShift, isRun, isPull;
    public float horizontalInput, jumpForce, sprintForce, dashForce, pullForce, gravityModifier, speed, pullCD, dashCD, xBound, yBound;
    public Vector2 currentSpeed, maxSpeed, turnLeft, turnRight, startPos;
    private Animator playerAnim;
    private Rigidbody2D playerRb;
    public AudioSource sound;
    void Start()
    {
        xBound = 10;
        yBound = 10;
        startPos = gameObject.transform.position;
        onGround = true;
        isPull = true;
        isDash = true;
        isShift = true;
        isRun = true;
        playerAnim = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
        //Physics2D.gravity *= gravityModifier;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Updatables
        turnLeft = new Vector2(0,180);
        turnRight = new Vector2(0,0);
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * speed * Mathf.Abs(horizontalInput) * Time.deltaTime);
        // Side check
        if (horizontalInput > 0)
        {
            transform.eulerAngles = turnRight;
        }
        else if (horizontalInput < 0)
        {
            transform.eulerAngles = turnLeft;
        }
        // Run
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            playerAnim.SetBool("IsRun", true);
            isDash = true;
            isShift = true;
            playerAnim.speed = 1;
        }
        else
        {
            playerAnim.SetBool("IsRun", false);
            isDash = true;
            isShift = true;
            playerAnim.speed = 1;
        }
        // Jump
        if (Input.GetKey(KeyCode.Space) && (onGround))
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerAnim.SetBool("IsGround", false);
            playerAnim.SetBool("IsCrouch", false);
            isDash = true;
            onGround = false;
            isShift = true;
            playerAnim.speed = 1;
        }
        // Sprint
        if (Input.GetKey(KeyCode.LeftShift) && (onGround) && (isRun))
        {
            transform.Translate(Vector2.right * speed * sprintForce * Mathf.Abs(horizontalInput) * Time.deltaTime);
            isDash = false;
            isShift = false;
            playerAnim.speed = 2;
        }
        // Pull
        if (Input.GetKey(KeyCode.LeftControl) && (onGround) && (horizontalInput != 0) && (isRun) && (pullCD == 0))
        {
            playerRb.AddForce(Vector2.right * pullForce * horizontalInput, ForceMode2D.Impulse);
            pullCD = 3;
        }
        // CD
        if (pullCD > 0)
        {
            pullCD -= Time.deltaTime;
            if (pullCD < 0)
            {
                pullCD = 0;
            }
        }
        if (dashCD > 0)
        {
            dashCD -= Time.deltaTime;
            if (dashCD < 0)
            {
                dashCD = 0;
            }
        }
        // Dash
        if (Input.GetKey(KeyCode.LeftShift) && (!onGround) && (isDash) && (isShift) && (dashCD == 0))
        {
                playerRb.AddForce(new Vector2(1, 0) * dashForce * horizontalInput, ForceMode2D.Impulse);
                isDash = false;
                isShift = false;
                playerAnim.SetBool("IsDash", true);
                playerAnim.SetBool("IsWalk", false);
                playerAnim.speed = 1;
                dashCD = 1.0f;
        }
        ReturnOutOfBounds();
    }

    // MaxSpeed Controller 
    void FixedUpdate()
    {
        currentSpeed = playerRb.velocity;
        maxSpeed = new Vector2(8, 12);
        if (currentSpeed.x > maxSpeed.x)
        {
            playerRb.velocity = new Vector2(maxSpeed.x, currentSpeed.y);
        }
        else if (currentSpeed.y > maxSpeed.y)
        {
            playerRb.velocity = new Vector2(currentSpeed.x, maxSpeed.y);
        }
        if (currentSpeed.x < -maxSpeed.x)
        {
            playerRb.velocity = new Vector2(-maxSpeed.x, currentSpeed.y);
        }
        else if (currentSpeed.y < -maxSpeed.y)
        {
            playerRb.velocity = new Vector2(currentSpeed.x, -maxSpeed.y);
        }
    }
    // onGround Check
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
            isDash = false;
            isShift = false;
            playerAnim.SetBool("IsGround", true);
            playerAnim.SetBool("IsDash", false);
        }
        if (collision.gameObject.CompareTag("Flag"))
        {
            NextLevelLoad(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.CompareTag("Spike"))
        {
            sound.pitch = 1.0f;
            sound.Play();
            gameObject.transform.position = startPos;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if ((pullCD > 2) || (dashCD > 0.5f))
            {
                sound.pitch = 0.2f;
                sound.Play();
                Destroy(collision.gameObject);
            }
            else
            {
                sound.pitch = 0.4f;
                sound.Play();
                gameObject.transform.position = startPos;
            }
        }
    }
    public void NextLevelLoad(int index)
    {
        SceneManager.LoadScene(index + 1);
    }
    
    public void ReturnOutOfBounds()
    {
        if ((Mathf.Abs(transform.position.x) > xBound) || (Mathf.Abs(transform.position.y) > yBound))
        {
            sound.pitch = 0.8f;
            sound.Play();
            gameObject.transform.position = startPos;
        }
    }
}

