using UnityEngine;


public class Projectile : MonoBehaviour{
    public Vector3 Direction;//public for debug change later

    [SerializeField] private ProjectileData projectileData;


    public void SetDirection(Vector3 direction){
        this.Direction = direction;
    }

    private void Update()
    {
        transform.position += Direction *projectileData.ProjectileSpeed * Time.deltaTime;
    }
}