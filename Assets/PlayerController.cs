using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10.0f;
    public float rotateSpeed = 10.0f;       // ȸ�� �ӵ�
    public float jumpForce = 1.2f;        // �����ϴ� ��

    public static int maxCoin;
    public static int coin = 0;
    int needCoin;
    bool CollectAllCoinIs = false;

    public bool isGround = true;            // ĳ���Ͱ� ���� �ִ��� Ȯ���� ����

    Rigidbody body;                         // ������Ʈ���� RigidBody�� �޾ƿ� ����

    public CoinScript eventSystem;

    public Text Coin_text;
    public GameObject nextLevelText;

    int nowLevel = 1;

    void Start()
    {
        nextLevelText.SetActive(false);
        body = GetComponent<Rigidbody>();   // GetComponent�� Ȱ���Ͽ� body�� �ش� ������Ʈ�� Rigidbody�� �־��ش�.
        Respawn();
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

        if (coin >= needCoin)     // ������ ������ �̻� ������
        {
            CollectAllCoinIs = true;
        }

        if (CollectAllCoinIs == true)
        {
            nextLevelText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                nextLevelText.SetActive(false);
                CollectAllCoinIs = false;
                nowLevel++;
                coin = 0;
                Respawn();
                Coin_text.text = "���� : " + coin + " / " + maxCoin;
            } 
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

        if (collision.collider.gameObject.CompareTag("Respawn"))
        {
            Respawn();
        }
    }
    
    void Respawn()
    {
        if (nowLevel == 1)
        {
            transform.position = new Vector3(-100, 2, 0);
            maxCoin = 45;
            needCoin = 30;
        }
        if (nowLevel == 2)
        {
            transform.position = new Vector3(100, 2, 0);
            maxCoin = 3;
            needCoin = 3;
        }
    }
}
