using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    Vector3 velocity;
    float sides = 30.0f;
    float speedMax = 0.3f;
    private static readonly int Color1 = Shader.PropertyToID("_Color");
    private Transform _transform;
    private Vector3 _pos;

    void Start()
    {
        _transform = transform;
        velocity = new Vector3(Random.Range(0.0f, speedMax),
                        Random.Range(0.0f, speedMax),
                        Random.Range(0.0f, speedMax));

    }

    Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        _transform.Translate(velocity);


        _pos = _transform.position;
        var posX = _pos.x;
        var posY = _pos.y;
        var posZ = _pos.z;

        if (posX > sides)
        {
            velocity.x = -velocity.x;
        }
        if (posX < -sides)
        {
            velocity.x = -velocity.x;
        }
        if (posY > sides)
        {
            velocity.y = -velocity.y;
        }
        if (posY < -sides)
        {
            velocity.y = -velocity.y;
        }
        if (posZ > sides)
        {
            velocity.z = -velocity.z;
        }
        if (posZ < -sides)
        {
            velocity.z = -velocity.z;
        }

        this.GetComponent<Renderer>().material.SetColor(Color1, new Color(posX/sides,
            posY/sides,
            posZ/sides));

    }
}
