using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_State : Enemy_State {

    private Player player;
    private readonly Enemy_State_Pattern enemy;
    private Animator anim;

    public Special_State(Enemy_State_Pattern enemy_State_Pattern) {
        enemy = enemy_State_Pattern;
        anim = enemy.GetComponent<Animator>();
        player = Singleton_Service.GetSingleton<Player>();
    }

    public void StartState() {
        switch (enemy.enemyType)
        {
            case Enemy_State_Pattern.EnemyType.GreenSlime:
                Debug.Log("slime special");
                break;
            case Enemy_State_Pattern.EnemyType.RedSlime:
                Debug.Log("slime special");
                break;
            case Enemy_State_Pattern.EnemyType.GreenBat:
            case Enemy_State_Pattern.EnemyType.RedBat:
                anim.Play(enemy.animSpecialString);
                enemy.transform.GetChild(0).GetComponent<Bat>().StartCoroutine("SpecialAttack");
                break;
            case Enemy_State_Pattern.EnemyType.GreenGhost:
            case Enemy_State_Pattern.EnemyType.WhiteGhost:
                anim.Play(enemy.animSpecialString);
                enemy.gameObject.GetComponent<Ghost>().StartCoroutine("SpecialAttack");
                enemy.isInvincible = true;
                break;
            case Enemy_State_Pattern.EnemyType.GreenRabbit:
            case Enemy_State_Pattern.EnemyType.RedRabbit:
                Debug.Log("rabbit special");
                break;
            case Enemy_State_Pattern.EnemyType.MythDoc:
                enemy.GetComponent<Animation>().Play(enemy.animSpecialString);
                enemy.isInvincible = true;
                break;
            default:
                break;
        }
    }
    public void UpdateState() {
        Updating();
    }
    public void ToIdleState() {
        enemy.currentState = enemy.idleState;
        enemy.currentState.StartState();
        enemy.isInvincible = false;
    }
    public void ToChaseState() {
        enemy.currentState = enemy.chaseState;
        enemy.currentState.StartState();
        enemy.isInvincible = false;
    }
    public void ToAttackState()
    {
        enemy.currentState = enemy.attackState;
        enemy.currentState.StartState();
        enemy.isInvincible = false;
    }
    public void ToHurtState()
    {
        enemy.currentState = enemy.hurtState;
        enemy.currentState.StartState();
        enemy.isInvincible = false;
    }

    public void ToDieState()
    {
        enemy.currentState = enemy.dieState;
        enemy.currentState.StartState();
        player.IncreaseSkulls(1);
        enemy.isInvincible = false;
    }

    public void ToSpecialState()
    {
        Debug.Log("Same state");
    }
    // Update is called once per frame
    void Updating () {

	}
}
