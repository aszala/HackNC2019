using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour {
    void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag == "Enemy") {
            GameObject enemy = collider.gameObject;

            enemy.GetComponent<Enemy>().damage();
        }
    }
}
