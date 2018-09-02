using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour {
    [SerializeField] public ParticleSystem prefabExplosion;
    [SerializeField] GameObject deathStar;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParticleSystem ps = Instantiate(prefabExplosion, deathStar.transform.position, Quaternion.identity);
        ps.Play();
        AudioSource bang = GetComponent<AudioSource>();
        bang.Play();
        Destroy(ps.gameObject, ps.main.duration);
        Destroy(deathStar.gameObject);
        Jugador j = FindObjectOfType<Jugador>();
        j.GetComponent<SpriteRenderer>().enabled = false;
        Invoke("Final", 3);
    }
    public void Final()
    {
        SceneManager.LoadScene("VictoriaScene");
    }
}
