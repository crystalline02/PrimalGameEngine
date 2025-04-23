#pragma once
#include "../EngineAPI/GameEntity.h"

namespace primal::transform
{
	struct init_info
	{
		f32 position[3]{0., 0., 0.};
		f32 rotation[4]{0., 0., 0., 1.};
		f32 scale[3]{1., 1., 1.};
	};

	component create(const init_info& info, entity::game_entity entity);
	bool remove(component id);
}