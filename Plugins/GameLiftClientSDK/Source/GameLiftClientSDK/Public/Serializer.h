#pragma once

//#include "CoreMinimal.h"
//#include "Credentials.h"
//#include "ClientConfiguration.h"

#include "aws/core/client/AWSClient.h"
#include "aws/core/auth/AWSCredentials.h"
#include "aws/core/client/ClientConfiguration.h"

class AWSSerializer {

public:

	//Returns the same pointer 
	static void* NoChange(void* in);

	//FString to Aws String
	static Aws::String FStoAS(FString s);

	//Aws String to FString
	static FString AStoFS(Aws::String s);

	//static const Aws::Client::ClientConfiguration ClientConfiguration(FClientConfiguration config);
	//static const Aws::Auth::AWSCredentials Credentials(FCredentials credentials);

	//FString Pointer to Aws String Pointer
	static Aws::String* PFStoPAS(FString* s);

	//Aws String Pointer to FString Pointer
	static FString* PAStoPFS(Aws::String* s);

	//TMap to Aws::Map where key and value are of the same type
	template <typename T>
	static Aws::Map<T, T> TMaptoAwsMap(TMap<T, T> in_map);

	//TMap to Aws::Map where values remain the same
	template <typename K, typename V>
	static Aws::Map<K, V> TMaptoAwsMap(TMap<K, V> in_map);

	//TMap to Aws::Map where you need to convert between types <typename AwsKey, typename AwsValue, typename UnrealKey, typename UnrealValue >
	template <typename AK, typename AV, typename UK, typename UV >
	static Aws::Map<AK, AV> TMaptoAwsMap(TMap<UK, UV> in_map, AK* (*keyConverter)(UK*), AV* (*valueConverter)(UV*));

	//Aws::Map to TMap where key and value are of the same type
	template <typename T>
	static TMap<T, T> AwsMaptoTMap(Aws::Map<T, T> in_map);

	//Aws::Map to TMap where values remain the same
	template <typename K, typename V>
	static TMap<K, V> AwsMaptoTMap(Aws::Map<K, V> in_map);

	//Aws::Map to TMap where you need to convert between types <typename AwsKey, typename AwsValue, typename UnrealKey, typename UnrealValue >
	template <typename AK, typename AV, typename UK, typename UV>
	static TMap<UK, UV> AwsMaptoTMap(Aws::Map<AK, AV> in_map, UK* (*keyConverter)(AK*), UV* (*valueConverter)(AV*));

	//TArray to Aws::Vector of same class
	template <typename T>
	static Aws::Vector<T> TArraytoAwsVector(TArray<T> in_array);

	//TArray to Aws::Vector with class conversion
	template <typename AT, typename UT>
	static Aws::Vector<AT> TArraytoAwsVector(TArray<UT> in_array, AT* (*valueConverter)(UT*));

	//Aws::Vector to TArray of same class
	template <typename T>
	static TArray<T> AwsVectortoTArray(Aws::Vector<T> in_array);

	//Aws::Vector to TArray with class conversion
	template <typename AT, typename UT>
	static TArray<UT>  AwsVectortoTArray(Aws::Vector<AT> in_array, UT* (*valueConverter)(AT*));

	//TSet to Aws::Vector of same class
	template <typename T>
	static Aws::Vector<T> TSettoAwsVector(TSet<T> in_set);

	//TSet to Aws::Vector with class conversion
	template <typename AT, typename UT>
	static Aws::Vector<AT> TSettoAwsVector(TSet<UT> in_set, AT* (*valueConverter)(UT*));

	//Aws::Vector to TSet of same class
	template <typename T>
	static TSet<T> AwsVectortoTSet(Aws::Vector<T> in_array);

	//Aws::Vector to TSet with class conversion
	template <typename AT, typename UT>
	static TSet<UT> AwsVectortoTSet(Aws::Vector<AT> in_array, UT* (*valueConverter)(AT*));


};


template <typename T>
Aws::Map<T, T> AWSSerializer::TMaptoAwsMap(TMap<T, T> in_map) {
	Aws::Map<T, T> out_map = Aws::Map<T, T>();

	for (auto& element : in_map) {
		out_map.insert({ element.Key, element.Value });
	}

	return out_map;
};


template <typename K, typename V >
Aws::Map<K, V> AWSSerializer::TMaptoAwsMap(TMap<K, V> in_map) {
	Aws::Map<K, V> out_map = Aws::Map<K, V>();

	for (auto& element : in_map) {
		out_map.insert({ element.Key, element.Value });
	}

	return out_map;
};

