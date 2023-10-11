using System;
using System.Collections;
using UnityEngine;

public interface ITimeable
{
    void StartTimeCoroutine();
    void StopAllCoroutines();
}

public interface IDamageable
{
    void Damage(float amount);
}

public interface IHealable
{
    void Heal(float amount);
}

public class TimeManager : Progressive, IDamageable, IHealable, ITimeable
{
    private bool isTimeCoroutineRunning = false;
    public Action OnHitAction;
    public Action OnDeathAction;


    [HideInInspector]
    public int multiplyer = 1;

    private void Start()
    {
        StartTimeCoroutine();
    }

    private bool CheckMaxValue()
    {
        if (Value >= MaxValue) return true;
        return false;
    }

    private bool CheckMinValue()
    {
        if (Value <= 0) return true;
        return false;
    }

    public void StartTimeCoroutine()
    {
        if (isTimeCoroutineRunning)
            return;
        isTimeCoroutineRunning = true;
        StopAllCoroutines();
        StartCoroutine(TimeCoroutine());
    }

    private IEnumerator TimeCoroutine()
    {
        Value -= Time.deltaTime;
        if (CheckMinValue()) OnDeath();

        yield return new WaitForSeconds(0);
        StartCoroutine(TimeCoroutine());
    }

    public void Damage(float damageAmount)
    {
        OnHitAction?.Invoke();
        if (damageAmount > Value)
            damageAmount = Value;

        Value -= damageAmount * multiplyer;

        if (CheckMinValue()) OnDeath();
    }

    public void Heal(float healAmount)
    {
        if (CheckMaxValue())
            return;

        if (healAmount > MaxValue - Value)
            healAmount = MaxValue - Value;
        Value += healAmount;
    }

    private void OnDeath()
    {
        StopAllCoroutines();
        isTimeCoroutineRunning = false;
        Value = 0f;
        OnDeathAction?.Invoke();
    }
}
