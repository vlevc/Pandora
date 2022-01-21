// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "Modules/ModuleManager.h"
#include "aws/core/Aws.h"

namespace Aws {
	struct SDKOptions;
}

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

	Aws::SDKOptions initialOptions;

	void LoadAwsLibrary(const FString libraryName, const FString DllDir);

	bool LoadDll(const FString path, const FString name);

	void FreeDll(void*& dll_ptr);

	void FreeAllDll();

};
