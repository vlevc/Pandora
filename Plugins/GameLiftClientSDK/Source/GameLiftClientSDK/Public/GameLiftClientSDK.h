// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "Modules/ModuleManager.h"

#if WITH_GAMELIFT_CLIENT

#if PLATFORM_WINDOWS
#include "Windows/AllowWindowsPlatformTypes.h"
#endif

#include "aws/core/Aws.h"

#if PLATFORM_WINDOWS
#include "Windows/HideWindowsPlatformTypes.h"
#endif

#endif

class FGameLiftClientSDKModule : public IModuleInterface
{
public:

	/** IModuleInterface implementation */
	virtual void StartupModule() override;
	virtual void ShutdownModule() override;

	virtual void DescribeGameSessions();

private:
	/** Handle to the external dll we will load */
	static TSet<void*> ValidDllHandles;

#if WITH_GAMELIFT_CLIENT
	Aws::SDKOptions initialOptions;
#endif

	void LoadAwsLibrary(const FString libraryName);

	bool LoadDll(const FString name);

	void FreeDll(void*& dll_ptr);

	void FreeAllDll();

};
