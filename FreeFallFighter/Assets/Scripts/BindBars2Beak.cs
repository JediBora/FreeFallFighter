using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindBars2Beak : MonoBehaviour
{

    public Vector3 OffSet;
    public GameObject Owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Owner.transform.position + OffSet;
    }
}
