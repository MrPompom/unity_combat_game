using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public class PNJ
    {
        public int health;
        public int healthMax;
        public int domage;
    }
    public PNJ pnj;
    public void takeDomage(int domage)
    {
        pnj.health -= domage;

        if(pnj.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
