using UnityEngine;
using System.Collections;


public class PereControl : MonoBehaviour
{
    [HideInInspector]
    public float currentSpeed;
    [HideInInspector]
    public int dir;
    public float targetSpeed = 8, jumpHeight = 12, acceleration = 12, gravity = 20, sableFall = 3f, climbingSpeed = 10f;
    private Vector3 amountToMove;
    private PlayerPhysics physics;
    public bool isInSable;
    Sprite dead;

    // Use this for initialization
    void Start()
    {
        dead = Resources.Load("dead", typeof(Sprite)) as Sprite;
        physics = GetComponent<PlayerPhysics>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = 0;

        if (!physics.isDead && !physics.isInSable)
        {
            if (physics.isGrounded)
                amountToMove.y = 0;

            if (physics.canClimbOnVine)
                Climb();
            else if (Input.GetKeyDown("w"))
            {
                Jump();
            }

            if (Input.GetKey("a"))
            {
                dir = -1;
            }
            else if (Input.GetKey("d"))
            {
                dir = 1;
            }

            currentSpeed = incrementSpeed(currentSpeed, targetSpeed, acceleration, dir);
            amountToMove.x = currentSpeed;

            if(!physics.canClimbOnVine)
                amountToMove.y -= gravity * Time.deltaTime;


            physics.Move(amountToMove * Time.deltaTime);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = dead;
            if (physics.isInSable)
            {
                amountToMove.x = 0;
                amountToMove.y = -sableFall * Time.deltaTime;
                physics.Move(amountToMove * Time.deltaTime);
            }
        }
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

    void Jump()
    {
                if (physics.isGrounded)
                    amountToMove.y = jumpHeight;
    }
    void Climb()
    {
        if (Input.GetKey("w"))
        {
            amountToMove.y = climbingSpeed;
            physics.isGrounded = false;
        }
        else if (Input.GetKey("s"))
        {
            amountToMove.y = -climbingSpeed;
        }
        else
        {
            amountToMove.y = 0;
        }
    }

}

