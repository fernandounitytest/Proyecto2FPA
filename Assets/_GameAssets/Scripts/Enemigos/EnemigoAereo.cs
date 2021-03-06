﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemigoAereo : Enemigo {
    private bool vivo = true;
    [SerializeField] int speed = 1;
    [SerializeField] int distance = 20;
    private int currentDistance = 0;
    Tween tweenOscilar;
    // Use this for initialization
    void Start () {
        transform.localScale = new Vector3(-1, 1, 1);
        tweenOscilar = this.transform.DOMoveY(0.3f, 0.5f).SetLoops(-1,LoopType.Yoyo).SetRelative().SetEase(Ease.InOutSine);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        currentDistance++;
        if (currentDistance >= distance)
        {
            speed = speed * -1;
            currentDistance = 0;

            transform.localScale = new Vector3(transform.localScale.x*-1, 1, 1);
        }
    }

    protected override void Morir()
    {
        if (vivo)
        {
            vivo = false;
            base.Morir();
            tweenOscilar.Kill();
        }
    }
}
