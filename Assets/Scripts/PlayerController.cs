using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float horizontalMul;
    private float moveX;
    public TMP_Text score;
    int scoreNum = 0;
    int coinsCollected = 0;
    int blueCollected = 0;
    int coinsNotInv = 0;
    int blueNotInv = 0;
    bool alive = true;
    bool mainCamActive = true;
    bool fpsCamActive = false;
    public GameObject mainCam;
    public GameObject fpsCam;
    public float jumpForce;
    bool invincible = false;
    bool speedBoosted = false;
    float invTime = 5;
    float boostTime = 7;
    public AudioSource coinAudio;
    public AudioSource blueAudio;
    public AudioSource ironAudio;
    public AudioSource bombAudio;
    public AudioSource gameAudio;
    public AudioSource invAudio;
    bool collided = false;
 



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        mainCam.SetActive(mainCamActive);
        fpsCam.SetActive(fpsCamActive);

    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 v = context.ReadValue<Vector2>();
        moveX = v.x;
    }
    private void FixedUpdate()
    {
        if (!alive)
        {
            return;
        }
        if ((collided == true) && (scoreNum <= 0))
        {
            Die();
        }
        if((invincible == false)&&(coinsNotInv == 3))
        {
            invincible = true;
            invAudio.Play();
            coinsNotInv = 0;
        }

        if ((speedBoosted == false) && (blueNotInv == 3))
        {
            speedBoosted = true;
            speed *= 2;
            horizontalMul -= 0.5f;
            blueNotInv = 0;
        }
        if(invincible == true)
        {
            gameAudio.Pause();
            invTime -= Time.smoothDeltaTime;
            if(invTime < 0)
            {
                invincible = false;
                invTime = 5;
                invAudio.Stop();
                gameAudio.UnPause();
            }
        }
        if(speedBoosted == true)
        {
            boostTime -= Time.smoothDeltaTime;
            if (boostTime < 0)
            {
                speedBoosted = false;
                speed /= 2;
                horizontalMul += 0.5f;
                boostTime = 7;
            }
        }

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * moveX * speed * Time.fixedDeltaTime * horizontalMul;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
        score.text = "Score: " + scoreNum + "\n" + "Coins Collected: " + coinsCollected + "\n" + "Blue Spheres Collected: " + blueCollected;

    }

    private void moveRight()
    {
        rb.MovePosition(rb.position + new Vector3(1f, 0.0f, 0.0f));
    }

    private void moveLeft()
    {
        rb.MovePosition(rb.position + new Vector3(-1f, 0.0f, 0.0f));
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bomb")){
            if (invincible)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                bombAudio.Play();
                Die();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            collided = true;
            coinAudio.Play();
            Destroy(other.gameObject);
            scoreNum += 15;
            coinsCollected += 1;
            if (invincible == false)
            {
                coinsNotInv += 1;
            }
        }
        if (other.gameObject.CompareTag("IronBall"))
        {
            collided = true;
            if (invincible)
            {
                Destroy(other.gameObject);
            }
            else
            {
                ironAudio.Play();
                Destroy(other.gameObject);
                scoreNum -= 10;
            }
           
        }
        if (other.gameObject.CompareTag("BlueBall"))
        {
            collided = true;
            blueAudio.Play();
            Destroy(other.gameObject);
            scoreNum += 10;
            blueCollected += 1;
            if (speedBoosted == false)
            {
                blueNotInv += 1;
            }
            
        }
    }
    public void Jump()
    {
        /*        jumpForce = 100;
                if ((transform.position.y<0.3)&&(transform.position.y>0.0))
                {
                    rb.AddForce(Vector3.up * jumpForce);
                }
                */
        jumpForce = 100;
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, height / 2 + 0.1f);
        if (isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }

    public void JumpButton()
    {
        jumpForce = 200;
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, height/2 + 0.1f);
        if (isGrounded == true)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

    }

    public void OnSwitchCamera()
    {

            mainCamActive = !mainCamActive; ;
            fpsCamActive = !fpsCamActive;
            mainCam.SetActive(mainCamActive);
            fpsCam.SetActive(fpsCamActive);
    }

    public void Die()
    {
        alive = false;
        Invoke("GameOver", 1);
        
    }

     void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }


    // Update is called once per frame
    void Update()
    {

        if (SwipeManager.swipeRight)
        {
            moveRight();
        }

        if (SwipeManager.swipeLeft)
        {
            moveLeft();
        }

        if (transform.position.y < -1)
        {
            Die();
        }

    }
}
