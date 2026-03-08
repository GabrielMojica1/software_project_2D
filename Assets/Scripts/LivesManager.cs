using UnityEngine;
using System.Collections;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;

    public float invincibleTime = 2f;

    void Awake()
    {
        instance = this;
    }

    public void PlayerHit(Ship ship)
    {
        if (ship.isInvincible) return; 

        ship.lives--;
        LivesUI.instance.UpdateLives(ship.lives);

        if (ship.lives <= 0)
        {
            Destroy(ship.gameObject);
        }
        else
        {
            StartCoroutine(Invincibility(ship));
        }
    }

    IEnumerator Invincibility(Ship ship)
    {
        ship.isInvincible = true;
        SpriteRenderer sr = ship.shipSprite; 
        float timer = 0f;
        while (timer < invincibleTime)
        {
            sr.enabled = !sr.enabled; 
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }

        sr.enabled = true;
        ship.isInvincible = false;
    }
}