using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float scrollSpeed = 5;
    Vector2 startPos;
    public float lengthOfRepeat;
    public float timer;
    public GameObject sky;
    public GameObject cave;
    public GameObject deepCave;
    bool skyDone = false;
    bool caveDone = false;
    bool deepCaveDone = false;
    // Start is called before the first frame update
    void Start()
    {
        startPos = sky.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!skyDone)
        {
            float newPos = Mathf.Repeat(Time.time * scrollSpeed, lengthOfRepeat);
            sky.transform.position = startPos + Vector2.up * newPos;
           
        }
        
        if (timer >= 6)
        {
            skyDone = true;
            sky.SetActive(false);
            cave.SetActive(true);

        }
        if (!caveDone)
        {
            float newPos = Mathf.Repeat(Time.time * scrollSpeed, lengthOfRepeat);
            cave.transform.position = startPos + Vector2.up * newPos;
            
        }
        if (timer >= 12)
        {
            caveDone = true;
            cave.SetActive(false);
            deepCave.SetActive(true);
        }
        if (!deepCaveDone)
        {
            float newPos = Mathf.Repeat(Time.time * scrollSpeed, lengthOfRepeat);
            deepCave.transform.position = startPos + Vector2.up * newPos;

        }
        if (timer >= 18)
        {
            //deepCaveDone = true;
            //deepCave.SetActive(false);
        }
    }
}
