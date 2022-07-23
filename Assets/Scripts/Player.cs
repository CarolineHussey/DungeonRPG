using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{

    private SpriteRenderer spriteRenderer;
    private bool isKO = false;

    protected override void Start()
    {
        base.Start(); 
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }

    protected override void ReceiveDamage(Damage damage)
    {
        if (isKO)
            return;
        
        base.ReceiveDamage(damage); //this ensures that the rest of the function stays the same
        GameManager.instance.OnHitPointChange();
    }

    protected override void KnockOut()
    {
        isKO = true;
        GameManager.instance.KOAnimator.SetTrigger("Show");
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (!isKO)
            UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinID)
    {
       spriteRenderer.sprite = GameManager.instance.playerSprites[skinID]; //controls the rendered player sprite 
    }

    public void OnLevelUp()
    {
        maxHitPoint++;
        hitPoint = maxHitPoint;
        GameManager.instance.ShowText("Level Up!", 30, Color.blue, transform.position, Vector3.up * 40, 1.0f);
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            maxHitPoint++;
            hitPoint = maxHitPoint;
    }

    public void Heal(int healingAmount)
    {
        if(hitPoint < maxHitPoint)
        {
            hitPoint += healingAmount;
            GameManager.instance.ShowText("+" + healingAmount.ToString() + " hitpoints", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
            GameManager.instance.OnHitPointChange();
        }

        else
        {
            hitPoint = maxHitPoint;
            GameManager.instance.ShowText("Full Health", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
        }
            
            
    } 

    public void Respawn()
    {
        Heal(maxHitPoint);
        isKO = false;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;

    }

    public void ResetPlayer()
    {
        isKO = false;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
        hitPoint = 10;
        maxHitPoint = 10;
        SetLevel(0);

    }


}
