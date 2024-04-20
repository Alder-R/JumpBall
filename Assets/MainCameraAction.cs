using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class MainCameraAction : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;

    public Transform target;            //Player

    private float rotSensitive = 3f;    //ī�޶� ȸ�� ����
    private float dis = 0f;             //ī�޶�� �÷��̾� ������ �Ÿ�
    private float RotationMin = -45f;   //ī�޶� ȸ������ �ּ�
    private float RotationMax = 80f;    //ī�޶� ȸ������ �ִ�
    private float smoothTime = 0.12f;   //ī�޶� ȸ���ϴµ� �ɸ��� �ð�
    //�� 5���� value�� ���� ����
    private Vector3 targetRotation;
    private Vector3 currentVel;

    private float camera_dist = 0f; //���׷κ��� ī�޶������ �Ÿ�
    public float camera_width = -16f; //���γ���
    public float camera_height = 2f; //���γ���
    public float camera_fix = 3f;//�����ɽ�Ʈ �� ���������� �� �Ÿ�

    Vector3 dir;
    void Start()
    {

        //ī�޶󸮱׿��� ī�޶������ ����
        camera_dist = Mathf.Sqrt(camera_width * camera_width + camera_height * camera_height);

        //ī�޶󸮱׿��� ī�޶���ġ������ ���⺤��
        dir = new Vector3(0, camera_height, camera_width).normalized;

    }

    void LateUpdate()   //Player�� �����̰� �� �� ī�޶� ���󰡾� �ϹǷ� LateUpdate
    {
        Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;    //���콺 �¿�������� �Է¹޾Ƽ� ī�޶��� Y���� ȸ����Ų��
        Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;    //���콺 ���Ͽ������� �Է¹޾Ƽ� ī�޶��� X���� ȸ����Ų��
        //Xaxis�� ���콺�� �Ʒ��� ������(�������� �Է� �޾�����) ���� �������� ī�޶� �Ʒ��� ȸ���Ѵ� 

        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
        //X��ȸ���� �Ѱ�ġ�� �����ʰ� �������ش�.

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        this.transform.eulerAngles = targetRotation;
        //SmoothDamp�� ���� �ε巯�� ī�޶� ȸ��

        transform.position = target.position - transform.forward * dis;
        //ī�޶��� ��ġ�� �÷��̾�� ������ ����ŭ �������ְ� ��� ����ȴ�


        //����ĳ��Ʈ�� ���Ͱ�
        Vector3 ray_target = transform.up * camera_height + transform.forward * camera_width;

        RaycastHit hitinfo;
        Physics.Raycast(transform.position, ray_target, out hitinfo, camera_dist);

        if (hitinfo.point != Vector3.zero)//�����ɽ�Ʈ ������
        {
            //point�� �ű��.
            transform.position = hitinfo.point;
            //ī�޶� ����(�� ���� �Ⱥ��̰�)
            transform.Translate(dir * -1 * camera_fix);
        }
        else
        {
            //ī�޶���ġ������ ���⺤�� * ī�޶� �ִ�Ÿ� �� �ű��.
            transform.Translate(dir * camera_dist);
            //ī�޶� ����(�� ���� �Ⱥ��̰�)
            transform.Translate(dir * -1 * camera_fix);

        }
    }
}
