using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{

    //�����ӵ���
    public Transform BulletPoint;
    //�����ӵ�Ԥ����
    public GameObject BulletPre;
    //������
    private float Cd = 1f;
    //��ʱ�������Ƿ�ﵽ��������
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Cd)
        {
            //���ü�ʱ��
            timer = 0;

            //�����ӵ�
            Instantiate(BulletPre, BulletPoint.position, BulletPoint.rotation);
        }
    }
}
