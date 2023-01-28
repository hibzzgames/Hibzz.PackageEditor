# Hibzz.PackageEditor
![LICENSE](https://img.shields.io/badge/LICENSE-CC--BY--4.0-ee5b32?style=for-the-badge) [![Twitter Follow](https://img.shields.io/twitter/follow/hibzzgames?color=1a8cd8&style=for-the-badge)](https://twitter.com/hibzzgames) [![Discord](https://img.shields.io/discord/695898694083412048?color=788bd9&label=DIscord&style=for-the-badge)](https://discord.gg/tZdZFK7) ![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

***A tool for Unity to edit a git package within the package manager***

![Untitled](https://user-images.githubusercontent.com/37605842/215235067-a627f9cd-ac5b-4d8c-b06a-39cbd643d522.png)

## Installation
**Via Github**
This package can be installed in the Unity Package Manager using the following git URL.
```
https://github.com/Hibzz-Games/Hibzz.PackageEditor.git
```

Alternatively, you can download the latest release from the [releases page](https://github.com/Hibzz-Games/Hibzz.PackageEditor/releases) and manually import the package into your project.

<br>

## Problem I'm trying to solve
Across different Unity projects that I rapidly prototype, I use a lot of scripts that are shared between projects. To keep things streamlined (and shareable), I've created self-contained packages that I can easily import into any project using the Unity Package Manager. However, as I continue to work on the projects, I might find issues or want to add new features/improvements to the packages. To do this, I have to either work on the source repository directly, push an update, and then update the package in the project, or I have to manually copy the files over to the project and replace the UPM package. Both of these options are not ideal, and I wanted to find a better way to quickly iterate on my packages, test them, and then push them to the source repository.

## How it works
The package editor is a tool that allows you to edit a package that is installed using a git URL via the Unity Package Manager. It does this by creating a local copy of the package in the developer's `Documents/UnityPackageSource/` folder and creating a symbolic link to the local `Packages` folder to create an *Embedded* package. This allows you to edit the package in the project, and then push the changes to the source repository. Then once the you are done, you can revert to the original "production" package using this tool.

This way you can quickly iterate on your packages, across multiple projects, without having to worry about juggling multiple repositories or manually copying files over.

## Editing a package
- After installing the package, you can open the package manager and select the git package you want to edit.
- Click the `Switch to Development Mode` button to clone the repo and create a symbolic link to the package.
- Now, in the package directory, you can edit the files and test them in your project

## Reverting to the original package
- After you are done editing the package and have pushed your changes to the source repository, you can revert to the orginal package by clicking the `Revert to Production Mode` button.
- This will remove the symbolic link and restore the original package
  - Note: This will not delete the local copy of the package in the `Documents/UnityPackageSource/` folder. This is so that you can continue to edit the package without having to clone it again from other projects.


<br>

## Have a question or want to contribute?
If you have any questions or want to contribute, feel free to join the [Discord server](https://discord.gg/tZdZFK7) or [Twitter](https://twitter.com/hibzzgames). I'm always looking for feedback and ways to improve this tool. Thanks!
