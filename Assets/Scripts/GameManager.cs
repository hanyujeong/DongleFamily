using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dongle lastDongle;
    public GameObject donglePrefab;
    public Transform dongleGroup;
    public GameObject effectPrefab;
    public Transform effectGroup;

    public int score;
    public int maxLevel = 8;
    public bool isOver;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    void Start()
    {
        NextDongle();
    }

    Dongle GetDongle()
    {
        //이펙트 생성
        GameObject instantEffectObj = Instantiate(effectPrefab, effectGroup);
        ParticleSystem instantEffect = instantEffectObj.GetComponent<ParticleSystem>();

        //동글 생성
        GameObject instantDongleObj = Instantiate(donglePrefab, dongleGroup);
        Dongle instantDongle = instantDongleObj.GetComponent<Dongle>();
        instantDongle.effect = instantEffect;
        return instantDongle;
    }

    void NextDongle()
    {
        if (isOver)
        {
            return;
        }
            
        Dongle newDongle = GetDongle();
        lastDongle = newDongle;
        lastDongle.manager = this;
        lastDongle.level = Random.Range(0, maxLevel);
        lastDongle.gameObject.SetActive(true);

        StartCoroutine("WaitNext");
    }

    IEnumerator WaitNext()
    {     
        while (lastDongle != null)
        {
            yield return null;
        }
       
        yield return new WaitForSeconds(2.5f);
        
        NextDongle();
    }

    public void TouchDown()
    {
        if (lastDongle == null)
            return;

        lastDongle.Drag();
    }
    public void TouchUp()
    {
        if(lastDongle == null)
            return;

        lastDongle.Drop();
        lastDongle = null;

    }

    public void GameOver()
    {
        if (isOver)
            return;

        isOver = true;
        StartCoroutine("GameOverRoutine");
    }

    IEnumerator GameOverRoutine()
    {
        // 1. 장면 안에 활성화 되어있는 모든 동글 가져오기
        Dongle[] dongles = FindObjectsByType<Dongle>(FindObjectsSortMode.None);

        for (int i = 0; i < dongles.Length; i++)
        {
            dongles[i].rigid.simulated = false;       
        }

        //2. 1번의 목록을 하나씩 접근해서 지우기
        for (int i = 0; i < dongles.Length; i++)
        {
            dongles[i].Hide(Vector3.up * 100);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
