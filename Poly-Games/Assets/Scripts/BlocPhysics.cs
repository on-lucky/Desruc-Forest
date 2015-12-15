using UnityEngine;
using System.Collections;

public class BlocPhysics : MonoBehaviour
{

    public LayerMask collisionMask;
    [HideInInspector]
    public bool grounded;
    private BoxCollider2D boxCollider;
    private Vector2 c, s, p, amountToMove;
    private bool isMoving;
    public float skin = .00001f, gravity = 50f, rayLength = 0.05f;
    private float deltaX, deltaY, dir, collisionDir;
    [HideInInspector]
    public float currentSpeed;
    Ray2D ray;
    RaycastHit2D hit;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        c = boxCollider.offset;
        s = boxCollider.size;

    }

    void Update()
    {
        amountToMove.x = currentSpeed;
        currentSpeed = 0;
        if (grounded)
            amountToMove.y = 0;
        amountToMove.y -= gravity * Time.deltaTime;

        Move(amountToMove * Time.deltaTime);
    }

    // Use this for initialization
    public void Move(Vector3 moveAmount)
    {
        deltaY = moveAmount.y;
        deltaX = moveAmount.x;
        p = transform.position;

        grounded = false;

        //collisions haut/bas
        VerticalCollisions();
        HorizontalCollisions();

        isMoving = false;
        collisionDir = 0;

        Vector3 transformMod = new Vector3(deltaX, deltaY, 0);

        transform.position += transformMod;
    }

    void VerticalCollisions()
    {
        for (int i = 0; i < 3; i++)
        {
            float dir = -1;
            float x = (p.x + c.x - s.x / 2) + s.x / 2 * i;
            float y = p.y + c.y + s.y / 2 * dir;
            Vector2 origin = new Vector2(x, y);
            ray = new Ray2D(origin, new Vector2(0, dir));
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Abs(deltaY), collisionMask);

            Debug.DrawRay(ray.origin, ray.direction);
            if (hit)
            {
                if (hit.transform.gameObject.tag == "filleBody")
                    hit.transform.parent.gameObject.GetComponent<PlayerPhysics>().isDead = true;

                float dst = Vector2.Distance(origin, hit.point);

                if (dst > skin)
                {
                    deltaY = dir * dst - dir * skin;
                }
                else
                {
                    deltaY = 0;
                }

                grounded = true;
                break;
            }
        }
    }
    void HorizontalCollisions()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                dir = Mathf.Pow(-1, j);
                float x = p.x + c.x + s.x / 2 * dir;
                float y = (p.y + c.y - s.y / 2) + s.y / 2 * i;
                Vector2 origin = new Vector2(x, y);
                ray = new Ray2D(origin, new Vector2(dir, 0));
                hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, collisionMask);
                Debug.DrawRay(ray.origin, ray.direction * rayLength);
                if (hit)
                {
                    float dst = Vector2.Distance(origin, hit.point);

                    if (hit.transform.gameObject.tag == "pereBody")
                    {
                        PereControl pereControl = hit.transform.parent.gameObject.GetComponent<PereControl>();
                        if (-collisionDir == pereControl.dir || collisionDir == 0)
                        {
                            deltaX = pereControl.currentSpeed * Time.deltaTime / 10;
                        }
                    }
                    else
                    {
                        collisionDir = dir;
                        if (dst > skin)
                        {
                            deltaX = dir * dst - dir * skin;
                        }
                        else
                        {
                            deltaX = 0;
                        }
                    }
                }
            }
        }
    }
}

