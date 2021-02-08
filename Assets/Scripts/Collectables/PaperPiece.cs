using UnityEngine;

public class PaperPiece : Collectable {

	public GameObject pieceManager;

	protected override void OnCollect(Collider2D coll) {
		base.OnCollect(coll);
		pieceManager.GetComponent<PaperPieceManager>().AddPiece((int) base.collectableId);
		Destroy(gameObject, .5f);
	}

}
