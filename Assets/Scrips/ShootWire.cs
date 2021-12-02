using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWire : MonoBehaviour
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
        source = GetComponent<AudioSource>();

        laser = this.gameObject.AddComponent<LineRenderer>();
        laser.startWidth = laserWidth;
        laser.endWidth = laserWidth;

        laserMat = Resources.Load("LazerBlue", typeof(Material)) as Material;
        laser.material = laserMat;
    }

    private void Update()
    {

        ray = new Ray(this.transform.position, transform.forward * range);


        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0)
                Fire();
            else
                Debug.Log("Out of wires");
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            PlayAnim();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            returnWire();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (ammo == 0)
            dummyWire.SetActive(false);
        else
            dummyWire.SetActive(true);

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out struck, range) && struck.transform.tag == "Wall")
        {
            if (ammo > 0 && animator.GetBool("goingUp"))
            {
                laser.enabled = true;
                laser.SetPosition(0, dummyWire.transform.position);
                laser.SetPosition(1, struck.point);
            }
        }
        else
            laser.enabled = false;

    }

    void Fire()
    {
        RaycastHit hit;

        if(Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range) && hit.transform.tag == "Wall" && animator.GetBool("goingUp"))
        {
            if (!cloneOut)
            {
                clone1 = Instantiate(tripwireObj1, hit.point, Quaternion.LookRotation(hit.normal));
                clone1.name = "clone1";
                source.PlayOneShot(twThrowSFX, source.volume);

                if (Physics.Raycast(clone1.transform.position, clone1.transform.forward, out hit, range) && hit.transform.tag == "Wall")
                {
                    mClone1 = Instantiate(cloneObj1, hit.point, Quaternion.LookRotation(hit.normal));
                    mClone1.name = "mClone1";
                    source.PlayOneShot(twThrowSFX, source.volume);


                }
                cloneOut = true;
            }
            else
            {
                clone2 = Instantiate(tripwireObj2, hit.point, Quaternion.LookRotation(hit.normal));
                clone2.name = "clone2";
                source.PlayOneShot(twThrowSFX, source.volume);



                if (Physics.Raycast(clone2.transform.position, clone2.transform.forward, out hit, range) && hit.transform.tag == "Wall")
                {
                    mClone2 = Instantiate(cloneObj2, hit.point, Quaternion.LookRotation(hit.normal));
                    mClone2.name = "mClone2";
                    source.PlayOneShot(twThrowSFX, source.volume);

                }
                cloneOut = true;
            }

            ammo--;

        }

        if(ammo == 1)
        {
            full.SetActive(false);
            half.SetActive(true);
        }

        if (ammo == 0)
        {
            half.SetActive(false);
            empty.SetActive(true);
        }
    }

    void returnWire()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range) && (hit.transform.CompareTag("clone1") || hit.transform.CompareTag("clone2") 
            || hit.transform.CompareTag("mClone1") || hit.transform.CompareTag("mClone2")) && ammo < 2)
        {
            Debug.Log("Returning");

            source.PlayOneShot(twInSFX, source.volume);

            DestroyWire(hit);
            ammo++;

            if (ammo == 1)
            {
                empty.SetActive(false);
                half.SetActive(true);
            }

            if (ammo == 2)
            {
                half.SetActive(false);
                full.SetActive(true);
            }

        }
    }

    void DestroyWire(RaycastHit hit)
    {
        if (hit.transform.CompareTag("clone1") || hit.transform.CompareTag("mClone1"))
        {
            Destroy(clone1);
            Destroy(mClone1);
            cloneOut = false;
        }
        else if(hit.transform.CompareTag("clone2") || hit.transform.CompareTag("mClone2"))
        {
            Debug.Log("Destroying clone2");
            Destroy(clone2);
            Destroy(mClone2);
        }

    }

    void PlayAnim()
    {
        source.PlayOneShot(twOutSFX, source.volume);
        if (animator.GetBool("goingUp"))
            animator.SetBool("goingUp", false);
        else
            animator.SetBool("goingUp", true);
    }

    void Reload()
    {
        ammo = 2;
        empty.SetActive(false);
        half.SetActive(false);
        full.SetActive(true);
    }
}
