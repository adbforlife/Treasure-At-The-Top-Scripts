using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle_State : MonoBehaviour, Enemy_State {

    private Player player;
    private RightHand rightHand;
    private readonly Enemy_State_Pattern enemy;
    private Animator anim;

    public Idle_State(Enemy_State_Pattern enemy_State_Pattern)	{
        enemy = enemy_State_Pattern;
        anim = enemy.GetComponent<Animator>();
        player = Singleton_Service.GetSingleton<Player>();
        rightHand = Singleton_Service.GetSingleton<RightHand>();
    }
	public void StartState(){
        if (enemy.canHaveAnimator)
        {
            anim.Play(enemy.animIdleString);
        }
        else
        {
            enemy.GetComponent<Animation>().Play(enemy.animIdleString);
        }
    }
	public void UpdateState(){
		Updating ();
	}
	public void ToIdleState()	{
		Debug.Log ("Same State");
	}
	public void ToChaseState()	{
		enemy.currentState = enemy.chaseState;
        //rightHand.StartCoroutine(rightHand.LongVibration(0.5f, 1));
        enemy.currentState.StartState();
        switch (enemy.enemyType)
        {
            case Enemy_State_Pattern.EnemyType.MythDoc:
                enemy.StartCoroutine("MythDocSkill");
                break;
            default:
                break;
        }
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
        /*if (rightHand.transform.position.y > player.transform.position.y + 1.9f)
        {
            ToChaseState();
        }*/
    }
}
