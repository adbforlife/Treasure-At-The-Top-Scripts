using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy_State	{
	void StartState();
	void UpdateState();
	void ToIdleState();
	void ToChaseState();
    void ToAttackState();
    void ToHurtState();
    void ToDieState();
    void ToSpecialState();
}