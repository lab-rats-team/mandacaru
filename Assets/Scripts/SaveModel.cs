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
		return ((levelData >> collIdx) & 1) == 1;
	}

	public void SetCollectableCollected(int collIdx) {
		levelData |= 0b0001 << collIdx;
	}

}