template <typename AK, typename AV, typename UK, typename UV >
Aws::Map<AK, AV> AWSSerializer::TMaptoAwsMap(TMap<UK, UV> in_map, AK* (*keyConverter)(UK*), AV* (*valueConverter)(UV*)) {
	Aws::Map<AK, AV> out_map = Aws::Map<AK, AV>();

	for (auto element : in_map) {
		auto old_key = element.Key;
		auto old_value = element.Value;

		AK* new_key = static_cast<AK*>(keyConverter(&old_key));
		AV* new_value = static_cast<AV*>(valueConverter(&old_value));

		out_map.insert({ *new_key, *new_value });
	}

	return out_map;
};

template <typename T>
TMap<T, T> AWSSerializer::AwsMaptoTMap(Aws::Map<T, T> in_map) {
	TMap<T, T> out_map = TMap<T, T>();

	for (auto element : in_map) {
		out_map.Add(element.first, element.second);
	}

	return out_map;
}

template <typename K, typename V >
TMap<K, V> AWSSerializer::AwsMaptoTMap(Aws::Map<K, V> in_map) {
	TMap<K, V> out_map = TMap<K, V>();

	for (auto element : in_map) {
		out_map.Add(element.first, element.second);
	}

	return out_map;
}

template <typename AK, typename AV, typename UK, typename UV>
TMap<UK, UV> AWSSerializer::AwsMaptoTMap(Aws::Map<AK, AV> in_map, UK* (*keyConverter)(AK*), UV* (*valueConverter)(AV*)) {
	TMap<UK, UV> out_map = TMap<UK, UV>();

	for (auto element : in_map) {
		auto old_key = element.first;
		auto old_value = element.second;

		UK* new_key = static_cast<UK*>(keyConverter(&old_key));
		UV* new_value = static_cast<UV*>(valueConverter(&old_value));

		out_map.Add(*new_key, *new_value);
	}

	return out_map;
}


template <typename T>
Aws::Vector<T> AWSSerializer::TArraytoAwsVector(TArray<T> in_array) {
	Aws::Vector<T> out_vector = Aws::Vector<T>();

	for (auto elem : in_array) {
		out_vector.push_back(elem);
	}
}

template <typename AT, typename UT>
Aws::Vector<AT> AWSSerializer::TArraytoAwsVector(TArray<UT> in_array, AT* (*valueConverter)(UT*)) {
	Aws::Vector<AT> out_vector = Aws::Vector<AT>();

	for (auto elem : in_array) {
		auto old_value = elem;
		AT* new_value = static_cast<AT*>(valueConverter(&old_value));

		out_vector.push_back(*new_value);
	}

	return out_vector;
}

template <typename T>
TArray<T> AWSSerializer::AwsVectortoTArray(Aws::Vector<T> in_array) {
	TArray<T> out_array = TArray<T>();

	for (auto elem : in_array) {
		out_array.Add(elem);
	}
	return out_array;
}


template <typename AT, typename UT>
TArray<UT>  AWSSerializer::AwsVectortoTArray(Aws::Vector<AT> in_array, UT* (*valueConverter)(AT*)) {
	TArray<UT> out_array = TArray<UT>();

	for (auto elem : in_array) {
		auto old_value = elem;
		UT* new_value = static_cast<UT*>(valueConverter(&old_value));
		out_array.Add(*new_value);
	}

	return out_array;
}


template <typename T>
Aws::Vector<T> AWSSerializer::TSettoAwsVector(TSet<T> in_set) {
	Aws::Vector<T> out_vector = Aws::Vector<T>();

	for (auto elem : in_array) {
		out_vector.push_back(elem);
	}
}



template <typename AT, typename UT>
Aws::Vector<AT> AWSSerializer::TSettoAwsVector(TSet<UT> in_array, AT* (*valueConverter)(UT*)) {
	Aws::Vector<AT> out_vector = Aws::Vector<AT>();

	for (auto elem : in_array) {
		auto old_value = elem;
		AT* new_value = static_cast<AT*>(valueConverter(&old_value));

		out_vector.push_back(*new_value);
	}

	return out_vector;
}


template <typename T>
TSet<T> AWSSerializer::AwsVectortoTSet(Aws::Vector<T> in_array) {
	TSet<T> out_array = TSet<T>();

	for (auto elem : in_array) {
		out_array.Add(elem);
	}
	return out_array;
}


template <typename AT, typename UT>
TSet<UT>  AWSSerializer::AwsVectortoTSet(Aws::Vector<AT> in_array, UT* (*valueConverter)(AT*)) {
	TSet<UT> out_array = TSet<UT>();

	for (auto elem : in_array) {
		auto old_value = elem;
		UT* new_value = static_cast<UT*>(valueConverter(&old_value));
		out_array.Add(*new_value);
	}

	return out_array;
}