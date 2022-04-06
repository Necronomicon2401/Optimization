using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAIBenchmark : MonoBehaviour
{
    [SerializeField] private UpdateManager manager;
    Transform[] tanks;
    public int numberOfTanks;
    public GameObject tankPrefab;
    private Transform player;
    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        tanks = new Transform[numberOfTanks];
        manager.Initialize(numberOfTanks);
        for (int i = 0; i < numberOfTanks; i++)
        {
            tanks[i] = Instantiate(tankPrefab).transform;
            tanks[i].position = new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50));
            manager.Add(tanks[i].gameObject.GetComponent<TankAI>());
        }
    }
}
