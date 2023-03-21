using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    private AIConponent aiComponent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        aiComponent = GetComponent<AIConponent>();

        Transform obstacleParent = FindObjectOfType<ObstacleParent>().transform;
        Vector3 lookatPosition = new Vector3(obstacleParent.position.x, transform.parent.position.y, obstacleParent.position.z);
        transform.parent.LookAt(lookatPosition);
        lookatPosition = new Vector3(obstacleParent.position.x, transform.position.y, obstacleParent.position.z);
        transform.LookAt(lookatPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (aiComponent == null)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                Crouch(true);
            }

            if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
            {
                Crouch(false);
            }
        }
    }

    public void Jump()
    {
        SetAnimatorTrigger(CharacterAnimatorKeys.Jump);
    }

    public void Crouch(bool sit, bool onlySetTrigger = false)
    {
        if (sit)
            SetAnimatorTrigger(CharacterAnimatorKeys.SitTrigger);
        if (!onlySetTrigger)
            SetAnimatorBool(CharacterAnimatorKeys.Sit, sit);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            rb.constraints = RigidbodyConstraints.None;

            Vector3 force = new Vector3(
                UnityEngine.Random.Range(50, 130),
                UnityEngine.Random.Range(50, 130),
                UnityEngine.Random.Range(50, 130));
            rb.AddForce(force);
            SetAnimatorTrigger(CharacterAnimatorKeys.Fall);
            if (aiComponent == null)
            {
                GameManager.instance.GameOver();
            }
        }
    }


    private void SetAnimatorTrigger(CharacterAnimatorKeys key)
    {
        animator.SetTrigger(key.ToString());
    }
    private void SetAnimatorBool(CharacterAnimatorKeys key, bool active)
    {
        animator.SetBool(key.ToString(), active);
    }

    enum CharacterAnimatorKeys
    {
        Idle,
        Sit,
        SitTrigger,
        Fall,
        Jump
    }
}
