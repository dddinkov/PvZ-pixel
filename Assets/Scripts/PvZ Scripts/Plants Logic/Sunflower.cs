using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : MonoBehaviour
{
    public GameObject sunPrefab;
    public float cooldown = 7.0f;
    private float time = 0.0f;
    public ParticleSystem shinyParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if(time >= cooldown)
        {
            DropSun();
            time = 0.0f;
        }
    }

    private void DropSun()
    {
        GameObject sun = Instantiate(sunPrefab, transform.position, Quaternion.identity);
        Sun sunScript = sun.GetComponent<Sun>();
        sunScript.fallOffset = 20.0f;
        sunScript.maxFallDistance = 1.0f;
        shinyParticles.Emit(10);
    }
}
