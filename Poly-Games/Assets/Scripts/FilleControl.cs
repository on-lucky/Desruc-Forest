using UnityEngine;
using System.Collections;


public class FilleControl : MonoBehaviour
{

    float currentSpeed;
    public float targetSpeed = 8, jumpHeight = 12, acceleration = 12, gravity = 20;
    private Vector3 amountToMove;
    private PlayerPhysics physics;
    Sprite dead;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        dead = Resources.Load("dead", typeof(Sprite)) as Sprite;
        physics = GetComponent<PlayerPhysics>();
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        int dir = 0;

        if (!physics.isDead)
        {
            if (Input.GetKey("left"))
            {
                dir = -1;
            }
            else if (Input.GetKey("right"))
            {
                dir = 1;
            }
            if (physics.isGrounded)
            {
                amountToMove.y = 0;
                if (Input.GetKeyDown("space") || Input.GetKeyDown("up"))
                {
                    amountToMove.y = jumpHeight;
                }
            }

        currentSpeed = incrementSpeed(currentSpeed, targetSpeed, acceleration, dir);
        amountToMove.x = currentSpeed;
        amountToMove.y -= gravity * Time.deltaTime;
        physics.Move(amountToMove * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = dead;
        }
        anim.SetBool("Ground", physics.isGrounded);
        anim.SetFloat("Speed", currentSpeed);
    }
    private float incrementSpeed(float s, float t, float a, int d)
    {
        t *= d;
        if (s == t)
        {
            return s;
        }
        else
        {
            s += a * Time.deltaTime * d;
            if (Mathf.Abs(s) < Mathf.Abs(t))
            {
                return s;
            }
            else
                return t;
        }
    }
}

