// Copyright Epic Games, Inc. All Rights Reserved.

#pragma once

#include "Modules/ModuleManager.h"
#include "aws/core/Aws.h"

class FGameLiftClientSDKModule : public IModuleInterface
{
public:

	/** IModuleInterface implementation */
	virtual void StartupModule() override;
	virtual void ShutdownModule() override;

private:
	/** Handle to the external dll we will load */
	static TSet<void*> ValidDllHandles;
//	static void* CEventDll;
//	static void* CCommonDll;
//	static void* ChecksumDll;
	static void* CoreDll;
	static void* GameLiftDll;

	Aws::SDKOptions initialOptions;


	bool LoadDll(const FString path, void*& dll_ptr, const FString name);

	void FreeDll(void*& dll_ptr);

	void FreeAllDll();
};
