using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;

    private float mouseX, mouseY;//获取鼠标移动的值
    public float mouseSensitivity;//鼠标灵敏度

    public float xRotation;// 用来累加mouseY

    private void Update()
    {
        //鼠标左右移动的值
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //鼠标上下移动的值
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation,-60,30);//限制y轴角度

        player.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
