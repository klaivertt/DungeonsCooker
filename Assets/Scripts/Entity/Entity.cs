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

    [SerializeField] protected float damageAnimationTimer = 0f;

#endregion Variables

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.sprite = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<Collider>();
        this.rb = GetComponent<Rigidbody>();
        this.agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (this.hasTakeDamage)
            this.UpdateDamageAnimation();
    }

    protected void TakeDamage(int damage)
    {
        this.health -= damage;
        this.hasTakeDamage = true;
        this.damageAnimationTimer = this.damageTime;
        this.sprite.color = Color.red;
    }

    protected void UpdateDamageAnimation()
    {
        this.damageAnimationTimer -= Time.deltaTime;
        if (this.damageAnimationTimer < 0f)
        {
            this.hasTakeDamage = false;
            this.sprite.color = Color.white;
        }
        else
        {
            float factor = Mathf.Sin(this.damageAnimationTimer * 10);
            this.sprite.color = new Color(1, 1 * factor, 1 * factor);
            //this.sprite.color = Color.red;
        }
    }
}
