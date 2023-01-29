#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using Debug = UnityEngine.Debug;
using UnityEditor.PackageManager;

namespace Hibzz.PackageEditor
{
	public class PackageEditor
	{
		public const string k_DocumentSubFolder = "UnityGitPackages";  // Name of the submodule folder in the User's "My Documents" folder
		public const string k_ProgressBarName = "Hibzz.PackageEditor"; // 
		
		public const int k_Steps = 6;

		public PackageEditorDB Database = null;

		public PackageEditor()
		{
			// update the database
			Database = PackageEditorDB.Reload();
		}

		// Switch from git mode to embed mode
		public void SwitchToEmbed(PackageInfo packageInfo)
		{
			// the package id for a package installed with git is `package_name@package_giturl`
			// so we extract the url out
			string repoUrl = packageInfo.packageId.Substring(packageInfo.packageId.IndexOf('@') + 1);

			// figure out the path to which the repo must be cloned to
			string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			path += $"\\{k_DocumentSubFolder}\\{packageInfo.name}\\";

			// update the progress bar
			UpdateProgress("Initializing", 1);

			// create the file path if it doesn't exist and clone the repo there
			// (according .net documentation, we don't need to check if it exists or not)
			Directory.CreateDirectory(path);
			Clone(repoUrl, path);

			// add the entry to the database
			UpdateProgress("Updating Database", 3);
			Database.Entries.Add(new PackageEditorDBEntry() { Name = packageInfo.name, URL = repoUrl });

			// remove the git package
			UpdateProgress("Removing package downloaded from Git", 4);
			Client.Remove(packageInfo.name);

			// create a symbolic link to the packages folder
			UpdateProgress("Creating symbolic link to the cloned repository", 5);
			string destination = Path.GetFullPath("Packages\\") + packageInfo.name;
			CreateSymlink(source: path, destination);

			// Perform the serialization / save
			UpdateProgress("Serializing Database", 6);
			PackageEditorDB.Store(Database);

			// Done!
			EditorUtility.ClearProgressBar();
		}

		// switch from embed mode to git
		public void SwitchToGit(PackageInfo packageInfo)
		{
			// remove the package
			UpdateProgress("Removing the symlink", 2);
			string packagePath = Path.GetFullPath("Packages\\") + packageInfo.name;
			Directory.Delete(packagePath);

			// install the one with the git url from the entries
			UpdateProgress("Reinstalling git package", 4);
			var data = Database.Entries.Find((data) => data.Name == packageInfo.name);
			Client.Add(data.URL);

			// Update the database
			UpdateProgress("Updating database", 6);
			Database.Entries.RemoveAll((data) => data.Name == packageInfo.name);
			PackageEditorDB.Store(Database);

			// Done!
			EditorUtility.ClearProgressBar();
		}

		// check if the given repo is part of the database
		public bool IsPackageInDatabase(string name)
		{
			// no database found
			if(Database is null) { return false; }

			// no entries with the given name found in the database
			if (!Database.Entries.Any((data) => data.Name == name)) { return false; }

			// found
			return true;
		}

		// clone the given url if possible
		void Clone(string url, string path)
		{
			// if there's already content in the path, we skip the cloning process
			if(Directory.EnumerateFileSystemEntries(path).Any()) 
			{
				Debug.Log("Skipping Git Clone: ".Color("#1473e6").Bold() +
					"A developer environment already exists for the requested package.\n" +
					$"If this is not the intended effect, please delete the appropriate folder in the Documents/{k_DocumentSubFolder}/");
				return;
			}

			// Configure the process that can perform the git clone
			var process = new Process()
			{
				StartInfo = new ProcessStartInfo()
				{
					FileName = "git",
					Arguments = $"clone {url} .",
					WorkingDirectory = path
				}
			};

			// update the progress bar
			UpdateProgress("Cloning the git repository", 2);

			// activate the process and wait for it to complete
			process.Start();
			process.WaitForExit();

			
		}

		// Creates a symbolic link between the source and the given destination
		void CreateSymlink(string source, string destination)
		{
			source = source.Substring(0, source.Length - 1);

			// configure the process to create a symlink
			Process process = new Process()
			{
				StartInfo = new ProcessStartInfo()
				{
					FileName  = "cmd.exe",
					Arguments = $"/c mklink /D \"{destination}\" \"{source}\"",
					Verb      = "runas"
				}
			};

			// Start the process and wait for it to be done
			process.Start();
			process.WaitForExit();
		}

		// update the progress bar
		void UpdateProgress(string info, float currentStep)
		{
			EditorUtility.DisplayProgressBar(k_ProgressBarName, info, currentStep / k_Steps);
		}
	}
}

#endif
