// Copyright Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

public class GameLiftClientSDK : ModuleRules
{
	public GameLiftClientSDK(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		//PrivatePCHHeaderFile = "Private/AWSBaseModulePrivatePCH.h";

		PublicDependencyModuleNames.AddRange(new string[] { "Engine", "Core", "CoreUObject", "InputCore", "Projects" });
		PrivateDependencyModuleNames.AddRange(new string[] { });

		PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "Public"));
		PrivateIncludePaths.Add(Path.Combine(ModuleDirectory, "Private"));

		PublicIncludePaths.Add(ModuleDirectory);

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			string BaseDirectory = System.IO.Path.GetFullPath(System.IO.Path.Combine(ModuleDirectory, "..", ".."));
			string SDKDirectory = System.IO.Path.Combine(BaseDirectory, "ThirdParty", "GameLiftClientSDK", Target.Platform.ToString());

			//System.Console.WriteLine("BaseDirectory : {0}", BaseDirectory);
			//System.Console.WriteLine("SDKDirectory : {0}", SDKDirectory);

			bool bHasGameLiftClientSDK = System.IO.Directory.Exists(SDKDirectory);

			if (bHasGameLiftClientSDK)
			{
				if (Target.Type == TargetRules.TargetType.Editor || Target.Type == TargetRules.TargetType.Client)
				{
					System.Console.WriteLine("------------------> WITH_GAMELIFT_CLIENT=1");
					PublicDefinitions.Add("WITH_GAMELIFT_CLIENT=1");
					if (Target.Platform == UnrealTargetPlatform.Win64)
                    {
						PublicDefinitions.Add("USE_IMPORT_EXPORT");

						PublicDefinitions.Add("AWS_USE_IO_COMPLETION_PORTS=1");
						PublicDefinitions.Add("__clang_analyzer__=0");
						PublicDefinitions.Add("AWS_DEEP_CHECKS=0");
						PublicDefinitions.Add("WIN32_LEAN_AND_MEAN");

						PublicLibraryPaths.Add(SDKDirectory);

						// Add the import library
						PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-cpp-sdk-core.lib"));
						PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-cpp-sdk-gamelift.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-http.lib")); 
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-io.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-mqtt.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-crt-cpp.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-s3.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-auth.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-cal.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-common.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-compression.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-c-event-stream.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-checksums.lib"));
						//PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "testing-resources.lib"));

						// Ensure that the DLL is staged along with the executable
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-cpp-sdk-core.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-cpp-sdk-gamelift.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-http.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-io.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-mqtt.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-crt-cpp.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-s3.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-auth.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-cal.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-common.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-compression.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-c-event-stream.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "aws-checksums.dll"));
						RuntimeDependencies.Add(System.IO.Path.Combine(SDKDirectory, "testing-resources.dll"));

						// Delay-load the DLL, so we can load it from the right place first
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
						PublicDelayLoadDLLs.Add("testing-resources.dll");
					}
				}
                else
                {
					PublicDefinitions.Add("WITH_GAMELIFT_CLIENT=0");
				}
			}
			else
            {
				PublicDefinitions.Add("WITH_GAMELIFT_CLIENT=0");
			}
		}
	}
}
