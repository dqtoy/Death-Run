using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public LayerMask groundLayer;
    public float speed = 10.0f;
    public float jumpSpeed = 10f;
    public float flySpeed = 1f;
    private ParticleEffectScript particleEffects;
    private Rigidbody2D rb;
    private Animator playerAnimator;
    private bool canJump = true;
    private bool canFly = true;
    private bool isFlying = false;
    public float fuelLoseCap;
    public GameObject destroyedPlayer;
    public float destroySpeedOnObstacle = 2f;
    public float destroySpeedOnSpike = .1f;
    private float speedIncreaseFactor = 1;
    private float multiplier = .000001f;
    private float initX = 0;
    private float distanceTravelled = 0;
    private bool animatorSetToRunOnce = true;
    private Vector2 pausedVelocity = Vector2.zero;
    private bool onJump = false;
    

    void CheckOnGround()
    {
        if (this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling") &&
            IsGrounded() &&
            !canJump)
        {
            //Debug.Log("On Ground");
            playerAnimator.SetBool("Jump", false);
            //FindObjectOfType<AudioManager>().Play("running");
            canJump = true;
        }
    }

    void CheckFly()
    {
        //canJump == false && canFly && GameManager.flyFuel > 0 &&
        if (!canJump && canFly && GameManager.flyFuel > 0 && !onJump) //added after flyfuel
        {
            if (Input.GetMouseButton(0) && (rb.velocity.y != 0f)) //putting 0 in place of 1
            {
                GameManager.flyFuel -= (Time.deltaTime * fuelLoseCap);
                rb.velocity = transform.up * flySpeed;
                isFlying = true;
                playerAnimator.SetBool("Flying", true);
                StartRunning();
            }
            else
            {
                playerAnimator.SetBool("Flying", false);
                isFlying = false;
            }
        } else if (GameManager.flyFuel <= 0)
        {
            playerAnimator.SetBool("Flying", false);
            isFlying = false;
        }
    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = .7f;
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    void CheckJump()
    {
        if (Input.GetMouseButtonDown(0) && canJump && IsGrounded())
        {
            onJump = true;
            FindObjectOfType<AudioManager>().Play("jump takeoff");
            playerAnimator.SetBool("Jump", true);
            canJump = false;
            rb.velocity = transform.up * jumpSpeed;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            onJump = false;
        }
    }

    void StartRunning()
    {
        if (animatorSetToRunOnce)
        {
            playerAnimator.Play("Run");
            animatorSetToRunOnce = false;
        }
        speedIncreaseFactor = speedIncreaseFactor + ((int)Time.time) * multiplier;
        Vector2 velocity = transform.right * speed * speedIncreaseFactor;
        if (velocity.x > 11f)
        {
            multiplier = 0;
        }
        //Debug.Log("Velocity X: " + velocity.x);
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;
        distanceTravelled = transform.position.x - initX;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            GameManager.score = GameManager.prevScore + (int)distanceTravelled / 10;
        }
    }

    void CheckThrow()
    {
        if (Input.GetMouseButtonDown(2))
        {
            playerAnimator.SetTrigger("Throw");
        }
    }

    void Start() {
        GameManager.score = GameManager.prevScore;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        particleEffects = GameObject.FindGameObjectWithTag("ParticleEffects").GetComponent<ParticleEffectScript>();
        initX = this.transform.position.x;
    }

    void ApplyGamePaused()
    {
        if (GameManager.gamePaused)
        {
            //Pause the game
            rb.isKinematic = true;
            pausedVelocity = rb.velocity;
            rb.velocity = Vector2.zero;
            //rb.gravityScale = 0;
            rb.isKinematic = true;
        }
    }

    void ApplyGamePlayed()
    {
        if (!GameManager.gamePaused)
        {
            rb.velocity = pausedVelocity;
            rb.isKinematic = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlyPowerPickUp")
        {
            int ran = Random.Range(1, 4);
            string pickupString = "pickup" + ran;
            FindObjectOfType<AudioManager>().Play("pickup1");
            GameManager.flyPickupCount++;
            GameManager.flyFuel += GameManager.pickupToFuelFactor;
            Transform tr = collision.transform;
            particleEffects.ShowPickupEffect(tr);
            collision.gameObject.SetActive(false);
            //Debug.Log("Destroyed");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacles" ||
            collision.gameObject.tag == "Spikes")
        {
            if (collision.gameObject.tag == "Obstacles")
            {
                particleEffects.ShowBloodObstacleEffect(transform);
            }
            else
            {
                particleEffects.ShowBloodSpikeEffect(transform);
            }
            GameObject destroyed;
            destroyed = Instantiate(destroyedPlayer as GameObject);
            destroyed.transform.position = this.gameObject.transform.position;
            if (!GameManager.showAdScreen && GameManager.elligibleForRevive)
            {
                GameManager.showAdScreen = true;
                GameManager.rewardUsed = 1;
            }
            else if(!GameManager.elligibleForRevive)
            {
                GameManager.showAdScreen = false;
                GameManager.playerDead = true;
            }
            ContactPoint2D contact = collision.contacts[0];
            Vector3 dir = contact.point - new Vector2(destroyed.transform.position.x, destroyed.transform.position.y);
            dir = -dir.normalized;
            Rigidbody2D[] rgb = destroyed.GetComponentsInChildren<Rigidbody2D>();
            foreach (Rigidbody2D r in rgb)
            {
                if (collision.gameObject.tag == "Obstacles")
                {
                    FindObjectOfType<AudioManager>().Play("saw death");
                    r.AddForce(new Vector2(destroySpeedOnObstacle, destroySpeedOnObstacle), ForceMode2D.Impulse);
                }
                if (collision.gameObject.tag == "Spikes")
                {
                    FindObjectOfType<AudioManager>().Play("spike death");
                    r.AddForce(new Vector2(destroySpeedOnSpike, destroySpeedOnSpike), ForceMode2D.Impulse);
                }
            }
            //GameManager.showAdScreen = true;
            Destroy(this.gameObject);
        }
    }


    void AudioPlayer()
    {
        if (isFlying)
        {
            FindObjectOfType<AudioManager>().Play("jetpack");
        }
        if (!IsGrounded())
        {
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    void Update() {

        if (GameManager.gameStarted)
        {
            StartRunning();
            CheckJump();
            CheckOnGround();
            CheckFly();
            CheckThrow();
            AudioPlayer();
            ApplyGamePaused();
            
        }
    }
}
