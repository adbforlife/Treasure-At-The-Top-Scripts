using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die_State : Enemy_State {

    private Player player;
    private SceneController scene;
    private readonly Enemy_State_Pattern enemy;
    private Animator anim;

    public Die_State(Enemy_State_Pattern enemy_State_Pattern) {
        enemy = enemy_State_Pattern;
        anim = enemy.GetComponent<Animator>();
        player = Singleton_Service.GetSingleton<Player>();
        scene = Singleton_Service.GetSingleton<SceneController>();
    }

    public void StartState() {
        if (enemy.canHaveAnimator)
        {
            anim.Play(enemy.animDieString);
        }
        else
        {
            enemy.GetComponent<Animation>().Play(enemy.animDieString);
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
        Debug.Log("Don't change states if the monster's dead");
    }
    public void ToDieState()
    {
        Debug.Log("same state");
    }

    public void ToSpecialState()
    {
        enemy.currentState = enemy.specialState;
    }
    // Update is called once per frame
    void Updating () {
        
    }
}
