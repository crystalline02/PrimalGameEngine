#pragma once
#include "ComponentsCommon.h"


FOWARD_DECLARE_INIT_INFO(transform)

namespace primal::game_entity
{
	struct  entity_info
	{
		primal::transform::init_info& info;
	};

	entity_id create_entity(entity_info& info);
	bool remove_entity(entity_id id);
	bool is_alive(entity_id id);
}