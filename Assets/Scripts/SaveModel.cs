using UnityEngine;

[System.Serializable]
public class SaveModel {

	int levelId, checkpointId, playerHp;
	// pode guardar coisas coletadas, segredos descobertos, estatísticas... etc

	public SaveModel() {
		levelId = 1;
	}

	public int GetLevelId() { return levelId; }

	public int GetCheckpointId() { return checkpointId; }

	public int GetPlayerHp() { return playerHp; }

}
