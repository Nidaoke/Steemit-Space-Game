using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCode : MonoBehaviour {

    public GameObject part1, part2, part3;

    public float waitTime;

	// Use this for initialization
	void Start () {
        StartCoroutine(Burn());
	}
	
	IEnumerator Burn()
    {
        part1.SetActive(true);

        yield return new WaitForSeconds(waitTime);

        part2.SetActive(true);
        part1.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        part3.SetActive(true);
        part2.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        Destroy(gameObject);
    }
}
