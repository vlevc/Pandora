// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "Modules/ModuleManager.h"

#if WITH_GAMELIFT_CLIENT
#include "aws/core/Aws.h"
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

	void LoadAwsLibrary(const FString libraryName, const FString DllDir);

	bool LoadDll(const FString path, const FString name);

	void FreeDll(void*& dll_ptr);

	void FreeAllDll();

};
