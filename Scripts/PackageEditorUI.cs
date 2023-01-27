#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.UI;
using UnityEngine.UIElements;

using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using Debug = UnityEngine.Debug;
using System.Linq;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace Hibzz.PackageEditor
{
	[InitializeOnLoad]
	public class PackageEditorUI : IPackageManagerExtension
	{
		static PackageEditorUI()
		{
			// add it to the UPM extension
			PackageManagerExtensions.RegisterExtension(new PackageEditorUI());
		}

		public PackageEditorUI()
		{
			// reload the editor ui
			editorDB = PackageEditorDB.Reload();
		}

		// A reference to the Package Editor's Database (local to the project)
		PackageEditorDB editorDB = null;

		// The root element of the extension UI
		VisualElement root;

		public VisualElement CreateExtensionUI()
		{
			root = new VisualElement()
			{
				style =
				{
					alignSelf = Align.FlexStart,
					flexDirection = FlexDirection.Row
				}
			};

			return root;
		}

		public void OnPackageAddedOrUpdated(PackageInfo packageInfo) { }

		public void OnPackageRemoved(PackageInfo packageInfo) { }

		public void OnPackageSelectionChange(PackageInfo packageInfo) 
		{
			// start by resetting the extension
			root.Clear();

			if(packageInfo == null) { return; }

			if(packageInfo.source == PackageSource.Git)
			{
				// then add a button to the root
				Button button = new Button();
				button.text = "Switch to development mode";
				button.clicked += () => SwitchToEmbed(packageInfo);
				root.Add(button);
			}
			else if(packageInfo.source == PackageSource.Embedded)
			{
				// no database found
				if(editorDB is null) { return; }

				// database has no entry of the package
				if(!editorDB.Entries.Any((data) => data.Name == packageInfo.name)) { return; }

				// data base has entry of the package... so, add the button to revert to production
				Button button = new Button();
				button.text = "Revert to production mode";
				button.clicked += () => SwitchToGit(packageInfo);
				root.Add(button);
			}
		}

		void SwitchToEmbed(PackageInfo packageInfo)
		{
			// the package id for a package installed with git is `package_name@package_giturl`
			// so we extract the url out
			string repoUrl = packageInfo.packageId.Substring(packageInfo.packageId.IndexOf('@') + 1);

			
			PackageEditorDB.Store(editorDB);
		}

		void SwitchToGit(PackageInfo packageInfo) 
		{
			editorDB.Entries.RemoveAll((data) => data.Name == packageInfo.name);
			PackageEditorDB.Store(editorDB);
		}
	}
}

#endif
