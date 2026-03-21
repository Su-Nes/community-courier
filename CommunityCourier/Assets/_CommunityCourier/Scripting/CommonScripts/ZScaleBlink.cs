using System.Collections;
using UnityEngine;

public class ZScaleBlink : MonoBehaviour
{
    [SerializeField] private Vector2 blinkRate;
    [SerializeField] private float blinkDuration = .2f;
    private float blinkTimer, startZ;

    [SerializeField] private Transform[] eyes;
    
    private void Start()
    {
        startZ = eyes[0].localScale.z;
        RandomizeBlinkTimer();
    }

    private void Update()
    {
        if (blinkTimer > 0)
            blinkTimer -= Time.deltaTime; 
        else
        {
            StartCoroutine(Blink());
            RandomizeBlinkTimer();
        }
    }

    private void RandomizeBlinkTimer()
    {
        blinkTimer = Random.Range(blinkRate.x, blinkRate.y);
    }

    private IEnumerator Blink()
    {
        foreach (Transform eye in eyes)
        {
            eye.localScale = new Vector3(eye.localScale.x, eye.localScale.y, 0f);
            yield return new WaitForSeconds(blinkDuration);
            eye.localScale = new Vector3(eye.localScale.x, eye.localScale.y, startZ);
        }
    }
}
