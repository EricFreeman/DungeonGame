using System.Collections;
using UnityEngine;

public class CameraScreenShake : MonoBehaviour
{
    public static CameraScreenShake Instance;

    private Vector3 _origin;

    private void Start()
    {
        Instance = this;
        _origin = transform.localPosition;
    }

    public void Shake(float duration, float magnitude, float frequency)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude, frequency));
    }

    public IEnumerator ShakeRoutine(float duration, float magnitude, float frequency)
    {
        var elapsed = 0f;

        while (elapsed < duration)
        {
            var damp = 1f - (elapsed / duration);
            var rand = Random.insideUnitCircle * magnitude * damp;

            transform.localPosition = new Vector3(rand.x, rand.y, _origin.z);
            elapsed += Time.deltaTime + frequency;

            yield return new WaitForSeconds(frequency);
        }

        transform.localPosition = _origin;
    }
}
