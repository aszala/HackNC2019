using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour {

    public GameObject lookAt;

    private Animator animator;

    void Start() {
        transform.LookAt(lookAt.transform);
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (Random.Range(0f, 1f) > 0.5f)
            SetTrigger("Sitting");
        else
            SetTrigger("Cheering");
    }
    void SetTrigger(string name) {
        try {
            foreach (AnimatorControllerParameter parameter in animator.parameters)
                animator.SetBool(parameter.name, false);

            animator.SetBool(name, true);
        } catch { }
    }
}
