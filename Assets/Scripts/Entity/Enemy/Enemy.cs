using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] protected Vector3 target;

    [SerializeField] float timerWander;
    [SerializeField] float rangeWanger = 10;
    [SerializeField] float rangeDetection = 5;
    [SerializeField] float angleDetection = 60;

    bool playerInRange = false;

    //Drop item

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!this.hasTakeDamage)
        {
            DetectionPlayer();
            if (this.playerInRange)
            {
                this.agent.SetDestination(this.target);
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

    void OnDestroy()
    {
        //Instantiate Drop Item
    }

    void MoveToTarget()
    {
        this.agent.nextPosition = this.target;
    }

    void DetectionPlayer()
    {
        this.playerInRange = false;
        if (Vector3.Distance(this.transform.position, this.target) < this.rangeDetection) // a portée du joueur
        {
            Vector3 directionToPlayer = this.target - this.transform.position;
            if (Vector3.Angle(this.transform.forward, directionToPlayer) < this.angleDetection) // dans son champ de vision
            {
                RaycastHit hit;
                if (Physics.Raycast(this.transform.position, directionToPlayer, out hit, this.rangeDetection))
                {
                    if (hit.transform.position == this.target)
                    {
                        this.playerInRange = true;
                    }
                }
            }
        }
    }

    Vector3 GetRandomPosition()
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
}
