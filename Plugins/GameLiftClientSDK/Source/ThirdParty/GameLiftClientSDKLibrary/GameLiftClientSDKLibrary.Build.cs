// Fill out your copyright notice in the Description page of Project Settings.

using System.IO;
using UnrealBuildTool;

public class GameLiftClientSDKLibrary : ModuleRules
{
	public GameLiftClientSDKLibrary(ReadOnlyTargetRules Target) : base(Target)
	{
		Type = ModuleType.External;

		PublicIncludePaths.Add(ModuleDirectory);

		PublicDefinitions.Add("USE_IMPORT_EXPORT");

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			string WindowPath = System.IO.Path.Combine(ModuleDirectory, UnrealTargetPlatform.Win64.ToString());

			PublicLibraryPaths.Add(WindowPath);

			System.Console.WriteLine("----------------------------------------------------------------");
			System.Console.WriteLine("WindowPath : {0}", WindowPath);

			// Add the import library
			PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-cpp-sdk-core.lib"));
			PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "Win64", "aws-cpp-sdk-gamelift.lib"));

			// Delay-load the DLL, so we can load it from the right place first
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

			System.Console.WriteLine("----------------------------------------------------------------");

			// Add the import library
			//PublicAdditionalLibraries.Add(Path.Combine(ModuleDirectory, "x64", "Release", "ExampleLibrary.lib"));

			// Delay-load the DLL, so we can load it from the right place first
			//PublicDelayLoadDLLs.Add("ExampleLibrary.dll");

			// Ensure that the DLL is staged along with the executable
			//RuntimeDependencies.Add("$(PluginDir)/Binaries/ThirdParty/GameLiftClientSDKLibrary/Win64/ExampleLibrary.dll");
		}
	}
}
