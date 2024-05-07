using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform; // 假设这是玩家的Transform组件的引用  

    void Start()
    {
        player = GameObject.Find("Player");
        // 获取玩家的Transform组件引用  
        playerTransform = player.transform; // 假设player是一个已经定义好的GameObject或者Transform变量  
    }

    void Update()
    {
        // 朝向玩家  
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        // 计算从敌人到玩家的方向  
        Vector3 direction = playerTransform.position - transform.position;

        // 确保方向不是零向量  
        if (direction != Vector3.zero)
        {
            // 旋转敌人以面向玩家  
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // 平滑旋转，这里的5f是旋转速度，可以根据需要调整  
        }
    }
}
