using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public TextMesh hpText;
    public int initHp = 60;

    public Image hpImage;

    public Image DamageBackground;

    public Color damageColor;
    
    int currHp;
    float currAmont;
    float decreaseRate = 0.003f;

    bool invictus = false;

    // Start is called before the first frame update
    void Start()
    {
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

    IEnumerator getDamage()
    {
        float stepTime = 0.25f;
        float fadeTime = 1.75f;
        float stayTime = 1f;
        invictus = true;
        for(float t = 0; t < stepTime; t += Time.deltaTime)
        {
            float currRate = t / stepTime;
            DamageBackground.color = new Color(damageColor.r, damageColor.g, damageColor.b, damageColor.a * currRate);
            yield return 0;
        }
        yield return new WaitForSeconds(stayTime);
        for(float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            float currRate = 1 - t / fadeTime;
            DamageBackground.color = new Color(damageColor.r, damageColor.g, damageColor.b, damageColor.a * currRate);
            yield return 0;
        }
        DamageBackground.color = Color.clear;
        invictus = false;
        yield return 0;
    }

    void OnTriggerEnter(Collider collider) {
        if(invictus)
            return;
        var name = collider.name;
        GameObject obj = collider.gameObject;
        if(obj.tag == "Danmaku")
        {
            currHp -= 1;
            GameObject.Find("Player").GetComponentInChildren<AudioManager>().PlayDamageSE();
            Destroy(obj);
            StartCoroutine(getDamage());
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
