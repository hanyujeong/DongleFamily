using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dongle lastDongle;
    public GameObject donglePrefab;
    public Transform dongleGroup;

    void Start()
    {
        NextDongle();
    }

    Dongle GetDongle()
    {
        GameObject instant = Instantiate(donglePrefab, dongleGroup);
        Dongle instantDongle = instant.GetComponent<Dongle>();
        return instantDongle;
    }

    void NextDongle()
    {
        Dongle newDongle = GetDongle();
        lastDongle = newDongle;

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

}
