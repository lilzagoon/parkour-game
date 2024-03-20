using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTiredState : BossBaseState
{
    private BossStateManager _stateManager;

    public override void EnterState(BossStateManager boss)
    {
        boss.rb.mass = 10f;
        boss.speed = 0f;
    }

    public override void UpdateState(BossStateManager boss)
    {
   
    }

    public override void OnTrig(BossStateManager boss, Collider other)
    {

    }
}