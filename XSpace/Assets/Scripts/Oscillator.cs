using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = Vector3.zero;
    [SerializeField][Range(0,1)] float movementFactor = 0;
    [SerializeField] float period = 2f;

    Vector3 startingPostion = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        startingPostion = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPostion + offset;
    }
}
