using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIConponent : MonoBehaviour
{
    private GameObject detectObject;
    private BoxCollider detectionCollider;

    private Character character;
    private Vector3 triggerBoxRotationAngles = new Vector3(0, 65, 0);
    private Vector3 triggerBoxPosition = new Vector3(0, 0.5f, 0);
    private Vector3 triggerBoxSize = new Vector3(2, 3, 7);

    //for not to be perfect
    public float faultPercent = 0;
    private void Awake()
    {
        character = GetComponent<Character>();
        detectObject = new GameObject();
        detectObject.transform.parent = transform;
        detectObject.transform.localEulerAngles = triggerBoxRotationAngles;
        detectObject.transform.localPosition = triggerBoxPosition;
        detectionCollider = detectObject.AddComponent<BoxCollider>();
        detectionCollider.isTrigger = true;
        detectionCollider.size = triggerBoxSize;
        detectionCollider.center = GetComponent<CapsuleCollider>().center;
        detectObject.layer = LayerMask.NameToLayer("CharacterBody");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (other.transform.position.y > detectObject.transform.position.y)
            {
                character.Crouch(true, true);
            }
            else
            {
                character.Jump();
            }



            //changing the amount of obstacle detection of box collider in range of a random number within the fault percent
            // the change value can be positive or negative
            float randomPercentValue = Random.Range(-faultPercent, faultPercent) * triggerBoxSize.z / 100;
            triggerBoxSize.z += randomPercentValue;

            detectionCollider.size = triggerBoxSize;
        }
    }
}
