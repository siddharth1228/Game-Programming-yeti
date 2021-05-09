using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PermanentUI : MonoBehaviour
{
    public int icecreams=0;
    public int health=5;
    public Text iceCreamText;
    public Text healthAmount;
    public bool isPower=false;
    public static PermanentUI perm;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if(!perm)
        {
            perm=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

}
