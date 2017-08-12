using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_State : MonoBehaviour, Enemy_State {

    private Player player;
    private readonly Enemy_State_Pattern enemy;
    private Animator anim;

  
	public Attack_State(Enemy_State_Pattern enemy_State_Pattern)	{
		enemy = enemy_State_Pattern;
        anim = enemy.GetComponent<Animator>();
        player = Singleton_Service.GetSingleton<Player>();
    }
	public void StartState()	{
        if (enemy.canHaveAnimator)
        {
            anim.Play(enemy.animAttackString);
        }
        else
        {
            enemy.GetComponent<Animation>().Play(enemy.animAttackString);
        }
    }
	public void UpdateState()	{
		Updating ();
	}
	public void ToIdleState()	{
        enemy.currentState = enemy.idleState;
        enemy.currentState.StartState();
    }
	public void ToChaseState()	{
		enemy.currentState = enemy.chaseState;
        enemy.currentState.StartState();
    }
    public void ToAttackState()
    {
        Debug.Log("Same State");
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
        if (Vector3.Distance(player.transform.position, enemy.transform.position) > enemy.distanceThreshold)
        {
            ToChaseState();
        }
    }
}
