#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hibzz.PackageEditor
{
	[Serializable]
	public class PackageEditorDB
	{
		const string k_DatabaseName = "PackageEditorDB.json";

		public List<PackageEditorDBEntry> Entries = new List<PackageEditorDBEntry>();

		#region Utilities

		public static PackageEditorDB Reload()
		{
			string filepath = Path.GetFullPath("Packages\\") + k_DatabaseName;
			if(File.Exists(filepath))
			{
				string contents = File.ReadAllText(filepath);
				return JsonUtility.FromJson<PackageEditorDB>(contents);
			}

			return new PackageEditorDB();
		}

		public static void Store(PackageEditorDB db)
		{
			string filepath = Path.GetFullPath("Packages\\") + k_DatabaseName;
			string contents = JsonUtility.ToJson(db);
			File.WriteAllText(filepath, contents);
		}

		#endregion
	}

	[Serializable]
	public struct PackageEditorDBEntry 
	{
		public string Name;
		public string URL;
	}
}

#endif
