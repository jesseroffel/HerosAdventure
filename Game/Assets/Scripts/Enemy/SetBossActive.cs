using UnityEngine;
using System.Collections;

public class SetBossActive : MonoBehaviour {
    public Boss Bossscript;
    public GameObject Block;
    private bool Active = true;

    void OnTriggerEnter(Collider collision)
    {
        if (Bossscript && Active)
        {
            if (collision.isTrigger)
            {
                if (collision.gameObject.tag == "Player")
                {
                    Bossscript.SetBossActive();
                    Block.SetActive(true);
                    Active = false;
                }
            }
        }
    }
}
