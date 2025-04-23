#pragma once
#pragma warning(disable: 4530)
#include <cstdint>
#include <assert.h>
#include <memory>
#include <unordered_map>
#include <string>

#include "PrimitiveTypes.h"
#include "Id.h"
#include "../Utilities/Utilities.h"

#ifdef  _WIN64
#include <DirectXMath.h>
#endif //  _WIN64



// 'tmp' is used as a global variable.This is to ensure that 'register_script' function is called when program starts.
#define REGISTER_SCRIPT(script_class) \
class script_class; \
namespace \
{ \
	u8 registered_##script_class = primal::script::detail::register_script(primal::script::detail::string_hash()(#script_class), \
		&primal::script::detail::create_script<script_class>); \
};