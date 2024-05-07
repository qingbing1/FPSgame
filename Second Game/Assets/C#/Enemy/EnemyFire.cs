using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    //关联子弹点
    public Transform BulletPoint;
    //关联子弹预设体
    public GameObject BulletPre;
    //开火间隔
    private float Cd = 1f;
    //计时器（看是否达到开火间隔）
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Cd)
        {
            //重置计时器
            timer = 0;

            //创建子弹
            Instantiate(BulletPre, BulletPoint.position, BulletPoint.rotation);
        }
    }
}
