using UnityEngine;
using System.Collections;

namespace HexasphereGrid {

	partial class Hexasphere : MonoBehaviour {

		const float EARTH_WATER_MASK_OCEAN_LEVEL_MAX_ALPHA = 16f / 255f;

		#region public API

		/// <summary>
		/// Applies the height map provided by heightMap texture to sphere grid. It uses red channel as height (0..1), multiplied by the maxExtrusionAmount value.
		/// </summary>
		/// <param name="heightMap">Height map.</param>
		/// <param name="maxExtrusionAmount">Max extrusion amount.</param>
		/// <param name="rampColors">Gradient of colors that will be mapped to the height map.</param>
		public void ApplyHeightMap (Texture2D heightMap, Texture2D waterMask, Texture2D rampColors = null) {

			heights = heightMap.GetPixels ();
			if (waterMask != null) {
				waters = waterMask.GetPixels ();
			} else {
				waters = null;
			}
			heightMapWidth = heightMap.width;
			heightMapHeight = heightMap.height;								
			LoadRampColors (rampColors);
			UpdateHeightMap ();
		}

		/// <summary>
		/// Applies the height map provided by heightMap texture to sphere grid. It uses red channel as height (0..1), multiplied by the maxExtrusionAmount value.
		/// </summary>
		/// <param name="heightMap">Height map.</param>
		/// <param name="maxExtrusionAmount">Max extrusion amount.</param>
		/// <param name="rampColors">Gradient of colors that will be mapped to the height map.</param>
		public void ApplyHeightMap (Texture2D heightMap, float seaLevel = 0.1f, Texture2D rampColors = null) {
												
			heights = heightMap.GetPixels ();
			waters = null;
			heightMapWidth = heightMap.width;
			heightMapHeight = heightMap.height;								
			LoadRampColors (rampColors);
			UpdateHeightMap (seaLevel);
		}

		void LoadRampColors (Texture2D rampColors) {
			if (rampColors == null) {
				if (defaultRampTexture == null) {
					defaultRampTexture = Resources.Load<Texture2D> ("Textures/HexasphereDefaultRampTex");
				}
				gradientColors = defaultRampTexture.GetPixels ();
				rampWidth = defaultRampTexture.width;
			} else {
				gradientColors = rampColors.GetPixels ();
				rampWidth = rampColors.width;
			}
		}

		/// <summary>
		/// Reuses previous heightmap texture which results faster update if you need to change rampColors or seaLevel dynamically.
		/// </summary>
		/// <param name="maxExtrusionAmount">Max extrusion amount.</param>
		/// <param name="rampColors">Gradient of colors that will be mapped to the height map.</param>
		void UpdateHeightMap (float seaLevel = 0.1f, Texture2D rampColors = null) {
			if (tiles == null || heights == null)
				return;

			extruded = true;
			if (rampColors) {
				gradientColors = rampColors.GetPixels ();
				rampWidth = rampColors.width;
			}
			for (int k = 0; k < tiles.Length; k++) {
				Vector3 p = tiles [k].center;
				float latDec = Mathf.Asin (p.y * 2.0f);
				float lonDec = -Mathf.Atan2 (p.x, p.z);
				if (_invertedMode)
					lonDec *= -1f;
				float u = (lonDec + Mathf.PI) / (2f * Mathf.PI);
				float v = latDec / Mathf.PI + 0.5f;
				int px = (int)(u * heightMapWidth);
				int py = (int)(v * heightMapHeight);
				if (py >= heightMapHeight)
					py = heightMapHeight - 1;
				float h = heights [py * heightMapWidth + px].r;
				// Water mask supplied?
				if (waters != null) {
					bool isWater = waters [py * heightMapWidth + px].a < EARTH_WATER_MASK_OCEAN_LEVEL_MAX_ALPHA;
					if (isWater) {
						h = 0;
						tiles [k].canCross = false;
					}
				} else {
					if (h <= seaLevel) {
						h = 0;
						tiles [k].canCross = false;
					}
				}
				SetTileExtrudeAmount (k, h);
				int gc = (int)((rampWidth - 1) * h);
				tiles [k].heightMapValue = h;
				SetTileColor (k, gradientColors [gc]);
			}
		}

