using UnityEngine;
using UnityEngine.AI;

public class Entity : MonoBehaviour
{
    #region Variables
    protected int health;

    public int Health
    {
        get => this.health;
    }

    protected int damage;

    public int Damage
    {
        get => this.damage;
    }

    protected float speed;

    public float Speed
    {
        get => this.speed;
    }

    public Vector3 Position
    {
        get => this.transform.position;
    }

    [SerializeField] protected SpriteRenderer sprite;

    public SpriteRenderer Sprite
    {
        get => this.sprite;
    }

    [SerializeField] protected Collider collider;

    [SerializeField] protected Rigidbody rb;

    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected bool hasTakeDamage = false;

    [SerializeField] protected float damageTime;

    [SerializeField] protected float timer = 0f;

    [SerializeField] protected bool isDead;

    [SerializeField] protected float deathTime;

    public bool IsDead
    {
        get => this.isDead;
    }

    #endregion Variables

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    virtual protected void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<Collider>();
        this.rb = GetComponent<Rigidbody>();
        this.agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (this.isDead)
        {
            this.UpdateDeathAnimation();
        }
        else if (this.hasTakeDamage)
        {
            this.UpdateDamageAnimation();
        }
    }

    virtual protected void OnDestroy()
    {

    }

    virtual protected void TakeDamage(int damage)
    {
        this.health -= damage;
        this.hasTakeDamage = true;
        this.timer = this.damageTime;
        this.sprite.color = Color.red;
    }

    virtual protected void UpdateDamageAnimation()
    {
        this.timer -= Time.deltaTime;
        if (this.timer < 0f)
        {
            this.hasTakeDamage = false;
            this.sprite.color = Color.white;
        }
        else
        {
            float factor = Mathf.Sin(this.timer * 10);
            this.sprite.color = new Color(1, 1 * factor, 1 * factor);
        }
    }

    virtual protected void SetDeath()
    {
        this.isDead = true;
        this.agent.enabled = false;
        this.collider.enabled = false;
        this.rb.isKinematic = true;
    }

    virtual protected void UpdateDeathAnimation()
    {
        this.timer += Time.deltaTime;
        if (this.timer < this.deathTime)
        {
            float angle = Mathf.Lerp(0, 90, this.timer / this.deathTime);
            this.transform.rotation = Quaternion.Euler(angle, 0f, 0f);
        }
    }
}
