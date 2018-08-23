using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallItem : BuildItem {

    public float period;
    float time;
    public Ball ballPrefab;
    public float speed;

	// Use this for initialization
	void Start () {
        time = Time.time + period;
	}

    public override void DeSelect()
    {
        time = Time.time + period;
        base.DeSelect();
    }

    // Update is called once per frame
    void Update () {
		if(!selected && Time.time > time)
        {
            time = time + period;
            Ball b = Instantiate(ballPrefab);
            b.transform.position = transform.position;
            b.Launch(transform.forward, speed);
            Library.playSound(transform.position, sound);

            StartCoroutine(reactFX());
        }
	}

    public override bool TriggerEnter(Ball ball)
    {
        return false;
    }
}
