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

    [SerializeField] protected Sprite sprite;

    public Sprite Sprite
    {
        get => this.sprite;
    }

    [SerializeField] protected Collider collider;

    [SerializeField] protected Rigidbody rb;

    [SerializeField] protected NavMeshAgent agent;

    [SerializeField] protected Color color = Color.red;

    protected bool hasTakeDamage = false;

    [SerializeField] protected float damageTime;

    [SerializeField] protected float damageAnimationTimer = 0f;

#endregion Variables

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.hasTakeDamage)
            this.UpdateDamageAnimation();
    }

    protected void TakeDamage(int damage)
    {
        this.health -= damage;
        this.hasTakeDamage = true;
        this.damageAnimationTimer = this.damageTime;
    }

    protected void UpdateDamageAnimation()
    {
        this.damageAnimationTimer -= Time.deltaTime;
        if (this.damageAnimationTimer < 0f)
        {
            this.hasTakeDamage = false;
        }
        else
        {

        }
    }
}
