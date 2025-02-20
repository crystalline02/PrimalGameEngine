#pragma once
#include "CommonHeaders.h"
#include "../Components/ComponentsCommon.h"
#include "TransformComponent.h"

namespace primal::game_entity
{
	struct entity final
	{
	public:
		constexpr explicit entity(entity_id id = entity_id(id::invalid_id)) : _id(id) {}
		constexpr entity_id get_id() const { return _id; }
		constexpr id::generation_type get_generation() const { return id::generation(_id); }
		entity_id get_index() const { return entity_id(id::index(_id)); }
		transform::component get_transform() const;
		bool is_valid() const { return id::is_valid(_id); }
		bool is_alive() const;
		bool remove();
	private:
		entity_id _id;
	};
}