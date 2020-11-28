using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigid2DAnimationController : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb2D;

    public virtual void Start()
    {
        if (anim == null) { anim = this.GetComponent<Animator>(); }
        if (rb2D == null) { rb2D = this.GetComponent<Rigidbody2D>(); }
    }
}
