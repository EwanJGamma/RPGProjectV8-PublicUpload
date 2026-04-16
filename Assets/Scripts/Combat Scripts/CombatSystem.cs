using System.Collections;
using Unity.VisualScripting;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum CombatState {NULL, START, PTURN, ETURN, VICTORY, DEFEAT }
//This holds the states we'll be using during the battle so the script knows what it should do next based on the actions taken by the player or enemy.



public class CombatSystem : MonoBehaviour
{
    //This is the combat script, it handles all the stuff we need for setting up combat and making it run smoothly. Including the UI.
    public GameObject playerCombatPrefab;
    public GameObject enemyCombatPrefab;
    public CombatState state;
    public GameObject playerHUD;
    public GameObject SkillMenu;
    public GameObject CombatMenu;
    public GameObject BackButton;
    public GameObject GuardButton;
    public GameObject SlashButton;
    public GameObject MundareButton;
    public HPSPManagement hpspManagement;
    public Unit Unit;
    public PlayerHUD PlayerHUD; 
    public int sceneBuildIndex;
    public EnemyDestroyed enemyDestroyed;
    public EnemyHandler enemyHandler;
    public bool isLevel25;
    public bool isLevel50;
    public int ranNum;

    //This sends the information of the combat stations to the script so we can always put the Player / Enemy in the right place.
    public Transform playerCombatStation;
    public Transform enemyCombatStation;

    //These are the Player and Enemy units and hold all the stats like health, damage and attack.
    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;
    //The dialogue that appears at the top of the screen

    public bool inCombat;
    public bool guard;

    void Start()
    {
        state = CombatState.NULL;
        StartCoroutine(CombatSetup());
        //This makes the Combat system flow as turn based, Setting the state to combat start
    }


    IEnumerator CombatSetup()
    {
        GameObject playerCombatGO = Instantiate(playerCombatPrefab, playerCombatStation);
        playerUnit = playerCombatGO.GetComponent<Unit>();

        //increases the players HP to Level 25 values if the player has activated the correct Level to fight Raging Tiger, Demonstrating Difficulty scaling
        if (isLevel25 == true)
        {
            playerUnit.HDmg(500);
            playerUnit.maxHP += 500;
        }

        GameObject enemyCombatGO = Instantiate(enemyCombatPrefab, enemyCombatStation);
        enemyUnit = enemyCombatGO.GetComponent<Unit>();

        dialogueText.text = "The " + enemyUnit.unitName + " draw's closer...";

        yield return new WaitForSeconds(2f);

        dialogueText.text = "You brace for Combat";
        state = CombatState.PTURN;


    }

    void PTURN()
    {
        dialogueText.text = "What will " + playerUnit.unitName + " do...?";
    }

