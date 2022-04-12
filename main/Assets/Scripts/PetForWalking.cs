using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetForWalking : MonoBehaviour
{
    public float speed = 1.0f;

    private Animator animator;
    private Vector3 movePosition;
    private bool isMove = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        animator.SetInteger("animation", 1);
        isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMove)
        {
            // Pet Moves TapPosition
            transform.position = Vector3.MoveTowards(transform.position, movePosition, Time.deltaTime * speed);
            transform.LookAt(movePosition);

            animator.SetInteger("animation", 21);
            if (transform.position == movePosition)
            {
                animator.SetInteger("animation", 1);
                isMove = false;
            }
        }
    }


    //Get TapPosition & Move Pet
    public void movePet(Vector3 move)
    {
        movePosition = move;
        isMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Food")
        {
            other.gameObject.SetActive(false);
            WalkingManager.food++;
        }
    }
}
