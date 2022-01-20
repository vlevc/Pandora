// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

public class GameLiftClientSDK : ModuleRules
{
	public GameLiftClientSDK(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;

		// PrivatePCHHeaderFile = "Private/AWSBaseModulePrivatePCH.h";

		PublicDependencyModuleNames.AddRange(new string[] { "Engine", "Core", "CoreUObject", "InputCore", "Projects", "GameLiftClientSDKLibrary", });
		PrivateDependencyModuleNames.AddRange(new string[] { });

		PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "Public"));
		PrivateIncludePaths.Add(Path.Combine(ModuleDirectory, "Private"));

		PublicDefinitions.Add("USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("USE_AWS_MEMORY_MANAGEMENT=0");

		PublicDefinitions.Add("FORCE_ANSI_ALLOCATOR=1");

		PublicDefinitions.Add("AWS_USE_IO_COMPLETION_PORTS=1");
		PublicDefinitions.Add("__clang_analyzer__=0");
		PublicDefinitions.Add("AWS_DEEP_CHECKS=0");
		PublicDefinitions.Add("WIN32_LEAN_AND_MEAN"); 

		PublicDefinitions.Add("WIN32");
		PublicDefinitions.Add("_WINDOWS");
		//PublicDefinitions.Add("AWS_GAMELIFT_EXPORTS");
		PublicDefinitions.Add("PLATFORM_WINDOWS");
		PublicDefinitions.Add("ENABLE_BCRYPT_ENCRYPTION");
		PublicDefinitions.Add("ENABLE_WINDOWS_CLIENT");
		PublicDefinitions.Add("AWS_SDK_VERSION_MAJOR=1");
		PublicDefinitions.Add("AWS_SDK_VERSION_MINOR=9");
		PublicDefinitions.Add("AWS_SDK_VERSION_PATCH=172");
		PublicDefinitions.Add("USE_WINDOWS_DLL_SEMANTICS");
		//PublicDefinitions.Add("USE_IMPORT_EXPORT=1");
		//PublicDefinitions.Add("AWS_CRT_CPP_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_HTTP_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_IO_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_USE_IO_COMPLETION_PORTS");
		//PublicDefinitions.Add("AWS_COMMON_USE_IMPORT_EXPORT");
		///PublicDefinitions.Add("AWS_CAL_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_COMPRESSION_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_MQTT_USE_IMPORT_EXPORT");
		PublicDefinitions.Add("AWS_MQTT_WITH_WEBSOCKETS");
		//PublicDefinitions./Add("AWS_AUTH_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_CHECKSUMS_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_EVENT_STREAM_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_S3_USE_IMPORT_EXPORT");
		PublicDefinitions.Add("CMAKE_INTDIR=Debug");
		//PublicDefinitions.Add("aws_cpp_sdk_gamelift_EXPORTS");
							  
		PublicDefinitions.Add("_WINDLL");
		PublicDefinitions.Add("_MBCS");

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
