using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiEnemy : MonoBehaviour
{
    [SerializeField] private int speed;

    [SerializeField] private int distanceOfPatrol;
    [SerializeField] private Transform pointOfPatrol;
    [SerializeField] private float stopDistanceOfPatrol;
    [SerializeField] private Transform stopChase;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform graphics;
    [SerializeField] private Transform helpers;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float rangeAttack = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private float coolDownAttack = 1f;

    private bool movementRight;
    private Transform player;
    private Rigidbody2D rigidbody;
    private float currentDirection = 1;
    private float currentDirectionChase = 1;
    private Vector2 currentPosition;
    private Vector2 enemyPos;
    private bool attack = true;
    private RaycastHit2D hit;



    bool patrolling = false;
    bool canChase = true;
    bool chase = false;
    bool goBack = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        if (patrolling || chase || goBack)
        {
            animator.SetBool("Walk", true);            
        }        
        if (CanAttack())
        {
            Attack();            
        }       
    }

    private void FixedUpdate()
    {
        currentPosition = transform.position;        

        if (Vector2.Distance(transform.position, pointOfPatrol.position) < distanceOfPatrol && chase == false)
        {
            patrolling = true;           
        }

        if (Vector2.Distance(transform.position, player.position) < stopDistanceOfPatrol && canChase)
        {
            chase = true;
            patrolling = false;
            goBack = false;            
        }

        if (Vector2.Distance(transform.position, player.position) > stopDistanceOfPatrol)
        {
            goBack = true;
            chase = false;           
        }
        if (transform.position.x < stopChase.position.x)
        {
            canChase = false;
            chase = false;
            goBack = true;
            StartCoroutine(CanChangeChase());
        }


        if (patrolling == true)
        {
            Patrolling();            
        }
        else if (chase == true)
        {
            Chase();            
        }
        else if (goBack == true)
        {
            GoBack();           
        }        

        if (currentPosition.x > transform.position.x)
        {
            currentDirectionChase = -1;
        }
        if (currentPosition.x < transform.position.x)
        {
            currentDirectionChase = 1;
        }        
    }

    private bool CanAttack()
    {
        hit = Physics2D.Raycast(shootPoint.position, shootPoint.right, rangeAttack);        
        
        if (hit.collider!= null)
        {
            if (hit.collider.GetComponent<PlayerMovement>() != null)
            {                
                return true;
            }
            return false;
        }
        else
        {            
            return false;            
        }
    }


    public void Attack()
    {
        if (attack == true)
        {
            attack = false;
            animator.SetTrigger("Attack");
            SetDamage();
            Invoke("AttackReset", coolDownAttack);
        }
    }

    private void AttackReset()
    {
        attack = true;
    }

    private void SetDamage()
    {        
        Health target = null;       

        if (hit.collider != null)
        {
            target = hit.transform.root.GetComponent<Health>();
            target?.Hit(damage);
        }
    }


    private void Patrolling()
    {
        if (transform.position.x > pointOfPatrol.position.x + distanceOfPatrol)
        {
            movementRight = false;
            currentDirection = -1;
        }
        else if (transform.position.x < pointOfPatrol.position.x - distanceOfPatrol)
        {
            movementRight = true;
            currentDirection = 1;
        }

        if (movementRight)
        {           
            ChangeTransformPatrolling(currentDirection);            
        }
        else
        {            
            ChangeTransformPatrolling(currentDirection);            
        }
    }

    private void Chase()
    {        
        ChangeTransformChaseGoBack(player, currentDirectionChase);
    } 

    private void GoBack()
    {       
        ChangeTransformChaseGoBack(pointOfPatrol, currentDirectionChase);
    }

    
    IEnumerator CanChangeChase()
    {
        yield return new WaitForSeconds(0.5f);
        canChase = true;
    }    

    private void ChangeTransformPatrolling(float currentDirection)    
    {        
        ChangeGraphics(currentDirection);
        transform.position = new Vector2(transform.position.x + speed*currentDirection * Time.deltaTime, transform.position.y); 
    }

    private void ChangeTransformChaseGoBack(Transform target, float currentDirection)
    {        
        ChangeGraphics(currentDirection);        
        enemyPos = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.position = new Vector2(enemyPos.x, transform.position.y);
    }   

    private void ChangeGraphics(float currentDirection)
    {
        graphics.localScale = new Vector3(currentDirection, 1f, 1f);
        float xAngle = currentDirection > 0 ? 0f : 180f;
        helpers.localEulerAngles = new Vector3(0f, xAngle, 0f);
    }
}
