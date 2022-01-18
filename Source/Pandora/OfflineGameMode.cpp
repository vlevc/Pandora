// Fill out your copyright notice in the Description page of Project Settings.


#include "OfflineGameMode.h"
#include "PandoraCharacter.h"
#include "UObject/ConstructorHelpers.h"

AOfflineGameMode::AOfflineGameMode() {

	// set default pawn class to our Blueprinted character
	static ConstructorHelpers::FClassFinder<APawn> PlayerPawnBPClass(TEXT("/Game/ThirdPersonCPP/Blueprints/ThirdPersonCharacter"));
	if (PlayerPawnBPClass.Class != NULL)
	{
		DefaultPawnClass = PlayerPawnBPClass.Class;
	}
}