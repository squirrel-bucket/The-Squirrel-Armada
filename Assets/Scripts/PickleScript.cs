using UnityEngine;

public class PickleScript : MonoBehaviour
{
    private Animator animator;
    private DataPersistenceManager dataPersistenceManager;
    private Collider2D checkpoint;
    private Damagable playerDamagable;

    private bool _isActivated = false;

    public bool IsActivated => _isActivated;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        checkpoint = GetComponent<Collider2D>();

        GameObject dpm = GameObject.FindGameObjectWithTag("DPM");
        if (dpm != null)
            dataPersistenceManager =
                dpm.GetComponent<DataPersistenceManager>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerDamagable = player.GetComponent<Damagable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        Activate();
    }

    private void Activate()
    {
        if (_isActivated)
            return;

        _isActivated = true;

        GameManager.lastCheckpointPos = this.transform.position;

        if (animator != null)
            animator.SetBool(AnimationStrings.isActivated, true);

        if (playerDamagable != null)
            playerDamagable.Health = playerDamagable.MaxHealth;

        if (dataPersistenceManager != null)
            dataPersistenceManager.SaveGame();
        else
            Debug.LogError(
                "Checkpoint: DataPersistenceManager not found!");

        if (checkpoint != null)
            checkpoint.enabled = false;

        Debug.Log("Checkpoint activated at " + transform.position);
    }
}