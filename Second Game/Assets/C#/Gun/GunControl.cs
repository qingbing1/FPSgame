using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunControl : MonoBehaviour
{
    public GameObject SuperGun;
    public GameObject Gun;
    //���������
    public Transform FirePoint;
    //��������Ԥ����
    public GameObject FirePre;
    //�����ӵ���
    public Transform BulletPoint;
    //�����ӵ�Ԥ����
    public GameObject BulletPre;
    //����ǹ����Ч
    public AudioClip clip;
    //�������ӵ���Ч
    public AudioClip Check;
    //�����ӵ�UI
    public Text BulletText;

    //�ӵ�����
    private int BulletNum = 10;
    //������
    private float Cd = 0.3f;
    //��ʱ�������Ƿ�ﵽ��������
    private float timer = 0;

    //�����������
    private AudioSource gunPlayer;

    private void Start()
    {
        //��ȡ���������
        gunPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Cd && BulletNum > 0 && Input.GetMouseButton(0))
        {
            //���ü�ʱ��
            timer = 0;
            //��������
            Instantiate(FirePre, FirePoint.position, FirePoint.rotation);
            //�����ӵ�
            Instantiate(BulletPre, BulletPoint.position, BulletPoint.rotation);
            //�ӵ�������һ,ˢ��UI
            BulletNum--;
            BulletText.text = BulletNum + "";
            //����ǹ��
            gunPlayer.PlayOneShot(clip);
            //����������һ���ӵ����Զ����ӵ�
            if (BulletNum == 0)
            {
                //���Ŷ���
                GetComponent<Animator>().SetTrigger("Reload");
                //���Ż��ӵ���Ч
                gunPlayer.PlayOneShot(Check);
                //1.5s�󻻺��ӵ�
                Invoke("ReLoad",1.5f);
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && BulletNum != 10)
        {
            //��յ���
            BulletNum = 0;
            //���Ŷ���
            GetComponent<Animator>().SetTrigger("Reload");
            //���Ż��ӵ���Ч
            gunPlayer.PlayOneShot(Check);
            //1.5s�󻻺��ӵ�
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
        //�����ӵ�����
        BulletNum = 10;
        //ˢ��UI
        BulletText.text = BulletNum + "";
    }
}
