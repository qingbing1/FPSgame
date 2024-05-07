using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //给子弹刚体组件一个速度
        GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
