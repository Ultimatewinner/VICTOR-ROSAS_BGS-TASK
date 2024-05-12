using System.Collections;
using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private State state;
    private EnemyMovement enemyMovement;
    private Vector3 startingPos;
    private float roamingRadius = 5f;
    private Animator anim;
    public MoneyManager moneyManager;

    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        state = State.Roaming;
        startingPos = transform.position;
    }

    private void Start()
    {
        StartCoroutine(Roaming());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Sword"))
        {
            anim.SetBool("Death", true);
            moneyManager.AddMoney(50);
            enemyMovement.enabled = false;
        }
    }

    private IEnumerator Roaming()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPos = GetRoamingPos();
            enemyMovement.TranslateTowards(roamPos);
            yield return new WaitForSeconds(Random.Range(1f, 3f)); 
        }
    }

    private Vector2 GetRoamingPos()
    {
        Vector2 randomDir = Random.insideUnitCircle * roamingRadius;
        return startingPos + new Vector3(randomDir.x, randomDir.y);
    }
}