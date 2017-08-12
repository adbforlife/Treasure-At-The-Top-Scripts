using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_State : Enemy_State {

    private Player player;
    private readonly Enemy_State_Pattern enemy;
    private Animator anim;

    public Chase_State(Enemy_State_Pattern enemy_State_Pattern) {
        enemy = enemy_State_Pattern;
        anim = enemy.GetComponent<Animator>();
        player = Singleton_Service.GetSingleton<Player>();
    }

    public void StartState() {
        if (enemy.canHaveAnimator)
        {
            anim.Play(enemy.animMoveString);
        }
        else
        {
            enemy.GetComponent<Animation>().Play(enemy.animMoveString);
        }
    }
    public void UpdateState() {
        Updating();
    }
    public void ToIdleState() {
        enemy.currentState = enemy.idleState;
        enemy.currentState.StartState();
    }
    public void ToChaseState() {

    }
    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
        enemy.currentState.StartState();
    }
    public void ToHurtState()
    {
        if (enemy.animHurtString != null)
        {
            enemy.currentState = enemy.hurtState;
            enemy.currentState.StartState();
        }
    }

    public void ToDieState()
    {
        enemy.currentState = enemy.dieState;
        enemy.currentState.StartState();
        player.IncreaseSkulls(1);
    }

    public void ToSpecialState()
    {
        enemy.currentState = enemy.specialState;
    }
    // Update is called once per frame
    void Updating () {
        Vector3 deltaPos = (player.transform.position - enemy.transform.position) * enemy.moveSpeed * enemy.waitTime;
        enemy.transform.position += new Vector3(deltaPos.x, 0f, deltaPos.z);
        if (Vector3.Distance(player.transform.position, enemy.transform.position) < enemy.distanceThreshold)
        {
            ToAttackState();
        }
	}
}
