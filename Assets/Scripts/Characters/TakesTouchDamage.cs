using UnityEngine;

public class TakesTouchDamage : MonoBehaviour
{
    // iframes dur should come from character stats
    public float damageCooldown = 1.0f;
    private float lastDamageTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() is Enemy enemy
            && Time.time > lastDamageTime + damageCooldown)
        {
            DealDamage(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() is Enemy enemy 
            && Time.time > lastDamageTime + damageCooldown)
        {
            DealDamage(collision);
        }
    }

    private void DealDamage(Collider2D player)
    {

        Debug.Log("player hit");
        //PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        //if (playerHealth != null)
        //{
        //    playerHealth.TakeDamage(damageAmount);
        //    lastDamageTime = Time.time;
        //}
    }
}
