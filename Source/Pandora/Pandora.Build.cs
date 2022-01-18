// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class Pandora : ModuleRules
{
	public Pandora(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] { "Core", "CoreUObject", "Engine", "InputCore", "HeadMountedDisplay", "GameLiftServerSDK", "Http", "Json", "JsonUtilities" });
	}
}
