using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateManager : MonoBehaviour
{

    public Transform player;
    public float speed;
    public float timer;
    public bool timerRunning;
    public Collider punchTrigger;
    public IEnumerator attackCoroutine;
    public float tiredTimer;
    public Rigidbody rb;

    private BossBaseState currentState;
    public BossChaseState ChaseState = new BossChaseState();
    public BossAttackState AttackState = new BossAttackState();
    public BossTiredState TiredState = new BossTiredState();
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        currentState = ChaseState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        Debug.Log("Current state = " + currentState);
    }

    public void SwitchState(BossBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            currentState.OnTrig(this, other);
        }
    }
}
