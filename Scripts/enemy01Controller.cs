﻿using UnityEngine;
using System.Collections;

public class enemy01Controller : MonoBehaviour {

    /* Version 0.1
     AI movement to player, damage to player, dash away
     Jordy
   */

    private GameObject Player;
    private float maxHealth = 20.0f;
    private float currentHealth;
    private float enemy01Speed = 5.0f;

    //
    private Rigidbody2D rbEnemy01;
    Vector2 direction = Vector2.zero;
    private bool playerCollisionCheck = false;

    //
    private float enemyKbForce = 300.0f;
    private float kbRange;


    void Start()
    {
        Player = GameObject.Find("Player");
        rbEnemy01 = GetComponent<Rigidbody2D>();
        kbRange = 1.0f;
        currentHealth = maxHealth;
    }

    void Update()
    {
        movementEnemy01();
        knockbackAtHit();
    }

    void movementEnemy01()
    {   if (playerCollisionCheck == false)
        {
            direction = Player.transform.position - transform.position;
            direction = direction.normalized;
            rbEnemy01.AddForce(direction * enemy01Speed);
        }
    }

    void knockbackAtHit()
    {
        if (playerCollisionCheck)
        {
            Debug.Log(playerCollisionCheck);
            if(transform.position.x >= Player.transform.position.x)
            {
                rbEnemy01.AddRelativeForce(new Vector2(kbRange, kbRange) * enemyKbForce);
                playerCollisionCheck = false;
            } else {
                rbEnemy01.AddRelativeForce(new Vector2(-kbRange, kbRange) * enemyKbForce);
                playerCollisionCheck = false;
            }
        }
    } //Voorlopig
   
    public void healthEnemy01(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0){
            //Deathanimation
            //AddPoints, alleen in prototype
            Debug.Log("You killed enemy01");
            Destroy(this.gameObject);   
        } else {

            return;
        }
    }

    void OnTriggerEnter2D(Collider2D playerCollision)
    {
        if(playerCollision.transform.tag == "Player")
        {
            playerCollisionCheck = true;
            //roep public void player op om damage te doen
            Player.gameObject.GetComponent<Player> ().health -= 1; // moet ik nu iets veranderen aan die versie? Heb deze regel erbij gevoegd. Kevin
            Debug.Log("Player verliest health");
        }
    }
}
