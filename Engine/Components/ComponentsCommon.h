# pragma once
#include "CommonHeaders.h"
#include "Id.h"

namespace primal::game_entity
{
	DEFINE_ID_TYPE(entity_id)
}

namespace primal::transform
{
	DEFINE_ID_TYPE(transform_id)
}

#define FORWRAD_DECLARE(type, ns) \
namespace ns \
{ \
	struct type; \
}

#define FOWARD_DECLARE_INIT_INFO(component) \
FORWRAD_DECLARE(init_info, primal::component) 