// Copyright Epic Games, Inc. All Rights Reserved.

#include "PandoraGameMode.h"
#include "PandoraCharacter.h"
#include "UObject/ConstructorHelpers.h"
#include "GameLiftServerSDK.h"

APandoraGameMode::APandoraGameMode()
{
	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPersonCPP/Blueprints/ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}

void APandoraGameMode::BeginPlay()
{
    Super::BeginPlay();

#if WITH_GAMELIFT && 0 // Does not work - follow Amazon instruction ...
    auto InitSDKOutcome = Aws::GameLift::Server::InitSDK();

    if (InitSDKOutcome.IsSuccess()) {
        auto onStartGameSession = [](Aws::GameLift::Server::Model::GameSession GameSessionObj, void* Params)
        {
            FStartGameSessionState* State = (FStartGameSessionState*)Params;

            State->Status = Aws::GameLift::Server::ActivateGameSession().IsSuccess();
        };

        auto OnUpdateGameSession = [](Aws::GameLift::Server::Model::UpdateGameSession UpdateGameSessionObj, void* Params)
        {
            FUpdateGameSessionState* State = (FUpdateGameSessionState*)Params;
        };

        auto OnProcessTerminate = [](void* Params)
        {
            FProcessTerminateState* State = (FProcessTerminateState*)Params;
            auto GetTerminationTimeOutcome = Aws::GameLift::Server::GetTerminationTime();
            if (GetTerminationTimeOutcome.IsSuccess()) {
                State->TerminationTime = GetTerminationTimeOutcome.GetResult();
            }

            auto ProcessEndingOutcome = Aws::GameLift::Server::ProcessEnding();

            if (ProcessEndingOutcome.IsSuccess()) {
                State->Status = true;
                FGenericPlatformMisc::RequestExit(false);
            };
        };

        auto OnHealthCheck = [](void* Params)
        {
            FHealthCheckState* State = (FHealthCheckState*)Params;
            State->Status = true;

            return State->Status;
        };

        TArray<FString> CommandLineTokens;
        TArray<FString> CommandLineSwitches;
        int Port = FURL::UrlConfig.DefaultPort;

        // Pandora.exe token -port=7777
        FCommandLine::Parse(FCommandLine::Get(), CommandLineTokens, CommandLineSwitches);

        for (FString Str : CommandLineSwitches) {
            FString Key;
            FString Value;

            if (Str.Split("=", &Key, &Value)) {
                if (Key.Equals("port")) {
                    Port = FCString::Atoi(*Value);
                }
            }
        }

        const char* LogFile = "aLogFile.txt";
        const char** LogFiles = &LogFile;
        auto LogParams = new Aws::GameLift::Server::LogParameters(LogFiles, 1);
    }
#endif 

    //Let's run this code only if GAMELIFT is enabled. Only with Server targets!
#if WITH_GAMELIFT 

    //Getting the module first.
    FGameLiftServerSDKModule* gameLiftSdkModule = &FModuleManager::LoadModuleChecked<FGameLiftServerSDKModule>(FName("GameLiftServerSDK"));

    //InitSDK establishes a local connection with GameLift's agent to enable communication.
    gameLiftSdkModule->InitSDK();

    //Respond to new game session activation request. GameLift sends activation request 
    //to the game server along with a game session object containing game properties 
    //and other settings. Once the game server is ready to receive player connections, 
    //invoke GameLiftServerAPI.ActivateGameSession()
    auto onGameSession = [=](Aws::GameLift::Server::Model::GameSession gameSession)
    {   
        gameLiftSdkModule->ActivateGameSession();
    };

    FProcessParameters* params = new FProcessParameters();
    params->OnStartGameSession.BindLambda(onGameSession);

    //OnProcessTerminate callback. GameLift invokes this before shutting down the instance 
    //that is hosting this game server to give it time to gracefully shut down on its own. 
    //In this example, we simply tell GameLift we are indeed going to shut down.
    params->OnTerminate.BindLambda([=]() {gameLiftSdkModule->ProcessEnding(); });

    //HealthCheck callback. GameLift invokes this callback about every 60 seconds. By default, 
    //GameLift API automatically responds 'true'. A game can optionally perform checks on 
    //dependencies and such and report status based on this info. If no response is received  
    //within 60 seconds, health status is recorded as 'false'. 
    //In this example, we're always healthy!
    params->OnHealthCheck.BindLambda([]() {return true; });

    //Here, the game server tells GameLift what port it is listening on for incoming player 
    //connections. In this example, the port is hardcoded for simplicity. Since active game
    //that are on the same instance must have unique ports, you may want to assign port values
    //from a range, such as:
    //const int32 port = FURL::UrlConfig.DefaultPort;
    //params->port;
    params->port = 7777;

    //Here, the game server tells GameLift what set of files to upload when the game session 
    //ends. GameLift uploads everything specified here for the developers to fetch later.
    TArray<FString> logfiles;
    logfiles.Add(TEXT("aLogFile.txt"));
    params->logParameters = logfiles;

    //Call ProcessReady to tell GameLift this game server is ready to receive game sessions!
    gameLiftSdkModule->ProcessReady(*params);

#endif
}
