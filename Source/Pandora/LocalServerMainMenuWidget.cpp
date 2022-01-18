// Fill out your copyright notice in the Description page of Project Settings.


#include "LocalServerMainMenuWidget.h"

ULocalServerMainMenuWidget::ULocalServerMainMenuWidget(const FObjectInitializer& ObjectInitializer)
	: Super(ObjectInitializer)
{
	Endpoint = FString::Printf(TEXT("http://localhost:9080"));
	FleetID = FString::Printf(TEXT("fleet-1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"));
}

void ULocalServerMainMenuWidget::OnDescribeGameSessions()
{

}