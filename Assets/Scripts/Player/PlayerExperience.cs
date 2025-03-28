using System;
using UnityEngine;
using UnityEngine.Events;


public class PlayerExperience : MonoBehaviour {
    private float _currentEXP = 0;

    private int _level = 1;

    private float _gainMultiplier = 1;


    [SerializeField] private int LinearGrowth = 7;

    public event UnityAction OnLevelUp;

    public void setMultiplier(float gainMultiplier){
        _gainMultiplier = gainMultiplier;
    }

    private float GetLevelupRequirement(){
        return _level * LinearGrowth;//Exp will jsut be a linear graph
    }

    private void CheckForLevelup(){
        float currentReq = GetLevelupRequirement();
        if(_currentEXP > currentReq){
            _currentEXP -= currentReq;
            _level++;
            OnLevelUp?.Invoke();
            CheckForLevelup();
        }
    }

    private void IncreaseEXP(float exp){
        _currentEXP += Mathf.Max(exp * _gainMultiplier, 0);//Make sure you can't take away exp
        CheckForLevelup();
    }
    private void HandleShard(ExperienceShard shard){
        if(shard){
            IncreaseEXP(shard.EXPValue);
            shard.OnArrival -= HandleShard;
            Destroy(shard.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("ExpShard")){
            ExperienceShard current = other.gameObject.GetComponent<ExperienceShard>();
            if(current){
                current.SetTarget(transform);
                current.OnArrival += HandleShard;
            }
        }
    }
}