    IEnumerator PAttack()
    {


        //Attacks the Enemy
        bool isDead = enemyUnit.TDmg(playerUnit.damage);
        dialogueText.text = "You Strike the " + enemyUnit.unitName + " for " + playerUnit.damage + " damage!";
        yield return new WaitForSeconds(2f);
        //Is the enemy dead now?
        if (isDead)
        {
            dialogueText.text = "You have defeated the " + enemyUnit.unitName + "!";
            yield return new WaitForSeconds(2f);
            state = CombatState.VICTORY;
            EndCombat();
        }
        else
        {
            state = CombatState.ETURN;
            StartCoroutine(EAttack());
        }
    }
    IEnumerator SlashAttack()
    {
        bool isDead = enemyUnit.TDmg(playerUnit.slashDamage);
        dialogueText.text = "You Strike the " + enemyUnit.unitName + " for " + playerUnit.slashDamage + " damage!";
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            dialogueText.text = "Your slash has slain the " + enemyUnit.unitName + "!";
            yield return new WaitForSeconds(2f);
            state = CombatState.VICTORY;
            EndCombat();
        }
        else
        {
            state = CombatState.ETURN;
            StartCoroutine(EAttack());
        }
    }
    IEnumerator MundareAttack()
    {
        bool isDead = enemyUnit.TDmg(playerUnit.mundareDamage);
        dialogueText.text = "You burn the " + enemyUnit.unitName + " for " + playerUnit.mundareDamage + " damage!";
        yield return new WaitForSeconds(2f);
        if (isDead)
        {
            dialogueText.text = "Your flames ashen the " + enemyUnit.unitName + "!";
            yield return new WaitForSeconds(2f);
            state = CombatState.VICTORY;
            EndCombat();
        }
        else
        {
            state = CombatState.ETURN;
            StartCoroutine(EAttack());
        }
    }

    IEnumerator EAttack()
    {
        //This causes the enemy to have a Unique "Boss" Attack if the Enemy is Raging Tiger, causing it to roar and drain your spirit
        if (enemyUnit.unitName == "RagingTiger")
        {
            dialogueText.text = "The " + enemyUnit.unitName + " lets out a mighty roar...";
            yield return new WaitForSeconds(2f);
            dialogueText.text = "The " + enemyUnit.unitName + "'s roar fills you with dread, sapping your spirit...";
            playerUnit.SPCost(enemyUnit.roarSpiritDamage);
            PlayerHUD.SetSP(playerUnit.SP);


        }
        //This makes the Enemy do many things, first off it makes the Physical attack of the player & boss damage by 10, but boosts both of your holy abilities by larger increments
        //It also makes the Boss have a chance to use one of 3 different attacks, 1 of which scales more, 1 which scales a little and 1 which is a gradually weakening physical attack
        if (enemyUnit.unitName == "DivineGeneral")
        {
            dialogueText.text = "The " + enemyUnit.unitName + " chimes his bell";
            yield return new WaitForSeconds(2f);
            dialogueText.text = "The " + enemyUnit.unitName + "'s bell decreases physical and boosts holy damage";
            playerUnit.damage = playerUnit.damage - 10;
            enemyUnit.damage = enemyUnit.damage - 10;
            playerUnit.mundareDamage = playerUnit.mundareDamage + 50;
            enemyUnit.angelPiercerDamage = enemyUnit.angelPiercerDamage + 50;
            enemyUnit.bloodSpearDamage = enemyUnit.bloodSpearDamage + 25;
            if (enemyUnit.damage < 10)
            {
                enemyUnit.damage = 10;
            }
            yield return new WaitForSeconds(2f);
            ranNum = Random.Range(1, 4);

            {
                if (ranNum == 1)
                {
                    dialogueText.text = "The " + enemyUnit.unitName + " casts Angel Piercer!";
                    yield return new WaitForSeconds(2f);
                    playerUnit.TDmg(enemyUnit.angelPiercerDamage);
                    PlayerHUD.SetHP(playerUnit.HP);
                }
                else if (ranNum == 2)
                {
                    dialogueText.text = "The " + enemyUnit.unitName + " casts Blood Spear!";
                    yield return new WaitForSeconds(2f);
                    playerUnit.TDmg(enemyUnit.bloodSpearDamage);
                    PlayerHUD.SetHP(playerUnit.HP);
                }           
                else
                { 
                    dialogueText.text = "The " + enemyUnit.unitName + "thrusts with his Twinblades";
                    yield return new WaitForSeconds(2f);
                    playerUnit.TDmg(enemyUnit.damage);
                    PlayerHUD.SetHP(playerUnit.HP);
                }
            }             
        }
    

            dialogueText.text = "The " + enemyUnit.unitName + " strikes...";

            yield return new WaitForSeconds(2f);

            //This checks if the Player is Guarding, Halves DMG taken if they are, and then applies the damage to the player.

            if (guard == false)
            {
                playerUnit.TDmg(enemyUnit.damage * 2);
            }

            bool isDead = playerUnit.TDmg(enemyUnit.damage);
            PlayerHUD.SetHP(playerUnit.HP);

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = CombatState.DEFEAT;
                EndCombat();
            }

            //This makes sure the player isnt accidentally kept  guarding across multiple turns.

            else
            {
                guard = false;
                state = CombatState.PTURN;
                PTURN();
            }

        }

        void EndCombat()
        {
            if (state == CombatState.VICTORY)
            {
                //Since the Player wins on his turn, the call of the Enemy to use up the Guard button isnt used, so we reset it here on victory incase the player wins by 
                //Something such as Tick Damage or another character killing the enemy while the player's guarding. Preventing Guarding across battles.
                //Updated to check which enemy was defeated and set the bool to correctly identify it in the Overworld.
                guard = false;

                if (enemyUnit.unitName == "Guardsman")
                {
                    EnemyHandler.guardsman1 = true;
                }
                if (enemyUnit.unitName == "RagingTiger")
                {
                    EnemyHandler.ragingtiger1 = true;
                }
                if (enemyUnit.unitName == "DivineGeneral")
                {
                    EnemyHandler.divinegeneral1 = true;
                }
            SceneManager.LoadScene(0);
            }
            else if (state == CombatState.DEFEAT)
            {
                dialogueText.text = "You have been defeated by the " + enemyUnit.unitName + "...";


            }
        }
    


    public void OnAttackButton()
    {
        if (state != CombatState.PTURN)
            return;
        if (state == CombatState.PTURN)
            StartCoroutine(PAttack());
    }

    public void OnSkillButton()
    {
        if (state != CombatState.PTURN)
            return;
        if (state == CombatState.PTURN)
            CombatMenu.SetActive(false);
            SkillMenu.SetActive(true);
    }

    public void OnBackButton()
    {
        if (state != CombatState.PTURN)
            return;
        if (state == CombatState.PTURN)
            CombatMenu.SetActive(true);
        SkillMenu.SetActive(false);
    }

    public void OnGuardButton()
    {
        if (state != CombatState.PTURN)
            return;
        if (state == CombatState.PTURN)
        {
            if (playerUnit.SP < playerUnit.guardCost)
            {
                dialogueText.text = "Not enough Spirit to Guard!";
                return;
            }
            else
            {
                playerUnit.SPCost(playerUnit.guardCost);
                PlayerHUD.SetSP(playerUnit.SP);
                guard = true;
                dialogueText.text = "You brace yourself for the next attack...";
                CombatMenu.SetActive(true);
                SkillMenu.SetActive(false);
                state = CombatState.ETURN;
                StartCoroutine(EAttack());
            }
        }
    }
    public void OnSlashButton()
    {
        if (state != CombatState.PTURN)
            return;
        if (state == CombatState.PTURN)
        {
            if (playerUnit.SP < playerUnit.mundareCost)
            {
                dialogueText.text = "Not enough Spirit to Cast Mundare";
                return;
            }
            else
            {
                playerUnit.SPCost(playerUnit.slashCost);
                PlayerHUD.SetSP(playerUnit.SP);
                StartCoroutine(SlashAttack());
                CombatMenu.SetActive(true);
                SkillMenu.SetActive(false);
            }
        }
    }
    public void OnMundareButton()
    {
        if (state != CombatState.PTURN)
            return;
        if (state == CombatState.PTURN)
        {
            if (playerUnit.SP < playerUnit.mundareCost)
            {
                dialogueText.text = "Not enough Spirit to Cast Mundare";
                return;
            }
            else
            {
                playerUnit.SPCost(playerUnit.mundareCost);
                PlayerHUD.SetSP(playerUnit.SP);
                StartCoroutine(MundareAttack());
                CombatMenu.SetActive(true);
                SkillMenu.SetActive(false);
            }
        }
    }
}
