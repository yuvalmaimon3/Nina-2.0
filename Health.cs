using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Vector3 spawnLocation;
    public Transform spawnEffect;
    public Image healthBar;
    public bool isAlive = true;
    public float myHealth = 100f;
    public AudioClip deadClip;
    public AudioClip spwanClip;

    public bool isBlock = false;
    private Animator playerAnimator;
    private AudioSource playerAudioSour;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerAudioSour = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = myHealth / 100f;
    }

    public void Damage(float dmg)
    {
        if (!isBlock)
        {
            myHealth -= dmg;

            if (myHealth <= 0)
            {
                isAlive = false;
                playerAnimator.SetTrigger("Dead");
                playerAudioSour.clip = deadClip;
                playerAudioSour.Play();
                StartCoroutine(Spawn());
            }
        }
    }
    public void Heals(float amount)
    {
        myHealth += amount;
        if (myHealth > 100)
            myHealth = 100;
    }

    public IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        transform.parent.position = spawnLocation;
        playerAudioSour.clip = spwanClip;
        playerAudioSour.Play();
        Instantiate(spawnEffect, transform.parent.position, Quaternion.identity);
        myHealth = 100f;
        isAlive = true;
        playerAnimator.SetTrigger("Idle");
    }
}
