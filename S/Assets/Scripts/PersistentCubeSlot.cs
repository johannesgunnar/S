using UnityEngine;
using System.Collections;

public class PersistentCubeSlot : MonoBehaviour {
	public static int numCubesOn = 0;
	
	public int numCubesInside = 0;
	public string name = "test";
	public Material sourceMaterial;
	public Renderer[] materialObjects;
	Material ourMaterial;
	public Color onColor;
	float time = 0.0f;
	
	void Start()
	{
		ourMaterial = new Material(sourceMaterial);
		ourMaterial.name = ""+name+"(instance)";
		foreach(Renderer r in materialObjects) r.material = ourMaterial;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "SoundCube")
		{
			numCubesInside++;
			if (numCubesInside == 1)
			{
				numCubesOn++;
				if (numCubesOn == 3)
				{
					if (GameObject.Find("Stream 3").GetComponent<ParticleRenderer>().enabled==false)
					{
						GameObject.Find("Stream 3").GetComponent<ParticleRenderer>().enabled = true;
						WorldState.streamsSolved++;
					}
				}
				time = Time.time;
			}
		}
	}
	
	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == "SoundCube")
		{
			numCubesInside--;
			if (numCubesInside == 0 && WorldState.streamsSolved < 4)
			{
				numCubesOn--;
				if (GameObject.Find("Stream 3").GetComponent<ParticleRenderer>().enabled==true)
				{
					GameObject.Find("Stream 3").GetComponent<ParticleRenderer>().enabled = false;
					WorldState.streamsSolved--;
				}
						
				time = Time.time;
			}
		}
	}
	
	void Update()
	{
		if (numCubesInside > 0)
		{
			ourMaterial.color = Color.Lerp(Color.black, onColor,( Time.time-time)/1.5f);
		}
		else 
		{
			ourMaterial.color = Color.Lerp(onColor, Color.black,( Time.time-time)/1.5f);

		}
	}
}
