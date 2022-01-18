// Copyright Epic Games, Inc. All Rights Reserved.

#include "GameLiftClientSDK.h"
#include "Core.h"
#include "HAL/PlatformProcess.h"
#include "Interfaces/IPluginManager.h"
#include "Misc/Paths.h"



#define LOCTEXT_NAMESPACE "FGameLiftClientSDKModule"

//void* FGameLiftClientSDKModule::CEventDll = nullptr;
//void* FGameLiftClientSDKModule::CCommonDll = nullptr;
//void* FGameLiftClientSDKModule::ChecksumDll = nullptr;
void* FGameLiftClientSDKModule::CoreDll = nullptr;
void* FGameLiftClientSDKModule::GameLiftDll = nullptr;

TSet<void*> FGameLiftClientSDKModule::ValidDllHandles = TSet<void*>();

void FGameLiftClientSDKModule::StartupModule()
{
#if PLATFORM_WINDOWS && PLATFORM_64BITS
    //If we are on a windows platform we need to Load the DLL's
    UE_LOG(LogTemp, Display, TEXT("Start Loading AWS Base DLL's"));
    const FString PluginDir = IPluginManager::Get().FindPlugin("GameLiftClientSdk")->GetBaseDir();
    const FString DllDir = FPaths::Combine(*PluginDir, TEXT("Source"), TEXT("ThirdParty"), TEXT("GameLiftClientSDKLibrary"), TEXT("Win64"));

    FWindowsPlatformProcess::AddDllDirectory(*DllDir);

    /*
    const FString CCommonName = "aws-c-common";
    const FString CCommonPath = FPaths::Combine(DllDir, CCommonName) + TEXT(".") + FPlatformProcess::GetModuleExtension();
    if (!FGameLiftClientSDKModule::LoadDll(CCommonPath, FGameLiftClientSDKModule::CCommonDll, CCommonName)) {
        FGameLiftClientSDKModule::FreeAllDll();
    }

    const FString ChecksumName = "aws-checksums";
    const FString ChecksumPath = FPaths::Combine(DllDir, ChecksumName) + TEXT(".") + FPlatformProcess::GetModuleExtension();
    if (!FGameLiftClientSDKModule::LoadDll(ChecksumPath, FGameLiftClientSDKModule::ChecksumDll, ChecksumName)) {
        FGameLiftClientSDKModule::FreeAllDll();
    }

    const FString CEventName = "aws-c-event-stream";
    const FString CEventPath = FPaths::Combine(DllDir, CEventName) + TEXT(".") + FPlatformProcess::GetModuleExtension();
    if (!FGameLiftClientSDKModule::LoadDll(CEventPath, FGameLiftClientSDKModule::CEventDll, CEventName)) {
        FGameLiftClientSDKModule::FreeAllDll();
    }
    */

    const FString CoreName = "aws-cpp-sdk-core";
    const FString CorePath = FPaths::Combine(DllDir, CoreName) + TEXT(".") + FPlatformProcess::GetModuleExtension();
    if (!FGameLiftClientSDKModule::LoadDll(CorePath, FGameLiftClientSDKModule::CoreDll, CoreName)) {
        FGameLiftClientSDKModule::FreeAllDll();
    }

    const FString GameLiftName = "aws-cpp-sdk-gamelift";
    const FString GameLiftPath = FPaths::Combine(DllDir, GameLiftName) + TEXT(".") + FPlatformProcess::GetModuleExtension();
    if (!FGameLiftClientSDKModule::LoadDll(GameLiftPath, FGameLiftClientSDKModule::GameLiftDll, GameLiftName)) {
        FGameLiftClientSDKModule::FreeAllDll();
    }
#endif

    //The Aws::SDKOptions struct contains SDK configuration options.
    //An instance of Aws::SDKOptions is passed to the Aws::InitAPI and 
    //Aws::ShutdownAPI methods.  The same instance should be sent to both methods.
    initialOptions.loggingOptions.logLevel = Aws::Utils::Logging::LogLevel::Info;

    //The AWS SDK for C++ must be initialized by calling Aws::InitAPI.
    Aws::InitAPI(initialOptions);
}

void FGameLiftClientSDKModule::ShutdownModule()
{
    //Before the application terminates, the SDK must be shut down. 
    Aws::ShutdownAPI(initialOptions);

#if PLATFORM_WINDOWS && PLATFORM_64BITS
    FGameLiftClientSDKModule::FreeAllDll();
#endif
}

bool FGameLiftClientSDKModule::LoadDll(const FString path, void*& dll_ptr, const FString name) {
    //load the passed in dll and if it successeds then add it to the valid set
    UE_LOG(LogTemp, Error, TEXT("Attempting to load DLL %s from %s"), *name, *path);
    dll_ptr = FPlatformProcess::GetDllHandle(*path);

    if (dll_ptr == nullptr) {
        UE_LOG(LogTemp, Error, TEXT("Could not load %s from %s"), *name, *path);
        return false;
    }

    UE_LOG(LogTemp, Display, TEXT("Loaded %s from %s"), *name, *path);
    FGameLiftClientSDKModule::ValidDllHandles.Add(dll_ptr);
    return true;
}

void FGameLiftClientSDKModule::FreeDll(void*& dll_ptr) {
    //free the dll handle
    if (dll_ptr != nullptr) {
        FPlatformProcess::FreeDllHandle(dll_ptr);
        dll_ptr = nullptr;
    }
}

void FGameLiftClientSDKModule::FreeAllDll() {
    //Free all the current valid dll's
    for (auto dll : FGameLiftClientSDKModule::ValidDllHandles) {
        FGameLiftClientSDKModule::FreeDll(dll);
    }
    FGameLiftClientSDKModule::ValidDllHandles.Reset();
}

#undef LOCTEXT_NAMESPACE
	
IMPLEMENT_MODULE(FGameLiftClientSDKModule, GameLiftClientSDK)
