using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carreta : MonoBehaviour {
    enum Estado {  Idle, ToRight, ToLeft};
    private Estado estado = Estado.Idle;
    [SerializeField] int speed = 1;
    [SerializeField] int distance = 20;
    private int currentDistance = 0;
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
