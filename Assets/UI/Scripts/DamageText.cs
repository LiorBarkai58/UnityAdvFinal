using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float lifeTime = 3f;
    private Vector3 Offset = new Vector3(0, 1.5f, 0);
    public Vector3 RandomIntensity = new Vector3(0.5f, 0, 0);

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomIntensity.x, RandomIntensity.x),
            Random.Range(-RandomIntensity.y, RandomIntensity.y),
            Random.Range(-RandomIntensity.z, RandomIntensity.z));
    }
}
