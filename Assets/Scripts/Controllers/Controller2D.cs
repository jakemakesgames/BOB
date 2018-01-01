using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : RaycastController
{
    public CollisionInfo collisions;
    [HideInInspector]
    public Vector2 playerInput;

    public override void Start()
    {
        base.Start();
        collisions.faceDir = 1;
    }

    public void Move(Vector2 deltaMove, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (deltaMove.x != 0)
        {
            collisions.faceDir = (int)Mathf.Sign(deltaMove.x);
        }

        HorizontalCollisions(ref deltaMove);

        if (deltaMove.y != 0)
        {
            VerticalCollisions(ref deltaMove);
        }

        transform.Translate(deltaMove);

        if (standingOnPlatform)
        {
            collisions.below = true;
        }
    }

    // DRAW THE HORIZONTAL RAYS //
    void HorizontalCollisions(ref Vector2 velocity)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        if (Mathf.Abs(velocity.x) < skinWidth)
        {
            rayLength = 2 * skinWidth;
        }

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.green);

            if (hit)
            {
                if (hit.distance == 0)
                {
                    continue;
                }

                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                collisions.left = directionX == -1;
                collisions.right = directionX == 1;

            }
        }
    }

    // DRAW THE VERTICAL RAYS //
    void VerticalCollisions(ref Vector2 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.green);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }


    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;
        public int faceDir;

        public void Reset()
        {
            // Resetting the BOOLS //
            above = below = false;
            left = right = false;
        }
    }
}
