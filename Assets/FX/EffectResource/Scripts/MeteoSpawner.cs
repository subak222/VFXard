using UnityEngine;
using System.Collections;

public class MeteoSpawner : MonoBehaviour {

	public GameObject meteo;
	public int spawnCount = 1;
	public float spawnStartDelay = 2f;
	public float spawnBetweenDelay = 0.1f;
	public float rangeX = 1f;
    public float rangeY = 1f;
    public float rangeZ = 1f;

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(spawnStartDelay);
		while(spawnCount > 0)
		{
			var curPos = transform.position;
			Instantiate(meteo, new Vector3(
				curPos.x + Random.Range(-rangeX, rangeX), 
				curPos.y + Random.Range(-rangeY, rangeY),  
				curPos.z + Random.Range(-rangeZ, rangeZ)), meteo.transform.rotation);
			yield return new WaitForSeconds(spawnBetweenDelay);
			spawnCount --;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
