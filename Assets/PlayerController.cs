using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float rotateSpeed = 10.0f;       // ȸ�� �ӵ�
    public float jumpForce = 1.6f;        // �����ϴ� ��
    public static int coin = 0;

    public bool isGround = true;            // ĳ���Ͱ� ���� �ִ��� Ȯ���� ����

    Rigidbody body;                         // ������Ʈ���� RigidBody�� �޾ƿ� ����

    public CoinScript eventSystem;

    void Start()
    {
        body = GetComponent<Rigidbody>();   // GetComponent�� Ȱ���Ͽ� body�� �ش� ������Ʈ�� Rigidbody�� �־��ش�.
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
            // �ٶ󺸴� �������� �̵�
            GetComponent<Rigidbody>().velocity += direction * Speed * Time.deltaTime;
        }

        // �����̽��ٸ� ������(�Ǵ� ������ ������), �׸��� ĳ���Ͱ� ���� �ִٸ�
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            // AddForce(����, ���� ��� ���� ���ΰ�)
            body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // ������ ���������Ƿ� isGround�� false�� �ٲ�
            isGround = false;
        }
    }

    // �浹 �Լ�
    void OnCollisionEnter(Collision collision)
    {
        // �ε��� ��ü�� �±װ� "Ground"���
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGround�� true�� ����
            isGround = true;
        }
    }
}
