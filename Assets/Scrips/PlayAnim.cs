using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    [SerializeField] private Animator moveArm = null;
    [SerializeField] private string armUp = "ArmUp";

    public void Check()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            moveArm.Play(armUp, 0, 0.0f);
        }
    }
}
