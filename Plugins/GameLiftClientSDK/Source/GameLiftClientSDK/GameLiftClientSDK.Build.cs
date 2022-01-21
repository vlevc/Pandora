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
		//PublicDefinitions.Add("USE_AWS_MEMORY_MANAGEMENT=0");

		//PublicDefinitions.Add("FORCE_ANSI_ALLOCATOR=1");

		PublicDefinitions.Add("AWS_USE_IO_COMPLETION_PORTS=1");
		PublicDefinitions.Add("__clang_analyzer__=0");
		PublicDefinitions.Add("AWS_DEEP_CHECKS=0");
		PublicDefinitions.Add("WIN32_LEAN_AND_MEAN");

		//PublicDefinitions.Add("_ITERATOR_DEBUG_LEVEL=2");

		//PublicDefinitions.Add("WIN32");
		//PublicDefinitions.Add("_WINDOWS");
		//PublicDefinitions.Add("AWS_GAMELIFT_EXPORTS");
		//PublicDefinitions.Add("PLATFORM_WINDOWS");
		//PublicDefinitions.Add("ENABLE_BCRYPT_ENCRYPTION");
		//PublicDefinitions.Add("ENABLE_WINDOWS_CLIENT");
		//PublicDefinitions.Add("AWS_SDK_VERSION_MAJOR=1");
		//PublicDefinitions.Add("AWS_SDK_VERSION_MINOR=9");
		//PublicDefinitions.Add("AWS_SDK_VERSION_PATCH=172");
		//PublicDefinitions.Add("USE_WINDOWS_DLL_SEMANTICS");
		//PublicDefinitions.Add("USE_IMPORT_EXPORT=1");
		//PublicDefinitions.Add("AWS_CRT_CPP_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_HTTP_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_IO_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_USE_IO_COMPLETION_PORTS");
		//PublicDefinitions.Add("AWS_COMMON_USE_IMPORT_EXPORT");
		///PublicDefinitions.Add("AWS_CAL_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_COMPRESSION_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_MQTT_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_MQTT_WITH_WEBSOCKETS");
		//PublicDefinitions./Add("AWS_AUTH_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_CHECKSUMS_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_EVENT_STREAM_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("AWS_S3_USE_IMPORT_EXPORT");
		//PublicDefinitions.Add("CMAKE_INTDIR=Debug");
		//PublicDefinitions.Add("aws_cpp_sdk_gamelift_EXPORTS");

		//PublicDefinitions.Add("_WINDLL");
		//PublicDefinitions.Add("_MBCS");
/*
		string WindowPath = System.IO.Path.Combine(ModuleDirectory, UnrealTargetPlatform.Win64.ToString());

		PublicIncludePaths.Add(ModuleDirectory);
		PublicLibraryPaths.Add(WindowPath);

		System.Console.WriteLine("*****************************************************************");
		System.Console.WriteLine("WindowPath : {0}", WindowPath);

		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-http.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-io.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-mqtt.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-cpp-sdk-core.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-cpp-sdk-gamelift.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-crt-cpp.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-s3.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-auth.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-cal.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-common.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-compression.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-c-event-stream.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-checksums.lib"));

		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-http.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-io.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-mqtt.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-cpp-sdk-core.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-cpp-sdk-gamelift.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-crt-cpp.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-s3.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-auth.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-cal.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-common.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-compression.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-c-event-stream.dll"));
		RuntimeDependencies.Add(System.IO.Path.Combine(WindowPath, "aws-checksums.dll"));

		PublicDelayLoadDLLs.Add("aws-c-http.dll");
		PublicDelayLoadDLLs.Add("aws-c-io.dll");
		PublicDelayLoadDLLs.Add("aws-c-mqtt.dll");
		PublicDelayLoadDLLs.Add("aws-cpp-sdk-core.dll");
		PublicDelayLoadDLLs.Add("aws-cpp-sdk-gamelift.dll");
		PublicDelayLoadDLLs.Add("aws-crt-cpp.dll");
		PublicDelayLoadDLLs.Add("aws-c-s3.dll");
		PublicDelayLoadDLLs.Add("aws-c-auth.dll");
		PublicDelayLoadDLLs.Add("aws-c-cal.dll");
		PublicDelayLoadDLLs.Add("aws-c-common.dll");
		PublicDelayLoadDLLs.Add("aws-c-compression.dll");
		PublicDelayLoadDLLs.Add("aws-c-event-stream.dll");
		PublicDelayLoadDLLs.Add("aws-checksums.dll");

		System.Console.WriteLine("*****************************************************************");
*/

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
