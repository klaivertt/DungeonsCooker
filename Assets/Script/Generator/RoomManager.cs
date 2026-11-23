using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    Roomanizer[] m_roomanizer;
    int m_difficulty;
    protected override void Awake() 
    {
        base.Awake();
        Generate();

    }

    void Generate()
    {
        m_roomanizer = FindObjectsByType<Roomanizer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
