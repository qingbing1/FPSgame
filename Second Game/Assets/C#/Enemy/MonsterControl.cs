using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterControl : MonoBehaviour
{
    //public GameObject Emeny;
    public int hp = 3;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Bullet")
        {
            Hp();
        }
    }
    public void Hp()
    {
        hp--;
        Debug.Log(hp);

        if (hp == 0)
        {
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<Animator>().SetBool("IsMoving", false);
            GetComponent<Animator>().SetBool("Shoot", false);
            GetComponent<Animator>().SetBool("IsRuning", false);
            Invoke("DieOver", 3f);
        }
        
    }

    public void DieOver()
    {
        Destroy(gameObject);
    } 
}
