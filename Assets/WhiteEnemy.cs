using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteEnemy : Enemy
{

    private const float startVelocity = 5.0f;
    private const float startAngularVelocity = 200.0f;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        rigid.velocity = this.transform.up * startVelocity;
        //rigid.velocity = Random.insideUnitCircle.normalized * startVelocity;
        rigid.angularVelocity = Random.RandomRange(-startAngularVelocity, startAngularVelocity);
    }

}
