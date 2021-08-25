using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour {

    private Animator animator;
    private GameObject player;

    private Image damageIndicator;

    private int health = 2;
    private static int enemiesDefeated = 0, timesHit = 0;

    void Start() {
        animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("MainCamera");
        damageIndicator = GameObject.Find("DamgeIndicator").GetComponent<Image>();
    }

    void Update() {
        transform.LookAt(player.transform);
        transform.rotation = Quaternion.Euler(0.0f, transform.rotation.eulerAngles.y, 0.0f);

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > 11 && health > 0) {
            Vector3 delta = transform.forward * 7.0f * Time.deltaTime;
            delta.y = 0.0f;

            transform.position += delta;

            SetTrigger("Walk");
        } else if (health > 0) {
            SetTrigger("Attack");
        } else {
            SetTrigger("Die");
        }

        GameObject.Find("Times Hit").GetComponent<Text>().text = "Times Hit: " + timesHit;
        GameObject.Find("EnemiesDefeated").GetComponent<Text>().text = "Enemies Defeated: " + enemiesDefeated;

        if (damageIndicator.color.a > 0.009f)
            damageIndicator.color = new Color(damageIndicator.color.r, damageIndicator.color.g, damageIndicator.color.b, damageIndicator.color.a - 0.008f);
    }

    void HitPlayer() {
        timesHit++;
        if (damageIndicator.color.a < 0.6f)
            damageIndicator.color = new Color(damageIndicator.color.r, damageIndicator.color.g, damageIndicator.color.b, damageIndicator.color.a + 0.1f);
    }

    void SetTrigger(string name) {
        try {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
                animator.ResetTrigger(parameter.name);

            animator.SetTrigger(name);
        } catch { }
    }

    public void damage() {
        health -= 1;

        Vector3 delta = -transform.forward * 4.0f;
        delta.y = 0.0f;

        transform.position += delta;

        if (health <= 0) {
            SetTrigger("Die");
            enemiesDefeated++;
            StartCoroutine(die());
        }
    }

    IEnumerator die() {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
