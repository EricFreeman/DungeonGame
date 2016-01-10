using UnityEngine;
using System.Collections.Generic;

[RequireComponent( typeof(MeshFilter) )]
[RequireComponent( typeof(MeshRenderer) )]
public class Decal : MonoBehaviour {

	public Material material;
	public Sprite sprite;

	public float maxAngle = 90.0f;
	public float pushDistance = 0.009f;
	public LayerMask affectedLayers = -1;

    void OnDrawGizmosSelected() {
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube( Vector3.zero, Vector3.one );
	}

	public Bounds GetBounds() {
		Vector3 size = transform.lossyScale;
		Vector3 min = -size/2f;
		Vector3 max =  size/2f;

		Vector3[] vts = new Vector3[] {
			new Vector3(min.x, min.y, min.z),
			new Vector3(max.x, min.y, min.z),
			new Vector3(min.x, max.y, min.z),
			new Vector3(max.x, max.y, min.z),

			new Vector3(min.x, min.y, max.z),
			new Vector3(max.x, min.y, max.z),
			new Vector3(min.x, max.y, max.z),
			new Vector3(max.x, max.y, max.z),
		};

		for(int i=0; i<8; i++) {
			vts[i] = transform.TransformDirection( vts[i] );
		}

		min = max = vts[0];
		foreach(Vector3 v in vts) {
			min = Vector3.Min(min, v);
			max = Vector3.Max(max, v);
		}

		return new Bounds(transform.position, max-min);
	}


}