/// Summary
//    During the game :
//      On collision enter : 
//        Destroy the other game object
//        Call GameOver method from MainManager script
/// 



using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Manager.GameOver();
    }
}
