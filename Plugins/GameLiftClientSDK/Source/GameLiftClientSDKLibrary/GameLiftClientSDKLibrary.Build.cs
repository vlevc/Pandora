// Fill out your copyright notice in the Description page of Project Settings.

using System.IO;
using UnrealBuildTool;

public class GameLiftClientSDKLibrary : ModuleRules
{
	public GameLiftClientSDKLibrary(ReadOnlyTargetRules Target) : base(Target)
	{
		Type = ModuleType.External;

		PublicIncludePaths.Add(ModuleDirectory);

		string SDKDirectory = System.IO.Path.Combine(ModuleDirectory, Target.Platform.ToString());

		//System.Console.WriteLine("SDKDirectory : {0}", SDKDirectory);

		bool bHasGameLiftSDK = System.IO.Directory.Exists(SDKDirectory);

		if (bHasGameLiftSDK)
		{
			if (Target.Type == TargetRules.TargetType.Editor || Target.Type == TargetRules.TargetType.Client)
			{
				PublicDefinitions.Add("USE_IMPORT_EXPORT");

				PublicDefinitions.Add("AWS_USE_IO_COMPLETION_PORTS=1");
				PublicDefinitions.Add("__clang_analyzer__=0");
				PublicDefinitions.Add("AWS_DEEP_CHECKS=0");

				// Add the import library
				PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-cpp-sdk-core.lib"));
				PublicAdditionalLibraries.Add(Path.Combine(SDKDirectory, "aws-cpp-sdk-gamelift.lib"));

				// Delay-load the DLL, so we can load it from the right place first
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-http.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-http.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-io.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-io.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-mqtt.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-mqtt.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-cpp-sdk-core.dll"), System.IO.Path.Combine(SDKDirectory, "aws-cpp-sdk-core.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-cpp-sdk-gamelift.dll"), System.IO.Path.Combine(SDKDirectory, "aws-cpp-sdk-gamelift.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-crt-cpp.dll"), System.IO.Path.Combine(SDKDirectory, "aws-crt-cpp.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-s3.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-s3.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-auth.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-auth.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-cal.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-cal.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-common.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-common.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-compression.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-compression.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-event-stream.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-event-stream.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-checksums.dll"), System.IO.Path.Combine(SDKDirectory, "aws-checksums.dll"));

				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-http.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-http.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-io.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-io.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-mqtt.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-mqtt.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-cpp-sdk-core.dll"), System.IO.Path.Combine(SDKDirectory, "aws-cpp-sdk-core.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-cpp-sdk-gamelift.dll"), System.IO.Path.Combine(SDKDirectory, "aws-cpp-sdk-gamelift.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-crt-cpp.dll"), System.IO.Path.Combine(SDKDirectory, "aws-crt-cpp.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-s3.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-s3.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-auth.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-auth.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-cal.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-cal.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-common.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-common.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-compression.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-compression.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-c-event-stream.dll"), System.IO.Path.Combine(SDKDirectory, "aws-c-event-stream.dll"));
				RuntimeDependencies.Add(System.IO.Path.Combine("$(TargetOutputDir)", "aws-checksums.dll"), System.IO.Path.Combine(SDKDirectory, "aws-checksums.dll"));

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
			}
		}
	}
}