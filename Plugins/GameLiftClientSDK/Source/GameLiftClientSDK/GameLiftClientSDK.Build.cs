// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

public class GameLiftClientSDK : ModuleRules
{
	public GameLiftClientSDK(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		
		PublicDependencyModuleNames.AddRange(new string[] { "Engine", "Core", "CoreUObject", "InputCore", "Projects" });
		PublicDependencyModuleNames.AddRange(new string[] { "GameLiftClientSDKLibrary" });
		PrivateDependencyModuleNames.AddRange(new string[] { });

		//PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "..", "GameLiftClientSDKLibrary"));
		PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "Public"));
		PrivateIncludePaths.Add(Path.Combine(ModuleDirectory, "Private"));

		if (Target.Type == TargetRules.TargetType.Editor || Target.Type == TargetRules.TargetType.Client)
		{
			PublicDefinitions.Add("WITH_GAMELIFT_CLIENT=1");
			if (Target.Platform == UnrealTargetPlatform.Win64)
            {
				//PublicDefinitions.Add("AWS_USE_IO_COMPLETION_PORTS=1");
				//PublicDefinitions.Add("__clang_analyzer__=0");
				//PublicDefinitions.Add("AWS_DEEP_CHECKS=0");
				//PublicDefinitions.Add("WIN32_LEAN_AND_MEAN");
			}
		}
        else
        {
			PublicDefinitions.Add("WITH_GAMELIFT_CLIENT=0");
		}
	}
}
