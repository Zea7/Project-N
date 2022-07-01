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

    // 움직임을 나타낼 벡터
    private Quaternion movementAngle;

    // 회전을 나타낼 벡터
    private Vector3 movement;

    // Start is called before the first frame update
    // Update is called once per frame
    private void Start() {
        anim = GetComponent<Animator>();
        characterRigidbody = GetComponent<Rigidbody>();
    }

    private void TurnCharacter(){

        // rigidbody의 회전
        characterRigidbody.rotation = Quaternion.Slerp(
            characterRigidbody.rotation,
            movementAngle,
            rotateSpeed * Time.deltaTime
        );

        // 몸체 (material)의 회전
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

        // 플레이어가 바라보는 방향대로 움직이도록 함
        movement = transform.forward;
        movement.y = 0;
        movementAngle = transform.rotation * Quaternion.Euler(new Vector3(0, inputX*rotateAnglePerTick, 0));

        movement = movement.normalized * (inputZ > 0 ? runSpeed : runSpeed * 0.7f) * inputZ;
        Debug.Log(movement);

        // rigidbody의 velocity를 조절하여 움직임
        characterRigidbody.velocity = movement;

        // horizontal input이 존재하면 회전
        if (inputX != 0){
            TurnCharacter();
        }
        anim.SetBool("isForward", inputZ > 0);
        anim.SetBool("isBackward", inputZ < 0);
    }
}
