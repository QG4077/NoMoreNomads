using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomadAnimationController : Rigid2DAnimationController
{
    public override void Start()
    {
        base.Start();
    }

    void FixedUpdate()
    {
        anim.SetFloat("FallSpeed", Mathf.Abs(rb2D.velocity.y));
    }
}
