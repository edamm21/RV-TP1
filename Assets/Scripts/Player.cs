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
    private int zombieCount = 0;
    public GameObject zombie;

    private void Start() {
        Time.timeScale = 1f;
    }
    
    private void Update() {
        if (zombieCount < 20) {
            CreateZombie();
        }
        
        if (hasLost) {
            gameOverText.text = "Game over! Press R to restart or Q to quit.";
            Time.timeScale = 0.001f;
        }

        if (hasLost && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(CURRENT_SCENE_INDEX);
        }/* else if (hasLost && Input.GetKeyDown(KeyCode.Q)) {
            QuitGame();
        }*/

        if (!hasLost && Input.GetKeyDown(KeyCode.Space)) {
            FireBullet();
        }
    }
    
    private void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.name != "Bullet(Clone)") {
            hasLost = true;
        }
    }

    /*private void QuitGame() {
        if (Debug.isDebugBuild) {
            UnityEditor.EditorApplication.isPlaying = false;    
        }
        else {
            Application.Quit();
        }
    }*/

    private void FireBullet() {
        Rigidbody bulletClone = (Rigidbody)Instantiate(bullet, transform.position, transform.rotation);
        bulletClone.velocity = transform.forward * bulletSpeed;
    }

    private void CreateZombie() {
        // x: [-14, 14], z: [0, 10], y:0
        Random x = new Random();
        int xPosition = x.Next(-14, 14);
        Random z = new Random();
        int zPosition = z.Next(0, 10);
        Vector3 randomZombiePosition = new Vector3(xPosition, 0, zPosition);
        GameObject zombieClone = (GameObject)Instantiate(zombie, randomZombiePosition, new Quaternion(0, -180, 0, 0));
        zombieCount++;
    }
}
