using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireRaycast : MonoBehaviour
{
    public float range = 10f;
    public float laserWidth = .02f;
    public int ammo = 2;

    public AudioClip twThrowSFX, twInSFX, twOutSFX;
    public GameObject full, half, empty, dummyWire;
    public Camera mainCam;
    public GameObject tripwireObj1, tripwireObj2, cloneObj1, cloneObj2;
    public Animator animator;

    private RaycastHit struck;
    private Ray ray;
    private GameObject clone1, clone2, mClone1, mClone2;
    private bool cloneOut = false;
    private AudioSource source;
    private Material laserMat;

    private LineRenderer laser;

    private void Start()
    {
        laser = this.gameObject.AddComponent<LineRenderer>();
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserMat = Resources.Load("LazerBlue", typeof(Material)) as Material;
    }

    private void Update()
    {

        ray = new Ray(this.transform.position, transform.forward * range);


        


    }
    
}


