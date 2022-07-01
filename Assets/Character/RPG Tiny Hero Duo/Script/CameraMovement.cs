using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Camera cam;
    private Transform cameraArm;

    private void Start() {
        cam = GetComponentInChildren<Camera>();
        cameraArm = GetComponent<Transform>();
    }

    private void Update() {
        if(Input.GetMouseButton(0))
            CameraMove();
    }

    // 카메라 움직임을 담당하는 함수
    private void CameraMove(){
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        cameraArm.rotation = Quaternion.Euler(camAngle.x - mouseDelta.y, camAngle.y + mouseDelta.x, camAngle.z);
    }
}