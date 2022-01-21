// Copyright Epic Games, Inc. All Rights Reserved.

#include "GameLiftClientSDK.h"
#include "Core.h"
//#include "HAL/PlatformProcess.h"
#include "Interfaces/IPluginManager.h"
#include "Misc/Paths.h"
#include "HAL/UnrealMemory.h"

#if WITH_GAMELIFT_CLIENT
#include "aws/core/utils/logging/LogLevel.h"
#include "aws/core/utils/memory/MemorySystemInterface.h"
#include "aws/core/client/ClientConfiguration.h"
#include "aws/gamelift/GameLiftClient.h"
#include "aws/gamelift/model/DescribeGameSessionsRequest.h"
#endif

#include "Serializer.h"

#define LOCTEXT_NAMESPACE "FGameLiftClientSDKModule"

#if WITH_GAMELIFT_CLIENT

class UEMemoryManager : public Aws::Utils::Memory::MemorySystemInterface
{
public:
    void Begin() override {}
    void End() override {}
    void* AllocateMemory(std::size_t blockSize, std::size_t alignment, const char* /*allocationTag = nullptr*/) override {
        return FMemory::Malloc(static_cast<SIZE_T>(blockSize), static_cast<SIZE_T>(alignment));
    }
    void FreeMemory(void* memoryPtr) override {
        FMemory::Free(memoryPtr);
    }
};

static UEMemoryManager ue4MemoryManager;

#endif

TSet<void*> FGameLiftClientSDKModule::ValidDllHandles = TSet<void*>();

void FGameLiftClientSDKModule::LoadAwsLibrary(const FString libraryName, const FString DllDir)
{
    const FString libraryPath = FPaths::Combine(DllDir, libraryName) + TEXT(".") + FPlatformProcess::GetModuleExtension();
    if (!FGameLiftClientSDKModule::LoadDll(libraryPath, libraryName)) {
        FGameLiftClientSDKModule::FreeAllDll();
    }
}

void FGameLiftClientSDKModule::StartupModule()
{
#if WITH_GAMELIFT_CLIENT && PLATFORM_WINDOWS && PLATFORM_64BITS
    //If we are on a windows platform we need to Load the DLL's
    UE_LOG(LogTemp, Display, TEXT("Start Loading AWS Base DLL's"));
    const FString PluginDir = IPluginManager::Get().FindPlugin("GameLiftClientSdk")->GetBaseDir();
    const FString DllDir = FPaths::Combine(*PluginDir, TEXT("ThirdParty"), TEXT("GameLiftClientSDK"), TEXT("Win64"));

    FWindowsPlatformProcess::AddDllDirectory(*DllDir);

    LoadAwsLibrary("aws-cpp-sdk-core", DllDir); 
    LoadAwsLibrary("aws-cpp-sdk-gamelift", DllDir);

    //LoadAwsLibrary("aws-crt-cpp", DllDir);
    //LoadAwsLibrary("aws-c-event-stream", DllDir);
    //LoadAwsLibrary("aws-c-common", DllDir);
    //LoadAwsLibrary("aws-c-mqtt", DllDir);
    //LoadAwsLibrary("aws-c-s3", DllDir);
    //LoadAwsLibrary("aws-c-auth", DllDir);
    //LoadAwsLibrary("aws-c-http", DllDir);
    //LoadAwsLibrary("aws-c-io", DllDir);
    //LoadAwsLibrary("aws-c-cal", DllDir);
    //LoadAwsLibrary("aws-checksums", DllDir);
    //LoadAwsLibrary("aws-c-compression", DllDir); 

    Aws::SDKOptions* pInitialOptions = new Aws::SDKOptions();

    //The Aws::SDKOptions struct contains SDK configuration options.
    //An instance of Aws::SDKOptions is passed to the Aws::InitAPI and 
    //Aws::ShutdownAPI methods.  The same instance should be sent to both methods.
    initialOptions.loggingOptions.logLevel = Aws::Utils::Logging::LogLevel::Info;

    //The AWS SDK for C++ must be initialized by calling Aws::InitAPI.
    Aws::InitAPI(initialOptions);
#endif

}

void FGameLiftClientSDKModule::ShutdownModule()
{

#if WITH_GAMELIFT_CLIENT
    //Before the application terminates, the SDK must be shut down. 
    Aws::ShutdownAPI(initialOptions);
#endif

#if PLATFORM_WINDOWS && PLATFORM_64BITS
    FGameLiftClientSDKModule::FreeAllDll();
#endif
}

bool FGameLiftClientSDKModule::LoadDll(const FString path, const FString name) {
    //load the passed in dll and if it successeds then add it to the valid set
    UE_LOG(LogTemp, Error, TEXT("Attempting to load DLL %s from %s"), *name, *path);
    void* dll_ptr = FPlatformProcess::GetDllHandle(*path);

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

void FGameLiftClientSDKModule::DescribeGameSessions()
{
#if WITH_GAMELIFT_CLIENT
    Aws::Client::ClientConfiguration clientConfig;
    Aws::GameLift::GameLiftClient gameLiftClient(clientConfig);

    const std::string testString("http://localhost:9080");
    gameLiftClient.OverrideEndpoint(testString);

    Aws::GameLift::Model::DescribeGameSessionsRequest gameSessionsRequest;
    gameSessionsRequest.SetFleetId("fleet-1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d");
    auto result = gameLiftClient.DescribeGameSessions(gameSessionsRequest);
#endif
}

#undef LOCTEXT_NAMESPACE
	
IMPLEMENT_MODULE(FGameLiftClientSDKModule, GameLiftClientSDK)
