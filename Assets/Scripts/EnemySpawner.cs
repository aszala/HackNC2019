using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private List<GameObject> spawnPoints = new List<GameObject>();

    public GameObject enemyParent;

    public GameObject cloner;

    private float timer = 3.5f;

    public int spawnedEnemies = 0;
    public int stage = 0;

    void Start() {
        GameObject locations = GameObject.Find("SpawnPoints");
        enemyParent = GameObject.Find("Enemies");

        for (int i=0;i<locations.transform.childCount;i++) {
            spawnPoints.Add(locations.transform.GetChild(i).gameObject);
        }

    }

    void Update() {
        timer -= Time.deltaTime;

        if (timer <= 0 && spawnedEnemies < 2) {
            if (Random.Range(0f, 1f) > 0.5f) {
                GameObject newEnemy = Instantiate(cloner);
                newEnemy.transform.position = spawnPoints[stage].transform.position;

                newEnemy.transform.parent = enemyParent.transform;
            } else {
                GameObject newEnemy2 = Instantiate(cloner);
                newEnemy2.transform.position = spawnPoints[stage+1].transform.position;

                newEnemy2.transform.parent = enemyParent.transform;
            }

            timer = 3.5f;

            spawnedEnemies++;
        }
    }

}