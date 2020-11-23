using UnityEngine;

public class MeshRendererSorting : MonoBehaviour
{
	public string sortingLayerName;
	public int sortingOrder;

    void Start()
	{
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = sortingLayerName;
        meshRenderer.sortingOrder = sortingOrder;
	}
}
