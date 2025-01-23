#pragma once
#include "ComponentsCommon.h"


namespace primal::transform
{
DEFINE_ID_TYPE(transform_id)

	struct init_info
	{
		f64 position[3];
		f64 rotation[4];
		f64 scale[3];
	};

	transform_id create_transform(init_info& info, game_entity::entity_id entity);
	bool remove_transform(transform_id id);
}