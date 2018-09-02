using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OneUp : MonoBehaviour {
    [SerializeField] Jugador j;
    Tween tweenOscilar;
    // Use this for initialization
    void Start () {
        tweenOscilar = this.transform.DOMoveY(0.3f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetRelative();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        j.VidaActual += 1;
        tweenOscilar.Kill();
        Destroy(this.gameObject);
    }

}
