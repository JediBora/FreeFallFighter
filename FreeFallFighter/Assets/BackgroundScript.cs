using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{

 
   

    public float cloudScrollSpeed = 5;
    public float caveScrollSpeed = 5;
    Vector2 startPosClouds;
   // Vector2 startPosCave;
    public float cloudLengthOfRepeat;
   // public float caveLengthOfRepeat;
    public float timer;
    public GameObject sky;
    public GameObject clouds;
    public GameObject cave;


    bool skyDone = false;
    bool caveDone = false;

    public ButtonMasher script;

    // Start is called before the first frame update
    void Start()
    {
        startPosClouds = clouds.transform.position;
        //startPosCave = cave.transform.position;
      

    }

    // Update is called once per frame
    void Update()
    {



        // timer += Time.deltaTime;
        if (!skyDone)
        {
            float newPos = Mathf.Repeat(Time.time * cloudScrollSpeed, cloudLengthOfRepeat);
            clouds.transform.position = startPosClouds + Vector2.up * newPos;

        }

        if (script.player01wins || script.player02wins)
        {
            skyDone = true;
            caveDone = true;
            
            cave.SetActive(true);
        }

        if (caveDone)
        {
            StartCoroutine("DelayClouds");
            cave.transform.Translate(Vector3.up * caveScrollSpeed);
        }

    }
    IEnumerator DelayClouds()
    {
        yield return new WaitForSeconds(2);
        
        sky.SetActive(false);
        clouds.SetActive(false);

    }

}
