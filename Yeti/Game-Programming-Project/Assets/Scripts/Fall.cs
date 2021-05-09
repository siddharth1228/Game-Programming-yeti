using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
       if(collision.gameObject.tag=="Player")
       {
           HandleHealth();
           SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       }
   }
   private void HandleHealth()
    {
        PermanentUI.perm.health-=1;
        PermanentUI.perm.healthAmount.text=PermanentUI.perm.health.ToString();
        if(PermanentUI.perm.health<=0)
        {
            PermanentUI.perm.health=5;
            PermanentUI.perm.healthAmount.text=PermanentUI.perm.health.ToString();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
