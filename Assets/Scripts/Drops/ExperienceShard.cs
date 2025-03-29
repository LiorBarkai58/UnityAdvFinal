using UnityEngine;
using UnityEngine.Events;


public class ExperienceShard : MonoBehaviour {
    [SerializeField] private float flySpeed = 5;
    private Transform target;

    private bool Arrived = false;

    private float DistanceThreshold = 0.05f;

    private float expValue = 0;

    public float EXPValue => expValue;

    public event UnityAction<ExperienceShard> OnArrival;

    public void SetEXP(float exp){
        this.expValue = exp;
    }
    public void SetTarget(Transform target){
        this.target = target;
    }

    private void Update()
    {
        if(target && !Arrived){
            if(Vector3.Distance(transform.position, target.position) < DistanceThreshold){
                Arrived = true;
                OnArrival?.Invoke(this);
            }
            else{
                Vector3 direction = (target.position-transform.position).normalized;
                transform.position += direction * flySpeed * Time.deltaTime;
            }
        }
    }
}