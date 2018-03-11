using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;
using VRGIN.Controls.Speech;
using VRGIN.Core;
using VRGIN.Helpers;
using VRGIN.Visuals;

namespace ioVR
{
    /// <summary>
    /// Context class that can be serialized as an XML. Normally, you would implement this yourself and simply override getters with sensible values.
    /// </summary>
    [XmlRoot("Context")]
    public class ioVRContext : IVRManagerContext
    {
        DefaultMaterialPalette _Materials;
        VRSettings _Settings;

        public ioVRContext()
        {
            // We'll keep those always the same
            _Materials = new DefaultMaterialPalette();
			bool isNew = !System.IO.File.Exists("VRSettings.xml");
			_Settings = VRSettings.Load<VRSettings>("VRSettings.xml");

			// IPDのデフォルトを10に設定(巨人化防止)
			if (isNew)
			{
				_Settings.IPDScale = 10f;
				_Settings.Save();
			}
			// Set defaults
			ConfineMouse = true;
            EnforceDefaultGUIMaterials = false;
            GUIAlternativeSortingMode = false;
            GuiLayer = "Default";
            GuiFarClipPlane = 1000f;
            GuiNearClipPlane = -1000f;
            IgnoreMask = 0;
            InvisibleLayer = "Ignore Raycast";
            PrimaryColor = Color.cyan;
            SimulateCursor = true;
            UILayer = "UI";
            UILayerMask = LayerMask.GetMask(UILayer);
            UnitToMeter = 1f;
            NearClipPlane = 0.1f;
            PreferredGUI = GUIType.uGUI;
        }

        [XmlIgnore]
        public IMaterialPalette Materials { get { return _Materials; } }

        [XmlIgnore]
        public VRSettings Settings { get { return _Settings; } }

        [XmlIgnore]
        public Type VoiceCommandType { get { return typeof(VoiceCommand); } }

        public bool ConfineMouse { get; set; }

        public bool EnforceDefaultGUIMaterials { get; set; }

        public bool GUIAlternativeSortingMode { get; set; }

        public float GuiFarClipPlane { get; set; }

        public string GuiLayer { get; set; }

        public float GuiNearClipPlane { get; set; }

        public int IgnoreMask { get; set; }

        public string InvisibleLayer { get; set; }

        public Color PrimaryColor { get; set; }

        public bool SimulateCursor { get; set; }

        public string UILayer { get; set; }

        public int UILayerMask { get; set; }

        public float UnitToMeter { get; set; }

        public float NearClipPlane { get; set; }

        public GUIType PreferredGUI { get; set; }
    }
}
