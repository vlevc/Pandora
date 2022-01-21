// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

public class GameLiftClientSDK : ModuleRules
{
	public GameLiftClientSDK(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		//PrivatePCHHeaderFile = "Private/AWSBaseModulePrivatePCH.h";

		PublicDependencyModuleNames.AddRange(new string[] { "Engine", "Core", "CoreUObject", "InputCore", "Projects", "GameLiftClientSDKLibrary", });
		PrivateDependencyModuleNames.AddRange(new string[] { });

		PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "Public"));
		PrivateIncludePaths.Add(Path.Combine(ModuleDirectory, "Private"));

		PublicDefinitions.Add("USE_IMPORT_EXPORT");

		PublicDefinitions.Add("AWS_USE_IO_COMPLETION_PORTS=1");
		PublicDefinitions.Add("__clang_analyzer__=0");
		PublicDefinitions.Add("AWS_DEEP_CHECKS=0");
		PublicDefinitions.Add("WIN32_LEAN_AND_MEAN");

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			PublicDefinitions.Add("WITH_BASE=1");
		}
		else
		{
			PublicDefinitions.Add("WITH_BASE=0");
		}
	}
}
