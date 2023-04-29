using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Common Variables")]
    [SerializeField] protected EnemyObject enemy;
    [ReadOnly, SerializeField] protected float health;
    [ReadOnly, SerializeField] protected float damage;
    [Header("Cooldowns")]
    [ReadOnly, SerializeField] protected float attackCooldown;
    [SerializeField] private float minCooldown;
    [HideInInspector] public bool takenDamage;
    [HideInInspector] public float maxHealth;
    protected new Rigidbody rigidbody;
    protected Transform target;

    private Animator enemyAnim;
    private readonly int Explode = Animator.StringToHash("Explode");

    public UnityAction<float> HealthChange;
    private void OnHealthChange(float currentHealth) => HealthChange?.Invoke(currentHealth);

    protected virtual void Awake()
    {
        health = enemy.health;
        maxHealth = enemy.maxHealth;
        damage = enemy.damage;
        attackCooldown = enemy.attackCooldown ;

        OnHealthChange(health);

        target = GameObject.FindWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody>();
        enemyAnim = gameObject.GetComponent<Animator>();
        enemyAnim.runtimeAnimatorController = enemy.animController;
    }

    private void ReceivedDamage(float amountToLose)
    {
        health -= amountToLose;
        takenDamage = health < maxHealth;
        OnHealthChange(health);

        if (health > 0) return;

        StartCoroutine(TimerToDestroy());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(col.gameObject);
            // ReceivedDamage(PlayerManager.Instance.Damage);
        }
    }

    protected void CheckAttackCooldown()
    {
        if (inCooldown)
            attackCooldown -= Time.deltaTime;
    }

    private IEnumerator TimerToDestroy()
    {
        GetComponent<BoxCollider>().enabled = false;
        enemyAnim.SetTrigger(Explode);

        yield return new WaitForSeconds(0.12f);
        AudioSystem.Instance.PlaySfx("explode");
        yield return new WaitForSeconds(enemyAnim.GetCurrentAnimatorStateInfo(0).length +
                                        enemyAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        Destroy(gameObject);
    }

    #region getters
    protected bool canDetectPlayer => Vector2.Distance(transform.position, target.position) <= enemy.detectRange;
    protected void StopEnemy() => rigidbody.velocity = Vector3.zero;
    protected bool closerToPlayer => target && Vector3.Distance(transform.position, target.position) <= enemy.attackRange;
    protected bool inCooldown => attackCooldown > 0;
    protected bool hasCollision => GetComponent<BoxCollider>().enabled;
    #endregion
}