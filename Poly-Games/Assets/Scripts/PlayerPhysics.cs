using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour {

    public LayerMask collisionMask;
    [HideInInspector]
    public bool isGrounded, isDead, isInBuisson, isInSable, canClimbOnVine, isBodyInVine, isHandsInVine, isInAura, isHandsInAura;
    private bool climbingSlope, pere;
    private BoxCollider2D boxCollider;
    private Vector2 c, s, p;
    public float skin = .005f, slopeMult = 10, maxClimbAngle = 70;
    private float deltaX, deltaY, angle;
    Ray2D ray;
    RaycastHit2D hit;

    void Awake()
    {
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        c = boxCollider.offset;
        s = boxCollider.size;
        if (gameObject.tag == "pere")
            pere = true;
    }

	// Use this for initialization
	public void Move(Vector3 moveAmount)
    {
        if (isInBuisson && isInAura && pere && !isDead)
        { 
            isDead = true;
        }
        

        deltaY = moveAmount.y;
        deltaX = moveAmount.x;

        ResetCollisions();
        UpdateVineStatus();

        if (!isInSable)
        {
            HorizontalCollisions();
            VerticalCollisions();
        }
        else if(isInSable)
            isDead = true;

            Vector3 transformMod = new Vector3(deltaX, deltaY, 0);

            transform.position += transformMod;
        
    }

    void HorizontalCollisions()
    {
        float rayLength = Mathf.Abs(deltaX) + skin;
        for (int i = 0; i < 3; i++)
        {
            float dir = Mathf.Sign(deltaX);
            float x = p.x + c.x + s.x / 2 * dir;
            float y = (p.y + c.y - s.y / 2) + s.y / 2 * i;
            Vector2 origin = new Vector2(x, y);
            ray = new Ray2D(origin, new Vector2(dir, 0));
            hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, collisionMask);

            Debug.DrawRay(ray.origin, ray.direction * rayLength);
            if (hit)
            {
                float dst = Vector2.Distance(origin, hit.point) - skin;
                angle = Vector2.Angle(hit.normal, ray.direction) - 90;
                if (i == 0 && angle < maxClimbAngle && angle > 0)
                {
                    rayLength = hit.distance;
                    float climbVelocityY = Mathf.Sin(angle * Mathf.Deg2Rad) * Mathf.Abs(deltaX);
                    if (deltaY <= climbVelocityY)
                    {
                        deltaY = climbVelocityY;
                        deltaX = Mathf.Cos(angle * Mathf.Deg2Rad) * deltaX;
                        climbingSlope = true;
                        isGrounded = true;
                    }
                }
                if (!climbingSlope || angle > maxClimbAngle)
                {
                    deltaX = dir * dst;
                    rayLength = hit.distance;

                    if (climbingSlope)
                    {
                        deltaY = Mathf.Tan(angle * Mathf.Deg2Rad) * Mathf.Abs(deltaX);
                    }
                }


                
            }
        }
    }

    void VerticalCollisions()
    {
        float rayLength = Mathf.Abs(deltaY) + skin;
        for (int i = 0; i < 3; i++)
        {
            float dir = Mathf.Sign(deltaY);
            float x = (p.x + c.x - s.x / 2) + s.x / 2 * i;
            float y = p.y + c.y + s.y / 2 * dir;
            Vector2 origin = new Vector2(x, y);
            ray = new Ray2D(origin, new Vector2(0, dir));
            hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, collisionMask);

            Debug.DrawRay(ray.origin, ray.direction * rayLength);
            if (hit)
            {
                float dst = Vector2.Distance(origin, hit.point) - skin;
                deltaY = dir * dst;
                rayLength = hit.distance;
                isGrounded = true;
                if(climbingSlope)
                    deltaX = deltaY / Mathf.Tan(angle * Mathf.Deg2Rad) * Mathf.Sign(deltaX);
                
            }
        }
    }

    void UpdateVineStatus()
    {
        if (isHandsInVine)
        {
            canClimbOnVine = true;
        }
        if (isHandsInAura)
        {
            canClimbOnVine = false;
        }
        if(canClimbOnVine && !isBodyInVine)
        {
            canClimbOnVine = false;
        }
    }

    void ResetCollisions()
    {
        isGrounded = false;
        climbingSlope = false;
        p = transform.position;
    }


}
