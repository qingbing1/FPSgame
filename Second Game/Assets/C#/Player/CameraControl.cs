using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

    private float mouseX, mouseY;//��ȡ����ƶ���ֵ
    public float mouseSensitivity;//���������

    public float xRotation;// �����ۼ�mouseY

    private void Update()
    {
        //��������ƶ���ֵ
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //��������ƶ���ֵ
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation,-60,30);//����y��Ƕ�

        player.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
