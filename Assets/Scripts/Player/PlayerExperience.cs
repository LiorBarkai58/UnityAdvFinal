using System;
using UnityEngine;


public class PlayerExperience : MonoBehaviour {
    private float _currentEXP = 0;

    private int _level = 1;

    private float _gainMultiplier = 1;
    public float CurrentEXP
    {
        get => _currentEXP;
        set { _currentEXP = value; }
    }
    public int Level
    {
        get => _level;
        set { _level = value; }
    }




    [SerializeField] private int LinearGrowth = 7;

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