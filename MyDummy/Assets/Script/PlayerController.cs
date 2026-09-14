using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Transform cameraTransform;

    [Header("이동 설정")]

    [SerializeField] private float walkSpeed = 3f;

    [SerializeField] private float runSpeed = 10f;

    [SerializeField] private float rotationSpeed = 10f;

    [Header("바닥 설정")]

    [SerializeField] private float gravity = -20f;


    private CharacterController controller;

    private float verticaVelocity;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null ) { return; }

        Vector2 input = Vector2.zero;
        if (keyboard.aKey.isPressed)
            input.x -= 1f;
        if (keyboard.dKey.isPressed)
            input.x += 1f;
        if (keyboard.sKey.isPressed)
            input.y -= 1f;
        if (keyboard.wKey.isPressed)
            input.y += 1f;

        input = Vector2.ClampMagnitude(input, 1f);


        // 카메라 설정

        Vector3 cameaForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        //카메라의 위아래 기울기는 이동에 사용 하지 않는다.
        cameaForward.y = 0f;
        cameraRight.y = 0f;

        cameaForward.Normalize();
        cameraRight.Normalize();


        // 카메라 기준 이동 방향
        Vector3 moveDirection = cameaForward * input.y + cameraRight * input.x;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        // 쉬프트 달리기

        bool isRunning = keyboard.leftShiftKey.isPressed;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        //수평 이동

        controller.Move(moveDirection * currentSpeed * Time.deltaTime);

        //이동 방향 계산

        if (moveDirection.sqrMagnitude > 0.001f )
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if(controller.isGrounded && verticaVelocity < 0f)
        {
            verticaVelocity = -2f;
        }
        else
        {
            verticaVelocity += gravity * Time.deltaTime;
        }

        controller.Move(Vector3.up * verticaVelocity * Time.deltaTime);


        // Idle, Walk, Run
        float animationSpeed = 0f;

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            animationSpeed = isRunning ? 1f : 0.5f;
        }

        animator.SetFloat("speed" , animationSpeed , 0.1f, Time.deltaTime);
    }
}
