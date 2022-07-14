using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Gamemanager is a script that is recognised in Unity
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        //prevent duplication of GameManager (caused by DontDestroOnLoad) in the event that the same scene is loaded more than once.  
        if (GameManager.instance != null) //if there is already an instance of GameManager, then don't create another!
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        instance = this; //once the game starts, we will assign this to the gameManager it finds in the scene
        SceneManager.sceneLoaded += LoadState; //The function is called when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable; //a table of xp required to get to the next level

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator KOAnimator;
    public GameObject hud;
    public GameObject menu;

    //Logic
    public int coins;
    public int experience;

    //allows us to access the floating Text from anywhere in the code
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    /// <summary>
    /// this function will be called by the menu.  if it is false, we are not going to change anything in the menu. if it returns true, it will try to upgrade weapon - update sprite and variables. 
    /// </summary>
    /// <returns></returns>
    /// 
    //when the player clicks on the upgrade button, this function will be called.  
    public bool TryUpgradeWeapon()
    {
        //is weapon max level? if true, we can't upgrade as we already have the top upgrade! 
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;
        //do we have enough coins to upgrade? compare upgrade cost to amount of coins held.
        if(coins >= weaponPrices[weapon.weaponLevel]) //if we have enough coins
        {
            coins -= weaponPrices[weapon.weaponLevel]; 
            weapon.UpgradeWeapon(); //call UpgradeWeapon() to implement
            return true;
        }
        //else if we don't have enough coins but our weapon is not at the top level...
        return false;
    }

    private void Update()
    {
        //Debug.Log(GetCurrentLevel()); //to test that the function works!
    }
    
    //hitpoint bar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    }
    
    //Experience system

    public int GetCurrentLevel()
    {
        int r = 0;//level (return value)
        int next = 0;//how much xp we need to progress

        while (experience >= next) //while experience is >= next
        {
            next += xpTable[r];//increment next by xp required for next level (level = index of xpTable)
            r++;//level up

            if (r == xpTable.Count) //if we are at the max level
                return r;
        }
        return r;   
    }
    public int GetXpToLevel(int level) //get the total xp to reach the level passed as an arguement
    {
        int r = 0;//current level
        int xp = 0;// xp 

        while(r < level) //current level < next level
        {
            xp += xpTable[r]; // xp += xp required for next level
            r++;
        }
        return xp;
    }
    
    public void GrantXp(int xp)
    {
        int currentlevel = GetCurrentLevel();//first check level
        experience += xp;//then grant xp
        if (currentlevel < GetCurrentLevel()) //if our current level is higher than the level we were before we got the xp, then
            OnLevelUp();//level up!
    }

    public void OnLevelUp()
    {
        //Debug.Log("OnLevelUp");
        player.OnLevelUp();
        OnHitPointChange();
    }
    
    // On Scene Load

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    //KO Menu

    public void Respawn()
    {
        KOAnimator.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        player.Respawn();
    }
    public void SaveState() 
    {
        string s = "";
        s += "empty" + "|"; //preferedSkin
        s += coins.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString(); //weaponLevel
        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState; 
        
        if (!PlayerPrefs.HasKey("SaveState")) //if there is no SaveState
            return; //do nothing
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //Change player skin (placeholder)
        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]); //gets the experience
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());//gets current player level
        weapon.SetWeaponLevel(int.Parse(data[3])); //gets the weapon.  

      
    }
    public void WipeGameData()
    {
        KOAnimator.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        player.Respawn();
        coins = 0;
        experience = 0; 
        player.hitPoint = 10;
        player.maxHitPoint = 10;
        player.SetLevel(1);
        weapon.SetWeaponLevel(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
