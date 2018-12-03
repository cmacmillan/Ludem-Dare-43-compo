using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy : Enemy
{

    // Use this for initialization
    public float steeringRate;
    public float maxSpeed;
    public float forwardForce;
    public float dotProductThresholdToEnterCharge = .5f;
    public Animator anim;
    void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (gameManager.ship != null)
        {
            var step = steeringRate * Time.deltaTime;
            var directionToShip = (gameManager.ship.transform.position - this.transform.position).normalized;
            Vector3 newDir = Vector3.RotateTowards(this.transform.up, directionToShip, step, 0.0f);
            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, newDir);
            rigid.AddForce(this.transform.up * forwardForce * Time.deltaTime);
            if (rigid.velocity.magnitude > maxSpeed)
            {
                rigid.velocity = (rigid.velocity - (rigid.velocity.magnitude - maxSpeed) * .9f * rigid.velocity.normalized);
            }
            /*if (rigid.velocity.magnitude > maxSpeed)
            {
                rigid.velocity = maxSpeed * rigid.velocity.normalized;
			}*/
            if (Vector3.Dot(this.transform.up, directionToShip) > dotProductThresholdToEnterCharge)
            {
                anim.SetBool("IsCharging", true);
            }
            else
            {
                anim.SetBool("IsCharging", false);
            }
        }
    }
}
