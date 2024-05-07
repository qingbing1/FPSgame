using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //获取自动寻路组件
    private NavMeshAgent enemyAgent;
    private EnemyState childState = EnemyState.RestingState;
    private EnemyState state = EnemyState.NormalState;

    public float restTime = 2;//休息时间
    private float restTimer = 0;//计时器

    //获取主角
    private GameObject player;

    public float moveToPlaerDistance;//移向玩家的距离，警戒范围
    public float attckDistance;//攻击距离

    private float sideLookAngle = 310f;//修改的角度

    //关联子弹点
    public Transform BulletPoint;
    //关联子弹预设体
    public GameObject BulletPre;
    //开火间隔
    private float Cd = 1f;
    //计时器（看是否达到开火间隔）
    private float timer = 0;

    public enum EnemyState
    {
        NormalState,
        FightingState,
        MovingState,
        RestingState
    }

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();

        //初始化值
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position,this.transform.position) <= moveToPlaerDistance)
        {
            state = EnemyState.FightingState;
        }else if(Vector3.Distance(player.transform.position, this.transform.position) > moveToPlaerDistance){
            state = EnemyState.NormalState;
        }

        if(state == EnemyState.NormalState)
        {
            if (childState == EnemyState.RestingState)
            {
                GetComponent<Animator>().SetBool("IsMoving",false);
                GetComponent<Animator>().SetBool("Shoot", false);
                GetComponent<Animator>().SetBool("IsRuning", false);
                restTimer += Time.deltaTime;
                if (restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childState = EnemyState.MovingState;
                }
            }else if (childState == EnemyState.MovingState)
            {
                GetComponent<Animator>().SetBool("IsMoving", true);
                GetComponent<Animator>().SetBool("Shoot", false);
                GetComponent<Animator>().SetBool("IsRuning", false);
                if (enemyAgent.remainingDistance <= 0)
                {
                    restTimer = 0;//计时器归零
                    childState = EnemyState.RestingState; 
                }
            }
        }
        else if(state == EnemyState.FightingState)
        {
            GetComponent<Animator>().SetBool("IsRuning",true);
            GetComponent<Animator>().SetBool("IsMoving", false);
            GetComponent<Animator>().SetBool("Shoot", false);
            enemyAgent.SetDestination(player.transform.position);
            
            if (Vector3.Distance(player.transform.position, this.transform.position) <= attckDistance)
            {
                //攻击逻辑
                enemyAgent.speed = 0;
                GetComponent<Animator>().SetBool("Shoot",true);
                timer += Time.deltaTime;
                if (timer > Cd)
                {
                    //重置计时器
                    timer = 0;

                    //创建子弹
                    Instantiate(BulletPre, BulletPoint.position, BulletPoint.rotation);
                }
                LookAtPlayer();
                
            }
        }
    }

    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-2, 2f), 0, Random.Range(-2, 2f));
        return transform.position + randomDir.normalized * Random.Range(5, 10);
    }

    private void LookAtPlayer()
    {
        // 计算从敌人到玩家的方向  
        Vector3 direction = player.transform.position - transform.position;

        // 确保方向不是零向量  
        if (direction != Vector3.zero)
        {
            // 旋转敌人以面向玩家  
            Quaternion rotation = Quaternion.LookRotation(direction);

            // 创建一个表示绕Y轴旋转的四元数  
            Quaternion sideLookRotation = Quaternion.AngleAxis(-sideLookAngle, Vector3.up);

            // 将两个旋转相乘来组合它们 
            Quaternion finalRotation = sideLookRotation * rotation;

            transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * 5f); // 平滑旋转，这里的5f是旋转速度，可以根据需要调整  
        }
    }
}
