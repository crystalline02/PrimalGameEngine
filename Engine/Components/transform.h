#pragma once
#include "ComponentsCommon.h"

#include "../EngineAPI/GameEntity.h"
#include "../EngineAPI/TransformComponent.h"


namespace primal::transform
{
	
	struct init_info
	{
		f32 position[3]{0., 0., 0.};
		f32 rotation[4]{0., 0., 0., 1.};
		f32 scale[3]{1., 1., 1.};
	};

	component create_transform(const init_info& info, game_entity::entity entity);
	bool remove_transform(component id);
}