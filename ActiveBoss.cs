using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBoss : MonoBehaviour
{
    [SerializeField]
    private GameObject [] objectsToActive;

    //Pop up all the icon when game end
    private void OnTriggerEnter(Collider player)
    {
        if (player.CompareTag( "Player"))
        {
            for (int i = 0; i < objectsToActive.Length; i++)
            {
                objectsToActive[i].SetActive(true);

            }
        }
        
    }
}
