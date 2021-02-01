[System.Serializable]
public class SaveModel {

	int levelId, playerHp, levelData;
	float pX, pY;

	public SaveModel() {
		levelId = 1;
		levelData = 0;
		playerHp = 8;
		pX = -30.32f;
		pY = -5.296f;
	}

	public int GetLevelId() { return levelId; }
	public void SetLevelId(int levelId) { this.levelId = levelId; }

	public int GetPlayerHp() { return playerHp; }
	public void SetPlayerHp(int playerHp) { this.playerHp = playerHp; }

	public float GetPlayerX() { return pX; }
	public void SetPlayerX(float pX) { this.pX = pX; }

	public float GetPlayerY() { return pY; }
	public void SetPlayerY(float pY) { this.pY = pY; }

	public float GetPlayerZ() { return 0f; }

	public bool IsCollectableCollected(int collIdx) {
		int r = (levelData >> collIdx) & (int.MaxValue - 1);
		return r == 0b0001;
	}

	public void SetCollectableCollected(int collIdx) {
		levelData |= 0b0001 << collIdx;
	}

}
