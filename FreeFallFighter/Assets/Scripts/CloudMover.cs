using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveUp();
        if (transform.position.y > 7)
        {
            ReStart();
        }
        
    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * Speed);
    }

    

    void ReStart()
    {
        transform.position = new Vector3(Random.Range(-8, 8),-8,0);
    }
}
