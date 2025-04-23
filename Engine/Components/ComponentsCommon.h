# pragma once
#include "../Common/CommonHeaders.h"

namespace primal::entity
{
	DEFINE_ID_TYPE(entity_id)
}

namespace primal::transform
{
	DEFINE_ID_TYPE(transform_id)
}

namespace primal::script
{
	DEFINE_ID_TYPE(script_id)
}

#define FORWRAD_DECLARE(type, ns) \
namespace ns \
{ \
	struct type; \
}

#define FOWARD_DECLARE_INIT_INFO(component) \
FORWRAD_DECLARE(init_info, primal::component) 