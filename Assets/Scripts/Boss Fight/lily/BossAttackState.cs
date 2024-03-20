using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    private BossStateManager _stateManager;
    public override void EnterState(BossStateManager boss)
    {
        boss.timer = 2f;
        boss.timerRunning = false;
        Debug.Log("Entering attack state");
    }

    public override void UpdateState(BossStateManager boss)
    {
        while (boss.timer > 0f)
        {
            boss.speed = 0f;
            boss.punchTrigger.enabled = true;
            boss.timer = boss.timer -= Time.deltaTime;
        }

        
        if (boss.timer <= 0f)
        {
            boss.SwitchState(boss.ChaseState);
        }
    }

    public override void OnTrig(BossStateManager boss, Collider other)
    {
        
    }

}
