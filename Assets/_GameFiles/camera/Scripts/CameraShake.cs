using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    float trauma;
    public float traumaHealRate;
    public float MaxOffsetXshake, MaxOffsetYshake;
    float xOffsetShake, yOffsetShake;
    Vector3 previous;

    // Use this for initialization
    void Start()
    {
        previous = transform.position;
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
        gameObject.transform.position = previous;
        previous = transform.position;
        xOffsetShake = MaxOffsetXshake * GetTrauma() * Random.Range(-1, 1);
        yOffsetShake = MaxOffsetYshake * GetTrauma() * Random.Range(-1, 1);

        gameObject.transform.Translate(xOffsetShake, yOffsetShake, 0);
    }

    float GetTrauma()
    {
        return trauma * trauma;
    }

    public void AddTrauma(float t)
    {
        trauma += t;
    }
}
