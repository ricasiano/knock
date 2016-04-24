// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class SelectDoor : MonoBehaviour {
	public string doorToActivate;
	public string doorToOpen;
	public string doorAnimation;
	public string doorPlayerTarget;
	void Start() {
		Gazed(false);
	}
	
	public void Gazed(bool gazedAt) {
		var myHighlight = GameObject.FindGameObjectWithTag(doorToActivate).GetComponent("Light");
		myHighlight.light.enabled = gazedAt;

	}

	public void Opened(bool gazedAt) {

		var myPlayer = GameObject.FindGameObjectWithTag ("MyPlayerDude").animation;
		myPlayer.Play (doorPlayerTarget);
		myPlayer.audio.Play ();
		StartCoroutine (OpenDoor (5));
	}

	IEnumerator OpenDoor(float duration)
	{
		yield return new WaitForSeconds(duration);
		var myOpenedDoor = GameObject.FindGameObjectWithTag(doorToOpen).animation;
		myOpenedDoor.Play (doorAnimation);
		StartCoroutine (showMonster (3));
	}

	IEnumerator showMonster(float duration)
	{
		yield return new WaitForSeconds(duration);

		var dead = Random.Range (0, 100);
		if (dead >= 20) {
			Application.LoadLevel("GameProper");
		} 
		else {
			var myClownL = GameObject.FindGameObjectWithTag("ClownL");
			myClownL.audio.Play ();
			myClownL.transform.localScale += new Vector3(1F, 1F, 1F);
			var myClownR = GameObject.FindGameObjectWithTag("ClownR");
			myClownR.transform.localScale += new Vector3(1F, 1F, 1F);		
			StartCoroutine (GameOver (5));
		}
	}

	IEnumerator GameOver(float duration)
	{
		yield return new WaitForSeconds(duration);
		Application.LoadLevel("Splash");
	}
}
