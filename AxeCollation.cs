using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollation : MonoBehaviour
{
    public float Dmg = 25f;
    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag( "Player"))
        {
            Health player_hp = player.GetComponent<Health>();
            player_hp.Damage(Dmg);
        }
    }

}
