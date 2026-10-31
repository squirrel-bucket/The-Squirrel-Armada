using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class HealthPickup : MonoBehaviour, DataPersistence
{
    [SerializeField] private string id;

    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    Animator animator;
    public int healthRestore = 15;
    private bool collected = false;
    private SpriteRenderer visual;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        visual = this.GetComponent<SpriteRenderer>();
    }

    public void LoadData(GameData data)
    {
        data.pickupsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            visual.gameObject.SetActive(false);
        }
        else
        {
            visual.gameObject.SetActive(true);
        }
    }
    public void SaveData(ref GameData data)
    {
        if (data.pickupsCollected.ContainsKey(id))
        {
            data.pickupsCollected.Remove(id);
        }
        data.pickupsCollected.Add(id, collected);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damageable = collision.GetComponent<Damagable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                CollectPickup();
            }
        }
    }

    private void CollectPickup()
    {
        collected = true;
        visual.gameObject.SetActive(false);
    }

    public bool isIdle
    {
        get
        {
            return animator.GetBool(AnimationStrings.isIdle);
        }
    }

}
