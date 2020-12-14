using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public TextMesh hpText;
    public int initHp;

    public Image hpImage;

    public Image DamageBackground;

    public Color damageColor;
    
    int currHp;
    float currAmont;
    float decreaseRate = 0.003f;

    // Start is called before the first frame update
    void Start()
    {
        if(GlobalInfo.currDifficulty == GlobalInfo.Difficulty.Easy) initHp = GlobalInfo.easy_hp;
        if(GlobalInfo.currDifficulty == GlobalInfo.Difficulty.Normal) initHp = GlobalInfo.normal_hp;
        if(GlobalInfo.currDifficulty == GlobalInfo.Difficulty.Hard) initHp = GlobalInfo.hard_hp;
        if(GlobalInfo.currDifficulty == GlobalInfo.Difficulty.Lunatic) initHp = GlobalInfo.lunatic_hp;
        if(GlobalInfo.currDifficulty == GlobalInfo.Difficulty.Extra) initHp = GlobalInfo.extra_hp;
        if(GlobalInfo.currDifficulty == GlobalInfo.Difficulty.Phantasm) initHp = GlobalInfo.phantasm_hp;
        currHp = initHp;
        hpText.text = "hp : " + currHp.ToString();
        Debug.Log("collider start");
        hpImage.fillAmount = 1.0f;
        currAmont = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        hpText.text = "hp : " + currHp.ToString();
        float targetAmont = 1.0f * currHp / initHp;
        if(currAmont > targetAmont)
            currAmont -= decreaseRate;
        else
            currAmont = targetAmont;
        hpImage.fillAmount = currAmont;
    }

    public void ResetHp()
    {
        currHp = initHp;
    }

    IEnumerator getDamage()
    {
        int damageSteps = 50;
        float stepTime = 0.005f;
        float fadeTime = 0.005f;
        float stayTime = 0.05f;
        for(int i = 0; i < damageSteps; i++)
        {
            float currRate = stepTime * i;
            DamageBackground.color = new Color(damageColor.r, damageColor.g, damageColor.b, damageColor.a * currRate);
            yield return new WaitForSeconds(stepTime);
        }
        yield return new WaitForSeconds(stayTime);
        for(int i = damageSteps - 1; i >= 0; i--)
        {
            float currRate = fadeTime * i;
            DamageBackground.color = new Color(damageColor.r, damageColor.g, damageColor.b, damageColor.a * currRate);
            yield return new WaitForSeconds(stepTime);
        }
        DamageBackground.color = Color.clear;
        yield return 0;
    }

    void OnTriggerEnter(Collider collider) {
        var name = collider.name;
        GameObject obj = collider.gameObject;
        if(obj.tag == "Danmaku")
        {
            currHp -= 1;
            GameObject.Find("Player").GetComponentInChildren<AudioManager>().PlayDamageSE();
            Destroy(obj);
            if(currHp <= 0)
            {
                GameObject.Find("GameController").GetComponent<GameController>().OnHPEmpty();
            }
            else
            {
                StartCoroutine(getDamage());
            }
        }
        //Debug.Log("on collide enter : " + name);
    }

    void OnTriggerExit(Collider collider) {
        var name = collider.name;
        //Debug.Log("on collide exit : " + name);
    }

    void OnTriggerStay(Collider collider) {
        var name = collider.name;
        //Debug.Log("on collide stay : " + name);
    }
}
