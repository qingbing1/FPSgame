using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    public GameObject SuperGun;
    public GameObject Gun;
    //关联火焰点
    public Transform FirePoint;
    //关联火焰预制体
    public GameObject FirePre;
    //关联子弹点
    public Transform BulletPoint;
    //关联子弹预设体
    public GameObject BulletPre;
    //关联枪声音效
    public AudioClip clip;
    //关联换子弹音效
    public AudioClip Check;
    //关联子弹UI
    public Text BulletText;

    //子弹个数
    private int BulletNum = 10;
    //开火间隔
    private float Cd = 0.3f;
    //计时器（看是否达到开火间隔）
    private float timer = 0;

    //声音播放组件
    private AudioSource gunPlayer;

    private void Start()
    {
        //获取播放器组件
        gunPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Cd && BulletNum > 0 && Input.GetMouseButton(0))
        {
            //重置计时器
            timer = 0;
            //创建火焰
            Instantiate(FirePre, FirePoint.position, FirePoint.rotation);
            //创建子弹
            Instantiate(BulletPre, BulletPoint.position, BulletPoint.rotation);
            //子弹个数减一,刷新UI
            BulletNum--;
            BulletText.text = BulletNum + "";
            //播放枪声
            gunPlayer.PlayOneShot(clip);
            //如果打完最后一发子弹，自动换子弹
            if (BulletNum == 0)
            {
                //播放动画
                GetComponent<Animator>().SetTrigger("Reload");
                //播放换子弹音效
                gunPlayer.PlayOneShot(Check);
                //1.5s后换号子弹
                Invoke("ReLoad",1.5f);
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && BulletNum != 10)
        {
            //清空弹夹
            BulletNum = 0;
            //播放动画
            GetComponent<Animator>().SetTrigger("Reload");
            //播放换子弹音效
            gunPlayer.PlayOneShot(Check);
            //1.5s后换号子弹
            Invoke("ReLoad", 1.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SuperGun.SetActive(true);
            Gun.SetActive(false);
        }
    }

    void ReLoad()
    {
        //重置子弹个数
        BulletNum = 10;
        //刷新UI
        BulletText.text = BulletNum + "";
    }
}
