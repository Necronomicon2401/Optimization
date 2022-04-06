using UnityEngine;

public class TankAI : MonoBehaviour, IUpdatable
{
    private Transform tankTransform;
    private Transform player;
    private Vector3 move;

    // Start is called before the first frame update
    void Start()
    {
        move = new Vector3(0, 0, 0.05f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        tankTransform = transform;
    }

    public void Tick()
    {
        if (player == null) return;
        var pos = player.transform.position;
        tankTransform.LookAt(pos);
        tankTransform.Translate(move);
    }
}