using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameJuice : MonoBehaviour
{
    public GameObject player01bar;
    public GameObject player02bar;
    public Vector3 player01Amount;
    public Vector3 player02Amount;
    public Vector3 player01InitialAmount;
    public Vector3 player02InitialAmount;
    public bool itScaledPlayer01 = false;
    public bool itScaledPlayer02 = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShakingBar();
    }

    void ShakingBar()
    {

        if (itScaledPlayer01)
        {
            iTween.ScaleUpdate(player01bar, player01Amount, 2);
        }
        else
        {
            iTween.ScaleUpdate(player01bar, player01InitialAmount, 2);
        }

        if (itScaledPlayer02)
        {
            iTween.ScaleUpdate(player02bar, player01Amount, 2);
        }
        else
        {
            iTween.ScaleUpdate(player02bar, player02InitialAmount, 2);
        }

        //StartCoroutine(WaitAndScale());
        //itScaled = true;




    }
    //private IEnumerator WaitAndScale()
    //{
    //    if (itScaled)
    //    {
    //        yield return new WaitForSeconds(1);
    //        iTween.ScaleUpdate(player01bar, player02amount, 2);
    //        itScaled = false;
    //    }
    //}
}
