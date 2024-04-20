using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float rotateSpeed = 10.0f;       // 회전 속도
    public float jumpForce = 1.6f;        // 점프하는 힘
    public static int coin = 0;

    public bool isGround = true;            // 캐릭터가 땅에 있는지 확인할 변수

    Rigidbody body;                         // 컴포넌트에서 RigidBody를 받아올 변수

    public CoinScript eventSystem;

    void Start()
    {
        body = GetComponent<Rigidbody>();   // GetComponent를 활용하여 body에 해당 오브젝트의 Rigidbody를 넣어준다.
    }

    // public Transform cameraTransform;
    public new GameObject camera;

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float aaaa = camera.transform.rotation.eulerAngles.y;

        Vector3 dir = new Vector3(h, 0, v);
        Vector3 direction = Quaternion.AngleAxis(aaaa, Vector3.up) * dir;

        if (!(h == 0 && v == 0))
        {
            // 바라보는 방향으로 이동
            GetComponent<Rigidbody>().velocity += direction * Speed * Time.deltaTime;
        }

        // 스페이스바를 누르면(또는 누르고 있으면), 그리고 캐릭터가 땅에 있다면
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            // AddForce(방향, 힘을 어떻게 가할 것인가)
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // 땅에서 떨어졌으므로 isGround를 false로 바꿈
            isGround = false;
        }
    }

    // 충돌 함수
    void OnCollisionEnter(Collision collision)
    {
        // 부딪힌 물체의 태그가 "Ground"라면
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGround를 true로 변경
            isGround = true;
        }
    }
}
