using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    #region State Machine Variables
    EnemyBaseState currentState;

    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyHurtState HurtState = new EnemyHurtState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyAttackState AttackState = new EnemyAttackState();
    #endregion

    [SerializeField] EnemyDataSO data;

    int damage;
    float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } }

    GameObject player;
    PlayerController playercontroller;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); playercontroller = player.GetComponent<PlayerController>();
        maxHealth = data.maxHealth; curHealth = maxHealth;
        damage = data.damage;
        moveSpeed = data.moveSpeed;
    }

    private void Start() { ChangeState(ChaseState); }

    private void Update() { currentState.UpdateState(this); }

    private void LateUpdate() { currentState.LateUpdateState(this); }

    private void OnCollisionEnter(Collision collision) { currentState.OnCollisionEnter(this); }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerWeapon")) TakeDamage(1);
        if (collision.CompareTag("Player")) playercontroller.TakeDamage(damage);
    }

    public void ChangeState(EnemyBaseState newState)
    {
        currentState?.ExitState(this); 
        currentState = newState; 
        currentState.EnterState(this);
    }

    #region Health & TakeDamage()
    [field: SerializeField] public int maxHealth { get; set; }
    [field: SerializeField] public int curHealth { get; set; }

    public void TakeDamage(int damageAmount)
    {
        //ChangeState(HurtState);
        curHealth -= damageAmount;
        if (curHealth <= 0) { Destroy(gameObject); }
    }
    #endregion
}