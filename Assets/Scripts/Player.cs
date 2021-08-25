using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private EnemySpawner enemySpawner;

    public GameObject spear;

    private bool stopUpdate = false, pickedUpSpear = false, bigBadIsAlive = false;

    public GameObject bigBadGuyPrefab;

    void Start() {
        enemySpawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
    }

    void Update() {
        if (!stopUpdate && enemySpawner.stage == 0 && enemySpawner.enemyParent.transform.childCount == 0) {
            transform.LookAt(spear.transform);
            //transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

            float distance = Vector3.Distance(transform.position, spear.transform.position);

            if (distance > 8f) {
                Vector3 delta = transform.forward * 13.0f * Time.deltaTime;
                delta.y = 0.0f;

                transform.position += delta;
            } else {
                StartCoroutine(PickipSpear());
                stopUpdate = true;
            }
        } else if (!bigBadIsAlive && !stopUpdate && enemySpawner.stage == 1 && enemySpawner.enemyParent.transform.childCount == 0) {
            GameObject bigBadGuy = Instantiate(bigBadGuyPrefab);

            bigBadGuy.AddComponent<BigBadGuy>();
            bigBadGuy.transform.position = GameObject.Find("Boss Spawn").transform.position;

            bigBadIsAlive = true;
        }

        if (pickedUpSpear) {
            spear.transform.localPosition = new Vector3(-0.07f, -0.006f, -0.018f);
            spear.transform.localRotation = Quaternion.Euler(0.449f, 0f, -90f);

            Vector3 direction = (GameObject.Find("SpawnPoint 2").transform.position - transform.position);

            if (direction.x < 1.0f && direction.y < 1.0f && direction.z < 1.0f) {
                transform.LookAt(GameObject.Find("SpawnPoint 2").transform);
            }

            var rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6.0f);
        }
    }

    IEnumerator PickipSpear() {
        yield return new WaitForSeconds(1.4f);
        spear.transform.parent = GameObject.Find("RigidRoundHand_L").transform.GetChild(5);

        pickedUpSpear = true;

        enemySpawner.stage++;
        enemySpawner.spawnedEnemies = 0;

        yield return new WaitForSeconds(2.0f);

        stopUpdate = false;
    }

}
