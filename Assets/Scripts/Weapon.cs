using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage structure
    public int[] damagepoint = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13}; //how much damage is taken by the enemy
    public float[] pushForce = { 1.6f, 1.7f, 1.8f, 1.9f, 2.0f, 2.1f, 2.2f, 2.3f, 2.4f, 2.5f, 2.6f, 2.8f, 3.0f};

    //Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer; //so we can change the sprite of our weapon; changed private -> public so it can be assigned in the inspector

    //Swing logic
    private Animator anim;
    private float cooldown = 0.2f;
    private float lastSwing;
    
    /*override the start function in Collidable (as we've inherited from collidable, we will need to use start for the weapon logic, 
     * but we also need to use the start in collidable.  
     * so overriding will enable us to utilise Start in both scripts - we can override start, add in the existing functionality from Collidable, 
     * then add the weapon logic.
     * */
    protected override void Start() 
    {
        base.Start();// the same as using boxCollider = GetComponent<BoxCollider2D>(); from the Collidable script.
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update() //override rationale here same as for Start
    {
        base.Update();
        //if the time of last swing is greater than cooldown, permitted to swing again.  
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown) 
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;

            //create a new damage object and send it to the fighter we have hit

            Damage damage = new Damage
            {
                damageAmount = damagepoint[weaponLevel], 
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", damage);
            Debug.Log(coll.name);
        }
    }
    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

    }
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void ResetWeapon()
    {
        weaponLevel = 0;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

    }

}
