using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    #region Variables

    [SerializeField] protected Transform target;

    [SerializeField] float timerWander;
    [SerializeField] float rangeWanger = 10;
    [SerializeField] float rangeDetection = 5;
    [SerializeField] float angleDetection = 60;

    bool playerInRange = false;

    //Drop item

    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        this.target = FindAnyObjectByType<Entity>().transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!this.isDead)
        {
            if (!this.hasTakeDamage)
            {
                DetectionPlayer();
                if (this.playerInRange)
                {
                    this.agent.SetDestination(this.target.position);
                }
                else
                {
                    this.timerWander -= Time.deltaTime;
                    if (this.timerWander <= 0)
                    {
                        this.timerWander = 10.0f;
                        this.agent.SetDestination(GetRandomPosition());
                    }
                }
            }

            base.Update();
        }
    }

    protected override void OnDestroy()
    {
        //Instantiate Drop Item
    }


    virtual protected void DetectionPlayer()
    {
        this.playerInRange = false;
        if (Vector3.Distance(this.transform.position, this.target.position) < this.rangeDetection) // a portée du joueur
        {
            Vector3 directionToPlayer = this.target.position - this.transform.position;
            if (Vector3.Angle(this.transform.forward, directionToPlayer) < this.angleDetection) // dans son champ de vision
            {
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, directionToPlayer, out hit, this.rangeDetection))
                {
                    if (hit.transform.position == this.target.position)
                    {
                        this.playerInRange = true;
                    }
                }
            }
        }
    }

    virtual protected Vector3 GetRandomPosition()
    {
        int count = 10;

        Vector3 desiredPosition = Vector3.zero;
        do
        {
            Vector2 dir = Random.insideUnitCircle;
            Vector3 offset = new Vector3(dir.x, 0, dir.y) * this.rangeWanger;
            desiredPosition = this.transform.position + offset;
        }
        while (count-- > 0 && !NavMesh.SamplePosition(desiredPosition, out NavMeshHit hit, 1.0f, NavMesh.AllAreas));


        return desiredPosition;
    }
    virtual protected private void OnDrawGizmos()
    {
        if (playerInRange)
        {
            Gizmos.color = new Color(1, 0, 0, 1f);
        }
        else
        {
            Gizmos.color = new Color(0, 1, 0, 1f);
        }
        Gizmos.DrawWireSphere(transform.position, rangeDetection);

        Quaternion rotationLeft = Quaternion.Euler(0, -angleDetection, 0);
        Vector3 dirLeft = rotationLeft * transform.forward;
        Quaternion rotationRight = Quaternion.Euler(0, angleDetection, 0);
        Vector3 dirRight = rotationRight * transform.forward;

        Gizmos.DrawLine(transform.position, transform.position + dirLeft * rangeDetection);
        Gizmos.DrawLine(transform.position, transform.position + dirRight * rangeDetection);
    }
}
