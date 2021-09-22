using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Health healing = other.GetComponent<Health>();
            healing.Heals(30);
            Destroy(gameObject);
            //effect
        }
    }
}
