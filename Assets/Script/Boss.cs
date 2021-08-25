using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    public Animator PlayerAnim;
    public GameObject BossCamera;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject BallThrower1;
    public GameObject BallThrower2;
    public GameObject Joystic;
    public GameObject AttackButton;
    public GameObject JumpButton;
    public BossTrigger BossTriggerScript;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //first cut is in BossTrigger Script............
    public void CameraCut2_Player()
    {
        BossCamera.SetActive(false);
        animator.SetBool("Talk1",false);
        PlayerAnim.SetBool("Talk",true);
        
    }
    //Third cut is in Player Script..................
    public void CameraCut4_Player()
    {
        BossCamera.SetActive(false);
        animator.SetBool("Talk2",false);
        Joystic.SetActive(true);
        JumpButton.SetActive(true);
        AttackButton.SetActive(true);
        BossTriggerScript.triggered = false;
        StartCoroutine("SpawnEnemy1");
        
    }

    IEnumerator SpawnEnemy1()
    {
        yield return new WaitForSeconds(2f);
        BallThrower1.SetActive(true);
        BallThrower2.SetActive(true);
        GameObject enemy = Instantiate(Enemy1,Enemy1.transform.position,Quaternion.identity);
        enemy.SetActive(true);
        StartCoroutine("SpawnEnemy2");
        
        yield return new WaitForSeconds(8f);
        GameObject enemy2 = Instantiate(Enemy1,Enemy1.transform.position,Quaternion.identity);
        enemy2.SetActive(true);
    }

    IEnumerator SpawnEnemy2()
    {
        yield return new WaitForSeconds(14f);
        GameObject enemy =Instantiate(Enemy2,Enemy2.transform.position,Quaternion.identity);
        enemy.SetActive(true);
        yield return new WaitForSeconds(20f);
        GameObject enemy2 =Instantiate(Enemy2,Enemy2.transform.position,Quaternion.identity);
        enemy2.SetActive(true);
        StartCoroutine("SpawnEnemy1Again");
    }






    IEnumerator SpawnEnemy1Again()
    {
        yield return new WaitForSeconds(2f);
        GameObject enemy = Instantiate(Enemy1,Enemy1.transform.position,Quaternion.identity);
        enemy.SetActive(true);
        StartCoroutine("SpawnEnemy2Again");
    }

    IEnumerator SpawnEnemy2Again()
    {
        yield return new WaitForSeconds(8f);
        GameObject enemy =Instantiate(Enemy2,Enemy2.transform.position,Quaternion.identity);
        enemy.SetActive(true);
    }

}
