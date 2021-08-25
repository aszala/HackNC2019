using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BigBadGuy : MonoBehaviour {

    private Animator animator;
    private GameObject player;

    private bool attackOne = false, attackTwo = false;

    void Start() {
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update() {
        transform.LookAt(player.transform);
        transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > 11) {
            Vector3 delta = transform.forward * 7.0f * Time.deltaTime;
            delta.y = 0.0f;

            transform.position += delta;

            SetTrigger("Walk");
        } else {
            if (!attackOne)
                SetTrigger("Attack 1");
            else if (!attackTwo)
                SetTrigger("Attack 2");
        }
    }

    void SetTrigger(string name) {
        try {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
                animator.ResetTrigger(parameter.name);

            animator.SetTrigger(name);
        } catch { }
    }

    void BlockOne() {
        if (GameObject.Find("Sword").transform.rotation.y >= .70 && GameObject.Find("Sword").transform.rotation.y <= 1) {
            attackOne = true;
            SetTrigger("Attack 2");
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void BlockTwo() {
        if (GameObject.Find("Sword").transform.rotation.y >= .70 && GameObject.Find("Sword").transform.rotation.y <= 1) {
            attackTwo = true;
            SetTrigger("Die");
        }
    }

}
