using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSprite : MonoBehaviour
{
    Animator anim;

    bool x;
      public void nextPhase()
      {
        x = true;
        anim.SetBool("x", x);
      }

    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
