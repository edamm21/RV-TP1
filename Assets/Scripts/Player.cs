using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Player : MonoBehaviour {

    public GameObject player;
    public bool hasLost = false;
    public Text gameOverText;
    private static int CURRENT_SCENE_INDEX = 0;
    public int zombiesKilled = 0;
    public Rigidbody bullet;
    public float bulletSpeed = 10f;
    public int zombieCount = 0;
    public GameObject zombie;
	private Camera camera;
	private float cooldown;
	private int zombieMaxCount = 10; // initial value
    private float restartCooldown = 0.0f;
    private bool canRestart = false;

    private void Start() {
		camera = GameObject.FindObjectOfType<Camera>(); 
        Time.timeScale = 1f;
		InvokeRepeating("IncreaseZombieMaxCount", 0f, 10f);
    }
    
    private void Update() {
        if (zombieCount < zombieMaxCount) {
            CreateZombie();
        }
        
        if(cooldown > 0.0f) {
			cooldown -= Time.deltaTime;
			if(cooldown < 0.0f) {
				cooldown = 0.0f;
			}
		}

        if(hasLost && restartCooldown > 0.0f) {
			restartCooldown -= Time.deltaTime * 3600;
			if(restartCooldown < 0.0f) {
				restartCooldown = 0.0f;
			}
		}

        if (hasLost && !canRestart) {
            gameOverText.text = "Game over! Press any key to restart";
            Time.timeScale = 0.001f;
            restartCooldown = 2f;
            canRestart = true;
        }

        if (hasLost && Input.anyKey && restartCooldown == 0.0f && canRestart) {
            SceneManager.LoadScene(CURRENT_SCENE_INDEX);
        }

        if (!hasLost && Input.anyKey && cooldown == 0.0f) {
			cooldown = 0.8f;
            FireBullet();
        }
    }
    
    private void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.name != "Bullet(Clone)") {
            hasLost = true;
        }
    }

    private void FireBullet() {
        Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, camera.transform.position, camera.transform.rotation);
        bulletClone.velocity = camera.transform.forward * bulletSpeed;
    }

    private void CreateZombie() {
        Random x = new Random();
        int xPosition = x.Next(-14, 14);
        Random z = new Random();
        int zPosition = z.Next(0, 8);
        Vector3 randomZombiePosition = new Vector3(xPosition, 0, zPosition);
        GameObject zombieClone = (GameObject)Instantiate(zombie, randomZombiePosition, new Quaternion(0, -180, 0, 0));
        zombieCount++;
    }

	private void IncreaseZombieMaxCount() {
		zombieMaxCount++;
	}
}
