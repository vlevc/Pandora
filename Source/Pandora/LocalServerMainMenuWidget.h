// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "LocalServerMainMenuWidget.generated.h"

/**
 * 
 */
UCLASS()
class PANDORA_API ULocalServerMainMenuWidget : public UUserWidget
{
	GENERATED_BODY()

public:
	ULocalServerMainMenuWidget(const FObjectInitializer& ObjectInitializer);

	UPROPERTY(EditAnywhere, BlueprintReadWrite)
		FString Endpoint;

	UPROPERTY(EditAnywhere, BlueprintReadWrite)
		FString FleetID;

	UFUNCTION(BlueprintCallable)
		void OnDescribeGameSessions();
};
