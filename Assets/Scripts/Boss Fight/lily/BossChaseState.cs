using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState: BossBaseState
{
    private BossStateManager _stateManager;
    
    public override void EnterState(BossStateManager boss)
    {
        boss.timer = 2f;
        boss.speed = 20f;
        boss.tiredTimer = 5f;
    }

    public override void UpdateState(BossStateManager boss)
    {
        boss.transform.position = Vector3.MoveTowards(boss.transform.position, boss.player.position,
            boss.speed * Time.deltaTime);
        boss.transform.LookAt(boss.player);
        boss.tiredTimer = boss.tiredTimer -= Time.deltaTime;

        if (boss.tiredTimer <= 0)
        {
            boss.SwitchState(boss.TiredState);
        }
    }
    
    public override void OnTrig(BossStateManager boss, Collider other)
    {
        Debug.Log("collding less wahoo");
        if (other.tag == "Player")
        {
            Debug.Log("colliding yippee wahoo");
            boss.SwitchState(boss.AttackState);
        }
    }

}
