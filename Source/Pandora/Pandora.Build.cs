// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class Pandora : ModuleRules
{
	public Pandora(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] { "Core", "CoreUObject", "Engine", "InputCore", "HeadMountedDisplay", "Http", "Json", "JsonUtilities" });

		PublicDependencyModuleNames.AddRange(new string[] { "GameLiftClientSDK" });
		PublicDependencyModuleNames.AddRange(new string[] { "GameLiftServerSDK" });

		if (Target.Type == TargetRules.TargetType.Editor || Target.Type == TargetRules.TargetType.Client)
		{
			PublicDependencyModuleNames.AddRange(new string[] { "GameLiftClientSDK" });
		}

		if (Target.Type == TargetRules.TargetType.Server)
		{
			PublicDependencyModuleNames.AddRange(new string[] { "GameLiftServerSDK" });
		}

	}
}
