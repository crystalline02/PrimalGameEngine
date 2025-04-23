#pragma once
#include "../EngineAPI/GameEntity.h"

#include "Transform.h"
#include "Script.h"

namespace primal::entity
{
	struct  entity_info
	{
		transform::init_info* transform_info = nullptr;
		script::init_info* script_info = nullptr;
#pragma region  MemeoryArragement
		//entity_info() = default;

		//entity_info(const entity_info& other)
		//{
		//	if (other.transform_info)
		//	{
		//		transform_info = new transform::init_info();
		//		memcpy(transform_info, other.transform_info, sizeof(transform::init_info));
		//	}
		//}

		//entity_info(entity_info&& other) noexcept
		//{
		//	transform_info = other.transform_info;
		//	other.transform_info = nullptr;
		//}

		//entity_info& operator=(const entity_info& other)
		//{
		//	if (&other != this)
		//	{
		//		if (transform_info)
		//			delete transform_info;
		//		if (other.transform_info)
		//		{
		//			transform_info = new transform::init_info();
		//			memcpy(transform_info, other.transform_info, sizeof(transform::init_info));
		//		}
		//		else
		//		{
		//			transform_info = nullptr;
		//		}
		//	}
		//	return *this;
		//}

		//entity_info& operator=(entity_info&& other) noexcept
		//{
		//	if (this != &other)
		//	{
		//		if (transform_info)
		//		{
		//			delete transform_info;
		//		}

		//		transform_info = other.transform_info;
		//		other.transform_info = nullptr;
		//	}
		//	return *this;
		//}

		//~entity_info()
		//{
		//	if(transform_info)
		//		delete transform_info;
		//}
#pragma endregion
	};

	game_entity create(const entity_info& info);
	bool remove(game_entity e);
	bool is_alive(game_entity e);
	id::generation_type get_max_generation();
	id::generation_type get_min_generation();
	f64 get_average_generation();;
}