using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chasing : MonoBehaviour {

    public Camera camera;
    public GameObject thisObject;
    public float zombieVelocity;
    public GameObject playerObject;
    public Text zombiesKilledText;

    private void Start() {
        camera = GameObject.FindObjectOfType<Camera>();
        playerObject = GameObject.Find("Player");
        zombiesKilledText = Text.FindObjectOfType<Text>();
    }
    
    private void Update() {
        bool playerHasLost = playerObject.GetComponent<Player>().hasLost;
        // Only update the object's position if player hasn't died already
        if (!playerHasLost) {
            Vector3 newPosition = Vector3.MoveTowards(thisObject.transform.position, camera.transform.position, zombieVelocity); 
            thisObject.transform.position = new Vector3(newPosition.x, thisObject.transform.position.y, newPosition.z);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        // zombie was hit by bullet
        if (collision.collider.name == "Bullet(Clone)") {
            Player player = playerObject.GetComponent<Player>();
            player.zombiesKilled++;
            zombiesKilledText.text = "Zombies killed:" + player.zombiesKilled;
            Destroy(thisObject); // remove zombie from game
            Destroy(collision.gameObject); // remove the colliding bullet as well
        }
    }
}
