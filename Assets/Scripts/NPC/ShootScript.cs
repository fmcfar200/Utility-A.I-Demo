﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour {


    protected NPCMovementScript npcMovementScript;
    UtilityAIScript utilityScript;
    public int currentClip;
    int currentAmmo;

    int maxClip = 16;
    int maxAmmo = 4;

    float fireRate = 0.5f;
    float nextFire = 0.0f;

    float bulletForce = 100.0f;


    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    GameObject firedBullet;
    public Text ammoText;

    public float attackTimer;
    
   


	// Use this for initialization
	void Start ()
    {
        npcMovementScript = this.gameObject.GetComponent<NPCMovementScript>();
        utilityScript = this.gameObject.GetComponent<UtilityAIScript>();
        currentClip = maxClip;
        currentAmmo = maxAmmo;

        utilityScript.cooldownTime = 4.0f;

        //TEMP
        GetComponent<Rigidbody>().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(attackTimer.ToString());
        if (npcMovementScript.hostile != false)
        {
            if (attackTimer < 5.0f)
            {
                Attack();
            }
            else
            {
                utilityScript.coolingDown = true;

            }
        }

        ammoText.text = "Enemy Ammo Count: " + currentClip.ToString() + "/" + currentAmmo.ToString();
        
	
	}

    void FixedUpdate()
    {
        if (firedBullet != null)
        {
            firedBullet.GetComponent<BulletScript>().ApplyForce(this.gameObject, bulletForce);
        }
    }

    void AimAtPlayer()
    {
        gameObject.transform.LookAt(npcMovementScript.player.transform.position);
    }

    void Shoot()
    {
        firedBullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity) as GameObject;
        firedBullet.name = "Bullet";
       currentClip--;
    }

    void Attack()
    {
        attackTimer += Time.deltaTime;
        
        AimAtPlayer();
        npcMovementScript.MoveTowards();
        if (Time.time > nextFire && currentClip > 0)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
          
    }

    public IEnumerator Reload()
    {
        yield return new WaitForSeconds(2.0f);
        if (currentAmmo > 0)
        {
            if (currentClip < maxClip && maxAmmo > 0)
            {
                currentClip = maxClip;
                currentAmmo--;
            }
        }
    }

   
}
