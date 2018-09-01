using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementoMovimiento : MonoBehaviour {
    enum Estado {  Idle, ToRight, ToLeft};
    private Estado estado = Estado.Idle;
    [SerializeField] protected int speed = 1;
    [SerializeField] protected int distance = 20;
    protected int currentDistance = 0;
	// Use this for initialization
	void Start () {
        estado = Estado.ToRight;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        currentDistance++;
        if (currentDistance >= distance)
        {
            speed = speed * -1;
            currentDistance = 0;
        }
    }
}