		/// <summary>
		/// Takes colors from a texture and maps them to the hexasphere
		/// </summary>
		/// <param name="textureWithColors">Texture with colors.</param>
		public void ApplyColors (Texture2D textureWithColors) {
			// Load texture colors and dimensions
			Color32[] colors = textureWithColors.GetPixels32 ();
			int textureWidth = textureWithColors.width;
			int textureHeight = textureWithColors.height;

			// For each tile, determine its color
			for (int k = 0; k < tiles.Length; k++) {
				Vector3 p = tiles [k].center;

				// Convert center to texture coordinates
				float latDec = Mathf.Asin (p.y * 2.0f);
				float lonDec = -Mathf.Atan2 (p.x, p.z);
				float u = (lonDec + Mathf.PI) / (2f * Mathf.PI);
				float v = latDec / Mathf.PI + 0.5f;
				int px = (int)(u * textureWidth);
				int py = (int)(v * textureHeight);
				if (py >= textureHeight)
					py = textureHeight - 1;
				Color32 tileColor = colors [py * textureWidth + px];
				SetTileColor (k, tileColor);
			}
		}


		// The smaller of the two possible angles between the two vectors is returned, therefore the result will never be greater than 180 degrees or smaller than -180 degrees.
		// If you imagine the from and to vectors as lines on a piece of paper, both originating from the same point, then the /axis/ vector would point up out of the paper.
		// The measured angle between the two vectors would be positive in a clockwise direction and negative in an anti-clockwise direction.
		public float SignedAngle (Vector3 from, Vector3 to, Vector3 axis) {
			float unsignedAngle = Vector3.Angle (from, to);
			float sign = Mathf.Sign (Vector3.Dot (axis, Vector3.Cross (from, to)));
			return unsignedAngle * sign;
		}


		/// <summary>
		/// Adjusts object transform to match a given tile position and orientation.
		/// </summary>
		/// <param name="o">The gameobject to align.</param>
		/// <param name="tileIndex">Tile index.</param>
		
		
		//reference to transform of parent that holds all plants
		public Transform plants;
		public void ParentAndAlignToTile (GameObject o, int tileIndex, float altitude = 0, bool snapRotationToVertex0 = false, bool adjustScale = true, float scaleRatio = 1f) {
			//if my parent object that holds all plants object is null, get it
			if(plants == null) plants = transform.Find("plants");
			
			if (tileIndex < 0 || tileIndex >= tiles.Length)
				return;

			// Move to tile center
			Vector3 tileCenter = GetTileCenter (tileIndex, true);
			if (altitude != 0) {
				Vector3 dir = (tileCenter - transform.position).normalized * altitude;
				o.transform.position = GetTileCenter (tileIndex) + dir;
			} else {
				o.transform.position = GetTileCenter (tileIndex);
			}
			if(o.tag == "Plant")
			{
				o.transform.SetParent (plants, true);
				o.transform.LookAt (plants.position);
			}
			else {
				o.transform.SetParent (transform, true);
				o.transform.LookAt (transform.position);
			}
			
			

			// Set rotation
			Vector3 axis = tileCenter - transform.position;
			float rotation;
			if (snapRotationToVertex0) {
				Vector3 v0 = GetTileVertexPosition (tileIndex, 0, true);
				Vector3 v = v0 - tileCenter;
				rotation = -SignedAngle (v, o.transform.up, axis);
			} else {
				float angle = GetTileVertex0Angle (tileIndex);
				rotation = -angle - Mathf.PI * 0.5f;
			}
			o.transform.Rotate (axis, rotation, Space.World);

			// Adjust scale
			if (!adjustScale)
				return;
			Renderer renderer = o.GetComponent<Renderer> ();
			o.transform.localScale = Misc.Vector3one;
			float tileSize = 1, spriteSize = 1;
			spriteSize = Vector3.Distance (renderer.bounds.max, renderer.bounds.min) / 1.4142135f;
			if (tiles [tileIndex].vertices.Length == 6) {
				tileSize = Vector3.Distance (GetTileVertexPosition (tileIndex, 0), GetTileVertexPosition (tileIndex, 3));
			} else if (tiles[tileIndex].vertices.Length == 5) {
				 tileSize = Vector3.Distance (GetTileVertexPosition (tileIndex, 0), GetTileVertexPosition (tileIndex, 2));
			}
			float scale = scaleRatio * (tileSize / spriteSize);
			o.transform.localScale = new Vector3 (scale, scale, 1f);
		}

		#endregion

	}

}