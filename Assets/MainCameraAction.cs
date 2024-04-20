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

    private float rotSensitive = 3f;    //카메라 회전 감도
    private float dis = 0f;             //카메라와 플레이어 사이의 거리
    private float RotationMin = -45f;   //카메라 회전각도 최소
    private float RotationMax = 80f;    //카메라 회전각도 최대
    private float smoothTime = 0.12f;   //카메라가 회전하는데 걸리는 시간
    //위 5개의 value는 자유 변경
    private Vector3 targetRotation;
    private Vector3 currentVel;

    private float camera_dist = 0f; //리그로부터 카메라까지의 거리
    public float camera_width = -16f; //가로넓이
    public float camera_height = 2f; //세로높이
    public float camera_fix = 3f;//레이케스트 후 리그쪽으로 올 거리

    Vector3 dir;
    void Start()
    {

        //카메라리그에서 카메라까지의 길이
        camera_dist = Mathf.Sqrt(camera_width * camera_width + camera_height * camera_height);

        //카메라리그에서 카메라위치까지의 방향벡터
        dir = new Vector3(0, camera_height, camera_width).normalized;

    }

    void LateUpdate()   //Player가 움직이고 그 후 카메라가 따라가야 하므로 LateUpdate
    {
        Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;    //마우스 좌우움직임을 입력받아서 카메라의 Y축을 회전시킨다
        Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;    //마우스 상하움직임을 입력받아서 카메라의 X축을 회전시킨다
        //Xaxis는 마우스를 아래로 했을때(음수값이 입력 받아질때) 값이 더해져야 카메라가 아래로 회전한다 

        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
        //X축회전이 한계치를 넘지않게 제한해준다.

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        this.transform.eulerAngles = targetRotation;
        //SmoothDamp를 통해 부드러운 카메라 회전

        transform.position = target.position - transform.forward * dis;
        //카메라의 위치는 플레이어보다 설정한 값만큼 떨어져있게 계속 변경된다


        //레이캐스트할 벡터값
        Vector3 ray_target = transform.up * camera_height + transform.forward * camera_width;

        RaycastHit hitinfo;
        Physics.Raycast(transform.position, ray_target, out hitinfo, camera_dist);

        if (hitinfo.point != Vector3.zero)//레이케스트 성공시
        {
            //point로 옮긴다.
            transform.position = hitinfo.point;
            //카메라 보정(벽 안쪽 안보이게)
            transform.Translate(dir * -1 * camera_fix);
        }
        else
        {
            //카메라위치까지의 방향벡터 * 카메라 최대거리 로 옮긴다.
            transform.Translate(dir * camera_dist);
            //카메라 보정(벽 안쪽 안보이게)
            transform.Translate(dir * -1 * camera_fix);

        }
    }
}
