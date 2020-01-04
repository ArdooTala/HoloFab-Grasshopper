﻿using System;
using System.Text;
using Newtonsoft.Json;
// Rhino only includes
using System.Drawing;
using Rhino.Geometry;

namespace HoloFab {
	// Tools for processing robit data.
	public static partial class EncodeUtilities {
		public static double[] EncodePlane(Plane _plane) {
			Quaternion quaternion = new Quaternion();
			quaternion.SetRotation(Plane.WorldXY, _plane);
            
			double [] transformation = new double[] {
				_plane.OriginX/1000, _plane.OriginZ/1000, _plane.OriginY/1000,
				quaternion.A, quaternion.B, quaternion.C, quaternion.D
			};
			return transformation;
		}
		// Encode a Color.
		public static int[] EncodeColor(Color _color) {
			return new int[] { _color.A, _color.R, _color.G, _color.B };
		}
		// Encode a Location.
		public static float[] EncodeLocation(Point3d _point){
			return new float[] {(float)Math.Round(_point.X/1000.0,3),
					            (float)Math.Round(_point.Z/1000.0,3),
					            (float)Math.Round(_point.Y/1000.0,3)};
		}
	}
	// Part Shared with Unity.
	public static partial class EncodeUtilities {
		// Encode data into a json readable byte array.
		public static byte[] EncodeData(string header, Object item, out string message){
			string output = JsonConvert.SerializeObject(item);
			if (header != string.Empty)
				message = header + "|" + output;
			else
				message = output;
			return Encoding.UTF8.GetBytes(message);
        }
        // Decode Data into a string.
        public static string DecodeData(byte[] data) {
            return Encoding.UTF8.GetString(data);
        }
    }
}