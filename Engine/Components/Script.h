#pragma once

#include "../EngineAPI/GameEntity.h"

namespace primal::script
{
	struct init_info
	{
		detail::script_creater creater;
	};

	component create(const init_info& info, entity::game_entity e);
	bool remove(component script);
	bool exist(script_id id);
}
