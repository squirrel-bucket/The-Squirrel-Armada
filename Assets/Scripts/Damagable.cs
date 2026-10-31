using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour, DataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public UnityEvent<int, Vector2> damagableHit;
    public UnityEvent<int, int> healthChanged;
    Animator animator;
    Damagable playerDamagable;
    private SpriteRenderer visual;
    private bool isSlain = false;
    Color startColor;
    GameManager gameManager;
    EnemySpecs enemySpecs;

    private Vector3 test;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            //health less than zero player is dead
            if (_health <= 0)
            {
                IsAlive = false;
                SLAYYYYY();
            }
        }
    }
    public void LoadData(GameData data)
    {
        data.knightsSlain.TryGetValue(id, out isSlain);

        if (isSlain)
        {
            this.gameObject.SetActive(false);
        }
        if (!isSlain)
        {
            this.gameObject.SetActive(true);
            this.IsAlive = true;
            this.Health = MaxHealth;
            visual.color = startColor;
        }

        this.gameObject.transform.position = test;
    }
    public void SaveData(ref GameData data)
    {
        if (data.knightsSlain.ContainsKey(id))
        {
            data.knightsSlain.Remove(id);
        }
        data.knightsSlain.Add(id, isSlain);

    }

    private void SLAYYYYY()
    {
        isSlain = true;
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    
    public bool IsAlive { 
        get
        {
            return _isAlive;
        } 
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);
        } 
    }

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
        playerDamagable = player.GetComponent<Damagable>();
        visual = this.GetComponent<SpriteRenderer>();
        startColor = visual.color;
        gameManager = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        test = this.gameObject.transform.position;
        
    }

    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                //remove invincible
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            //notify either subscribed components that the damagable was hit to handle the knockback and whatnot
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }

        //unable to be hit
        return false;
    }
    // returns whether charac healed or not
    public bool Heal(int healthRestore)
    {
        if(IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }

}
