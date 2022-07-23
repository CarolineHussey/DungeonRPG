using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Text Fields
    public Text levelText, hitpointText, coinsText, upgradeCostText, xpText;

    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //when we get to the end of the list of characters to select
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0; //go back to the beginning

            OnSelectionChange();
        }
        else
        {
            currentCharacterSelection--;

            //when we get to the end of the list of characters to select
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1; //go back to the beginning

            OnSelectionChange();
        }
    }
    private void OnSelectionChange() 
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];//Selects the sprite in the main menu
        GameManager.instance.player.SwapSprite(currentCharacterSelection);//Changes the player sprite in game
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        //if TryUgradeWeapon succeeds, update emnu
        if(GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();


    }

    //update character information (Text fields declared above)
    public void UpdateMenu()
    {
        //Weapon
        // weapon logic will be added here later
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else 
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();
        //Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString(); 
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        coinsText.text = GameManager.instance.coins.ToString();

        //xp bar
        ///There are two states: max level, and not max level. first check if the player is at the max level. 
        ///
        int currentLevel = GameManager.instance.GetCurrentLevel();
        if (currentLevel == GameManager.instance.xpTable.Count) //if current level is equal to the max level
        {
            xpText.text = "MAX LEVEL"; 
            xpBar.localScale = new Vector3(1.0f, 0, 0); //display xp bar as full
        } 
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentLevel - 1);//find out how much xp we needed to get to the last level we reached
            int currLevelXp = GameManager.instance.GetXpToLevel(currentLevel);//get xp that is required to get to the next level

            //declare two fields that we can use to get the completion ratio for our xp bar - xp we have and xp we have to reach for the next level up
            int diff = currLevelXp - prevLevelXp;//the total amount of xp you need to get to next level
            int currentXpIntoLevel = GameManager.instance.experience - prevLevelXp; //finds how much xp we are into our current level

            float completionRatio = (float)currentXpIntoLevel / (float)diff;//make sure this is cast as a float - as we need to pass a float into the first parameter for the Vector3
            xpBar.localScale = new Vector3(completionRatio, 1, 1);//scales the xpBar according to the ratio we just calculated! (we only have to change the x axis)
            xpText.text = currentXpIntoLevel.ToString() + " / " + diff.ToString(); 
        }
    }
}
