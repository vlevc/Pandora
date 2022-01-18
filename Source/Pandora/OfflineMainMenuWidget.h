// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "Http.h"
#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "OfflineMainMenuWidget.generated.h"

/**
 * 
 */
UCLASS()
class PANDORA_API UOfflineMainMenuWidget : public UUserWidget
{
	GENERATED_BODY()
	
public:
	UOfflineMainMenuWidget(const FObjectInitializer& ObjectInitializer);

	UFUNCTION(BlueprintCallable)
		void OnLoginClicked();

	UPROPERTY(EditAnywhere)
		FString ApiGatewayEndpoint;

	UPROPERTY(EditAnywhere)
		FString LogingURI;

	UPROPERTY(EditAnywhere)
		FString StartSessionURI;

	UPROPERTY(BlueprintReadWrite)
		FString user;

	UPROPERTY(BlueprintReadWrite)
		FString pass;

private:
	FHttpModule* Http;

	void LoginRequest(FString usr, FString pwd);
	void OnLoginResponse(FHttpRequestPtr Request, FHttpResponsePtr Response, bool bWasSuccessful);
	void StartSessionRequest(FString idt);
	void OnStartSessionResponse(FHttpRequestPtr Request, FHttpResponsePtr Response, bool bWasSuccessful);
};
