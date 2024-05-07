using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform; // ����������ҵ�Transform���������  

    void Start()
    {
        player = GameObject.Find("Player");
        // ��ȡ��ҵ�Transform�������  
        playerTransform = player.transform; // ����player��һ���Ѿ�����õ�GameObject����Transform����  
    }

    void Update()
    {
        // �������  
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        // ����ӵ��˵���ҵķ���  
        Vector3 direction = playerTransform.position - transform.position;

        // ȷ��������������  
        if (direction != Vector3.zero)
        {
            // ��ת�������������  
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // ƽ����ת�������5f����ת�ٶȣ����Ը�����Ҫ����  
        }
    }
}
