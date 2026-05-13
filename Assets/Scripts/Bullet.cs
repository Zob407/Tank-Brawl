using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 25f;
    
    //void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("This is the name of the tank that was shot: "+collision.gameObject.name);
    //    Debug.Log("This is the name of the bullet: "+gameObject.name);
    //    if (collision.gameObject.name == "EnemyTank" && gameObject.name == "TankBullet")
    //    {
    //        //Destroy(gameObject);
    //        return;
    //    }
    //    else
    //    {
    //        Health health = collision.gameObject.GetComponent<Health>();
    //        if (health != null)
    //        {
    //            health.TakeDamage(damage);
    //        } 
    //        Destroy(gameObject);
    //    }
    //}
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("This is the name of the tank that was shot: " + collision.gameObject.name);
        Debug.Log("This is the name of the bullet: " + gameObject.name);
        if (collision.gameObject.name == "EnemyTank" && gameObject.name == "PlayerBullet")
        {
            Health health = collision.gameObject.GetComponent<Health>();//Get the health component of the enemy
            if (health != null)
            {
                health.TakeDamage(damage);//Apply the game of the player to the enemy tank
            }
            Destroy(gameObject);
        }
        else if (collision.gameObject.name == "PlayerTank" && gameObject.name == "EnemyBullet")
        {
            Health health = collision.gameObject.GetComponent<Health>();//Get the health component of the player
            if (health != null)
            {
                health.TakeDamage(damage);//Apply the game of the enemy to the player tank
            }
            Destroy(gameObject);
        }
        //if (collision.gameObject.name == "EnemyTank" && gameObject.name == "EnemyBullet")
        //{
        //    //Destroy(gameObject);
        //    return;
        //}
        //else
        //{
        //    Health health = collision.gameObject.GetComponent<Health>();
        //    if (health != null)
        //    {
        //        health.TakeDamage(damage);
        //    }
        //    Destroy(gameObject);
        //}
        //if (collision.gameObject.name == "PlayerTank" && gameObject.name == "PlayerBullet")
        //{
        //    //Destroy(gameObject);
        //    return;
        //}
        //else
        //{
        //    Health health = collision.gameObject.GetComponent<Health>();
        //    if (health != null)
        //    {
        //        health.TakeDamage(damage);
        //    }
        //    Destroy(gameObject);
        //}
    }
}