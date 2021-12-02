using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tripwire : MonoBehaviour
{
    private LineRenderer laser, laser2;
    private Ray ray;
    private RaycastHit struck, struck2;
    private bool enemySlowed = false;
    private bool moving = false;

    GameObject enemyObj;

    AudioSource source = null;
    Material laserMat;
    RaycastHit hit;

    public AudioClip twHitSFX;
    public float laserWidth = 0.2f;
    public float range = 10f;
    public float secondsToWait = 3f;

    void Start()
    {


        laser = this.gameObject.AddComponent<LineRenderer>();
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        source = GetComponent<AudioSource>();

        laserMat = Resources.Load("LazerBlue", typeof(Material)) as Material;
        CheckLaser();
    }

    private void Update()
    {

        if(moving)
        {
            laser.SetPosition(1, enemyObj.transform.position);
        }
        
        
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(this.transform.position, transform.forward * range, out struck, range))
        {
            if (struck.transform.tag == "Enemy" && enemySlowed == false)
            {
                StartCoroutine(DisableEnemy());
                enemySlowed = true;
            }
        }
    }

    void CheckLaser()
    {
        

        if(Physics.Raycast(this.transform.position, this.transform.forward * range, out hit, range))
        {
            laser.material = laserMat;
            laser.startColor = Color.cyan;
            laser.SetPosition(0, this.transform.position);
            laser.SetPosition(1, hit.point);
        }        
    }

    public IEnumerator DisableEnemy()
    {
        enemyObj = struck.collider.gameObject;
        EnemyAI enemy = (EnemyAI) enemyObj.GetComponent(typeof(EnemyAI));

        Debug.Log("Enemy Slowed");
        source.PlayOneShot(twHitSFX, source.volume);

        enemySlowed = true;
        enemy.ReduceSpeed();
        enemy.AlwaysVisibleTexture();
        moving = true;

        yield return new WaitForSeconds(secondsToWait-1);

        enemy.IncreaseSpeed();
        enemy.EnemyTexure();
        enemySlowed = false;
        moving = false;
        StartCoroutine(RemoveLaser());
    }

    public IEnumerator RemoveLaser()
    {
        this.transform.position = new Vector3(10, 10, 10);
        Destroy(laser);
        Destroy(laser2);

        yield return new WaitForSeconds(secondsToWait - 3);

        Destroy(this.transform.gameObject);

    }

}
