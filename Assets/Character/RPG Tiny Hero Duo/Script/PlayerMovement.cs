using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    // 달리는 속도.
    [SerializeField] private float runSpeed = 5f;

    // 회전하는 속도.
    [SerializeField] private float rotateSpeed = 5f;

    // 틱당 회전하는 최대 각도. 
    [SerializeField] private float rotateAnglePerTick = 45f;
    private Rigidbody characterRigidbody;
    private Animator anim;
    private Quaternion movementAngle;
    private Vector3 movement;

    // Start is called before the first frame update
    // Update is called once per frame
    private void Start() {
        anim = GetComponent<Animator>();
        characterRigidbody = GetComponent<Rigidbody>();
    }

    private void TurnCharacter(){

        characterRigidbody.rotation = Quaternion.Slerp(
            characterRigidbody.rotation,
            movementAngle,
            rotateSpeed * Time.deltaTime
        );
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            movementAngle,
            rotateSpeed * Time.deltaTime
        );
    }

    void Update()
    {
        float inputZ = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");

        movement = transform.forward;
        movement.y = 0;
        movementAngle = transform.rotation * Quaternion.Euler(new Vector3(0, inputX*rotateAnglePerTick, 0));

        movement = movement.normalized * (inputZ > 0 ? runSpeed : runSpeed * 0.7f) * inputZ;
        Debug.Log(movement);

        characterRigidbody.velocity = movement;

        if (inputX != 0){
            TurnCharacter();
        }
        anim.SetBool("isForward", inputZ > 0);
        anim.SetBool("isBackward", inputZ < 0);
    }
}
