using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beakScript : MonoBehaviour
{
    public TestPlayerMovement p1script;
    public TestPlayerMovement p2script;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
           
            p1script.peckActive = true;

        }
        if (collision.gameObject.tag == "Player1")
        {
            p2script.peckActive = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            p1script.peckActive = false;

        }
        if (collision.gameObject.tag == "Player1")
        {
            p2script.peckActive = false;

        }
    }
}
