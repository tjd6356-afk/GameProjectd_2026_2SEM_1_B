using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;




public class ThirdPersonCamera : MonoBehaviour
{
    [Header("따라갈 대상")]
    [SerializeField] private Transform target;

    [Header("카메라 위치")]
    [SerializeField] private float distance = 7f;
    [SerializeField] private float height = 1.5f;

    [Header("마우스 회전")]
    [SerializeField] private float mouseSensitivity = 0.12f;
    [SerializeField] private float minVerticalAngle = -30f;
    [SerializeField] private float maxVerticalAngle = 60f;

    [Header("마우스 스크롤")]
    [SerializeField] private float zoomSpeed = 0.01f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 10f;


    private float yaw;
    private float pitch;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Mouse mouse = Mouse.current;

        if (mouse == null)
        {
            return;
        }

        Vector2 mouseDelta = mouse.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivity;
        pitch -= mouseDelta.y * mouseSensitivity;
        pitch = Mathf.Clamp(distance, minDistance, maxDistance);
    }



    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 lookPonit = target.position + Vector3.up * height;
        Quaternion orbitRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 cameraOffset = orbitRotation * new Vector3(0f, 0f, -distance);
        transform.position = lookPonit + cameraOffset;
        transform. LookAt(lookPonit);


    }
}
