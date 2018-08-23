using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : RigidBehaviour {

    public static float minStep = 0.03f;

    bool active;
    public float life = 12;
    float death;
    public float goalDirection;
    public GameObject deathFX;
    Color trailColor;

    protected void Awake()
    {
        //base.Awake();
        active = true;
        setColor(GoogleARCore.ARCoretroller.randomColor());
        death = Time.time + life;
    }

    public void setColor(Color c)
    {
        GetComponent<TrailRenderer>().material.color = trailColor = c;
    }

    public void Launch(Vector3 direction, float speed)
    {
        rbody.velocity = direction * speed;
    }
	
	protected override void Update () {
        if (!active)
            return;
		if(Time.time > death)
        {
            Death(true);
        }
        else
        {
            float d = rbody.velocity.magnitude * Time.deltaTime;
            Ray r = new Ray(transform.position, rbody.velocity + (rbody.acceleration + Physics.gravity) * Time.deltaTime );
            RaycastHit hitInfo;
            if(Physics.Raycast(r,out hitInfo,d))
            {
                BuildItem bi = hitInfo.collider.GetComponent<BuildItem>();
                if (bi == null)
                {
                }
                else if (bi.TriggerEnter(this))
                {
                    rbody.velocity = Vector3.Reflect(rbody.velocity + (rbody.acceleration + Physics.gravity) * Time.deltaTime, hitInfo.normal);
                    transform.position += (hitInfo.distance - transform.localScale.x / 2) * rbody.velocity.normalized;
                    return;
                }
            }
        }
        base.Update();
	}

    public Ball Copy()
    {
        GameObject o = Instantiate(this.gameObject);
        Ball b = o.GetComponent<Ball>();
        b.setColor(this.trailColor);
        return b;
    }

    public void Death(bool showDeath=false)
    {
        if (!active)
            return;
        active = false;
        if(showDeath)
        {
            GameObject fx = Instantiate(deathFX);
            fx.transform.position = transform.position;
            fx.transform.rotation = transform.rotation;
        }
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("Destroy", 1f);
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
