using System;
using UnityEngine;
using UnityEngine.Events;


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
    [SerializeField] private ParticleSystem particles;

    public event UnityAction OnLevelUp;

    public event UnityAction<float, float> OnEXPChange;//Current exp/ Current requirement

    private void OnEnable()
    {
        SaveGameManager.OnSave += SaveXPAndLevel;
        SaveGameManager.OnLoad += LoadXPAndLevel;
    }

    private void OnDisable()
    {
        SaveGameManager.OnSave -= SaveXPAndLevel;
        SaveGameManager.OnLoad -= LoadXPAndLevel;
    }

    private void SaveXPAndLevel(SerializedSaveGame saveData)
    {
        saveData.currentEXP = _currentEXP;
        saveData.level = _level;
    }

    private void LoadXPAndLevel(SerializedSaveGame saveData)
    {
        _currentEXP = saveData.currentEXP;
        _level = saveData.level;
        OnEXPChange?.Invoke(_currentEXP, GetLevelupRequirement());
        OnLevelUp?.Invoke();
    }

    public void setMultiplier(float gainMultiplier){
        _gainMultiplier = gainMultiplier;
    }

    private float GetLevelupRequirement(){
        return _level * LinearGrowth;//Exp will jsut be a linear graph
    }

    private void CheckForLevelup(){
        float currentReq = GetLevelupRequirement();
        OnEXPChange?.Invoke(_currentEXP, currentReq);
        if(_currentEXP > currentReq){
            _currentEXP -= currentReq;
            _level++;
            particles.Play();
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