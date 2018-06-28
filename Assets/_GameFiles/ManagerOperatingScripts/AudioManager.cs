using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mangos {	
	
	enum soundIndx : int{
		mainBasic = 0,
		machingunn,
		spiderattack,
		spiderdamage,
		spiderdead,
		emptyShoot,
	}
	
	
	
	public class AudioManager : MonoBehaviour {

		public float volumeSFX = 1;
		public float volumeMusic = 1;
		public float MasterVolume = 1;

		public AudioClip[] clips;
		public int[] maxSimultaneousClip;
		public GameObject soundMaker;
		public int maxSimultaneousSounds;
		List<GameObject> sounds = new List<GameObject>();
		
		
		void Awake(){
			StaticManager.audioManager = this;
		}
		
		void Start()
		{
			for(int i = 0; i < clips.Length; i++)
			{
				GameObject temp = new GameObject();
				
				temp.AddComponent<AudioSource>();
				temp.GetComponent<AudioSource>().clip = clips[i];
				temp.AddComponent<DefaultSound>();
				temp.GetComponent<DefaultSound>().dj = temp.GetComponent<AudioSource>();
				temp.name = "soundMaker" + i.ToString();
				sounds.Add(temp);
				PoolManager.PreSpawn(sounds[i], (int)Mathf.Round(maxSimultaneousClip[i]/2), false);
				PoolManager.SetPoolLimit(sounds[i], maxSimultaneousClip[i]);
			}
		}
		
		
		public void PlayIndexedSound(int i, Vector3 pos){
			PoolManager.Spawn(sounds[i], pos, Quaternion.identity);
		}
		public void PlayBasicShot(Vector3 pos){
			PoolManager.Spawn(sounds[(int)soundIndx.mainBasic], pos, Quaternion.identity);
		}
	}
}

