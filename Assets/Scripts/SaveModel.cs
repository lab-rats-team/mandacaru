using UnityEngine;
[System.Serializable]
public class SaveModel {

	public int levelId, playerHp;
	public float pX, pY;

	private int levelData;

	public SaveModel() {
		levelId = 1;
		levelData = 0;
		playerHp = 8;
		pX = -30.32f;
		pY = -5.296f;
	}

	public bool IsCollectableCollected(int collIdx) {
		int a = levelData >> collIdx;
		Debug.Log("a " + a);
		int b = a & 0b1;
		Debug.Log("b " + b);
		//int r = (levelData >> collIdx) & (int.MaxValue - 1);
		Debug.Log("levelData  = " + levelData);
		//Debug.Log(r);
		return b == 0b0001;
	}

	public void SetCollectableCollected(int collIdx) {
		levelData |= 0b0001 << collIdx;
		Debug.Log("Setando coletavel " + collIdx);
		Debug.Log(levelData);
	}

}
