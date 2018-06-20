using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    float trauma;
    public float traumaHealRate;
    public float MaxOffsetXshake, MaxOffsetYshake;
    float xOffsetShake, yOffsetShake;
    Vector3 shaked, antiShaked;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            AddTrauma(0.2f);

        Shake();

        trauma -= traumaHealRate * Time.deltaTime;
        if (trauma < 0)
            trauma = 0;
    }

    void Shake()
    {
        gameObject.transform.position += antiShaked;
        xOffsetShake = MaxOffsetXshake * GetTrauma() * Mathf.PerlinNoise(Time.time*10, Random.Range(0f, 100f));
        yOffsetShake = MaxOffsetYshake * GetTrauma() * Mathf.PerlinNoise(-Time.time*10, Random.Range(0f, 100f));

        shaked.x = xOffsetShake;
        shaked.y = yOffsetShake;
        shaked.z = 0;

        antiShaked = -1 * shaked;

        gameObject.transform.position += shaked;
    }

    float GetTrauma()
    {
        return trauma * trauma;
    }

    public void AddTrauma(float t)
    {
        trauma += t;
        if (trauma > 1)
            trauma = 1;
    }
}
