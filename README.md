# Hibzz.DevMenu
![LICENSE](https://img.shields.io/badge/LICENSE-CC--BY--4.0-ee5b32?style=for-the-badge) [![Twitter Follow](https://img.shields.io/twitter/follow/hibzzgames?color=1a8cd8&style=for-the-badge)](https://twitter.com/hibzzgames) [![Discord](https://img.shields.io/discord/695898694083412048?color=788bd9&label=DIscord&style=for-the-badge)](https://discord.gg/tZdZFK7) ![Unity](https://img.shields.io/badge/unity-%23000000.svg?style=for-the-badge&logo=unity&logoColor=white) ![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)

***A tool for Unity to edit a git package within the package manager***

## Installation
**Via Github**
This package can be installed in the Unity Package Manager using the following git URL.
```
https://github.com/Hibzz-Games/Hibzz.PackageEditor.git
```

Alternatively, you can download the latest release from the [releases page](https://github.com/Hibzz-Games/Hibzz.PackageEditor/releases) and manually import the package into your project.

## Problem I'm trying to solve
Across different Unity projects that I rapidly prototype, I use a lot of scripts that are shared between projects. To keep things streamlined (and shareable), I've created self-contained packages that I can easily import into any project using the Unity Package Manager. However, as I continue to work on the projects, I might find issues or want to add new features/improvements to the packages. To do this, I have to either work on the source repository directly, push an update, and then update the package in the project, or I have to manually copy the files over to the project and replace the UPM package. Both of these options are not ideal, and I wanted to find a better way to quickly iterate on my packages, test them, and then push them to the source repository.

## How it works
The package editor is a tool that allows you to edit a package that is installed via the Unity Package Manager. It does this by creating a local copy of the package in the project's `Packages` folder, and then creating a symbolic link to the original package. This allows you to edit the package in the project, and then push the changes to the source repository. The package editor will also automatically update the package in the project when you push changes to the source repository.

