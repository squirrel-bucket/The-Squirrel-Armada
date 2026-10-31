using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damagable))]
public class Player_Controller : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    private float crouchSpeed = 2f;

    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damagable damagable;
    GameManager gameManager;

    public float CurrentMoveSpeed { get
        {
            if (CanMove)
            {
                if(IsMoving && !touchingDirections.IsOnWall)
            {
                if (touchingDirections.IsGrounded)
                {
                    if (IsRunning)
                    {
                        return runSpeed;
                    }
                    if (IsCrouching)
                    {
                        return crouchSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                    
                }
            else
            {
                //Air Move
                return airWalkSpeed;
            }

            }
            else
            {
                // idle speed 0
                return 0;
            }
            }
            else
            {
                //movement locked
                return 0;
            }
            
        } }
    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get
        {
            return _isMoving;
        } 
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        } 
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get 
        { 
            return _isRunning; 
        }
        set 
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value );
        }
    }

    private bool _isCrouching = false;

    public bool IsCrouching
    {
        get
        {
            return _isCrouching;
        }
        set
        {
            _isCrouching = value;
            animator.SetBool(AnimationStrings.isCrouching, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight;  } private set {
            if(_isFacingRight != value)
            {
                // flip the local scale
                transform.localScale *= new Vector2(-1, 1);
            }
            
            _isFacingRight= value;
        
        } }

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        } }

    public bool IsAlive { get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        } }


    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damagable = GetComponent<Damagable>();
        gameManager = GetComponent<GameManager>();
    }


    private void FixedUpdate()
    {
        if(!damagable.LockVelocity)
            rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);


        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        } else
        {
            IsMoving = false;
        }
    }


    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //face right
            IsFacingRight = true;
        } else if (moveInput.x < 0 && IsFacingRight)
        {
            //face left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsCrouching = true;
        }
        else if (context.canceled)
        {
            IsCrouching = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // to do: CHECK IF ALIVE AS WELL
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
   
        }
    }

    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }

}
