using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    Controller2D controller;
    Vector2 directionalInput;
    Camera mainCam;
    LvlNextManager lvlNextManager;
    KeysManager keysManager;

    [Header("Player's Position Reset Vector")]
    public Vector2 resetPos;

    [Header("Gravity / Velocity Variables")]
    float gravity;
    public float moveSpeed;
    Vector3 velocity;
    float velocityXSmoothing;
    float maxJumpVelocity;
    float minJumpVelocity;
    public Vector3 launch;

    [Header("Jump Variables")]
    public float maxJumpHeight = 4;
    public float mixJumpHeight = 1;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirbourne = .2f;
    float accelerationTimeGrounded = .1f;

    //public int doubleJump;
    public Vector2 doubleJump;
    [SerializeField]
    private bool canDoubleJump;

    public Vector2 wallClimb;
    public Vector2 wallJumpOff;
    public Vector2 wallLeap;

    public float wallStickTime = .25f;
    float timeToWallUnstick;

    public float wallSlideSpeedMax = 3f;

    bool wallSliding;
    int wallDirX;

    public bool resetLevel;
    public string resetLevelName;

    [Header("Particles")]
    public GameObject smokePuff;
    public GameObject bloodSplat;

    

    void Start()
    {
        controller = GetComponent<Controller2D>();
        mainCam = FindObjectOfType<Camera>();
        lvlNextManager = FindObjectOfType<LvlNextManager>();
        keysManager = FindObjectOfType<KeysManager>();

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        Debug.Log("Gravity: " + gravity + "Jump Velocity: " + maxJumpVelocity);

        canDoubleJump = false;
    }

    void Update()
    {
        CalcVelocity();
        HandleWallSliding();
        //controller.Move(velocity * Time.deltaTime, directionalInput);

        if (controller.collisions.above || controller.collisions.below)
        {
            canDoubleJump = true; ;
            velocity.y = 0;
        }

        #region JUMPING - TIDY UP LATER
        if (Input.GetAxis("Jump") != 0)
        {
            // IF PLAYER IS ON THE GROUND THEN JUMP //
            if (controller.collisions.below)
            {
                velocity.y = maxJumpVelocity;
                canDoubleJump = true;
            }
            else

            //canDoubleJump = true;
            // IF THE PLAYER IS WALL SLIDING //
            if (wallSliding)
            {
                // DO THE WALL CLIMB
                if (wallDirX == directionalInput.x)
                {
                    velocity.x = -wallDirX * wallClimb.x;
                    velocity.y = wallClimb.y;
                }
                // DO THE WALL JUMP OFF //
                else if (directionalInput.x == 0)
                {
                    velocity.x = -wallDirX * wallJumpOff.x;
                    velocity.y = wallJumpOff.y;
                }
                // DO THE WALL LEAP //
                else
                {
                    velocity.x = -wallDirX * wallLeap.x;
                    velocity.y = wallLeap.y;
                }
            }
        }

        #endregion

        controller.Move(velocity * Time.deltaTime);

    }

    public void SetDirectionalInput(Vector2 input)
    {
        directionalInput = input;
    }

    void HandleWallSliding()
    {
        //int wallDirX = (controller.collisions.left) ? -1 : 1;
        wallDirX = (controller.collisions.left) ? -1 : 1;

        //bool wallSliding = false;
        wallSliding = false;
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0)
        {
            wallSliding = true;

            if (velocity.y < -wallSlideSpeedMax)
            {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0)
            {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirX && directionalInput.x != 0)
                {
                    timeToWallUnstick -= Time.deltaTime;
                }
                else
                {
                    timeToWallUnstick = wallStickTime;
                }
            }

            else
            {
                timeToWallUnstick = wallStickTime;
            }
        }

    }

    void CalcVelocity()
    {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirbourne);
        velocity.y += gravity * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "LvlNext")
        {
            lvlNextManager.ChangeScene();
        }

        if (other.tag == "DeathCollider")
        {
            GameObject GO = Instantiate(smokePuff, transform.position, transform.rotation) as GameObject;
            Destroy(GO, 2f);
            ResetPlayer();
        }

        if (other.tag == "Hazard")
        {
            GameObject GO = Instantiate(bloodSplat, transform.position, transform.rotation) as GameObject;
            Destroy(GO, 2f);
            ResetPlayer();
        }

        if (other.tag == "Key")
        {
            keysManager.OpenDoor();
        }


    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "BrokenBlock")
        {
            Destroy(other.gameObject, 0.5f);
        }
    }

    public void ResetPlayer()
    {
        transform.position = resetPos;

        if (keysManager.hasKey && keysManager.doorOpened)
        {
            keysManager.ResetObjs();
        }
        else
        {
            return;
        }
    }
}