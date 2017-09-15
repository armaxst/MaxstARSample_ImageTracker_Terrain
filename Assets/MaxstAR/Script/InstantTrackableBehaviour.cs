using UnityEngine;
using System.IO;
using JsonFx.Json;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using UnityEngine.Rendering;

namespace maxstAR
{
    public class InstantTrackableBehaviour : AbstractInstantTrackableBehaviour
    {
		private Matrix4x4 trackableTransform;

		void Start()
		{
			trackableTransform = Matrix4x4.TRS(
				transform.position,
				transform.rotation,
				transform.localScale);
		}

		public override void OnTrackSuccess(string id, string name, Matrix4x4 poseMatrix)
        {
			Matrix4x4 newPose = trackableTransform * poseMatrix;

            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			Terrain[] terrainComponents = GetComponentsInChildren<Terrain>(true);

            // Enable renderers
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

			// Enable terrain
			foreach (Terrain component in terrainComponents)
			{
				component.enabled = true;
			}

			transform.position = MatrixUtils.PositionFromMatrix(newPose);
			transform.rotation = MatrixUtils.QuaternionFromMatrix(newPose);
			transform.localScale = MatrixUtils.ScaleFromMatrix(newPose);
        }

        public override void OnTrackFail()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
			Terrain[] terrainComponents = GetComponentsInChildren<Terrain>(true);

			// Disable renderer
			foreach (Renderer component in rendererComponents)
			{
				component.enabled = false;
			}

            // Disable collider
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }

			// Disable terrain
			foreach (Terrain component in terrainComponents)
			{
				component.enabled = false;
			}
        }
    }
}