using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UNIT_TYPE { PLAYER, ENEMY, EFFECT}
/// <summary>
/// Unit 클래스는 
/// </summary>
public class Unit : MonoBehaviour
{
    [Header("Unit Info")]
    [SerializeField] UnitStat Unitstat;
    [SerializeField] UNIT_TYPE UnitType;
    [SerializeField] Numerical numerical;

    private void Start()
    {
        numerical.Update();
    }

    virtual public void Update()
    {
        AttackUpdate();
        StateUpdate();
    }

    virtual public void LateUpdate()
    {
        StatUpdate();
    }

    public void TakeDmg(float dmg, ref Unit unit)
    {
        dmg -= (dmg - Unitstat.Stat.Deffensive);

        if (Unitstat.Stat.Hp <= 0)
            Die(ref unit);

        Hit(ref unit);
    }

    public void StatUpdate()
    {
        
    }

    virtual public void Die(ref Unit unit)
    {

    }

    virtual public void Hit(ref Unit unit)
    {

    }

    void AttackUpdate()
    {
        Unitstat.AttackCheckTime += Time.deltaTime;
        if(Unitstat.IsFire)
        {
            if (Unitstat.AttackCheckTime >= Unitstat.Stat.AttackTime)
            {
                Attack();
            }
        }
    }

    public virtual void Attack()
    {
        Unitstat.AttackCheckTime = 0;
    }

    void StateUpdate()
    {

    }

    public void AddState(StateInfo info)
    {
        Unitstat.StateInfoList.Add(info);
    }

    public virtual void Spawn(Vector2 pos, string usage)
    {
        gameObject.SetActive(true);
        transform.position = pos;
    }

}

//기본,
public enum UNIT_STATE { EMPTY, FAINT, SLOW }

[System.Serializable]
public struct Numerical
{
    public float Amount;
    public float MaxTime;
    public float CurTime;
    public bool IsAnimation;
    [SerializeField] AnimationCurve Curve;

    public void Update()
    {
        if (IsAnimation)
        {
            MaxTime = Curve.keys[Curve.length - 1].time;


            CurTime += Time.deltaTime;
            CurTime = Mathf.Clamp(CurTime, 0, MaxTime);
            Amount = Curve.Evaluate(CurTime);
        }
        else
        {
            Amount = 0;
        }
    }

    public bool IsFinal() { return CurTime >= MaxTime; }
}


[System.Serializable]
public struct Stats
{
    public float Speed;
    public float Hp;
    public float MaxHp;
    public float Power;
    public float AttackTime;
    public float Exp;
    public float Deffensive;
}

[System.Serializable]
public struct UnitStat
{
    public StateInfo CurState;
    public List<StateInfo> StateInfoList;
    public string Name;
    public bool IsAlive;

    public Stats NormalStat;
    public Stats Stat;
    public Stats AddStat;
    public Stats PctStat;

    public bool IsFire;
    public float AttackCheckTime;

    public void StatUpdate()
    {
        Stat.Speed = (NormalStat.Speed + AddStat.Speed) * PctStat.Speed;
        Stat.Hp = (NormalStat.Hp + AddStat.Hp) * PctStat.Hp;
        Stat.MaxHp = (NormalStat.MaxHp + AddStat.MaxHp) * PctStat.MaxHp;
        Stat.AttackTime = (NormalStat.AttackTime + AddStat.AttackTime) * PctStat.AttackTime;
        Stat.Exp = (NormalStat.Exp + AddStat.Exp) * PctStat.Exp;
        Stat.Deffensive = (NormalStat.Deffensive + AddStat.Deffensive) * PctStat.Deffensive;
    }
}

public struct StateInfo
{
    StateInfo(UNIT_STATE state, float time, float amount = 0)
    {
        State = state;
        Time = time;
        Amount = amount;
    }

    public UNIT_STATE State;
    public float Time;
    public float Amount;
}
