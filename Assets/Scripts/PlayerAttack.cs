using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform skillPoint;
    [SerializeField] private int attackDamage = 1;    
    [SerializeField] private float attackRange = 1;    
    [SerializeField] private Animator animator;
    [SerializeField] private float coolDownAttack;
    [SerializeField] private float coolDownSkill;
    [SerializeField] private GameObject skillPrefab;

    private bool attack = true;
    private bool skill = true;  

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();            
        }  
        
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {            
            Skill();
        }
    }


    private void Attack()
    {
        if (attack == true)
        {
            attack = false;
            animator.SetTrigger("Attack");
            SetDamage(attackDamage,attackRange);
            Invoke("AttackReset", coolDownAttack);
        }
    }

    private void AttackReset()
    {
        attack = true;
    }    

    private void Skill()
    {
        if (skill == true)
        {
            skill = false;
            animator.SetTrigger("Skill");           
            Invoke("SkillReset", coolDownSkill);
        }

    }

    private void SkillReset()
    {
        skill = true;
    }

    public void SkillInstitate()
    {
        Instantiate(skillPrefab, skillPoint.position, skillPoint.rotation);
    }



    private void SetDamage(int damage, float range)
    {
        Health target = null;
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.right, range);

        if (hit.collider != null)
        {
            target = hit.transform.root.GetComponent<Health>();
            target?.Hit(damage);
        }        
    }
}
