using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VRGIN.Core;

namespace ioVR
{
	// ゲーム特有の処理を実装する.
	class ioInterpreter : GameInterpreter
	{
		private HashSet<Camera> _CheckedCameras = new HashSet<Camera>();
		private List<Camera> _AdjustCamera = new List<Camera>();

		Quaternion RotAdjust = new Quaternion(0f, 0f, 0f, 0f);

		// GUIカメラの歪む問題補正.
		protected override void OnUpdate()
		{
			base.OnUpdate();

			// Find new GUI Camera.
			foreach (var camera in Camera.allCameras.Except(_CheckedCameras).ToList())
			{
				_CheckedCameras.Add(camera);
				if (VRGUI.Instance.IsInterested(camera))
				{
					VRLog.Info("Detected GUI camera ( {0} ) Adjusting Start", camera.name);
					_AdjustCamera.Add(camera);
				}
			}

			// Adjust Camera
			foreach (var camera in _AdjustCamera)
			{
				camera.transform.rotation = RotAdjust;
			}
		}

		// レベル読み込み時の処理.
		protected override void OnLevel(int level)
		{
			base.OnLevel(level);

			_CheckedCameras.Clear();
			_AdjustCamera.Clear();
		}

	}
}
