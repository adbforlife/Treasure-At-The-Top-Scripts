﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt_State : Enemy_State {

    private Player player;
    private readonly Enemy_State_Pattern enemy;
    private Animator anim;

    public Hurt_State(Enemy_State_Pattern enemy_State_Pattern) {
        enemy = enemy_State_Pattern;
        anim = enemy.GetComponent<Animator>();
        player = Singleton_Service.GetSingleton<Player>();
    }

    public void StartState() {
        if (enemy.canHaveAnimator)
        {
            anim.Play(enemy.animHurtString);
        }
        else
        {
            enemy.GetComponent<Animation>().Play(enemy.animHurtString);
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
        enemy.currentState = enemy.chaseState;
        enemy.currentState.StartState();
    }
    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
        enemy.currentState.StartState();
    }
    public void ToHurtState()
    {
        if (enemy.GetTempNumDamages() <= enemy.GetTolerance() && enemy.animHurtString != null)
        {
            //enemy.currentState.StartState();
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

	}
}
