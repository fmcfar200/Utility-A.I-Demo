﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour {


    protected NPCMovementScript npcMovementScript;

    int currentClip;
    int currentAmmo;

    int maxClip = 16;
    int maxAmmo = 4;

    float fireRate = 0.5f;
    float nextFire = 0.0f;


    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    public Text ammoText;
   


	// Use this for initialization
	void Start ()
    {
        npcMovementScript = this.gameObject.GetComponent<NPCMovementScript>();
        
        currentClip = maxClip;
        currentAmmo = maxAmmo;

        //TEMP
        GetComponent<Rigidbody>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (npcMovementScript.hostile != false)
        {
            AimAtPlayer();
            if (Time.time > nextFire && currentClip > 0)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }

            if (currentClip == 0 && currentAmmo > 0)
            {
                Reload();
            }

        }

        ammoText.text = "Enemy Ammo Count: " + currentClip.ToString() + "/" + currentAmmo.ToString();
        
	
	}

    void AimAtPlayer()
    {
        gameObject.transform.LookAt(npcMovementScript.player.transform.position);
    }

    void Shoot()
    {
       Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
       currentClip--;
    }

    void Reload()
    {
       if (currentClip < maxClip && maxAmmo > 0)
        {
            currentClip = maxClip;
            currentAmmo--;
        }
    }
}
