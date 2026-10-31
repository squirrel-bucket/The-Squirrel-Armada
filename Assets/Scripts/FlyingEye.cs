using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class FlyingEye : MonoBehaviour
{
    public float flightSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public List<Transform> waypoints;

    Animator animator;
    Rigidbody2D rb;
    Damagable damagable;

    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damagable = GetComponent<Damagable>();
    }

    private void Start()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            nextWaypoint = waypoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damagable.IsAlive)
        {
            if (CanMove && waypoints != null && waypoints.Count > 0)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }

    private void Flight()
    {
        //fly to next waypoint

        if (nextWaypoint == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        //checkif we already reached
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.linearVelocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        //see if we need to switch

        if(distance <= waypointReachedDistance)
        {
            //switch to next waypt
            waypointNum++;

            if(waypointNum >= waypoints.Count)
            {
                //loop back to original waypt
                waypointNum = 0;
            }

            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if(transform.localScale.x > 0)
        {
            //face right
            if(rb.linearVelocity.x < 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }

        }
        else {
            //face left
            if (rb.linearVelocity.x > 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }
}
